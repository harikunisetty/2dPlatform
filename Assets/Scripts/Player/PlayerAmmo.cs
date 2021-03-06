using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    float moveSpeed = 7f;

    Rigidbody2D rb;

     Player_MoveMent target;

    Vector2 moveDirection;

    void Start()
    {
        /*
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player_MoveMent>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
        */
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject); // collided(enemy) gameobject
            Destroy(this.gameObject); // ammo gameobject
        }

        if(other.gameObject.layer == 0)
        {
            Destroy(this.gameObject); // ammo gameobject
        }
    }
}
