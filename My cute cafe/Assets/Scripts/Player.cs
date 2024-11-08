using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;

    [SerializeField] private string name;
    [SerializeField] private int health;
    [SerializeField] private bool isDead;
    [SerializeField] private float speed;

    private void FixedUpdate()
    {
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

    public void ApplyDamage(int damage)
    {
        this.health = this.health - damage;
        if (this.health <= 0)
        {
            this.isDead = true;
        }

    }

    public void ApplyHeal(int heal) 
    {
        if (GetHealth() >= 100)
        {
            this.health = 100;
            Debug.Log($"100 hp was reached : {GetHealth()}");
            return;
        }
        this.health = this.health + heal;
        
    }

    public void Rename(string newName) 
    {
        this.name = newName;
    }
     
    private void UpSpeed(float speedUp)
    {
        this.speed = this.speed + speedUp;
        if(this.speed >= 10)
        {
            ApplyHeal(5);
        }
    }

    public float GetSpeed()
    {
        return this.speed;
    }


    public int GetHealth()
    {
        Rename("ARTEM");
        return this.health;    
    }

    private void OnMouseDown()
    {
        if(IsDead() == true)
        {
            Debug.Log($"Player is dead");

            return;
        }
        ApplyDamage(Random.RandomRange(1, 20)); // рандомное число от 1 до 20 
        Debug.Log($"Player Name: {GetName()}, Health: {GetHealth()}");
    }

    private void OnMouseExit()
    {
        ApplyHeal(5);
        Debug.Log($"Player was healed : {GetHealth()}");
    }

    public string GetName()
    {
        return this.name;
    }

    public bool IsDead()
    {
        return this.isDead;
    }


}
