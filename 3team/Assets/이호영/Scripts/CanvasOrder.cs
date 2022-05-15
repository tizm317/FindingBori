using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOrder : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject clearPanel;
    public GameObject gameOverPanel;

    //public GameObject re_rollBtn;
    public GameObject timer;
    public GameObject hintPanel;




    void Update()
    {
        if (optionPanel.activeSelf == true)
        {
            this.gameObject.GetComponent<Canvas>().sortingOrder = 2;
            //overriding sorting off
            //re_rollBtn.GetComponent<Canvas>().overrideSorting = false;
            timer.GetComponent<Canvas>().overrideSorting = false;
            this.GetComponent<Canvas>().sortingLayerName = "UI";    // To put the panel forward at puzzle part.

        }
        else if(clearPanel.activeSelf == true)
        {
            this.gameObject.GetComponent<Canvas>().sortingOrder = 2;
            //re_rollBtn.GetComponent<Canvas>().overrideSorting = false;
            timer.GetComponent<Canvas>().overrideSorting = false;
            this.GetComponent<Canvas>().sortingLayerName = "UI";

        }
        else if (gameOverPanel.activeSelf == true)
        {
            this.gameObject.GetComponent<Canvas>().sortingOrder = 2;
            //re_rollBtn.GetComponent<Canvas>().overrideSorting = false;
            timer.GetComponent<Canvas>().overrideSorting = false;
            this.GetComponent<Canvas>().sortingLayerName = "UI";
        }
        else if (hintPanel.activeSelf == true)
        {
            this.gameObject.GetComponent<Canvas>().sortingOrder = 2;
            //re_rollBtn.GetComponent<Canvas>().overrideSorting = false;
            timer.GetComponent<Canvas>().overrideSorting = false;
            this.GetComponent<Canvas>().sortingLayerName = "UI";
        }
        else
        {
            this.gameObject.GetComponent<Canvas>().sortingOrder = -1;
            //overriding sorting on
            //re_rollBtn.GetComponent<Canvas>().overrideSorting = true;
            timer.GetComponent<Canvas>().overrideSorting = true;
            this.GetComponent<Canvas>().sortingLayerName = "Default";
        }
    }
}
