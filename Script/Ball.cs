using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float _speed = 23f;
    Rigidbody _rigidbody;
    Vector3 _velocity;
    Renderer _renderer;
    void Start() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        Invoke("launch", 0.5f);
    }
    void launch()
    {
        _rigidbody.velocity = Vector3.up * _speed;

    }

    void FixedUpdate()
    {
        _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
        _velocity = _rigidbody.velocity;
        if (!_renderer.isVisible)
        {
            GameManager.instance.Balls--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal);
    }
}
