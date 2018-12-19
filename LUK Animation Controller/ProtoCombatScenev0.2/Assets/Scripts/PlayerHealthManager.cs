using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    //Public
    public int playerMaxHealth = 3;
    public int playerCurrentHealth;

    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    void Update()
    {
        if (playerCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;
    }

    public void HealPlayer(int healToGive)
    {
        playerCurrentHealth += healToGive;
    }

    public void ResetHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }
}