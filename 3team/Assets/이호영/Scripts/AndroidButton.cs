using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidButton : MonoBehaviour
{
    int ClickCount = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);
            //adMobManager.ShowFrontAd();

        }
        else if (ClickCount == 2)
        {
            CancelInvoke("DoubleClick");
            Application.Quit();
        }

    }
    void DoubleClick()
    {
        ClickCount = 0;
    }
}