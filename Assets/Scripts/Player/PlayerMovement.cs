using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerStats
{
    private Rigidbody2D _rb;

    [SerializeField] InputAction _action;

    private float _moveSpeedMultiplier = 1f; 




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        Movement(); 
    }


    private void Movement()
    {
        Vector2 _direction; 

        _direction = _action.ReadValue<Vector2>();
        _direction.Normalize();

        _rb.linearVelocity = _direction * (movementSpeed * _moveSpeedMultiplier);
    }



    private void OnEnable()
    {
        _action.Enable();
    }

    private void OnDisable()
    {
        _action.Disable();
    }
}
