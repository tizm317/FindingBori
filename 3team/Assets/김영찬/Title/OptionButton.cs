using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menuPanel;
    public GameObject gamePanel;
    //public AudioSource bgm;
    public void StartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void Menu_button()
    {
        Time.timeScale = 0;
        menuPanel.SetActive(true);
    }

    public void Xbutton()
    {
        Time.timeScale = 1;
        menuPanel.SetActive(false);
    }

    public void ProgCheck()
    {
        SceneManager.LoadScene("Progress");
    }

    public void ContinueButtion()
    {
        Time.timeScale = 0;
        gamePanel.SetActive(true);
    }
    public void ContinueXButton()
    {
        Time.timeScale = 1;
        gamePanel.SetActive(false);
    }
    public void Chapter1Button()
    {
        SceneManager.LoadScene("Main");
    }
    public void Chapter2Button()
    {
        SceneManager.LoadScene("Episode2");
    }
    public void Chapter3Button()
    {
        SceneManager.LoadScene("Episode3");
    }
   
    /*public void BGMbutton()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }*/
}
