using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara_scripit : MonoBehaviour
{
    [SerializeField] float speed = 1.25f;
    [SerializeField] float xMin = -1.90f, xMax = 1.90f, yMin = 0.3f, yMax = 0.75f;
    [SerializeField] Vector3 offset;

    private bool bounds = true;
    public Vector3 targetPostion;
    public Transform player;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        targetPostion = new Vector3(player.position.x, player.position.y, transform.position.z) - offset;

        transform.position = Vector3.Lerp(transform.position, targetPostion, speed * Time.fixedDeltaTime);

        if (bounds)
        {
            transform.position = new Vector3
                                           (
                                               Mathf.Clamp(player.position.x, xMin, xMax),
                                               Mathf.Clamp(player.position.y, yMin, yMax),
                                               transform.position.z
                                           );
        }

    }
}
