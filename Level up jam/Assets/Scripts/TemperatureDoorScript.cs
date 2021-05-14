using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureDoorScript : MonoBehaviour
{
    [SerializeField] Animator _Animator;
    [SerializeField] BoxCollider2D _Collider;
    public float minTempRequired;
    public float maxTempRequired;
    private CharacterScript _Player;
    private SoundbankScript _Soundbank;

    void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
        _Soundbank = GameObject.Find("SoundBank").GetComponent<SoundbankScript>();
        _Animator.speed = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            float bodyTemp = _Player.GetBodyTemp();
            if (bodyTemp < minTempRequired || bodyTemp > maxTempRequired)
            {
                Debug.Log("Sorry no passing");
            }
            else
            {
                OpenDoor();
            }
        }
    }

    public void OpenDoor()
    {
        _Soundbank.PlaySoundDoor();
        _Animator.speed = 1;
        Invoke("DisableCollider", 0.5f);
    }

    void DisableCollider()
    {
        _Collider.enabled = false;
    }
}
