using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameStateController : MonoBehaviour
{
    public Button readyButton;
    public TMP_Text countdownText;
    public float countdownTime = 3f;
    public MonoBehaviour[] carScriptsToEnable;

    void SetRaceScripts(bool enabled)
    {
        foreach (var script in carScriptsToEnable)
        {
            script.enabled = enabled;
        }
    }
    public void OnReadyButtonClicked()
    {
        readyButton.gameObject.SetActive(false);
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        SetRaceScripts(false);
        float currentTime = countdownTime;
        while (currentTime > 0)
        {
            countdownText.text = ((int)currentTime).ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(false);
        SetRaceScripts(true);
    }
}