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
    [SerializeField] string _InitialBiome;
    [SerializeField] float _JumpHeight = 7.0f;
    [SerializeField] GameObject _HUD;
    [SerializeField] GameObject _ReplayMenu;
    [SerializeField] GameObject _WinMenu;

    private SoundbankScript _Soundbank;

    public Animator _Animator;
   
    // Class variables
    Rigidbody2D _RigidBody;
    //PolygonCollider2D _Collider;
    BoxCollider2D _Collider;
    float _BodyTemperature;
    float _FootstepTimer;
    bool _PlayingMovementSound;
    double _health = 100.0;
    Slider _HealthSlider;
    Slider _TemperatureSlider;
    string _Biome;

    // Start is called before the first frame update
    void Start()
    {
        _BodyTemperature = _InitialBodyTemperature;
        _RigidBody = GetComponent<Rigidbody2D>();
        _Collider = GetComponent<BoxCollider2D>();
        _FootstepTimer = 0.0f;
        _PlayingMovementSound = false;
        Time.timeScale = 1f;
        _Soundbank = GameObject.Find("SoundBank").GetComponent<SoundbankScript>();
        _HealthSlider = GameObject.Find("HealthbarSlider").GetComponent<Slider>();
        _TemperatureSlider = GameObject.Find("TemperatureSlider").GetComponent<Slider>();
        _Biome = _InitialBiome;
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleTemperature();
        HandleHealth();
        DeathCheck();
        // Timer to start playing movement sounds only every 0.3 seconds so they do not overlap.
        if (_PlayingMovementSound)
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
        // Horizontal Movement
        float HorizontalInput = Input.GetAxisRaw("Horizontal"); // Horizontal axis controlled by A and D
        _Animator.SetFloat("Speed", Mathf.Abs(HorizontalInput) * Convert.ToInt32(IsGrounded()));
        float HorizontalMovement = HorizontalInput * _MovementSpeed * Time.deltaTime ; 
        if ( (HorizontalInput > 0.1 || HorizontalInput < -0.1) && !_PlayingMovementSound)
        {
            if(IsGrounded())
            {
                _Soundbank.PlaySoundFootsteps(_Biome);
            }
            else
            {
                _Soundbank.PlaySoundFloat();
            }
            _PlayingMovementSound = true;
        }
        Vector2 Movement = new Vector2(HorizontalMovement, _RigidBody.velocity.y);
        _RigidBody.velocity = Vector2.Lerp(_RigidBody.velocity, Movement, _SmoothMovement); // Smooths stopping/starting movement

        // Flip the sprite depending on walk direction
        if(_RigidBody.velocity.x > 0.5)
        {            
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(_RigidBody.velocity.x < -0.5)
        {            
            _RigidBody.transform.localScale = new Vector3(-1, 1, 1);
        }
        
        // Make it impossible to walk back outside camera view
        float CameraHorizontalHalfSize = Camera.main.orthographicSize * Screen.width / Screen.height;
        float CameraMin = Camera.main.transform.position.x - CameraHorizontalHalfSize;
        float CameraMax = Camera.main.transform.position.x + CameraHorizontalHalfSize;
        float X = Mathf.Clamp(_RigidBody.position.x, CameraMin, CameraMax);
        _RigidBody.position = new Vector2(X, _RigidBody.position.y);

        // Jump
        if (IsGrounded() && Input.GetButtonDown("Jump")) // default key set to space
        {
            _Animator.SetTrigger("Jump");
            _Soundbank.PlaySoundJump();
            _RigidBody.AddForce(new Vector2(0, _JumpHeight), ForceMode2D.Impulse);
        }
    }

    void handleTemperature()
    {
        if (_Biome == "Desert")
        {
            _BodyTemperature += 0.5f * Time.deltaTime;
        }
        else if (_Biome == "Winter")
        {
            _BodyTemperature -= 0.5f * Time.deltaTime;
        }
        else if (_Biome == "Forest")
        {
            if(_BodyTemperature < 35.99)
            {
                _BodyTemperature += 0.5f * Time.deltaTime;
            }
            else if(_BodyTemperature > 36.01)
            {
                _BodyTemperature -= 0.5f * Time.deltaTime;
            }
        }
        _BodyTemperature = Mathf.Clamp(_BodyTemperature, 25, 47); // Make the value max out on the sides of the temperature bar
        /*      if (Input.GetButtonDown("TemperatureUp")) // default key set to W
                {
                    _BodyTemperature += 1;
                }
                if (Input.GetButtonDown("TemperatureDown")) // default key set to S
                {
                    _BodyTemperature -= 1;
                }*/
        _TemperatureSlider.value = (int)_BodyTemperature;
    }

    void HandleHealth()
    {
        //Temperature goes up in desert, down in snow, goes back to 36.
        //Once below 30 you start losing health.
        //Once above 42 you start losing health.
        if (_BodyTemperature >= 42)
            _health -= 3 * Time.deltaTime;
        else if (_BodyTemperature <= 30)
        { //made this -5 for testing health drain
            _health -= 3 * Time.deltaTime;
        }
        _HealthSlider.value = (int)_health;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            _HUD.gameObject.SetActive(false);
            _WinMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
   
    void DeathCheck() 
    {
        if (gameObject.transform.position.y <= -5 || _health <= 0) 
        {
            Debug.Log("Pos: " + gameObject.transform.position.y);
            Debug.Log("Health: " + _health);
            _HUD.gameObject.SetActive(false);
            _ReplayMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public float GetBodyTemp()
    {
        return _BodyTemperature;
    }
    
    public string GetBiome()
    {
        return _Biome;
    }

    public void SetBiome(string new_biome)
    {
        Debug.Log("Biome set to: " + new_biome);
        _Biome = new_biome;
    }

    public bool IsGrounded()
    {
        // Check if we are on top of a floor object
        RaycastHit2D hit = Physics2D.BoxCast(_Collider.bounds.center, _Collider.bounds.size, 0, Vector2.down, 0.1f, _Floor);
        return hit.collider;
    }
}
