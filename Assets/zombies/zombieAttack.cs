using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieAttack : MonoBehaviour
{
    // GameObject zombie - Ataque del zombie a melee //
    public float timeBetweenAttacks;
    public int attackDamage;
    GameObject player;
    valorvidaP playerHealth;

    bool playerInRage;
    float timer;
    Animator anim;
    ControladorZombie zombieController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<valorvidaP>();
        anim = GetComponent<Animator>();
        zombieController = GetComponent<ControladorZombie>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            playerInRage = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == player)
        {
            playerInRage = false;
        }
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRage && zombieController.isDead == false)
        {
            Attack();
        }
    }

    public void Attack()
    {
        timer = 0;
        playerHealth.TakeDamage(attackDamage);
        anim.SetTrigger("attack");
    }
}
