using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Attack : MonoBehaviour
{


    [Header("Variables")]
    [SerializeField] float attackDelay = 0.1f;
    [SerializeField] float attackIntervals = 0.05f;
    [SerializeField] float hitValue = 10f;
    private float attackTimer;

    [Header("Player Components")]
    private PLayer_Health pHealth;

    void Start()
    {
        attackTimer = attackIntervals;

        if (pHealth == null)
            pHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PLayer_Health>();
    }

    public void CallAttack()
    {
        Attack(hitValue);
    }

    void Attack(float hitValue)
    {
        if (attackTimer <= 0f)
        {
            attackTimer = attackIntervals; 

            if (pHealth != null)
                pHealth.Damage(hitValue);
            else
                pHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PLayer_Health>();
        }
        else
            attackTimer -= attackDelay * Time.deltaTime; 
    }
}
