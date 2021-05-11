using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{
    // Editor settings
    [SerializeField] float _MovementSpeed;
    [SerializeField] [Range(0, 1)] float _SmoothMovement;
    [SerializeField] LayerMask _Floor;
    [SerializeField] float _InitialBodyTemperature;

    // Class variables
    Rigidbody2D _RigidBody;
    PolygonCollider2D _Collider;
    float _BodyTemperature;
    Text _BodyTemperatureText;

    // Start is called before the first frame update
    void Start()
    {
        _BodyTemperature = _InitialBodyTemperature;
        _RigidBody = GetComponent<Rigidbody2D>();
        _Collider = GetComponent<PolygonCollider2D>();
        _BodyTemperatureText = GameObject.Find("TemperatureValue").GetComponent<Text>();
        _BodyTemperatureText.text = _BodyTemperature.ToString();
        //_rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleTemperature();
    }

    void handleMovement()
    {
        
        // Horizontal
        float HorizontalMovement = Input.GetAxisRaw("Horizontal") * _MovementSpeed * Time.deltaTime ;// Horizontal axis controlled by A and D
        Vector2 Movement = new Vector2(HorizontalMovement, _RigidBody.velocity.y);
        _RigidBody.velocity = Vector2.Lerp(_RigidBody.velocity, Movement, _SmoothMovement); // Smooths stopping/starting movement
        if(_RigidBody.velocity.x > 0.5)
        {
            
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(_RigidBody.velocity.x < -0.1)
        {
            
            _RigidBody.transform.localScale = new Vector3(-1, 1, 1);
        }
        _RigidBody.rotation = 0;
        
        // Make it impossible to walk back outside camera view
        float CameraHorizontalHalfSize = Camera.main.orthographicSize * Screen.width / Screen.height;
        float CameraMin = Camera.main.transform.position.x - CameraHorizontalHalfSize;
        float CameraMax = Camera.main.transform.position.x + CameraHorizontalHalfSize;
        float X = Mathf.Clamp(_RigidBody.position.x, CameraMin, CameraMax);
        _RigidBody.position = new Vector2(X, _RigidBody.position.y);

        // Jump
        float JumpHeight = 6.0f;
        // Check if we are on top of a floor object
        RaycastHit2D hit = Physics2D.BoxCast(_Collider.bounds.center, _Collider.bounds.size, 0, Vector2.down, 0.1f, _Floor);
        if (Input.GetButtonDown("Jump") && hit.collider) // default key set to space
        {
            AkSoundEngine.PostEvent("Play_Footsteps", gameObject);
            _RigidBody.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);
        }
    }

    void handleTemperature()
    {
        if(Input.GetButtonDown("TemperatureUp")) // default key set to W
        {
            _BodyTemperature += 1;
            _BodyTemperatureText.text = _BodyTemperature.ToString();
        }
        if(Input.GetButtonDown("TemperatureDown")) // default key set to S
        {
            _BodyTemperature -= 1;
            _BodyTemperatureText.text = _BodyTemperature.ToString();
        }
    }

    public float GetBodyTemp() {
        return _BodyTemperature;
    }
}
