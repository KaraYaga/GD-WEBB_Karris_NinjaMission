using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] private float rotationSpeed, speed;
    private Rigidbody rigidbody;
    private bool hidden = false;
    private bool seen = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    //UPDATE MOVEMENT INPUT
    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    //FRAME UPDATE FOR PHSYICS BASED MOVEMENT
    private void FixedUpdate()
    {
        //TRANSLATION
        Vector3 movement = new Vector3(transform.forward.x * input.y * speed * Time.deltaTime, 0, transform.forward.z * input.y * speed * Time.deltaTime);
        rigidbody.MovePosition(transform.position + movement);

        //ROTATION
        Vector3 desiredRotation = new Vector3(0, input.x * rotationSpeed * Time.deltaTime, 0);
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(desiredRotation));
    }

//ATTACK
    void Attack()
    {
        if (hidden == true && )
        {
            Enemy enemyHealth = GetComponentInParent<currentHealth>();

            if (enemyHealth != null)
            {

                // Check if enemyHealth exists
                if (enemyHealth != null)
                {
                    //5 points of damage to the enemy
                    enemyHealth.TakeDamage(5);
                }
            }
        }
    }
}