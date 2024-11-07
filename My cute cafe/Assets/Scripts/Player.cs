using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private int health;
    [SerializeField] private bool isDead;
    [SerializeField] private float speed;
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
