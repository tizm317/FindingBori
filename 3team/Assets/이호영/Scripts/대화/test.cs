using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class test : MonoBehaviour, IPointerDownHandler
{
    public TalkManager talkManager; // talkData ���� �� ������ ������
    public Text TalkText;
    public int talkIndex;   //�� ��° �������
    public int ep;

    public GameObject storyLogContent;
    //public Text ttext;
    

    //���� Ȱ��ȭ��
    [SerializeField] private GameObject Tile;
    [SerializeField] private GameObject Background;
    [SerializeField] private GameObject TimeText;
    [SerializeField] private GameObject rerollbtn;
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private GameObject madScientist;

    // ȿ���� ����
    [SerializeField] private AudioClip[] audioClip; // ����� Ŭ�� �迭
    [SerializeField] private AudioSource Audio;     // ����� �ҽ�


    //public bool isPuzzleCleared; // ���� ���� ���� ep -> �̰� ��� �ƿ� �г��� ��Ȱ��ȭ


    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log(eventData);

        // �ؽ�Ʈ �� ȿ����
        text_SFX();

        if (ep < 4)
        {
            if (talkIndex < talkManager.talkData[ep].Length) //talkManager.talkLength[ep]
            {
                Talk(ep);
                talkIndex++;

                chracterImage();

                 

                if (talkIndex == talkManager.talkData[ep].Length) //talkManager.talkLength[ep]
                {
                    if(ep == 3)
                        SceneManager.LoadScene("title");
                    else
                    {
                        //���� Ȱ��ȭ : PlayAgain Ȱ��
                        Tile.SetActive(true);
                        Background.SetActive(true);
                        TimeText.SetActive(true);
                        rerollbtn.SetActive(true);

                        Ep1 ep1 = GameObject.Find("Main Camera").GetComponent<Ep1>();
                        Ep2 ep2 = GameObject.Find("Main Camera").GetComponent<Ep2>();
                        Ep3 ep3 = GameObject.Find("Main Camera").GetComponent<Ep3>();

                        if (ep1 != null)
                            ep1.PlayAgain();
                        else if (ep2 != null)
                            ep2.PlayAgain();
                        else if (ep3 != null)
                            ep3.PlayAgain();

                        ep++;
                        talkIndex = 0;
                    }
                    this.gameObject.SetActive(false);


                    //GameScript gameScript = GameObject.Find("Main Camera").GetComponent<GameScript>();
                    //gameScript.PlayAgain();

                    
                }
            }
            
        }
    }
 
    public void Talk(int ep)
    {
        string talkData = talkManager.GetTalk(ep, talkIndex);
        TalkText.text = talkData;
        if(talkIndex < talkManager.talkData[ep].Length - 1)
            storyLogContent.transform.GetChild(talkIndex).GetComponentInChildren<Text>().text = talkData;

        //ttext.text = talkData;
    }

    void chracterImage()
    {
        if (talkIndex == 2)
        {
            mainCharacter.SetActive(true);
        }
        if (ep == 1)
        {
            if (talkIndex == 29)
                madScientist.SetActive(true);
            else if (talkIndex == 30)
                mainCharacter.SetActive(false);
            else if (talkIndex == 38)
            {
                mainCharacter.SetActive(true);
                madScientist.SetActive(false);
            }
        }
        else if (ep == 2)
        {
            if (talkIndex == 10)
            {
                mainCharacter.SetActive(false);
                madScientist.SetActive(true);
            }
            else if (talkIndex == 15)
            {
                mainCharacter.SetActive(true);
                madScientist.SetActive(false);
            }
            else if (talkIndex == 16)
            {
                madScientist.SetActive(true);
            }
            else if(talkIndex == 17)
            {
                madScientist.SetActive(false);
            }
        }
        else if(ep == 3)
        {
            if(talkIndex == 2)
                mainCharacter.SetActive(true);
        }
    }

    void text_SFX()
    {
        /*
         * ȿ���� ����
         * audio.clip �� �ٲ��ְ�
         * �÷���
         * 
         * audioClip ������ audioClip �迭�� �־��
         * test.cs �� �ؽ�Ʈ ���� ��ũ��Ʈ�� Canvas - TextPanel �� �־��
         */

        //TalkManager.cs ����
        //talkManager.talkData[ep][talkIndex]
        // ep: ���Ǽҵ� / talkIndex : �ش� ���Ǽҵ� �� ���° �ؽ�Ʈ����

        // ���� ȿ������ ep ��ũ��Ʈ���� ���� ����

        if (ep == 0)
        {
            // ep �� 0 �� ��� (ep1)
            if(talkIndex == 13 || talkIndex == 18 || talkIndex == 42)
            {
                Audio.clip = audioClip[10];
                Audio.Play();
            }
            else if (talkIndex == 19)
            {
                // talkIndex �� 17 �� ��� ( 18��° �ؽ�Ʈ)
                // Audio.clip �� audioClip[0] (�� �Ҹ�) �־���
                Audio.clip = audioClip[0];
                // Audio �÷�����
                Audio.Play();
            }
            else if (talkIndex == talkManager.talkData[ep].Length - 3)
            {
                // talkIndex �� talkData[0]�� ���� - 3 �� ��� 
                // talkData[0].Length - 1 �� ������ �ؽ�Ʈ�ϱ� ������ �� �� �ؽ�Ʈ
                // �ڿ��� ���°� ���� �̷��� �� ���� ����
                Audio.clip = audioClip[4];
                Audio.Play();
            }
            else
            {
                // �⺻ ���� 10.mainbutton_2 �� audioClip[9] ��
                Audio.clip = audioClip[9];
            }
        }
        else if(ep == 1)
        {
            // ep �� 1 �� ��� (ep2)
            if(talkIndex == 1)
            {
                Audio.clip = audioClip[1];
                Audio.Play();
            }
            else if(talkIndex == 4)
            {
                Audio.clip = audioClip[3];
                Audio.Play();
            }
            else if (talkIndex == 5)
            {
                Audio.clip = audioClip[2];
                Audio.Play();
            }
            else
            {
                // �⺻ ���� 10.mainbutton_2 �� audioClip[9] ��
                Audio.clip = audioClip[9];
            }
        }
        else if (ep == 2)
        {
            // ep �� 2 �� ��� (ep3)
            if (talkIndex == 3)
            {
                Audio.clip = audioClip[6];
                Audio.Play();
            }
            else if (talkIndex == 19)
            {
                Audio.clip = audioClip[5];
                Audio.Play();
            }
            else
            {
                // �⺻ ���� 10.mainbutton_2 �� audioClip[9] ��
                Audio.clip = audioClip[9];
            }
        }
    }
}
