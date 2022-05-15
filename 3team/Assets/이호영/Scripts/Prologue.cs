using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using System.Collections; // �ڷ�ƾ..


public class Prologue : MonoBehaviour
{
    //[SerializeField] Sprite[] prolog;
    [SerializeField] GameObject prolog;
    [SerializeField] GameObject[] prologue;
    [SerializeField] GameObject textPanel;
    [SerializeField] GameObject OptPanel;
    //public AudioSource sound1;
    //public AudioSource sound2;
    //public AudioSource sound3;
    //public AudioSource sound4;
    //public AudioSource sound5;
    private Camera _camera;
    int idx;
    //bool fadeIn;
    bool fadeout;

    void Start()
    {
        _camera = Camera.main;
        idx = 1;
    }

    void Update()
    {
        // when OptionPanel is active,
        if (OptPanel.activeSelf)
            prolog.layer = 2;   // Ignore raycast
        else
            prolog.layer = 0;

        if (fadeout || textPanel.activeSelf)
        {
            StartCoroutine(WaitForIt());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                //if (!EventSystem.current.IsPointerOverGameObject())
                //{
                // UI Ŭ�� ���� ���̿��µ� ���ѷα� canvus renderer�� �ٲٸ鼭 ������ ��
                //}
                
                if (hit.collider.gameObject.name == "prologue")
                {

                    if (idx > 4)
                    {

                        StartCoroutine(Fadeout());
                        textPanel.SetActive(true);

                    }
                    else
                    {
                        //hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = prolog[idx];
                        StartCoroutine(Fadein(idx));
                        idx++;
                    }
                    //switch(idx)
                    //{
                    //    case 1:
                    //        sound1.Play();
                    //        break;
                    //    case 2:
                    //        sound2.Play();
                    //        break;
                    //    case 3:
                    //        sound3.Play();
                    //        break;
                    //    case 4:
                    //        sound4.Play();
                    //        break;
                    //    case 5:
                    //        sound5.Play();
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
            }
        }
    }


    private IEnumerator Fadein(int idx)
    {


        float t = 0f;
        while (prologue[idx].GetComponent<Image>().color != Color.white)
        {
            t += Time.deltaTime;
            prologue[idx].GetComponent<Image>().color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, t);
            yield return null;
        }
        //fadeIn = true;
    }

    private IEnumerator Fadeout()
    {

        float t = 0f;

        for (int i = 0; i < 4; i++)
            prologue[i].SetActive(false);


        while (prologue[4].GetComponent<Image>().color != new Color(1,1,1,0))
        {
            t += Time.deltaTime;
            prologue[4].GetComponent<Image>().color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), t);
            yield return null;
        }
        fadeout = true;
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(1.0f);
        prolog.SetActive(false);
    }
}
