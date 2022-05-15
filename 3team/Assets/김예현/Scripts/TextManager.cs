using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public Button button;
    public GameObject hidePanel;

    void Update()
    {
        if (button.interactable == false)
        {
            hidePanel.SetActive(true);
        }
        else
        {
            hidePanel.SetActive(false);
        }
    }

}
    

