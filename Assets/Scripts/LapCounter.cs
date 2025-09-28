using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LapCounter : MonoBehaviour
{
    public int totalLaps = 3;
    private int currentLap = 0;
    private bool passedCheckpoint = false;
    public bool isPlayer = false;
    public TMP_Text lapText;
    public TMP_Text raceTimerText;
    public TMP_Text winText;
    private float totalTime = 0f;
    private float raceTime = 0f;
    private float lapStartTime = 0f;

    private void Start()
    {
        UpdateLapText();
        lapStartTime = Time.time;
    }

    private void Update()
    {
        if (isPlayer)
        {
            totalTime = Time.time;
            raceTime = Time.time - lapStartTime;
            if (raceTimerText != null)
            {
                int minutes = (int)(raceTime / 60);
                int seconds = (int)(raceTime % 60);
                int milliseconds = (int)((raceTime * 100) % 100);
                raceTimerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            passedCheckpoint = true;
            Debug.Log(gameObject.name + " passed Checkpoint.");
        }

        if (other.gameObject.tag == "FinishLine")
        {
            if (passedCheckpoint)
            {
                currentLap++;
                Debug.Log(gameObject.name + " passed " + currentLap + " laps.");
                passedCheckpoint = false;

                if (isPlayer)
                {
                    float lapTime = Time.time - lapStartTime;
                    Debug.Log("Last lap time: " + lapTime);

                    lapStartTime = Time.time;

                    UpdateLapText();
                }

                if (currentLap >= totalLaps)
                {
                    if (isPlayer)
                    {
                        Debug.Log("You win!");
                        winText.text = "You win! " + totalTime + " Now you can drive freely.";
                    }
                    else
                    {
                        Debug.Log(gameObject.name + " win!");
                    }
                }
            }
            else
            {
                Debug.Log(gameObject.name + " crossed the finish line in the wrong direction!");
            }
        }
    }

    private void UpdateLapText()
    {
        if (lapText != null)
        {
            lapText.text = "Lap: " + currentLap + "/" + totalLaps;
        }
    }
}