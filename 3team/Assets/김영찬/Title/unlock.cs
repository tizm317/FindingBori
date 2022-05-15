using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class unlock : MonoBehaviour
{
    static int levelat;
    public GameObject stageNumObject;

    Button[] stages;

    void Start()
    {
        stages = stageNumObject.GetComponentsInChildren<Button>();

        levelat = PlayerPrefs.GetInt("levelReached");
        //print(levelat);
        for(int i = levelat +1 ;i<stages.Length -1;i++)
        {
            stages[i].interactable = false;
        }
    }

    private void Update()
    {
        //Button[] stages = stageNumObject.GetComponentsInChildren<Button>();

        levelat = PlayerPrefs.GetInt("levelReached");
        //print(levelat);
        for(int i = levelat +1 ;i<stages.Length -1;i++)
        {
            stages[i].interactable = false;
        }
    }
}
