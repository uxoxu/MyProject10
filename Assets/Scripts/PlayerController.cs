using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody2D;
    float axisH = 0.0f;             //入力
    public float speed = 3.0f;      //移動速度

    public float jumpForce = 350f;
    public int jumpCount = 0;

    public int score = 0;

    public static string gameState = "playing";//ゲームの状態
    bool onGround = false;          //地面に立っているフラグ



    // Start is called before the first frame update
    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        gameState = "playing";

    }

    // Update is called once per frame
    void Update()
    {

        //水平方向の入力をチェックする
        axisH = Input.GetAxisRaw("Horizontal");
        //向きの調整
        if (axisH > 0.0f)
        {
            //右移動
            Debug.Log("右移動");
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            //左移動
            Debug.Log("左移動");
            transform.localScale = new Vector2(-1, 1);//左右反転させる
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
            //地面の上or速度が0でない
            //速度を更新する
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
