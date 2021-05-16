using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Animator _Animator;
    [SerializeField] TemperatureDoorScript _Door;
    bool _IsPushed;
    private SoundbankScript _Soundbank;

    void Awake()
    {
        _Soundbank = GameObject.Find("SoundBank").GetComponent<SoundbankScript>();
        _Animator.speed = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !_IsPushed)
        {
            _IsPushed = true;
            _Animator.speed = 1;
            _Soundbank.PlaySoundLevelButton();
            _Door.OpenDoor();
        }
    }
}
