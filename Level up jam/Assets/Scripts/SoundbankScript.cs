using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundbankScript : MonoBehaviour
{
    public AK.Wwise.RTPC _Volume;

    // Start is called before the first frame update
    void Start()
    {
        _Volume.SetGlobalValue(1f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Conditional sounds
    public void PlaySoundFootsteps(string biome)
    {
        if (biome == "Winter")
        {
            PlaySoundFootstepConcrete();
        }
        else if(biome == "Desert")
        {
            PlaySoundFootstepGravel();
        }
        else if(biome == "Forest")
        {
            PlaySoundFootstepGrass();
        }
    }

    // This function should also stop the current background music and transition
    public void PlayBackgroundSound(string biome)
    {
        if(biome == "MainMenu")
        {
            StopDesertBiome();
            StopForestBiome();
            StopWinterBiome();
            PlayMainMenu();
        }
        else if(biome == "Winter")
        {
            StopDesertBiome();
            StopForestBiome();
            StopMainMenu();
            PlayWinterBiome();
        }
        else if(biome == "Desert")
        {
            StopWinterBiome();
            StopForestBiome();
            StopMainMenu();
            PlayDesertBiome();
        }
        else if(biome == "Forest")
        {
            StopWinterBiome();
            StopMainMenu();
            StopDesertBiome();
            PlayForestBiome();

        }
    }

    // One time play sounds
    public void PlaySoundButton()
    {
        AkSoundEngine.PostEvent("Play_SoundButton", gameObject);
    }

    public void PlaySoundJump()
    {
        AkSoundEngine.PostEvent("Play_Jump", gameObject);
    }

    public void PlaySoundFloat()
    {
        AkSoundEngine.PostEvent("Play_LongJump", gameObject);
    }

    public void PlaySoundDoor()
    {
        AkSoundEngine.PostEvent("Play_Money", gameObject);
    }

    public void PlaySoundSlider()
    {
        AkSoundEngine.PostEvent("Play_Slider", gameObject);
    }

    public void PlaySoundPlatform()
    {
        AkSoundEngine.PostEvent("Play_MovingPlatform", gameObject);
    }

    public void PlaySoundFootstepConcrete()
    {
        AkSoundEngine.PostEvent("Play_FootstepConcrete", gameObject);
    }

    public void PlaySoundFootstepGrass()
    {
        //AkSoundEngine.PostEvent("Play_Footsteps", gameObject);
        AkSoundEngine.PostEvent("Play_FootstepsGrass", gameObject);
    }

    public void PlaySoundFootstepGravel()
    {
        AkSoundEngine.PostEvent("Play_FootstepGravel", gameObject);
    }

    // Background music sounds
    public void PlayMainMenu()
    {
        AkSoundEngine.PostEvent("Play_Main_Menu", gameObject);
    }

    public void PlayForestBiome()
    {
        AkSoundEngine.PostEvent("Play_Forest_Biom", gameObject);
    }

    public void PlayWinterBiome()
    {
        AkSoundEngine.PostEvent("Play_Winter_Biom", gameObject);
    }

    public void PlayDesertBiome()
    {
        AkSoundEngine.PostEvent("Play_Desert_Biom", gameObject);
    }
    public void StopMainMenu()
    {
        AkSoundEngine.PostEvent("Stop_Main_Menu", gameObject);
    }
    public void StopForestBiome()
    {
        AkSoundEngine.PostEvent("Stop_Forest_Biom", gameObject);
    }

    public void StopWinterBiome()
    {
        AkSoundEngine.PostEvent("Stop_Winter_Biom", gameObject);
    }

    public void StopDesertBiome()
    {
        AkSoundEngine.PostEvent("Stop_Desert_Biom", gameObject);
    }
}
