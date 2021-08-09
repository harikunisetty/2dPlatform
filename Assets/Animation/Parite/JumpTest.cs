using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    [SerializeField] bool isGrounded;
    public float jumpForce = 10f;
    public int currentJump;
    public int maxJump = 2;
    public Rigidbody2D rbd2d;

    private void Awake()
    {
        rbd2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float forceY = 0f;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            currentJump++;

            if (!isGrounded)
            {
                if (currentJump <= maxJump)
                {
                    currentJump++;
                    forceY = jumpForce;
                }
            }
            else
            {
                currentJump++;
                forceY = jumpForce;
            }
        }

        rbd2d.AddForce(new Vector2(rbd2d.velocity.x, forceY), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Square")
        {
            isGrounded = true;
            currentJump = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name == "Square")
        {
            isGrounded = false;
        }
    }
}

