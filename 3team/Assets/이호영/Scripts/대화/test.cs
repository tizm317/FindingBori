using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class test : MonoBehaviour, IPointerDownHandler
{
    public TalkManager talkManager; // talkData 같은 거 쓰려고 가져옴
    public Text TalkText;
    public int talkIndex;   //몇 번째 대사인지
    public int ep;

    public GameObject storyLogContent;
    //public Text ttext;
    

    //퍼즐 활성화용
    [SerializeField] private GameObject Tile;
    [SerializeField] private GameObject Background;
    [SerializeField] private GameObject TimeText;
    [SerializeField] private GameObject rerollbtn;
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private GameObject madScientist;

    // 효과음 관련
    [SerializeField] private AudioClip[] audioClip; // 오디오 클립 배열
    [SerializeField] private AudioSource Audio;     // 오디오 소스


    //public bool isPuzzleCleared; // 퍼즐 깨면 다음 ep -> 이거 대신 아예 패널을 비활성화


    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log(eventData);

        // 텍스트 별 효과음
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
                        //퍼즐 활성화 : PlayAgain 활용
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
         * 효과음 관리
         * audio.clip 을 바꿔주고
         * 플레이
         * 
         * audioClip 종류는 audioClip 배열에 넣어둠
         * test.cs 는 텍스트 관련 스크립트라 Canvas - TextPanel 에 넣어둠
         */

        //TalkManager.cs 참고
        //talkManager.talkData[ep][talkIndex]
        // ep: 에피소드 / talkIndex : 해당 에피소드 내 몇번째 텍스트인지

        // 퍼즐 효과음은 ep 스크립트에서 따로 관리

        if (ep == 0)
        {
            // ep 가 0 인 경우 (ep1)
            if(talkIndex == 13 || talkIndex == 18 || talkIndex == 42)
            {
                Audio.clip = audioClip[10];
                Audio.Play();
            }
            else if (talkIndex == 19)
            {
                // talkIndex 가 17 인 경우 ( 18번째 텍스트)
                // Audio.clip 에 audioClip[0] (쥐 소리) 넣어줌
                Audio.clip = audioClip[0];
                // Audio 플레이함
                Audio.Play();
            }
            else if (talkIndex == talkManager.talkData[ep].Length - 3)
            {
                // talkIndex 가 talkData[0]의 길이 - 3 인 경우 
                // talkData[0].Length - 1 이 마지막 텍스트니까 마지막 전 전 텍스트
                // 뒤에서 세는게 빨라서 이렇게 한 거일 뿐임
                Audio.clip = audioClip[4];
                Audio.Play();
            }
            else
            {
                // 기본 세팅 10.mainbutton_2 인 audioClip[9] 로
                Audio.clip = audioClip[9];
            }
        }
        else if(ep == 1)
        {
            // ep 가 1 인 경우 (ep2)
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
                // 기본 세팅 10.mainbutton_2 인 audioClip[9] 로
                Audio.clip = audioClip[9];
            }
        }
        else if (ep == 2)
        {
            // ep 가 2 인 경우 (ep3)
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
                // 기본 세팅 10.mainbutton_2 인 audioClip[9] 로
                Audio.clip = audioClip[9];
            }
        }
    }
}
