using System.Collections;
using TMPro;
using UnityEngine;

public class NotificationsScript : MonoBehaviour
{
    private static NotificationsScript instance;
    public static NotificationsScript GetInstance() { return instance; }
    private string fullMessage = "";
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private TextMeshProUGUI[] texts;
    private bool showingMessage;
    private float timerValue;
    [SerializeField] private float timeToChangeColor;
    private int currentStage = 0;
    private void Start()
    {
        foreach (var text in texts)
        {
            text.color = new Color(1, 1, 1, 0);
        }
    }
    public void ShowMessage(string message)
    {
        fullMessage += "\n" + message;
        foreach (var text in texts)
        {
            text.text = fullMessage;
            text.color = new Color(1, 1, 1, 1);
        }

        //timerValue = 0f;
        //showingMessage = true;
    }
    //private void Update()
    //{
    //    ShowingMessage();
    //}
    //private void ShowingMessage()
    //{
    //    if (showingMessage)
    //    {
    //        switch (currentStage)
    //        {
    //            case 0:
    //                {
    //                    timerValue += Time.deltaTime / timeToChangeColor;
    //                    foreach (var text in texts)
    //                    {
    //                        text.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), timerValue);
    //                    }
    //                    if (timerValue >= 1f)
    //                    {
    //                        currentStage = 1;
    //                        timerValue = 0f;
    //                    }
    //                    break;
    //                }
    //            case 1:
    //                {
    //                    timerValue += Time.deltaTime;
    //                    if (timerValue >= 5f)
    //                    {
    //                        currentStage = 2;
    //                        timerValue = 0f;
    //                    }
    //                    break;
    //                }
    //            case 2:
    //                {
    //                    timerValue += Time.deltaTime / timeToChangeColor;
    //                    foreach (var text in texts)
    //                    {
    //                        text.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), timerValue);
    //                    }
    //                    if (timerValue >= 1f)
    //                    {
    //                        currentStage = 0;
    //                        showingMessage = false;
    //                    }
    //                    break;
    //                }

    //        }

    //    }
    //}


}
