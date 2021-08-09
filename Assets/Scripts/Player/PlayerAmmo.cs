using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    [SerializeField] float deathTime = 2f;
    private void Awake()
    {
        Destroy(this.gameObject, deathTime);
    }
}
