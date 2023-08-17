using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerHUD : MonoBehaviour
{
    private Pawn pawn;
    private int playerIndext;
    public TMP_Text livesText;
    public TMP_Text scoreText;
    public Image healthBar;

    private void Start()
    {
        pawn = GetComponentInParent<Pawn>();
        playerIndext = GameManager.Instance.GetPlayerIndext(pawn);
        UpdateScore();
        UpdateLives();
        gameObject.GetComponentInParent<Health>().OnHealthChanged.AddListener(UpdateHealthBar);
    }
    private void Update()
    {
        // TODO: Remove from here
        UpdateScore();
        UpdateLives();
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + GameManager.Instance.points[playerIndext];

    }

    public void UpdateLives()
    {
        livesText.text = "Lives: " + GameManager.Instance.lives[playerIndext];
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
