using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOrder_Ending : MonoBehaviour
{
    public GameObject optionPanel;

    // Update is called once per frame
    void Update()
    {
        if (optionPanel.activeSelf == true)
            this.gameObject.GetComponent<Canvas>().sortingOrder = 2;
        else
            this.gameObject.GetComponent<Canvas>().sortingOrder = -1;
    }
}
