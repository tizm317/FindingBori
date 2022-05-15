using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    [SerializeField] private Transform emptySpace = null;
    //[SerializeField] private Transform emptySpace_2 = null;
    //[SerializeField] private Transform emptySpace_3 = null;

    private Camera _camera;

    [SerializeField] private TilesScript[] tiles;
    [SerializeField] private TilesScript[] tiles_2;
    [SerializeField] private TilesScript[] tiles_3;

    TilesScript[][] tileArr;

    private int emptySpaceIndex = 4;                        //바꿔줘야 하는거

    // 정답 이후
    public bool _isFinished;
    [SerializeField] private GameObject endPanel;

    [SerializeField] private GameObject re_RollButton;

    //타이머
    [SerializeField] private Text endPanelTimerText;
    [SerializeField] private Text failPanelTimerText;

    // 시간 초과 패배
    private bool _isFailed;
    [SerializeField] private GameObject failPanel;
    private int timeLimit = 10;

    //
    [SerializeField] private GameObject Illerstrate;

    //퍼즐 (비)활성화용
    [SerializeField] private GameObject Tile;
    //[SerializeField] private GameObject Tile_2;
    //[SerializeField] private GameObject Tile_3;

    [SerializeField] private GameObject Background;

    public bool _isShuffled; // 텍스트 끝나고 퍼즐 다시 생길 때 셔플 하고 end 패널 뜨는거 막기위한 변수

    //퍼즐 종류
    public int puzzleNum;

    void Start()
    {
        _camera = Camera.main;
        shuffle();


    }

    void Update()
    {
        // 배열 변수들 어떻게 교체할지 고민..
        switch(puzzleNum)
        {
            case 1:
                emptySpaceIndex = 4;
                break;
            case 2:
                emptySpaceIndex = 13;
                break;
            case 3:
                emptySpaceIndex = 13;
                break;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(!_isFailed && !_isFinished) // 패널 떴을 때 퍼즐 이동 막음
            {

                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit)
                {
                    //Debug.Log(hit.transform.name);
                    if (Vector2.Distance(emptySpace.position, hit.transform.position) < 1.1)
                    {
                        Vector2 lastEmptySpacePosition = emptySpace.position;
                        TilesScript thisTile = hit.transform.GetComponent<TilesScript>(); // thisTile은 내가 클릭한 타일의 TilesScript
                        emptySpace.position = thisTile.targetPosition; // = hit.transform.position // = 내가 클릭한 타일의 TilesScript 의 targetpostion
                        thisTile.targetPosition = lastEmptySpacePosition; //hit.transform.position =

                        //index 찾아서 배열끼리도 바꿔주기
                        int tileIndex = findIndex(thisTile);
                        tiles[emptySpaceIndex] = tiles[tileIndex];
                        tiles[tileIndex] = null;
                        emptySpaceIndex = tileIndex;

                        _isShuffled = false;
                    }
                }
            }

        }

        if(!_isShuffled)
        {
            if (!_isFinished)
            {
                // 정답 확인
                int correctTiles = 0;
                foreach (var a in tiles)
                {
                    if (a != null)
                    {
                        if (a.isRightPlace)
                            correctTiles++;
                    }
                }
                var b = GetComponent<TimerScript>(); // getcomponent 여러번 쓰면 낭비 -> 변수에 넣어서

                if (correctTiles == tiles.Length - 1)
                {
                    _isFinished = true;
                    endPanel.SetActive(true);
                    //Debug.Log("You Won!");


                    b.StopTimer();
                    endPanelTimerText.text = (b.minutes < 10 ? "0" : "") + b.minutes + ":" + (b.seconds < 10 ? "0" : "") + b.seconds;

                    re_RollButton.SetActive(false);
                }

                //시간 초과 패배
                if (b.minutes >= timeLimit)
                {
                    _isFailed = true;
                    b.StopTimer();
                    failPanelTimerText.text = (b.minutes < 10 ? "0" : "") + b.minutes + ":" + (b.seconds < 10 ? "0" : "") + b.seconds;
                }
            }
        }

        

        if (_isFailed)
        {
            re_RollButton.SetActive(false);
            failPanel.SetActive(true);
        }
       
    }

    public void PlayAgain()
    {
        //Try Again 버튼
        _isFinished = false;
        if(endPanel)
            endPanel.SetActive(false);

        _isFailed = false;
        if (failPanel)
            failPanel.SetActive(false);

        shuffle();

        var a = GetComponent<TimerScript>();
        //초기화도 시켜줘야함
        a.minutes = 0;
        a.seconds = 0;
        a.AddToSecond();

        re_RollButton.SetActive(true);

    }

    public void GoNext()
    {
        //Go Next 버튼
        //일러스트
        Illerstrate.SetActive(true);

        //endPanel
        endPanel.SetActive(false);


        //퍼즐 비활성화
        Tile.SetActive(false);
        Background.SetActive(false);

    }

    public void shuffle()
    {
        int inversion;

        // 우하단에 빈공간이 아닐 경우 빈공간으로 바꿔준 후 셔플 돌림 
        // 그 조건을 기준으로 solvable 한지 판단 하기 때문에
        //if (emptySpaceIndex != 15)
        //{
        //    //포지션 바꿔줌
        //    var tileOn15LastPos = tiles[15].targetPosition;
        //    tiles[15].targetPosition = emptySpace.position;
        //    emptySpace.position = tileOn15LastPos;

        //    //배열 바꿔줌
        //    tiles[emptySpaceIndex] = tiles[15];
        //    tiles[15] = null;
        //    //index 바꿔줌
        //    emptySpaceIndex = 15;
        //}

        //do
        //{
        //    for (int i = 0; i <= 14; i++)
        //    {
        //        //포지션 바꿔줌
        //        var lastPos = tiles[i].targetPosition;
        //        int radomIndex = Random.Range(0, 14);
        //        tiles[i].targetPosition = tiles[radomIndex].targetPosition;
        //        tiles[radomIndex].targetPosition = lastPos;

        //        //배열도 바꿔줌
        //        var tile = tiles[i];
        //        tiles[i] = tiles[radomIndex];
        //        tiles[radomIndex] = tile;

        //    }
        //    //총 inversion 몇개인지
        //    inversion = GetInversions();
        //    //Debug.Log(inversion);
        //    Debug.Log(message: "Puzzle Shuffled");
        //} while (inversion % 2 != 0); // 짝수 아니면 셔플

        if(puzzleNum == 1)
        {
            if (emptySpaceIndex != 4)                                                   // emptySpaceIndex 바꿔줘야 함 : (15), 4, 13, 13
            {
                //포지션 바꿔줌
                var tileOn15LastPos = tiles[4].targetPosition;
                tiles[4].targetPosition = emptySpace.position;
                emptySpace.position = tileOn15LastPos;

                //배열 바꿔줌
                tiles[emptySpaceIndex] = tiles[4];
                tiles[4] = null;
                //index 바꿔줌
                emptySpaceIndex = 4;
            }
        }
        else
        {
            if (emptySpaceIndex != 13)                                                   // emptySpaceIndex 바꿔줘야 함 : (15), 4, 13, 13
            {
                //포지션 바꿔줌
                var tileOn15LastPos = tiles[13].targetPosition;
                tiles[13].targetPosition = emptySpace.position;
                emptySpace.position = tileOn15LastPos;

                //배열 바꿔줌
                tiles[emptySpaceIndex] = tiles[13];
                tiles[13] = null;
                //index 바꿔줌
                emptySpaceIndex = 13;
            }
        }

        

        do
        {
            for (int i = 0; i <= 11; i++)
            {
                if(puzzleNum == 1)
                {
                    if (i == 4)                                                         // 바꿔줘야 하는 거 4, 13, 13
                        continue;
                }
                else
                {
                    if (i == 13)                                                         // 바꿔줘야 하는 거 4, 13, 13
                        continue;
                }

                //포지션 바꿔줌
                var lastPos = tiles[i].targetPosition;
                //int radomIndex = Random.Range(0, 14);


                int rand;
                if (puzzleNum == 1)
                {
                    int[] arr = { 0, 1, 2, 3, 5, 6, 7, 8, 9, 10, 11 };                        // 바꿔줘야 하는 거 : (Random.Range(0,15?)), 4제외, 13제외, 13제외 
                    rand = arr[Random.Range(0, arr.Length)];
                }
                else if(puzzleNum == 2)
                {
                    int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 15, 16, 17, 18, 19 };                        // 바꿔줘야 하는 거 : (Random.Range(0,15?)), 4제외, 13제외, 13제외 
                    rand = arr[Random.Range(0, arr.Length)];
                }
                else
                {
                    int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 15, 16, 17, 18, 19,20,21,22,23,24,25,26,27,28,29 };                        // 바꿔줘야 하는 거 : (Random.Range(0,15?)), 4제외, 13제외, 13제외 
                    rand = arr[Random.Range(0, arr.Length)];
                }
                int radomIndex = rand;

                tiles[i].targetPosition = tiles[radomIndex].targetPosition;
                tiles[radomIndex].targetPosition = lastPos;

                //배열도 바꿔줌
                var tile = tiles[i];
                tiles[i] = tiles[radomIndex];
                tiles[radomIndex] = tile;

            }
            //총 inversion 몇개인지
            inversion = GetInversions();
            //Debug.Log(inversion);
            Debug.Log(message: "Puzzle Shuffled");
        } while (inversion % 2 != 0); // 짝수 아니면 셔플                              // 바꿔줘야 하는 거 (퍼즐 2,3) : 짝수 홀수


        _isShuffled = true;
    }

    public int findIndex(TilesScript ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i] != null)
            {
                if(tiles[i] == ts)
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
}
