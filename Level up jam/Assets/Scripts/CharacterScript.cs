using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    // Editor settings
    [SerializeField] [Range(0, 1)] float _SmoothMovement;
    [SerializeField] LayerMask _Floor;

    // Class properties
    Rigidbody2D _RigidBody;
    PolygonCollider2D _Collider;

    // Start is called before the first frame update
    void Start()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
        _Collider = GetComponent<PolygonCollider2D>();
        //_rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
    }

    void handleMovement()
    {
        // Horizontal
        float Speed = 1000.0f;
        float HorizontalMovement = Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime;
        Vector2 Movement = new Vector2(HorizontalMovement, _RigidBody.velocity.y);
        _RigidBody.velocity = Vector2.Lerp(_RigidBody.velocity, Movement, _SmoothMovement); // Smooths stopping/starting movement

        // Jump
        float JumpHeight = 6.0f;
        // Check if we are on top of a floor object
        RaycastHit2D hit = Physics2D.BoxCast(_Collider.bounds.center, _Collider.bounds.size, 0, Vector2.down, 0.1f, _Floor);
        if (Input.GetButtonDown("Jump") && hit.collider)
        {
            _RigidBody.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);
        }
    }
}
