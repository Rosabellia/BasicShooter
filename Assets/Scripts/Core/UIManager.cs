using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject TitleScreenObject;
    public GameObject OptionsMenu;
    public GameObject PauseMenu;
    public GameObject GameOver;
    
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider bgmVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        masterVolumeSlider.value = AudioManager.Instance.masterVolume;
        sfxVolumeSlider.value = AudioManager.Instance.sfxVolume;
        bgmVolumeSlider.value = AudioManager.Instance.bgmVolume;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideTitleScreenUI()
    {
        TitleScreenObject.SetActive(false);
    }

    public void ShowTitleScreenUI()
    {
        TitleScreenObject.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        PauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        PauseMenu.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        OptionsMenu.SetActive(true);
    }

    public void HideOptionsMenu()
    {
        OptionsMenu.SetActive(false);
    }

    public void ShowGameOver()
    {
        GameOver.SetActive(true);
    }
    public void HideGameOver()
    {
        GameOver.SetActive(false);
    }

    public void HandleGameStateChanged(GameState previousState, GameState newState)
    {
        switch (previousState)
        {
            case GameState.TitleState:
                HideTitleScreenUI();
                break;

            case GameState.OptionsState:
                HideOptionsMenu();
                break;

            case GameState.GameplayerState:
                break;

            case GameState.GameOverState:
                HideGameOver();
                break;

            case GameState.CreditsState:
                //Hide credits
                break;

            case GameState.PauseState:
                HidePauseMenu();
                break;
        }

        switch (newState)
        {
            case GameState.TitleState:
                ShowTitleScreenUI();
                break;

            case GameState.OptionsState:
                ShowOptionsMenu();
                break;

            case GameState.GameplayerState:
                break;

            case GameState.GameOverState:
                ShowGameOver();
                break;

            case GameState.CreditsState:
                //Show Credits
                break;

            case GameState.PauseState:
                ShowPauseMenu();
                break;
        }
    }
}
