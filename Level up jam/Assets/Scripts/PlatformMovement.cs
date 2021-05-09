using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform topPos, botPos;
    public float speed;
    public Transform platform;

    Vector3 destination;
    void Start()
    {
        destination = botPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == topPos.position)
            destination = botPos.position;
        else if (transform.position == botPos.position)
            destination = topPos.position;
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }
}
