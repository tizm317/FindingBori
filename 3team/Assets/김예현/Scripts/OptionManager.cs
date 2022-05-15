using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject StoryLogPanel;
    public GameObject IllustPanel;
    public GameObject HintPanel;


    [SerializeField] GameObject BlackImage;

    [SerializeField] GameObject Tile;
    [SerializeField] GameObject Tile_BG;



    public void onClickOption()
    {
        if (OptionPanel != null)
        {
            OptionPanel.SetActive(true);
            BlackImage.SetActive(true);
            Time.timeScale = 0;
            if(Tile_BG)
                if(Tile_BG.activeSelf)
                    Tile.SetActive(false);
        }
    }

    public void onClickContinue()
    {
        if (OptionPanel != null)
        {
            OptionPanel.SetActive(false);
            BlackImage.SetActive(false);
            Time.timeScale = 1;
            if (Tile_BG)
                if (Tile_BG.activeSelf)
                    Tile.SetActive(true);
        }
    }

    public void onClickStoryLog()
    {
        if (StoryLogPanel != null)
        {
            StoryLogPanel.SetActive(true);
        }
    }
    
    public void onClickExitStoryLog()
    {
        if (StoryLogPanel != null)
        {
            StoryLogPanel.SetActive(false);
        }
    }

    public void onClickIllust()
    {
        if (IllustPanel != null)
        {
            IllustPanel.SetActive(true);
        }
    }
    public void onClickExitIllust()
    {
        if (IllustPanel != null)
        {
            IllustPanel.SetActive(false);
        }
    }

    public void onClickHint()
    {
        if (HintPanel != null)
        {
            HintPanel.SetActive(true);
            BlackImage.SetActive(true);
        }
    }

    public void onClickExitHint()
    {
        if (HintPanel != null)
        {
            HintPanel.SetActive(false);
            BlackImage.SetActive(false);
        }
    }

    public void onClickQuit()
    {
        SceneManager.LoadScene("title");
    }

}
