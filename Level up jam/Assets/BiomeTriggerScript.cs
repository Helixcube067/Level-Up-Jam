using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeTriggerScript : MonoBehaviour
{
    [SerializeField] bool _IsDesert;
    [SerializeField] bool _IsForest;
    [SerializeField] bool _IsWinter;

    GameObject GameManager;
    private SoundbankScript _Soundbank;

    // Start is called before the first frame update
    void Start()
    {
        _Soundbank = GameObject.Find("SoundBank").GetComponent<SoundbankScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(_IsDesert)
            {
                _Soundbank.PlayBackgroundSound("Desert");
            }
            else if(_IsForest)
            {
                _Soundbank.PlayBackgroundSound("Forest");
            }
            else if(_IsWinter)
            {
                _Soundbank.PlayBackgroundSound("Winter");
            }
        }
    }
}
