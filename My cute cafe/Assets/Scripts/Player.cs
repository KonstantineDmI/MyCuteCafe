using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private DynamicJoystick _joystick;
    [SerializeField] private Animator _animator;
          
    [SerializeField] private string name;
    [SerializeField] private int health;
    [SerializeField] private bool isDead;
    [SerializeField] private float speed;
    [SerializeField] private float drag;
    [SerializeField] private float smoothRotationSpeed;
    

    private void FixedUpdate()
    {
        Movement();


    }
    

    private void Movement()
    {
        _rigidbody.drag = drag;
        _rigidbody.angularDrag = drag;

        _rigidbody.velocity = new Vector3(_joystick.Horizontal * speed, _rigidbody.velocity.y, _joystick.Vertical * speed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }

        if (health <= 0)
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

}   
