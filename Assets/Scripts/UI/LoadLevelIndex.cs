using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelIndex : MonoBehaviour
{
    [SerializeField] bool isobjectiveCompleted;

    public bool IsObjectiveCompleted
    {
        get { return isobjectiveCompleted; }
    }
    private void Start()
    {
        isobjectiveCompleted = false;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isobjectiveCompleted = true;
        }
    }

}
