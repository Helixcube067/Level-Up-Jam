using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureDoors : MonoBehaviour
{
    public float minTempRequired;
    public float maxTempRequired;
    private CharacterScript player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            float bodyTemp = player.GetBodyTemp();
            if (bodyTemp < minTempRequired || bodyTemp > maxTempRequired)
                Debug.Log("Sorry no passing");
            else
                this.gameObject.SetActive(false);
        }
    }
}
