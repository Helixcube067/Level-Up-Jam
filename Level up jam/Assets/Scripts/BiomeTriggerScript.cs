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
    private CharacterScript _Player;

    // Start is called before the first frame update
    void Start()
    {
        _Soundbank = GameObject.Find("SoundBank").GetComponent<SoundbankScript>();
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Trigger by player");
            if(_IsDesert)
            {
                _Soundbank.PlayBackgroundSound("Desert");
                _Player.SetBiome("Desert");
            }
            else if(_IsForest)
            {
                _Soundbank.PlayBackgroundSound("Forest");
                _Player.SetBiome("Forest");
            }
            else if(_IsWinter)
            {
                _Soundbank.PlayBackgroundSound("Winter");
                _Player.SetBiome("Winter");
            }
        }
    }
}
