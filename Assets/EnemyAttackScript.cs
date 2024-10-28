using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    private GameObject player;
    private bool playerInRange;
    private float timer;
    private float timer2;

    private bool DeathAttack = false;

    EnemyScript enemyScript;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }

        if (enemyScript.EnemyHP <= 120 && timer >= timeBetweenAttacks && playerInRange)
        {
            InstantDeath();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Attack()
    {
        timer = 0f;

        // Insert player health deduction logic here
        // Example: playerHealth.TakeDamage(attackDamage);
    }

    void InstantDeath()
    {
        timer2 = 0f;


    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
