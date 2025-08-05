using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GamedevGodDialog : MonoBehaviour
{
    [SerializeField] private string[] Sentences;
    [SerializeField] private TextMeshProUGUI DialogueText;
    [SerializeField] private float ColldownBetweenChars;
    [SerializeField] private float ColldownBetweenSentences;
    [SerializeField] private GameObject DialogueWin;
    private LevelScript levelScript;

    private void Start()
    {
        DialogueWin.SetActive(false);
        levelScript = LevelScript.GetInstance();
    }
    public void StartDialogue()
    {
        LoadSentences(levelScript.GetMechanicNames(), levelScript.GetMechanicComments());
        DialogueWin.SetActive(true);
        StartCoroutine(Dialogues());
    }
    IEnumerator Dialogues()
    {
        for (int i = 0; i < Sentences.Length; i++)
        {
            //Sentences
            string sentence = "";

            for (int j = 0; j < Sentences[i].Length; j++)
            {
                SoundController.GetInstance().PlayeTextSound();
                sentence += Sentences[i][j];
                DialogueText.text = sentence;
                yield return new WaitForSeconds(ColldownBetweenChars);
            }
            yield return new WaitForSeconds(ColldownBetweenSentences);
        }

        levelScript.EndGame();
    }

    private void LoadSentences(string[] mechanicNames, string[] comments) 
    {

        Sentences = new string[13];
        int localCounter = 0;

        for (int i = 0; i < 13; i++)
        {
            switch (i)
            {
                case 0:
                    {
                        Sentences[i] = "Приветствую тебя, разработчик!";
                        break;
                    }
                case 1:
                    {
                        Sentences[i] = "Я бог геймдева,и сейчас я буду оценивать игру, которую ты собрал.";
                        break;
                    }
                case 2:
                    {
                        Sentences[i] = "Итак, приступаем!";
                        break;
                    }
                case 11:
                    {
                        Sentences[i] = "Не всегда твои ожидания в выборе совпадают с реальным исходом. Дай ка подумать...";
                        break;
                    }
                case 12:
                    {
                        string score = levelScript.GetGameScoreText();
                        Sentences[i] = $"Я оцениваю твою игру как '{score}'";
                        break;
                    }
                default:
                    {
                        Sentences[i] = $"'{mechanicNames[localCounter]}'\n\n{comments[localCounter]}";
                        localCounter++;
                        break;
                    }
            }
        }
    }
}
