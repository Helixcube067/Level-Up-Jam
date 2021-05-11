using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderSound : MonoBehaviour
{
    // Start is called before the first frame update
    public void onClick()
    {
        AkSoundEngine.PostEvent("Play_Slider", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
