using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    //Public
    public int enemyMaxHealth = 3;
    public int enemyCurrentHealth;

    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        if (enemyCurrentHealth > enemyMaxHealth)
        {
            enemyCurrentHealth = enemyMaxHealth;
        }
    }

    public void HurtEnemy(int damageToGive)
    {
        enemyCurrentHealth -= damageToGive;
    }

    public void HealEnemy(int healToGive)
    {
        enemyCurrentHealth += healToGive;
    }

    public void ResetHealth()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }
}