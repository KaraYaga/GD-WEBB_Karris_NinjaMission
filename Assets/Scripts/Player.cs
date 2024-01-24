using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] private float rotationSpeed, speed;
    private Rigidbody rigidbody;
    private Vector2 input;
    private bool hidden = false;
    private bool seen = false;

// Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

//UPDATE MOVEMENT INPUT
    private void Update()
    {
        input = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
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
        if (hidden == true && UnityEngine.Input.GetKeyDown("F"))
        {


            //if (currentHealth != null)
            //{

            //    // Check if enemyHealth exists
            //    if (currentHealth != null)
            //    {
            //        //10 points of damage to the enemy
            //        currentHealth.TakeDamage(10);
            //    }
            //}
        }
    }
}