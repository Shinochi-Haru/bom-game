using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rb = default;
    [SerializeField] float _movePower = 0;
    [SerializeField] float _jumpPower = 0;
    private bool _isGrounded;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            this.transform.forward = dir;
        }
        dir = dir.normalized * _movePower;
        float y = _rb.velocity.y;

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            y = _jumpPower;
        }

        _rb.velocity = dir * _movePower + Vector3.up * y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            _isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            _isGrounded = false;
        }
    }
}