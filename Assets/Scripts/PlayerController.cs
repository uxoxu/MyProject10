using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody2D;
    float axisH = 0.0f;             //����
    public float speed = 3.0f;      //�ړ����x

    public float jumpForce = 350f;
    public int jumpCount = 0;

    public int score = 0;

    public static string gameState = "playing";//�Q�[���̏��
    bool onGround = false;          //�n�ʂɗ����Ă���t���O



    // Start is called before the first frame update
    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        gameState = "playing";

    }

    // Update is called once per frame
    void Update()
    {

        //���������̓��͂��`�F�b�N����
        axisH = Input.GetAxisRaw("Horizontal");
        //�����̒���
        if (axisH > 0.0f)
        {
            //�E�ړ�
            Debug.Log("�E�ړ�");
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            //���ړ�
            Debug.Log("���ړ�");
            transform.localScale = new Vector2(-1, 1);//���E���]������
        }
        if (Input.GetKeyDown(KeyCode.Space) && this.jumpCount < 2)
        {
            this.rbody2D.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }

    void FixedUpdate()
    {
        if (gameState != "playing")
        {
            return;
        }

        if (onGround || axisH != 0)
        {
            //�n�ʂ̏�or���x��0�łȂ�
            //���x���X�V����
            rbody2D.velocity = new Vector2(speed * axisH, rbody2D.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ScoreItem")
        {
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            score = item.value;

            Destroy(collision.gameObject);
        }
    }
}
