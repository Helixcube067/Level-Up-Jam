using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Editor settings
    [SerializeField] float _XMin;
    [SerializeField] float _XMax;
    [SerializeField] float _YMin;
    [SerializeField] float _YMax;

    // Class variables
    GameObject _Player;

    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        // Make the camera move with the player
        float X = Mathf.Clamp(_Player.transform.position.x, _XMin, _XMax);
        float Y = Mathf.Clamp(_Player.transform.position.y, _YMin, _YMax);
        // Make it impossible for the camera to move back
        X = Mathf.Max(X, transform.position.x);
        transform.position = new Vector3(X, Y, transform.position.z);
    }
}
