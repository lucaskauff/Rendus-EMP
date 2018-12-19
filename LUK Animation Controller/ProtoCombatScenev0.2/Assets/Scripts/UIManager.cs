using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image scanProgressBar;
    public Image crystalSlot;
    public CrystalController crystalControl;
    public Slider playerHealthBar;
    public PlayerHealthManager playerHealth;
    public Slider enemyHealthBar;
    public EnemyHealthManager enemyHealth;

    void Update()
    {
        scanProgressBar.GetComponent<Animator>().SetInteger("ScanProgress", crystalControl.scanProgress);
        
        playerHealthBar.maxValue = playerHealth.playerMaxHealth;
        playerHealthBar.value = playerHealth.playerCurrentHealth;

        enemyHealthBar.maxValue = enemyHealth.enemyMaxHealth;
        enemyHealthBar.value = enemyHealth.enemyCurrentHealth;
    }

    public void ChangeImageInCrystalSlot()
    {
        crystalSlot.GetComponent<Image>().color = Color.white;
        crystalSlot.GetComponent<Image>().sprite = crystalControl.scannedObject.GetComponent<SpriteRenderer>().sprite;
    }
}