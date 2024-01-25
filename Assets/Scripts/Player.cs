using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : MonoBehaviour
{
    //Player Singleton (God Program announces the current Chosen One on awake)
    public static Player playerInstance;

    [SerializeField] float speed = 50f;
    [SerializeField] float rotationSpeed = 50f;

    private Rigidbody _rigidbody;
    private Vector2 input;

    [SerializeField] GameObject seen;
    [SerializeField] GameObject unseen;
    [SerializeField] GameObject attack;

    //Sight Status
    private bool hidden = false;
    private bool canAttack = false;

    Enemy targetEnemy;

    //AWAKE SINGLETON

    private void Awake()
    {
        playerInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        // Popups inactive
        seen.gameObject.SetActive(false);
        unseen.gameObject.SetActive(false);
        attack.gameObject.SetActive(false);

    }

//UPDATE MOVEMENT INPUT AND POP UPS STATUS
    private void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        checkIfHidden();
        checkIfFacingEnemy();

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }

        // Activate and deactivate pop-ups based on conditions
        if (hidden)
        {
            unseen.gameObject.SetActive(true);

            if (canAttack)
            {
                attack.gameObject.SetActive(true);
                seen.gameObject.SetActive(false);
                unseen.gameObject.SetActive(true);
            }
            else
            {
                attack.gameObject.SetActive(false);
                seen.gameObject.SetActive(true);
                unseen.gameObject.SetActive(false);
            }
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
        foreach (Enemy enemy in GameObject.FindObjectsOfType(typeof(Enemy)))
        {
            //Checking if Hidden or Seen
            Vector3 enemyToPlayer;

            enemyToPlayer = (transform.position - enemy.transform.position).normalized;

            if (Vector3.Dot(enemyToPlayer, enemy.transform.forward) < 0)
            {
                hidden = true;
                unseen.gameObject.SetActive(true);
            }
            if (Vector3.Distance(enemy.transform.position, transform.position) < 300)
            {
                hidden = false;
                seen.gameObject.SetActive(true);
            }
        }
    }

//Dot Product to see if Facing Enemy and Can Attack or NOT
    void checkIfFacingEnemy()
    {
        canAttack = false; // Reset canAttack at the beginning

        foreach (Enemy enemy in GameObject.FindObjectsOfType(typeof(Enemy)))
        {
            if (hidden == true)
            {
                //Checking if Player is looking at enemy
                if (Vector3.Dot(enemy.transform.forward, transform.forward) > 0 && Vector3.Distance(enemy.transform.position, transform.position) < 150)
                {
                    canAttack = true;
                    targetEnemy = enemy;
                }
            }
        }
    }

//ATTACK

    void Attack()
    {
        if (hidden && canAttack && targetEnemy != null)
        {
            // Check if the targetEnemy is within attack range
            float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, transform.position);

            if (distanceToEnemy < 200)
            {
                if (targetEnemy.currentHealth > 0)
                {
                    // Deal damage to the enemy
                    targetEnemy.TakeDamage(10);
                }
            }
        }
    }
}