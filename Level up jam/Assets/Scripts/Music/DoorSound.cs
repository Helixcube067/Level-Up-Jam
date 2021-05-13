using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        AkSoundEngine.PostEvent("Play_Money", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
