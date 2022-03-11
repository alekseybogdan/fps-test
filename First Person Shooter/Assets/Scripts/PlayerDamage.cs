using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerDamage : MonoBehaviour
{
    //Use this to reference the text in the canvas
    public TextMeshProUGUI healthPanel;
    //Sets default health to 100
    public int health = 100;
    private void Start()
    {
        //Sets the health text at the start, we pass 0 as we don’t want to remove health.
        ApplyDamage(0);
    }
    void ApplyDamage(int damage)
    {
        if (healthPanel == null)
        {
            throw new Exception("healthPanel was not created.");
        }
        //Checks we has attached a health panel and out health is greater than 0
        if (health > 0)
        {
            //Stores the current health and subtracts the damage value
            health -= damage;
            //Sets the text on our panel.
            healthPanel.text = health.ToString();
        }
    }
}