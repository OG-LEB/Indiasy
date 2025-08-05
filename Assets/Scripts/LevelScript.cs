using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    private static LevelScript instance;
    public static LevelScript GetInstance() { return instance; }

    private void Awake()
    {
        instance = this;
        KatanaOff();
    }

    [SerializeField] private ParticleSystem KotelParticles;
    [SerializeField] private AudioSource KotelBoilSound;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject _playerCanvas;
    [SerializeField] private Transform KotelTransform;
    private bool isPlayerCanMove = false;
    [Header("Main menu elemets")]
    [SerializeField] private GameObject MainMenuCanvas;
    //[SerializeField] private GameObject LoadingScreenCanvas;
    //[SerializeField] private LoadingScreenScript loadingScript;
    bool CanStartMove;

    //[Space]
    //[Header("Links")]
    private ChoseMechanicScript _choseMechanicScript;
    private EnemySpawner _enemySpawner;

    [SerializeField] private GameObject KatanaSprite;
    [SerializeField] private PlayerKatanaScript KatanaScript;

    [Space]
    [Header("Wave")]
    [SerializeField] private GameObject KotelTitle;
    [SerializeField] private GameObject KotelProgressBarObject;
    [SerializeField] private Image ProgressBar;
    private bool BugsWave;
    [SerializeField] private float[] WaveProgressAmount;
    [SerializeField] private float WaveProgressStep;
    [SerializeField] private int WaveCounter;
    [SerializeField] private float currentWavePoints;
    [SerializeField] private float currentWavePointsMax;

    [Space]
    [Header("GameOver")]
    [SerializeField] private GameObject GameOverCanvas;

    [Space]
    [Header("LocationTranslation")]
    [SerializeField] private GameObject StartLocation;
    [SerializeField] private GameObject MainLocation;
    [SerializeField] private GameObject AltarLocation;
    [SerializeField] private GameObject KotelObj;
    [SerializeField] private GameObject DiskTrigger;
    [SerializeField] private GameObject DiskObj;

    [Space]
    [Header("FinalDialogueData")]
    [SerializeField] private string[] MechanicsName = new string[8];
    [SerializeField] private string[] MechanicsComments = new string[8];
    [SerializeField] private int YourGameScore;
    [SerializeField] private string GameScoreText;

    [Space]
    [Header("EndGame")]
    [SerializeField] private GameObject EndGameWindow;
    [SerializeField] private TextMeshProUGUI EndGameWinTitle;
    [SerializeField] private TextMeshProUGUI EndGameWinStatisticText;


    private void Start()
    {
        Time.timeScale = 1.0f;
        StartGame();
        _choseMechanicScript = GetComponent<ChoseMechanicScript>();
        _enemySpawner = GetComponent<EnemySpawner>();
        KotelTitle.SetActive(true);
        KotelProgressBarObject.SetActive(false);
        GameOverCanvas.SetActive(false);
        StartLocation.SetActive(true);
        MainLocation.SetActive(false);
        AltarLocation.SetActive(false);
        DiskObj.SetActive(false);
        DiskTrigger.SetActive(false);
        EndGameWindow.SetActive(false);
    }
    private void StartGame()
    {
        Player.GetComponent<FirstPersonController>().enabled = false;
        Player.GetComponent<Rigidbody>().isKinematic = true;
        isPlayerCanMove = false;
        _playerCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
        //LoadingScreenCanvas.SetActive(false);
        KatanaOff();
        SoundController.GetInstance().PlayMainSoundTrack();
    }
    private void Update()
    {
        if (!isPlayerCanMove && Input.anyKeyDown && CanStartMove)
        {
            EnablePlayerMovement();
            Debug.Log("WTF");
        }

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    _choseMechanicScript.OpenChooseMechanicWindow();
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    currentWavePoints = 20f;
        //}
        //BugsWaveCalculations();
    }
    private void FixedUpdate()
    {
        BugsWaveCalculations();

    }
    private void EnablePlayerMovement()
    {
        Player.GetComponent<Rigidbody>().isKinematic = false;
        _playerCanvas.SetActive(true);
        Player.GetComponent<FirstPersonController>().enabled = true;
        isPlayerCanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        //KatanaOn();
        CanStartMove = false;
    }
    public void NowPlayerCanStartMoving()
    {
        CanStartMove = true;
        //Debug.Log("NOW PLAYER CAN START MOVE");

    }
    public Transform GetPlayerTransform()
    {
        return Player.transform;
    }

    public void KatanaOff()
    {
        KatanaSprite.SetActive(false);
        KatanaScript.enabled = false;
    }
    public void KatanaOn()
    {
        KatanaSprite.SetActive(true);
        KatanaScript.enabled = true;
    }
    public Transform GetKotelTransform()
    {
        return KotelTransform;
    }
    public void StartBugsWave()
    {
        _enemySpawner.StartWave();
        KotelTitle.SetActive(false);
        KotelProgressBarObject.SetActive(true);
        BugsWave = true;
        currentWavePointsMax = WaveProgressAmount[WaveCounter];
        currentWavePoints = 0;
        KotelParticles.Play();
        KotelBoilSound.Play();

        SoundController.GetInstance().PlayFightSoundTrack();

    }
    public void StopWave()
    {
        KotelParticles.Stop();
        KotelBoilSound.Stop();

        KotelTitle.SetActive(true);
        KotelProgressBarObject.SetActive(false);
        BugsWave = false;
        WaveCounter++;
        _enemySpawner.StopWave();
        if (WaveCounter >= 8)
        {
            GameIsReady();
        }
        else
        {
            SoundController.GetInstance().PlayeDropInKotel();

            StartCoroutine(OpenChooseMechanicWindow());
        }
        SoundController.GetInstance().PlayChillSoundTrack();

    }
    private void BugsWaveCalculations()
    {
        if (BugsWave)
        {
            currentWavePoints += WaveProgressStep;

            ProgressBar.fillAmount = currentWavePoints / currentWavePointsMax;
            if (currentWavePoints >= currentWavePointsMax)
            {
                BugsWave = false;
                StopWave();
            }

            if (currentWavePoints <= 0)
            {
                GameOver();
            }
        }
    }
    public void GetDamageFromBug(float damageAmount)
    {
        Debug.Log("Kotel Gets Damage!");
        currentWavePoints -= damageAmount;
    }
    private void GameOver()
    {
        BugsWave = false;
        Player.GetComponent<FirstPersonController>().enabled = false;
        Player.GetComponent<Rigidbody>().isKinematic = true;
        isPlayerCanMove = false;
        _playerCanvas.SetActive(false);
        KatanaOff();
        Time.timeScale = 0.5f;
        GameOverCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void RestartGameButton()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    private void GameIsReady()
    {
        SoundController.GetInstance().PlayFinalDisk();
        KotelObj.SetActive(false);
        DiskObj.SetActive(true);
        DiskTrigger.SetActive(true);
    }
    public void ChangeLocationToAltar()
    {
        MainLocation.SetActive(false);
        AltarLocation.SetActive(true);
        YourGameScore = _choseMechanicScript.GetGameScore();
        CalculateGameScore();
        SoundController.GetInstance().PlayeSwordPickUp();

    }
    public void LoadMechanicDataAndCommentById(int id, string mechanicName, string Comment)
    {
        MechanicsName[id] = mechanicName;
        MechanicsComments[id] = Comment;
    }
    public string[] GetMechanicNames() { return MechanicsName; }
    public string[] GetMechanicComments() { return MechanicsComments; }
    private void CalculateGameScore()
    {
        if (YourGameScore < 14)
            GameScoreText = "Плохо";
        if (YourGameScore > 13 && YourGameScore < 20)
            GameScoreText = "Среднячок";
        if (YourGameScore > 19)
            GameScoreText = "Шедевр";
    }
    public string GetGameScoreText() { return GameScoreText; }
    public void EndGame()
    {
        Debug.Log("End Game!");
        //BugsWave = false;
        Player.GetComponent<FirstPersonController>().enabled = false;
        Player.GetComponent<Rigidbody>().isKinematic = true;
        isPlayerCanMove = false;
        _playerCanvas.SetActive(false);
        KatanaOff();
        Time.timeScale = 0.5f;
        EndGameWindow.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;

        //Load data 
        string endGameStats = "Итоги:\n\n";

        for (int i = 0; i < MechanicsName.Length; i++)
        {
            endGameStats += MechanicsName[i] + "\n";
        }
        endGameStats += $"\nОценка вашей игры: {GameScoreText}";
        EndGameWinStatisticText.text = endGameStats;

        switch (GameScoreText)
        {
            case "Плохо":
                {
                    EndGameWinTitle.text = "Вы проиграли, вам не удалось сделать шедевр.";
                    break;
                }
            case "Среднячок":
                {
                    EndGameWinTitle.text = "Вы проиграли, средний проект очень сильно отличается от шедевра.";
                    break;
                }
            case "Шедевр":
                {
                    EndGameWinTitle.text = "Поздравляем, вам удалось сделать шедевр!";
                    break;
                }

        }
    }
    public void PickUpSword()
    {
        SoundController.GetInstance().PlayeSwordPickUp();
        KatanaOn();
        StartLocation.SetActive(false);
        MainLocation.SetActive(true);
        //Timer to choose mechanic
        StartCoroutine(OpenChooseMechanicWindow());
        //SoundController.GetInstance().PlayChillSoundTrack();

    }
    private IEnumerator OpenChooseMechanicWindow()
    {
        //Debug.Log("Choose mechanic");
        yield return new WaitForSeconds(5f);
        _choseMechanicScript.OpenChooseMechanicWindow();
    }
}
