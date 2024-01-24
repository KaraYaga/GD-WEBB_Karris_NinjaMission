using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
//Player Singleton (God Program announces the current Chosen One on awake)
    public static Player playerInstance;

    //Enemy being targeted for sneak attack
    [SerializeField] Enemy targetEnemy;

    [SerializeField] float speed = 50f;
    [SerializeField] float rotationSpeed = 50f;

    private Rigidbody _rigidbody;
    private Vector2 input;

//Sight Status
    private bool hidden = false;

//AWAKE SINGLETON

    private void Awake()
    {
        playerInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

//UPDATE MOVEMENT INPUT
    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        checkIfHidden();

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }

//FRAME UPDATE FOR PHSYICS BASED MOVEMENT
    private void FixedUpdate()
    {
        //TRANSLATION
        Vector3 movement = new Vector3(transform.forward.x * input.y * speed * Time.deltaTime, 0, transform.forward.z * input.y * speed * Time.deltaTime);
        _rigidbody.MovePosition(transform.position + movement);

        //ROTATION
        Vector3 desiredRotation = new Vector3(0, input.x * rotationSpeed * Time.deltaTime, 0);
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(desiredRotation));

    }

//Dot Product HIDDEN or SEEN (Comparing vectors and directions)
    void checkIfHidden()
    {
        //Checking if Hidden or Seen
        Vector3 enemyToPlayer;

        enemyToPlayer = (transform.position - targetEnemy.transform.position).normalized;

        if (Vector3.Dot(enemyToPlayer, targetEnemy.transform.forward) < 0)
        {
            Debug.Log("The enemy doesn't see you!");

            hidden = true;
        }
        else
        {
            Debug.Log("You are seen!");

            hidden = false;
        }
    }

//ATTACK
    void Attack()
    {
        if (hidden == true)
        {
            //Checking if Player is looking at enemy
            if (Vector3.Dot(targetEnemy.transform.forward , transform.forward) > 0)
            {
                Debug.Log("You can attack!");

                if (targetEnemy.currentHealth > 0)
                {
                    //10 points of damage to the enemy
                    targetEnemy.TakeDamage(10);

                }
            }
            else
            {
                Debug.Log("You cannot attack!");
            }
        }
    }
}