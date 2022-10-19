using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otamesi : MonoBehaviour
{
    Rigidbody2D rbody2D;
    public float jumpForce = 350f;
    public int jumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&this.jumpCount<2)
        {
            this.rbody2D.AddForce(transform.up * jumpForce);
            jumpCount++;
        }
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            jumpCount =0;
        }
    }
}
