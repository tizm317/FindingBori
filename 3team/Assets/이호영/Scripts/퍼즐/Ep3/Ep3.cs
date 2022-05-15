using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections; // �ڷ�ƾ..

public class Ep3 : MonoBehaviour
{
    [SerializeField] private Transform emptySpace = null;
    private Camera _camera;
    [SerializeField] private TilesScript[] tiles;
    private int emptySpaceIndex = 13;                        //�ٲ���� �ϴ°�
    // ���� ����
    static int levelat;
    public bool _isFinished;
    [SerializeField] private GameObject endPanel;

    [SerializeField] private GameObject re_RollButton;
    [SerializeField] private GameObject hintButton;


    //Ÿ�̸�
    [SerializeField] private Text endPanelTimerText;
    [SerializeField] private Text failPanelTimerText;

    // �ð� �ʰ� �й�
    private bool _isFailed;
    [SerializeField] private GameObject failPanel;
    //private int timeLimit = 10;

    //
    [SerializeField] private GameObject Illerstrate;
    [SerializeField] GameObject iller_text;


    [SerializeField] private GameObject Tile;
    [SerializeField] private GameObject Background;

    [SerializeField] private GameObject BG; // illerstrate bg

    [SerializeField] private GameObject black_bg; // black image for illerstration bg


    bool fadeIn; // �Ϸ���Ʈ ���̵��� �� �Ǿ�����

    public bool _isShuffled; // �ؽ�Ʈ ������ ���� �ٽ� ���� �� ���� �ϰ� end �г� �ߴ°� �������� ����

    [SerializeField] private AudioClip[] audioClip; // ����� Ŭ�� �迭
    [SerializeField] private AudioSource Audio;     // ����� �ҽ�

    //���� ����
    public int puzzleNum;

    void Start()
    {
        Time.timeScale = 1; // �̾��ϱ� ���� ����
        _camera = Camera.main;
        shuffle();
        BG.SetActive(true);
    }

    void Update()
    {
        var b = GetComponent<TimerScript>(); // getcomponent ������ ���� ���� -> ������ �־
        
        //�ð� �ʰ� �й�
        if (b.minutes == 0 && b.seconds <= 0)
        {
            _isFailed = true;
            b.StopTimer();
            failPanelTimerText.text = (b.minutes < 10 ? "0" : "") + b.minutes + ":" + (b.seconds < 10 ? "0" : "") + b.seconds;
        }

        if (_isFailed)
        {
            re_RollButton.SetActive(false);
            hintButton.SetActive(false);
            failPanel.SetActive(true);
        }

        // when illerstrate fadein finished
        // Tile off
        if (fadeIn)
        {
            Tile.SetActive(false);
            StartCoroutine(MoveLerp());
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (!_isFailed && !_isFinished) // �г� ���� �� ���� �̵� ����
                {
                    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                    if (hit)
                    {
                        // ���� ���� ȿ����
                        Audio.clip = audioClip[0];
                        Audio.Play();

                        //Debug.Log(hit.transform.name);
                        if (Vector2.Distance(emptySpace.position, hit.transform.position) < 1.1)
                        {
                            Vector2 lastEmptySpacePosition = emptySpace.position;
                            TilesScript thisTile = hit.transform.GetComponent<TilesScript>(); // thisTile�� ���� Ŭ���� Ÿ���� TilesScript
                            emptySpace.position = thisTile.targetPosition; // = hit.transform.position // = ���� Ŭ���� Ÿ���� TilesScript �� targetpostion
                            thisTile.targetPosition = lastEmptySpacePosition; //hit.transform.position =

                            //index ã�Ƽ� �迭������ �ٲ��ֱ�
                            int tileIndex = findIndex(thisTile);
                            tiles[emptySpaceIndex] = tiles[tileIndex];
                            tiles[tileIndex] = null;
                            emptySpaceIndex = tileIndex;

                            _isShuffled = false;
                        }
                    }
                    
                }
            }
            else
            {
                // ���� �κп���
                // �ɼ� �޴� Ŭ���� ��
                if (Tile.activeSelf)
                    Audio.clip = audioClip[2];
            }

        }

