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
        //if (button.interactable == false)
        //{
        //    hidePanel.SetActive(true);
        //}
        //else
        //{
        //    hidePanel.SetActive(false);
        //}
    }


    static int levelat;
    public GameObject stageNumObject;

    void Start()
    {
        Button[] stages = stageNumObject.GetComponentsInChildren<Button>();
        int numByLevel = 1; // 레벨에 따른 오픈 되는 일러스트 개수

        levelat = PlayerPrefs.GetInt("levelReached");
        print(levelat);
        if (levelat == 0)
            numByLevel = 1;
        else if (levelat == 1)
            numByLevel = 9;
        else if (levelat == 2)
            numByLevel = 11;
        else
            numByLevel = 13;

        for (int i = numByLevel; i < stages.Length; i++)
        {

            stages[i].interactable = false;
        }
    }

}
