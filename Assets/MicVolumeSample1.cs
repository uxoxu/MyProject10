using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicVolumeSample1 : MonoBehaviour
{
    private readonly int SampleNum = (2 << 9); // �T���v�����O����2��N��(N=5-12)
    [SerializeField, Range(0f, 1000f)] float m_gain = 200f; // �{��
    AudioSource m_source;
    float[] currentValues;

    // Use this for initialization
    void Start()
    {
        m_source = GetComponent<AudioSource>();
        currentValues = new float[SampleNum];
        if ((m_source != null) && (Microphone.devices.Length > 0)) // �I�[�f�B�I�\�[�X�ƃ}�C�N������
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

    // Update is called once per frame
    void Update()
    {
        m_source.GetSpectrumData(currentValues, 0, FFTWindow.Hamming);
        float sum = 0f;
        for (int i = 0; i < currentValues.Length; ++i)
        {
            sum += currentValues[i]; // �f�[�^�i���g���т��Ƃ̃p���[�j�𑫂�
        }
        // �f�[�^���Ŋ��������̂ɔ{���������ĉ��ʂƂ���
        float volumeRate = Mathf.Clamp01(sum * m_gain / (float)currentValues.Length);
        Debug.Log(volumeRate);
    }
}
