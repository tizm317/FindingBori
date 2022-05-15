using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetBtn : MonoBehaviour
{
    public GameObject resetPanel;
    public void onClickReset()
    {
        if (resetPanel != null)
        {
            resetPanel.SetActive(true);
        }
    }
    public void onClickExitReset()
    {
        if (resetPanel != null)
        {
            resetPanel.SetActive(false);
        }
    }
    public void reset()
    {
        PlayerPrefs.SetInt("levelReached", 0);
    }
}
