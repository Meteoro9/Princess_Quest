using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _groundCheckDistance;
    [SerializeField] LayerMask _groundLayer;

    Rigidbody _rb;
    float _moveH;
    Vector3 _movement;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    void Update()
    {
        bool grounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);

        _moveH = Input.GetAxis("Horizontal");

        // Salto
        if (Input.GetButtonDown("Jump") && grounded)
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Movimiento horizontal
        _movement = Vector3.right * _moveH * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + _movement);

        // Rotación instantánea según la dirección
        if (_moveH > 0.1f) // Derecha
        {
            _rb.MoveRotation(Quaternion.Euler(0, 0f, 0));
        }
        else if (_moveH < -0.1f) // Izquierda
        {
            _rb.MoveRotation(Quaternion.Euler(0, 180f, 0));
        }
    }

    void OnDrawGizmos()
    {
        // Dibuja el raycast, Verde significa que toca el suelo, rojo que no 
        bool grounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);
        Gizmos.color = grounded ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _groundCheckDistance);
        
        // Dibuja una esfera pequeña al final del rayo
        Gizmos.DrawSphere(transform.position + Vector3.down * _groundCheckDistance, 0.05f);
    }
}