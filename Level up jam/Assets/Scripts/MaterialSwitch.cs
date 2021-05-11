using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]


public class MaterialSwitch : MonoBehaviour
{
   public string SwitchGroup = "Footsteps";
   public string Switch = "Grass";
   public string ExitSwitch = "Concrete";
   public GameObject Character;


   private void OnTriggerEnter(Collider other)
    {
        AkSoundEngine.SetSwitch("Footsteps", "Grass", gameObject);
    }
    private void OnTriggerExit(Collider other)
    {

    }  
}
