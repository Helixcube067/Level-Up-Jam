using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlatform : MonoBehaviour
{
    public double duration = 10.0;
    bool triggered = false;

    private void Update()
    {
        if (triggered)
            duration -= 0.01;
        if (duration <= 0)
            this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            triggered = true;
    }
}
