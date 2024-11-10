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
    [SerializeField] private float pickupRange = 2f;

    private GameObject currentItem;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            currentItem = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item") && currentItem == other.gameObject)
        {
            currentItem = null;
        }
    }

    private void Update()
    {
        if (currentItem != null && Vector3.Distance(transform.position, currentItem.transform.position) <= pickupRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpItem(currentItem);
            }

        }
    }

    private void PickUpItem(GameObject item)
    {
        Debug.Log("Подобрано: " + item.name);
        Destroy(item);
    }

}
