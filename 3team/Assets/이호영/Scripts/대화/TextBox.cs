using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextBox : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject textBox;

    RectTransform rt;

    // Start is called before the first frame update
    //void Start()
    //{
    //    for (int i = 0; i < talkManager.talkData[0].Length; i++)
    //    {
    //        Instantiate(textBox).transform.SetParent(transform);
    //        //GameObject.Find("TextBox(Clone)").GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
    //    }
    //    //GameObject.Find("TextBox(Clone)").GetComponent<RectTransform>().anchoredPosition = new Vector2(0,123);

    //    //this.transform.GetChild(1).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

    //    //Instantiate(textBox);
    //}

    // Update is called once per frame
    void Update()
    {

    }
}
