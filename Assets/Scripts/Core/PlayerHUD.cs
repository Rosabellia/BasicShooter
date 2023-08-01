using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    private Controller controller;
    private int playerIndext;
    public TMP_Text livesText;
    public TMP_Text scoreText;

    private void Start()
    {
        controller = GetComponentInParent<Controller>();
    }

    public void UpdateScore()
    {
        //scoreText.text = "Score: " + GameManager.Instance.points[];
    }

    public void UpdateLives()
    {

    }
}
