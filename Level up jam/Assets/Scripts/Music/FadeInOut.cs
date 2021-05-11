using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    
    public void LoadNextLevel(string name)
    {
        StartCoroutine(LevelLoad(name));
    }

    IEnumerator LevelLoad(string name)
    {
        yield return new WaitForSeconds(3f);
        Application.LoadLevel(name);
    }

   
    public void onClick()
    {
        AkSoundEngine.SetRTPCValue("Menu_Music", 0f, GameObject.FindGameObjectWithTag("MainCamera"), 4000);
    }
}
