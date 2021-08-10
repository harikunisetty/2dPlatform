using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private RaycastHit2D Hit;
    public float range = 0.1f;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GroundCheck()
    {
        Hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.up, range, layerMask);


        if (Hit.collider != null)
        {


            if (Hit.collider.gameObject.layer == 6)
            {
                Destroy(Hit.collider.gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Vector3(transform.position.x , transform.position.y, transform.position.z),
       Vector3.up* range);
    }
}