        if (!_isShuffled)
        {
            if (!_isFinished)
            {
                // ���� Ȯ��
                int correctTiles = 0;
                foreach (var a in tiles)
                {
                    if (a != null)
                    {
                        if (a.isRightPlace)
                            correctTiles++;
                    }
                }
                //var b = GetComponent<TimerScript>(); // getcomponent ������ ���� ���� -> ������ �־

                if (correctTiles == tiles.Length - 1)
                {
                    // ���� �ϼ� ȿ����
                    Audio.clip = audioClip[1];
                    Audio.Play();
                    //

                    _isFinished = true;
                    endPanel.SetActive(true);
                    //Debug.Log("You Won!");

                    levelat = PlayerPrefs.GetInt("levelReached");
                    if (levelat == 2)
                    {
                        levelat += 1;
                    }
                    //print(levelat);
                    PlayerPrefs.SetInt("levelReached", levelat);

                    b.StopTimer();
                    endPanelTimerText.text = (b.minutes < 10 ? "0" : "") + b.minutes + ":" + (b.seconds < 10 ? "0" : "") + b.seconds + " left";

                    re_RollButton.SetActive(false);
                    hintButton.SetActive(false);

                }

                ////�ð� �ʰ� �й�
                //if (b.minutes == 0 && b.seconds <= 0)
                //{
                //    _isFailed = true;
                //    b.StopTimer();
                //    failPanelTimerText.text = (b.minutes < 10 ? "0" : "") + b.minutes + ":" + (b.seconds < 10 ? "0" : "") + b.seconds;
                //}

            }
        }



