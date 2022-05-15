using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public int seconds, minutes;
    [SerializeField] private Text timeText;


    void Start()
    {
        //AddToSecond();
        //playagain()에서 작동
    }

    public void AddToSecond()
    {
        timeText.gameObject.SetActive(true); // try again 할 때
        //seconds++;
        seconds--;

        if(seconds < 0)
        {
            if(minutes != 0)
            {
                minutes--;
                seconds = 59;
            }
            else
            {
                seconds = 0;
                minutes = 0;
            }
        }
        timeText.text = (minutes < 10 ? "0" : "") + minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
        Invoke(nameof(AddToSecond), time: 1);


        //if (seconds > 59)
        //{
        //    minutes++;
        //    seconds = 0;
        //}
        //timeText.text = (minutes < 10 ? "0" : "") + minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
        //Invoke(nameof(AddToSecond), time: 1);
    }

    public void StopTimer()
    {
        CancelInvoke(nameof(AddToSecond));
        timeText.gameObject.SetActive(false);
    }

}
