using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public float rotationSpeed = 1.0f;

    void Start()
    {
        if (player != null)
        {
            offset = transform.position - player.transform.position;
        }
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }

        // if (Input.GetMouseButton(1))
        // {
        //     float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        //     transform.Rotate(0, horizontalRotation, 0);
        // }
    }
}