        if (_isFailed)
        {
            re_RollButton.SetActive(false);
            failPanel.SetActive(true);
            hintButton.SetActive(false);

        }

    }

    public void PlayAgain()
    {
        //Try Again ��ư
        _isFinished = false;
        if (endPanel)
            endPanel.SetActive(false);

        _isFailed = false;
        if (failPanel)
            failPanel.SetActive(false);

        shuffle();

        var a = GetComponent<TimerScript>();
        //�ʱ�ȭ�� ���������
        a.minutes = 25;
        a.seconds = 0;
        a.AddToSecond();

        re_RollButton.SetActive(true);
        hintButton.SetActive(true);


    }

    public void GoNext()
    {
        //Go Next ��ư
        //�Ϸ���Ʈ
        Illerstrate.SetActive(true);
        iller_text.SetActive(true);
        //Illerstrate.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        StartCoroutine(ColorLerp());

        //endPanel
        endPanel.SetActive(false);

        //BG.SetActive(false);

        black_bg.SetActive(true);


        //���� ��Ȱ��ȭ
        // move to update
        Background.SetActive(false);

    }

    public void shuffle()
    {
        int inversion;

        // ���ϴܿ� ������� �ƴ� ��� ��������� �ٲ��� �� ���� ���� 
        // �� ������ �������� solvable ���� �Ǵ� �ϱ� ������
        //if (emptySpaceIndex != 15)
        //{
        //    //������ �ٲ���
        //    var tileOn15LastPos = tiles[15].targetPosition;
        //    tiles[15].targetPosition = emptySpace.position;
        //    emptySpace.position = tileOn15LastPos;

        //    //�迭 �ٲ���
        //    tiles[emptySpaceIndex] = tiles[15];
        //    tiles[15] = null;
        //    //index �ٲ���
        //    emptySpaceIndex = 15;
        //}

        //do
        //{
        //    for (int i = 0; i <= 14; i++)
        //    {
        //        //������ �ٲ���
        //        var lastPos = tiles[i].targetPosition;
        //        int radomIndex = Random.Range(0, 14);
        //        tiles[i].targetPosition = tiles[radomIndex].targetPosition;
        //        tiles[radomIndex].targetPosition = lastPos;

        //        //�迭�� �ٲ���
        //        var tile = tiles[i];
        //        tiles[i] = tiles[radomIndex];
        //        tiles[radomIndex] = tile;

        //    }
        //    //�� inversion �����
        //    inversion = GetInversions();
        //    //Debug.Log(inversion);
        //    Debug.Log(message: "Puzzle Shuffled");
        //} while (inversion % 2 != 0); // ¦�� �ƴϸ� ����


        if (emptySpaceIndex != 13)                                                   // emptySpaceIndex �ٲ���� �� : (15), 4, 13, 13
        {
            //������ �ٲ���
            var tileOn15LastPos = tiles[13].targetPosition;
            tiles[13].targetPosition = emptySpace.position;
            emptySpace.position = tileOn15LastPos;

            //�迭 �ٲ���
            tiles[emptySpaceIndex] = tiles[13];
            tiles[13] = null;
            //index �ٲ���
            emptySpaceIndex = 13;
        }


        do
        {
            for (int i = 0; i <= 11; i++)
            {
                if (puzzleNum == 1)
                {
                    if (i == 4)                                                         // �ٲ���� �ϴ� �� 4, 13, 13
                        continue;
                }
                else
                {
                    if (i == 13)                                                         // �ٲ���� �ϴ� �� 4, 13, 13
                        continue;
                }

                //������ �ٲ���
                var lastPos = tiles[i].targetPosition;
                //int radomIndex = Random.Range(0, 14);


                int rand;
                int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 15, 16, 17, 18, 19,20,21,22,23,24,25,26,27,28,29 };                        // �ٲ���� �ϴ� �� : (Random.Range(0,15?)), 4����, 13����, 13���� 
                rand = arr[Random.Range(0, arr.Length)];
                int radomIndex = rand;

                tiles[i].targetPosition = tiles[radomIndex].targetPosition;
                tiles[radomIndex].targetPosition = lastPos;

                //�迭�� �ٲ���
                var tile = tiles[i];
                tiles[i] = tiles[radomIndex];
                tiles[radomIndex] = tile;

            }
            //�� inversion �����
            inversion = GetInversions();
            //Debug.Log(inversion);
            //Debug.Log(message: "Puzzle Shuffled");
        } while (inversion % 2 != 0); // ¦�� �ƴϸ� ����                              // �ٲ���� �ϴ� �� (���� 2,3) : ¦�� Ȧ��


        _isShuffled = true;
    }

    public int findIndex(TilesScript ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    int GetInversions()
    {
        int inversionsSum = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            int thisTileInversion = 0;
            if (tiles[i] != null)
            {
                for (int j = i; j < tiles.Length; j++)
                {
                    if (tiles[j] != null)
                    {
                        if (tiles[i].number > tiles[j].number)
                        {
                            thisTileInversion++;
                        }
                    }
                }
            }
            inversionsSum += thisTileInversion;
        }
        return inversionsSum;
    }

    private IEnumerator ColorLerp()
    {
        float t = 0f;
        while(Illerstrate.GetComponent<SpriteRenderer>().color != Color.white)
        {
            t += Time.deltaTime;
            Illerstrate.GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, 2 * t);
            yield return null;
        }
        fadeIn = true;
    }

    private IEnumerator MoveLerp()
    {
        // illustration move to centre smoothly
        float t = 0f;
        while (Illerstrate.transform.position.y != 0)
        {
            t += Time.deltaTime;
            Illerstrate.transform.position = Vector3.Lerp(Illerstrate.transform.position, new Vector3(0, 0, 0), t * 1f * Time.deltaTime);
            //Color.Lerp(new Color(1, 1, 1, 0), Color.white, t);
            yield return null;
        }
    }
}
