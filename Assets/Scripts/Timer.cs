using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static bool isTimerAvailable;
    [SerializeField] private TextMeshProUGUI timeLabel;

    public static float time;

    private void Start()
    {
        isTimerAvailable = true;
    }

    private void Update()
    {
        if (isTimerAvailable)
        {
            time += Time.deltaTime;
            TimeShow(time);
        }
    }
    private void TimeShow(float timeToDisplay)
    {
        timeToDisplay += 1;
        float hours = Mathf.FloorToInt(timeToDisplay / 3600);
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeLabel.text = "Time: " + string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    public void ResetTime()
    {
        time = 0;
    }

    public void Stop()
    {
        isTimerAvailable = false;
    }

    public void Launch()
    {
        isTimerAvailable = true;
    }
}
