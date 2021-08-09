using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoveMent : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 1200f;
    [SerializeField] float runSpeed = 2f;
    [SerializeField] float maxVelocity = 5f;
    private bool facingRight = true;
    private float xInput, yInput;

    [Header("Jump")]
    [SerializeField] float jumpForce = 750f;
    [SerializeField] int currentJump, maxJump = 2;


    [Header("Ground")]
    [SerializeField] float range = 1f;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject groundCheckTrans;
    private RaycastHit2D leftHit, rightHIt;

    [Header("Fire")]
    [SerializeField] float attackIntervals = 0.5f;
    [SerializeField] float nextAttack;
    [SerializeField] float timer = 0;
    [SerializeField] Transform fireTrans;
    [SerializeField] GameObject ammo;
    private float hitValue = 10f;

    [Header("Enemies")]
    [SerializeField] string enLayerName;
    private int enemyLayer;

    [Header("Components")]
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] Animator anim;

    float forceY;

    void Start()
    {
        enemyLayer = LayerMask.NameToLayer(enLayerName);
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            Fire();
    }

    void FixedUpdate()
    {
        // Movement
        Movement();

        // Character filp
        if (xInput > 0 && !facingRight)
        {
            FlipCharcter();
        }
        else if (xInput < 0 && facingRight)
        {
            FlipCharcter();
        }

        // Ground Check
        GroundCheck();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Coin"))
        {
            Destroy(collider.gameObject);
            GameManager.Instance.UpdateCoins();
        }

        if (collider.transform.name == "Level Changer")
            GameManager.Instance.LoadNextScene(collider.GetComponent <LoadLevelIndex> ().LevelIndex);
    }

    void Movement()
    {
        float forceX = 0f;
        forceY = 0f;
        float velocity = Mathf.Abs(rigidbody2D.velocity.x);

        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        rigidbody2D.AddForce(new Vector2(forceX, 0f));

        if (xInput > 0)
        {
            // Run
            if (Input.GetKey(KeyCode.Z))
            {
                maxVelocity = 7f;

                if (velocity < maxVelocity)
                    forceX = (speed * runSpeed) * Time.fixedDeltaTime;
            }
            else
            {
                // Walk
                maxVelocity = 5f;

                if (velocity < maxVelocity)
                    forceX = speed * Time.fixedDeltaTime;
            }
        }
        else if (xInput < 0)
        {
            // Run
            if (Input.GetKey(KeyCode.Z))
            {
                maxVelocity = 7f;

                if (velocity < maxVelocity)
                    forceX = -(speed * runSpeed) * Time.fixedDeltaTime;
            }
            else
            {
                // Walk
                maxVelocity = 5f;

                if (velocity < maxVelocity)
                    forceX = -speed * Time.fixedDeltaTime;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            currentJump += 1;

            if (!isGrounded)
            {
                if (currentJump <= maxJump)
                {
                    forceY = jumpForce;
                }
            }
            else
            {
                forceY = jumpForce;
            }
        }
 
        rigidbody2D.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);

        // Animatiom
        anim.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.K))
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            anim.SetBool("Jump", true);
        else
            anim.SetBool("Jump", false);
    }

    void GroundCheck()
    {
        leftHit = Physics2D.Raycast(new Vector3(transform.position.x - 0.03f, transform.position.y, transform.position.z), Vector3.down, range, layerMask);

        rightHIt = Physics2D.Raycast(new Vector3(transform.position.x + 0.03f, transform.position.y, transform.position.z), Vector3.down, range, layerMask);

        if (leftHit.collider != null)
        {
            isGrounded = true;
            currentJump = 0;

            if (leftHit.collider.gameObject.layer == enemyLayer)
            {
                if (!leftHit.collider.GetComponent<AI_Basic>().isDead)
                {
                    leftHit.collider.GetComponent<AI_Basic>().isDead = true;
                    StompEnemy(leftHit.collider.gameObject);
                }
            }
        }
        else if (rightHIt.collider != null)
        {
            isGrounded = true;
            currentJump = 0;

            if (rightHIt.collider.gameObject.layer == enemyLayer)
            {
                if (!rightHIt.collider.GetComponent<AI_Basic>().isDead)
                {
                    rightHIt.collider.GetComponent<AI_Basic>().isDead = true;
                    StompEnemy(leftHit.collider.gameObject);
                }
               
            }
        }
        else
            isGrounded = false;
    }



    void FlipCharcter()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    void StompEnemy(GameObject obj)
    {
        Destroy(obj);

        GameManager.Instance.UpdateKillCount();
    }

    void Fire()
    {
        GameObject Go = Instantiate(ammo, fireTrans.position, Quaternion.identity, this.transform);
        Go.GetComponent<Rigidbody2D>().AddForce(this.transform.InverseTransformVector(Vector3.right) * 100f, ForceMode2D.Impulse);
        Debug.Log("Fire");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z),
        Vector3.down * range);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z),
         Vector3.down * range);
    }
}
