using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("Play_MovingPlatform", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
