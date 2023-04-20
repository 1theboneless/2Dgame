using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{
    Image healthBar;
    public Health player;
    public float maxHealth;
    public float CurrentHealth;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        healthBar = GetComponent<Image>();
        maxHealth = player.currentHealth;
    }
    private void Update()
    {
        healthBar.fillAmount = player.currentHealth / maxHealth;
    }

}

