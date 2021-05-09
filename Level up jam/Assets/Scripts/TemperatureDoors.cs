using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureDoors : MonoBehaviour
{
    public float tempRequired;
    private CharacterScript player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            if (player.GetBodyTemp() <= tempRequired)
                Debug.Log("Sorry no passing");
            else
                this.gameObject.SetActive(false);
        }
    }
}
