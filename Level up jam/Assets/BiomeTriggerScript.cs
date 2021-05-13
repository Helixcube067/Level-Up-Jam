using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeTriggerScript : MonoBehaviour
{
    [SerializeField] bool _IsDesert;
    [SerializeField] bool _IsForest;
    [SerializeField] bool _IsWinter;

    GameObject GameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AkSoundEngine.PostEvent("Stop_Forest_Biom", gameObject);
        }
    }
}
