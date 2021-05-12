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

    /* attempt to do parenting to try and make the platform smoother but has a bug where the character expands
     * im thinking the character expands due to being a child of the platform and adjusting its transform but it SHOULDNT be doing this since the setparent should be preventing that
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.transform.SetParent(this.gameObject.transform, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.transform.SetParent(null);
    }*/
}
