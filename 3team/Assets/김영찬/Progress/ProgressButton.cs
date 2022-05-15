using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressButton : MonoBehaviour
{
    public GameObject image;

    public void titleButton()
    {
        SceneManager.LoadScene("title");
    }
    public void showImage()
    {
        image.SetActive(true);
    }
    public void hideImage()
    {
        image.SetActive(false);
    }
}
