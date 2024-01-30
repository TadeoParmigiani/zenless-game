using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zombieHealth : MonoBehaviour
{
    public int maxHealth; //vida maxima del zombie
    public int currentHealth; //vida actual del zombie

    public bool isDead;

    Animator anim;

    //moneda item;

    
    
    void Start()
    {
   currentHealth = maxHealth;
   anim = GetComponent<Animator>();
   //item = GetComponent<moneda>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
public void TakeDamage(int amount){
    if(isDead == true) return;
    currentHealth -= amount;
    if (currentHealth <= 0) Death();
}
 void Death(){
    isDead = true;
    anim.SetTrigger("death");
    Destroy(gameObject, 2);
    //item.itemDropped();
 }
 
}

