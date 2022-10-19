using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;

    public bool isForceScrollX = false;                 //�����X�N���[���t���O
    public float forceScrollSpeedX = 0.5f;              //1�b�Ԃœ�����X����
    public bool isForceScrollY = false;                 //Y�������X�N���[���t���O
    public float forceScrollSpeedY = 0.5f;              //1�b�Ԃœ������x����

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //�v���C���[��T��
        if (player != null)
        {
            //�J�����̍X�V���W
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = transform.position.z;
            //������������
            if (isForceScrollX)
            {
                //�����X�N���[��
                x = transform.position.x + (forceScrollSpeedX * Time.deltaTime);
            }
            //���[�Ɉړ�������t����
            if (x < leftLimit)
            {
                x = leftLimit;
            }
            else if (x > rightLimit)
            {
                x = rightLimit;
            }
            //�c����������
            if (isForceScrollY)
            {
                //�c�����X�N���[��
                y = transform.position.y + (forceScrollSpeedY * Time.deltaTime);
            }
            //�㉺�Ɉړ�������t����
            if (y < bottomLimit)
            {
                y = bottomLimit;
            }
            else if (y > topLimit)
            {
                y = topLimit;
            }
            //�J�����ʒu��Vector3�����
            Vector3 v3 = new Vector3(x, y, z);
            transform.position = v3;

        }
    }
}
