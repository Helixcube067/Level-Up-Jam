using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureDoors : MonoBehaviour
{
    [SerializeField] Animator _Animator;
    [SerializeField] BoxCollider2D _Collider;
    public float minTempRequired;
    public float maxTempRequired;
    private CharacterScript player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
        _Animator.speed = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            float bodyTemp = player.GetBodyTemp();
            if (bodyTemp < minTempRequired || bodyTemp > maxTempRequired)
            {
                Debug.Log("Sorry no passing");
            }
            else
            {
                //AkSoundEngine.PostEvent("Play_Money", gameObject); TODO: play sound when sound is available
                _Animator.speed = 1;
                Invoke("DisableCollider", 0.5f);
            }
        }
    }
    void DisableCollider()
    {
        _Collider.enabled = false;
    }
}
