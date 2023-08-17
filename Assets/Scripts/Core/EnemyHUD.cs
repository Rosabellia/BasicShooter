using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour
{
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInParent<Health>().OnHealthChanged.AddListener(UpdateHealthBar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
