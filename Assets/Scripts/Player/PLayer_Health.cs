using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer_Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float health;
    void Start()
    {
        health = 100f;
    }

    public void Damage(float hitPoints)
    {
        if (health > 0f)
        {
            health -= hitPoints;
            UI_Basic.Instance.PlayerHealthUI(health);
            if (health <= 0f)
            {
                // Code for Death
                Debug.Log("Game OVer! " + health);
            }
        }
        else
        {
            Debug.Log("Game OVer! " + health);
        }
    }
}
