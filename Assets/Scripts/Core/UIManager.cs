using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject TitleScreenObject;
    public GameObject OptionsMenu;
    public GameObject PauseMenu;
    public GameObject GameOver;
    // Start is called before the first frame update
    void Start()
    {
        
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
        PauseMenu.SetActive(true);
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

    }
    public void HideGameOver()
    {

    }
}
