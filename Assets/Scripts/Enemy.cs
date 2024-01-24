using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    public int currentHealth;


 //SET HEALTH
    private void Start()
    {
        currentHealth = maxHealth; // Set initial health to the maximum health
    }

    private void Update()
    {


    }

//TAKE DAMAGE
    public void TakeDamage(int damage)
    {
        // Ensure the damage value is non-negative
        if (damage < 0)
        {
            Debug.LogWarning("Already dead");
            return;
        }

        // Subtract the damage from the current health
        currentHealth -= damage;

        // Check if the enemy's health has reached zero or below
        if (currentHealth <= 0)
        {
            Die();
        }
    }

//DIE
    private void Die()
    {
        // Perform any actions you want when the enemy dies
        Debug.Log("Enemy has died!");
        Destroy(gameObject); // Destroy the GameObject when the enemy dies
    }

}