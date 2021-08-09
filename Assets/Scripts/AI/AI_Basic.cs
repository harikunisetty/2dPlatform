using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Basic : MonoBehaviour
{
    [Header("Ai")]
    public bool isDead = true;
    [Header("AI State")]
    public AI_state Currentstate;
    public enum AI_state
    {
        Patrol, Attack, Dead
    }

    [Header("Movement")]
    public float speed = 250f;
    public bool Move = true;
    public bool moveRight = true;
    public float movespeed;
    public Vector3 direction;

    [Header("Raycast")]

    public float rayRange = 0.125f;
    public LayerMask layerMask;
    private RaycastHit2D hit;

    [Header("Components")]
    public Rigidbody2D rigidbody2D;
    public AI_Attack scr_aiAttack;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        Move = true;
        moveRight = true;
        movespeed = speed;
        Currentstate = AI_state.Patrol;
        rigidbody2D = GetComponent<Rigidbody2D>();

        scr_aiAttack = GetComponent<AI_Attack>();
    }

    void Movement()
    {
        if (moveRight)
            direction = transform.right;
        else
            direction = -transform.right;

        if (!Move)
            return;
        else
            rigidbody2D.AddForce(new Vector2(movespeed, transform.position.y) * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        switch (Currentstate)
        {
            case AI_state.Patrol:
                Movement();
                break;
            case AI_state.Attack:
                scr_aiAttack.CallAttack();
                break;
            case AI_state.Dead:
                break;
            default:
                break;
        }


        // Raycast
        hit = Physics2D.Raycast(transform.position, direction, rayRange, layerMask);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Move = false;

                Currentstate = AI_state.Attack;
            }
            else
            {
                Move = true;

                Currentstate = AI_state.Patrol;

                ChangeDirection();
            }
        }
        else
        {
            Move = true;
            Currentstate = AI_state.Patrol;
        }
    }

    private void ChangeDirection()
    {
        Vector3 scale;
        moveRight = !moveRight;
        if (moveRight)
        {
            movespeed = speed;
        }
        else
        {
            movespeed = -speed;
            scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction * rayRange);
    }
}
