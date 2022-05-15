using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustManager : MonoBehaviour
{
    public GameObject illustPanel;
    public GameObject hidePanel;
    public Button button;

    public void onClickIllust()
    {
        if (illustPanel != null)
        {
            illustPanel.SetActive(true);
        }
    }
    public void onClickExitIllust()
    {
        if (illustPanel != null)
        {
            illustPanel.SetActive(false);
        }
    }

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

    
        static int levelat;
        public GameObject stageNumObject;

        void Start()
        {
            Button[] stages = stageNumObject.GetComponentsInChildren<Button>();

            levelat = PlayerPrefs.GetInt("levelReached");
            print(levelat);
            for (int i = levelat + 1; i < stages.Length - 1; i++)
            {
                stages[i].interactable = false;
            }
        }
    
}
