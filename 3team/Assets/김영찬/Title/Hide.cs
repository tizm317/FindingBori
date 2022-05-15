using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hide : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    public GameObject panel;
    void Start()
    {
        button = GetComponent<Button>();
    }
    void Update()
    {
        if(button.interactable == false)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
