using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    // Start is called before the first frame update
   public void onClick()
    {
        AkSoundEngine.PostEvent("Play_SoundButton", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
