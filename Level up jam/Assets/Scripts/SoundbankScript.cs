using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundbankScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Volume control
    public void SetVolume(float volume)
    {
        AkSoundEngine.SetRTPCValue("MainVolume", volume);
    }

    public float GetVolume()
    {
        float volume;
        int type = 1;
        AkSoundEngine.GetRTPCValue("MainVolume", 0, 0, out volume, ref type);
        return volume;
    }

    // Conditional sounds
    public void PlaySoundFootsteps(string biome)
    {
        if (biome == "Winter")
        {
            PlaySoundFootstepSnow();
        }
        else if(biome == "Desert")
        {
            PlaySoundFootstepSand();
        }
        else if(biome == "Forest")
        {
            PlaySoundFootstepGrass();
        }
    }

    // This function should also stop the current background music and transition
    public void PlayBackgroundSound(string biome)
    {
        StopBackgroundSound();
        if (biome == "MainMenu")
        {
            PlayMainMenu();
        }
        else if(biome == "Winter")
        {
            PlayWinterBiome();
        }
        else if(biome == "Desert")
        {
            PlayDesertBiome();
        }
        else if(biome == "Forest")
        {
            PlayForestBiome();
        }
        else if(biome == "Win")
        {
            PlayWinSong();
        }
    }

    public void StopBackgroundSound()
    {
        StopWinterBiome();
        StopMainMenu();
        StopDesertBiome();
        StopForestBiome();
        StopWinSong();
    }

    // One time play sounds
    public void PlaySoundButton()
    {
        AkSoundEngine.PostEvent("Play_SoundButton", gameObject);
    }

    public void PlaySoundLevelButton()
    {
        AkSoundEngine.PostEvent("Play_LevelButton", gameObject);
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

    public void PlaySoundFootstepSand()
    {
        AkSoundEngine.PostEvent("Play_FootstepsSand", gameObject);
    }

    public void PlaySoundFootstepGrass()
    {
        AkSoundEngine.PostEvent("Play_FootstepsGrass", gameObject);
    }

    public void PlaySoundFootstepSnow()
    {
        AkSoundEngine.PostEvent("Play_FootstepsSnow", gameObject);
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

    public void PlayWinSong()
    {
        AkSoundEngine.PostEvent("Play_WinSong", gameObject);
    }

    public void PlayDesertBiome()
    {
        AkSoundEngine.PostEvent("Play_Desert_Boim", gameObject);
    }

    public void StopMainMenu()
    {
        AkSoundEngine.PostEvent("Stop_Main_Menu", gameObject);
    }

    public void StopForestBiome()
    {
        AkSoundEngine.PostEvent("Stop_Forest_Biom", gameObject);
    }

    public void StopWinSong()
    {
        AkSoundEngine.PostEvent("Stop_WinSong", gameObject);
    }

    public void StopWinterBiome()
    {
        AkSoundEngine.PostEvent("Stop_Winter_Biom", gameObject);
    }

    public void StopDesertBiome()
    {
        AkSoundEngine.PostEvent("Stop_Desert_Boim", gameObject);
    }
}
