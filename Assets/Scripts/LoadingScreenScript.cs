using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class LoadingScreenScript : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    private float progressBarValue = 0.1f;
    [SerializeField] private float progressBarTime;
    private bool isLoading;
    private bool isCubeLoading;
    [SerializeField] private Transform LoadingCube;
    [SerializeField] private GameObject Advice;
    [SerializeField] private LevelScript levelScript;

    [Header("MainMenu")]
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private string titleTextValue;
    [SerializeField] private TextMeshProUGUI LoadingText;
    [SerializeField] private string loadingTextValue;
    [SerializeField] private float charDelayTitle;
    [SerializeField] private float charDelayLoading;
    [SerializeField] private GameObject PlayButton;
    [SerializeField] private GameObject LoadingScreen;
    private void Start()
    {
        LoadingScreen.SetActive(false);
        Advice.SetActive(false);
        PlayButton.SetActive(false);
        titleTextValue = TitleText.text;
        TitleText.text = "";
        StartLoadingWindow();
    }
    //private void Update()
    //{
    //    if (isLoading)
    //    {
    //        if (progressBarValue < 1)
    //        {
    //            progressBarValue += 0.00035f;
    //        }
    //        else
    //        {
    //            isLoading = false;
    //            Advice.SetActive(true);
    //            isCubeLoading = true;
    //        }
    //        //_progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, progressBarValue, progressBarTime);
    //        _progressBar.fillAmount = progressBarValue;
    //    }
    //    if (isCubeLoading)
    //    {
    //        LoadingCube.Translate(Vector3.forward * 0.01f);
    //    }
    //}
    public void StartLoading()
    {
        //isLoading = true;
        levelScript.NowPlayerCanStartMoving();
        StartCoroutine(LoadingSymbols());
    }
    private void StartLoadingWindow() 
    {
        StartCoroutine(LoadMainMenu());
    }
    private IEnumerator LoadMainMenu() 
    {
        string title = "";
        for (int i = 0; i < titleTextValue.Length; i++)
        {
            SoundController.GetInstance().PlayeTextSound();
            title += titleTextValue[i];
            TitleText.text = title;
            yield return new WaitForSeconds(charDelayTitle);
        }
        PlayButton.SetActive(true);

    }
    public void PlayBtn()
    {
        //MainMenuCanvas.SetActive(false);
        LoadingScreen.SetActive(true);
        loadingTextValue = LoadingText.text;
        StartLoading();
        PlayButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
    private IEnumerator LoadingSymbols() 
    {
        string loadingSymbols = "";
        for (int i = 0; i < loadingTextValue.Length; i++)
        {
            SoundController.GetInstance().PlayeTextSound();
            loadingSymbols += loadingTextValue[i];
            LoadingText.text = loadingSymbols;
            //if (i == 50)
            //{
            //    levelScript.NowPlayerCanStartMoving();

            //}

            yield return new WaitForSeconds(charDelayLoading);
        }
    }
}
