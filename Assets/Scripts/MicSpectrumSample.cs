using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicSpectrumSample : MonoBehaviour
{
    private readonly int SampleNum = (2 << 9); // �T���v�����O����2��N��(N=5-12)
    [SerializeField, Range(0f, 1000f)] float m_gain = 200f; // �{��
    AudioSource m_source;
    LineRenderer m_lineRenderer;
    Vector3 m_sttPos;
    Vector3 m_endPos;
    float[] currentValues;

    // Use this for initialization
    void Start()
    {
        m_source = GetComponent<AudioSource>();
        m_lineRenderer = GetComponent<LineRenderer>();
        m_sttPos = m_lineRenderer.GetPosition(0);
        m_endPos = m_lineRenderer.GetPosition(m_lineRenderer.positionCount - 1);
        currentValues = new float[SampleNum];
        if ((m_source != null) && (Microphone.devices.Length > 0)) // �I�[�f�B�I�\�[�X�ƃ}�C�N������
        {
            if (m_source.clip == null) // �N���b�v���Ȃ���΃}�C�N�ɂ���
            {
                string devName = Microphone.devices[0]; // �����������Ă��Ƃ肠����0�Ԗڂ̃}�C�N���g�p
                int minFreq, maxFreq;
                Microphone.GetDeviceCaps(devName, out minFreq, out maxFreq); // �ő�ŏ��T���v�����O���𓾂�
                int ms = minFreq / SampleNum; // �T���v�����O���Ԃ�K�؂Ɏ��
                m_source.loop = true; // ���[�v�ɂ���
                m_source.clip = Microphone.Start(devName, true, ms, minFreq); // clip���}�C�N�ɐݒ�
                while (!(Microphone.GetPosition(devName) > 0)) { } // ������ƒl���Ƃ邽�߂ɑ҂�
                Microphone.GetPosition(null);
                m_source.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_source.GetSpectrumData(currentValues, 0, FFTWindow.Hamming);
        int levelCount = currentValues.Length / 8; // �����g���т͎��Ȃ�
        Vector3[] positions = new Vector3[levelCount];
        for (int i = 0; i < levelCount; i++)
        {
            positions[i] = m_sttPos + (m_endPos - m_sttPos) * (float)i / (float)(levelCount - 1);
            positions[i].y += currentValues[i] * m_gain;
        }
        m_lineRenderer.positionCount = levelCount;
        m_lineRenderer.SetPositions(positions);
    }
}