using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoseMechanicScript : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject MechanicWindow;
    [SerializeField] private TextMeshProUGUI WindowTitle;
    [SerializeField] private ChoseMechanicWindowVariation[] WindowVariation;
    [SerializeField] private int currentVariationid;
    private LevelScript levelScript;
    private PlayerKatanaScript playerKatana;

    [Space]
    [Header("Data")]
    [SerializeField] private string[] WindowTitles;
    [SerializeField] private string[] MechanicType;
    [SerializeField]
    private string[,] lowMechanics = { {"�����","RPG", "�������", "Action", "Adventure" },
    {"���������","��������","�������","��������","�����" },
    {"���������","����� ���������","������������� ����","���������� ��������","�����" },
    {"������� �����","�������� ���","�������� ���������","����������� ������� ��������","����������" },
    {"����������� ������","����� �������","������������� ������","Dark Ambient","Ambient" },
    {"������������ �������","VR","AR","Pixel Art","RTX"},
    {"��������","������","���������","������� ��������","�����" },
    {"������ ���","������ ���� ������","����� ������","��������� �������","�������� ������" },
    };
    [SerializeField]
    private string[,] MediumMechanics = { { "������������" ,"����������", "�����", "���������", "�������" },
    { "����� �����������" ,"�������������", "����", "�������������", "�������� ���" },
    { "������ ����������" ,"������� �����������", "�������� �������", "���������� �����������", "���������� �����������" },
    { "�����" ,"������������ �������", "�������� ���������", "������ �������", "�����" },
    { "���" ,"�����", "������", "Funk", "�����" },
    { "2D" ,"3D", "���������� �������", "����������", "VHS" },
    { "�����" ,"�������", "��������� ������", "���������", "������" },
    { "����� ������� � ��������" ,"������", "��������� �������", "��������� �����", "�������� � �������������" },
    };
    [SerializeField]
    private string[,] ExpensiveMechanics = { { "������" ,"���������� �������", "�����", "������", "������" },
    { "������" ,"���������", "����������", "���������� �����", "����������" },
    { "����������� ���" , "���� ����", "����� �� ���������", "�������� �����", "������� � NPC" },
    { "����� ������" ,"����������� ���� ����", "�����", "��������� ��������", "��������� ��������" },
    { "������� ���" ,"���������� ����� � �����", "�������", "�������", "�������" },
    { "Low Poly" ,"������� � ����� PS1", "2.5D �������", "�������, ������������ �� ����", "���������� �������" },
    { "����������" ,"������ ���", "����", "������� �����", "����������� ���" },
    { "������ ������ ������" ,"����� ������������� �������", "�������� ������", "����� �����", "��������� ����� �������" },
    };
    [SerializeField] private int YourGamePoints;
    private string currentMechanicLow;
    private string currentMechanicMedium;
    private string currentMechanicExpensive;
    private int currentMechanicIdLow;
    private int currentMechanicIdMedium;
    private int currentMechanicIdExpensive;

    //Comments
    [SerializeField]
    private string[,] lowMechanicComments = { {"��� �� ����� ����������!","������ ������ �����!", "������ �� �� ����!", "����� ���, �������!", "������ ����������� � ������ ���?" },
    {"�������, �������!","������� � ��������� � ����� �������!","��������� ���!","��� �������!","������ ����� ���� �������!" },
    {"��������� ���������!","������� ������ ������!","�� ��������� ������, �������...","������ �� ������� �� � ������ ����!","�� ����� ������..." },
    {"������ �� ���� �����!� ","����� ��� ������!","��� ����������� ������ �������!","������ �������, ������ ������!","������, ���� ����� ���������� ��� ����..." },
    {"��� ��� ���������!","�����������.","������������� ����������!","� � � � , � �� ��� �������?","������� �� ������ ����������." },
    {"������ ��� ����?","��� �����������!","����� ���������� �� ����������?","��� ����� ������ ���������!","�������!"},
    {"��� ����� ��� �����?","���, ��� ���� �����!","��� ����� ����� ������ ���� ��!","����� ������!","������ �� ���." },
    {"��������, �� ��������� ������!","��� ��� ����������!","��� �� �� ��������...","����� ���� ���� �������!","�� ����� ��� �� ���!" },
    };
    [SerializeField]
    private string[,] MediumMechanicComments = { { "��� �� ���������� �����������..." ,"��� ����� ��������� ������!", "��� ������� ��, ������� ������ �������, ���", "������ ����������!", "������� �����������, ������� ������� ����� ��� �����������..." },
    { "��������" ,"���������� ������ ���������� � ����", "��� �������!", "������� ����� ����!", "������� ���� ����� � ������, � ������ �������." },
    { "���� ��������� ��������, ���!" ,"����� ������ ������?", "���! ������ �� ������ ������ �� ���������!", "� �������� ������ �������!", "�������� ��� �������?" },
    { "����� �� ��������..." ,"� �� ����������?", "�������� ����", "��! ����� ���� ����� ����� � ��������!", "������� � ���� ���� � �������." },
    { "�����, �������!" ,"� ����� � ������� �������� ��� ���!", "���� ������ ������!", "�� �����... ", "�� ����� �� ���� �����" },
    { "� ������� �� ��������!" ,"���� �� �����������?", "������... � ��� ���������!", "��� �� ����! ��� ��������!", "���, � ��� ��� DVD �����!" },
    { "�� ���� ��������?" ,"��� �����!", "��������� ���", "������ ��������� ��������!", "�� - ����� ������!" },
    { "�� �� ���� � ������� � ������?" ,"� ���� � ����!", "��� ��� ����� ������...", "�������� ������������ ������!", "������� �������, � �� ������! ����� �� ����!" },
    };
    [SerializeField]
    private string[,] ExpensiveMechanicComments = { { "���, ��� �����?" ,"������ �� � �����, ����������!", "��� ����� ������...��������...", "������� ��� ���������� ����!", "�� ��� �� ��������!" },
    { "�� - �� - ��!" ,"������� �����, �������...", "������������ ������� � ������!", "���������� ������ ��� ������ ����!", "��� ������� �������!" },
    { "�� �� ������" , "������ ������ ������!", "�������, �����!", "�� �����!", "��, ���, ���������, �������." },
    { "��� ������, �������?" ,"��, ������������!", "�� � ����� �� �� �����", "� ������ ������ �� �����", "��� ��������� �� ��� ������?" },
    { "����� � ��������!" ,"�� � ����� �������������?", "������ ���  ����� ������!", "�� ���� ����", "������ ��� ���� ���������!" },
    { "�� ���������� ������!" ,"�� ����� - ������ ������� ������!", "���! ��� ����� ���������!", "��������� - ���������� ����!", "��� ��������� ��� ���������!" },
    { "���������, ��� �� �����?" ,"���������, ��� ��� �����?", "���������� ������ �����!", "�� ��", "������ ����������!" },
    { "���� ����� ����� ����!" ,"�� ��� ��� 100 ����� �� ����������!", "�� ����� ������������ ��������!", "��� ����� ��� �� 100 ��������!", "��� ��� ���� �����" },
    };

    [Space]
    [Header("Spawn mechanic")]
    [SerializeField] private GameObject MechanicObject;
    [SerializeField] private Transform PlayerCamera;
    private void Start()
    {
        levelScript = LevelScript.GetInstance();
        MechanicWindow.SetActive(false);
    }
    public void OpenChooseMechanicWindow()
    {
        if (playerKatana == null)
        {
            playerKatana = Player.GetComponent<PlayerKatanaScript>();
        }
        playerKatana.ChooseMechanicWindowShootingFix();
        LevelScript.GetInstance().KatanaOff();
        Player.GetComponent<FirstPersonController>().enabled = false;
        Player.GetComponent<Rigidbody>().isKinematic = true;
        Cursor.lockState = CursorLockMode.Confined;
        LoadWindow();

        MechanicWindow.SetActive(true);
        currentVariationid++;

    }
    private void LoadWindow()
    {
        foreach (var variation in WindowVariation)
        {
            variation.gameObject.SetActive(false);
        }
        WindowTitle.text = WindowTitles[currentVariationid];
        Button[] buttons = WindowVariation[currentVariationid].buttons;
        TextMeshProUGUI[] buttonsTexts = WindowVariation[currentVariationid].buttonsTexts;
        int randomButtonId = Random.Range(0, 2);
        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0:
                    {
                        //LOw
                        buttons[randomButtonId].onClick.AddListener(PickLowMechanic);
                        currentMechanicIdLow = Random.Range(0, 4);
                        currentMechanicLow = lowMechanics[currentVariationid, currentMechanicIdLow];
                        buttonsTexts[randomButtonId].text = "C://Users//God> " + currentMechanicLow;
                        break;
                    }
                case 1:
                    {
                        //Medium
                        buttons[randomButtonId].onClick.AddListener(PickMediumMechanic);
                        currentMechanicIdMedium = Random.Range(0, 4);
                        currentMechanicMedium = MediumMechanics[currentVariationid, currentMechanicIdMedium];
                        buttonsTexts[randomButtonId].text = "C://Users//God> " + currentMechanicMedium;
                        break;
                    }
                case 2:
                    {
                        //Expensive
                        buttons[randomButtonId].onClick.AddListener(PickExpensiveMechanic);
                        currentMechanicIdExpensive = Random.Range(0, 4);
                        currentMechanicExpensive = ExpensiveMechanics[currentVariationid, currentMechanicIdExpensive];
                        buttonsTexts[randomButtonId].text = "C://Users//God> " + currentMechanicExpensive;
                        break;
                    }
            }

            randomButtonId++;
            if (randomButtonId >= 3)
            {
                randomButtonId = 0;
            }
        }

        WindowVariation[currentVariationid].gameObject.SetActive(true);
    }
    public void CloseWindow()
    {
        MechanicWindow.SetActive(false);
        Player.GetComponent<FirstPersonController>().enabled = true;
        Player.GetComponent<Rigidbody>().isKinematic = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void PickLowMechanic()
    {
        int typeid = currentVariationid - 1;
        levelScript.LoadMechanicDataAndCommentById(typeid, MechanicType[typeid] + ": " + currentMechanicLow, lowMechanicComments[typeid, currentMechanicIdLow]);

        //Debug.Log($"�� ������ ������� {currentMechanicLow}");
        YourGamePoints += 1;
        SpawnMechanicObject(MechanicType[typeid] + ": " + currentMechanicLow);
        LevelScript.GetInstance().KatanaOn();

    }
    private void PickMediumMechanic()
    {
        int typeid = currentVariationid - 1;
        levelScript.LoadMechanicDataAndCommentById(typeid, MechanicType[typeid] + ": " + currentMechanicMedium, MediumMechanicComments[typeid, currentMechanicIdMedium]);


        YourGamePoints += 2;
        //Debug.Log($"�� ������ ��������� {currentMechanicMedium}");
        SpawnMechanicObject(MechanicType[typeid] + ": " + currentMechanicMedium);
        LevelScript.GetInstance().KatanaOn();

    }
    private void PickExpensiveMechanic()
    {
        int typeid = currentVariationid - 1;
        levelScript.LoadMechanicDataAndCommentById(typeid, MechanicType[typeid] + ": " + currentMechanicExpensive, ExpensiveMechanicComments[typeid, currentMechanicIdExpensive]);


        //Debug.Log($"�� ������ ������� ������������ {currentMechanicExpensive}");
        YourGamePoints += 3;
        SpawnMechanicObject(MechanicType[typeid] + ": " + currentMechanicExpensive);
        LevelScript.GetInstance().KatanaOn();

    }
    private void SpawnMechanicObject(string mechanic)
    {
        //Vector3 roundSpawn = Random.insideUnitSphere;
        int randomxValue = Random.Range(-10, 10);
        if (randomxValue > 0)
        {
            randomxValue = Mathf.Clamp(randomxValue, 5, 10);
        }
        if (randomxValue < 0)
        {
            randomxValue = Mathf.Clamp(randomxValue, -5, -10);
        }

        int randomyValue = Random.Range(-10, 10);
        if (randomyValue > 0)
        {
            randomyValue = Mathf.Clamp(randomyValue, 5, 10);
        }
        if (randomyValue < 0)
        {
            randomyValue = Mathf.Clamp(randomyValue, -5, -10);
        }
        Vector3 spawnPoint = new Vector3(randomxValue, 7, randomyValue);
        Debug.Log($"Spawn position is {spawnPoint}");
        GameObject obj = Instantiate(MechanicObject, spawnPoint, Quaternion.identity);
        obj.GetComponent<MechanicObject>().UpdateName(mechanic);
    }
    public int GetGameScore() { return YourGamePoints; }

}
