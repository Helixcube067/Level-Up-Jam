using System;
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
    [SerializeField] float _JumpHeight = 7.0f;

    public Animator _Animator;

    // Class variables
    Rigidbody2D _RigidBody;
    PolygonCollider2D _Collider;
    float _BodyTemperature;
    Text _BodyTemperatureText;
    float _FootstepTimer;
    bool _PlayingMovementSound;
    SceneMovement _SceneMovement;

    // Start is called before the first frame update
    void Start()
    {
        _BodyTemperature = _InitialBodyTemperature;
        _RigidBody = GetComponent<Rigidbody2D>();
        _Collider = GetComponent<PolygonCollider2D>();
        _BodyTemperatureText = GameObject.Find("TemperatureValue").GetComponent<Text>();
        _BodyTemperatureText.text = _BodyTemperature.ToString();
        _FootstepTimer = 0.0f;
        _PlayingMovementSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleTemperature();
        if(_PlayingMovementSound)
        {
            _FootstepTimer += Time.deltaTime;
            if (_FootstepTimer > 0.3f)
            {
                _PlayingMovementSound = false;
                _FootstepTimer = 0.0f;
            }
        }
    }
   
    void handleMovement()
    {
        // Horizontal
        float HorizontalInput = Input.GetAxisRaw("Horizontal");
        _Animator.SetFloat("Speed", Mathf.Abs(HorizontalInput) * Convert.ToInt32(IsGrounded()));
        float HorizontalMovement = HorizontalInput * _MovementSpeed * Time.deltaTime ;// Horizontal axis controlled by A and D
        if ( (HorizontalInput > 0.1 || HorizontalInput < -0.1) && !_PlayingMovementSound)
        {
            if(IsGrounded())
            {
                AkSoundEngine.PostEvent("Play_FootstepGravel", gameObject);
            }
            else
            {
                AkSoundEngine.PostEvent("Play_LongJump", gameObject);
            }
            _PlayingMovementSound = true;
        }
        Vector2 Movement = new Vector2(HorizontalMovement, _RigidBody.velocity.y);
        _RigidBody.velocity = Vector2.Lerp(_RigidBody.velocity, Movement, _SmoothMovement); // Smooths stopping/starting movement
        if(_RigidBody.velocity.x > 0.5)
        {            
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(_RigidBody.velocity.x < -0.5)
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
        if (IsGrounded() && Input.GetButtonDown("Jump")) // default key set to space
        {
            AkSoundEngine.PostEvent("Play_Jump", gameObject);
            _RigidBody.AddForce(new Vector2(0, _JumpHeight), ForceMode2D.Impulse);
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
   
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Death
        if (collision.gameObject.tag == "Death")
        {
            Die();
        }
    }
    
    void Die()
    {
        //TODO: Play death sound, show endgame screen?
        // Sending you back to the Title Screen when you die for now;
        _SceneMovement.LoadLevel("TitleScreen");
    }

    public float GetBodyTemp() {
        return _BodyTemperature;
    }

    public bool IsGrounded()
    {
        // Check if we are on top of a floor object
        RaycastHit2D hit = Physics2D.BoxCast(_Collider.bounds.center, _Collider.bounds.size, 0, Vector2.down, 0.1f, _Floor);
        return hit.collider;
    }
}
