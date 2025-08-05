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
    private string[,] lowMechanics = { {"Шутер","RPG", "Файтинг", "Action", "Adventure" },
    {"Киберпанк","Стимпанк","Фентези","Футуризм","Ретро" },
    {"Выживание","Крафт предметов","Интерактивное кино","Менеджмент ресурсов","Стелс" },
    {"Красные бочки","Открытый мир","Создание персонажа","Возможность грабить караваны","Дипломатия" },
    {"Оркестровая музыка","Звуки природы","Синтезаторная музыка","Dark Ambient","Ambient" },
    {"Реалистичная графика","VR","AR","Pixel Art","RTX"},
    {"Детектив","Рыцарь","Волшебник","Горячая красотка","Боксёр" },
    {"Спасти мир","Спасти свою любовь","Найти убийцу","Построить карьеру","Победить злодея" },
    };
    [SerializeField]
    private string[,] MediumMechanics = { { "Метроидвания" ,"Платформер", "Гонки", "Симулятор", "Рогалик" },
    { "Зомби апокалипсис" ,"Современность", "Нуар", "Средневековье", "Каменный век" },
    { "Захват территорий" ,"Решение головоломок", "Прокачка навыков", "Совместное прохождение", "Управление автомобилем" },
    { "Дрифт" ,"Перезагрузка локации", "Прокачка персонажа", "чтение записок", "Охота" },
    { "Рок" ,"Техно", "Металл", "Funk", "Рэгги" },
    { "2D" ,"3D", "Воксельная графика", "Минимализм", "VHS" },
    { "Зомби" ,"Военный", "Грабитель банков", "Сантехник", "Учёный" },
    { "Стать богатым и успешным" ,"Выжить", "Разгадать загадку", "Построить город", "Победить в соревнованиях" },
    };
    [SerializeField]
    private string[,] ExpensiveMechanics = { { "Кликер" ,"Визуальная новелла", "Квест", "Хоррор", "Аркада" },
    { "Пираты" ,"Пришельцы", "Супергерои", "Ограбление банка", "Приведения" },
    { "Бесконечный бег" , "Мини игры", "Побег из лабиринта", "Просмотр мемов", "Диалоги с NPC" },
    { "Смена скинов" ,"Возможность пить пиво", "Донат", "Говорящие предметы", "Подводное плавание" },
    { "Русский реп" ,"Бесплатные треки с ютуба", "Битбокс", "Хардбас", "Дабстеп" },
    { "Low Poly" ,"Графика в стиле PS1", "2.5D графика", "Графика, нарисованная от руки", "Казуальная графика" },
    { "Супергерой" ,"Зелёный огр", "Бомж", "Буханка хлеба", "Разработчик игр" },
    { "Купить сырные шарики" ,"Найти горечительный напиток", "Починить кресло", "Найти носки", "Разгадать тайну вокзала" },
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
    private string[,] lowMechanicComments = { {"Все мы любим пострелять!","Прощай личная жизнь!", "Только не по лицу!", "Майкл Бэй, держись!", "Готовы отправиться в другой мир?" },
    {"Вставай, самурай!","Красота и изяшество в одном флаконе!","Волшебный мир!","Это будущее!","Раньше трава была зеленее!" },
    {"Настоящее испытание!","Сначала добудь дерево!","За попкорном сгоняю, подожди...","Только не потрать всё в первый день!","Со спины значит..." },
    {"Только не стой рядом!” ","Делай что хочешь!","Это конструктор нашего времени!","Ничего личного, просто бизнес!","Милорд, орки хотят поговорить про пиво..." },
    {"Это… это прекрасно!","Успокаивает.","Универсальный инструмент!","Э э э э , а чё так страшно?","Спасибо за чёткое погружение." },
    {"Погоди… Это игра?","Дух захватывает!","Когда дополнения на реальность?","Это очень тонкое искусство!","Красиво!"},
    {"Кто украл мои носки?","Сир, это дело чести!","Моя магия круче твоего кунг фу!","Очень горячо!","Покажи ка уши." },
    {"Банально, но актуально всегда!","Как это романтично!","Где же он прячется...","Скоро свой офис откроем!","Ну давай раз на раз!" },
    };
    [SerializeField]
    private string[,] MediumMechanicComments = { { "Что то попахивает кастлванией..." ,"Это будет актуально всегда!", "Игр столько же, сколько частей форсажа, лол", "Полное погружение!", "Страшно представить, сколько времени нужно для прохождения..." },
    { "Мозгииии" ,"Достаточно просто посмотреть в окно", "Это стильно!", "Чумовое время было!", "Сначала была палка с камнем, а сейчас геймдев." },
    { "Ваши подданные поумнели, сер!" ,"Какой провод резать?", "Ура! Теперь ты умеешь сидеть за монитором!", "С друзьями всегда веселее!", "Механика или автомат?" },
    { "Ночью на парковке..." ,"А ты сохранился?", "Тамагочи прям", "Да! Пусть весь сюжет будет в записках!", "Гранату в воду кинь и радуйся." },
    { "Еееее, рооооок!" ,"В школе в туалете танцевал под это!", "ТЬМА СМЕРТЬ САТАНА!", "Ну ладно... ", "Аж тепло на душе стало" },
    { "А задники то красивые!" ,"Очки не понадобятся?", "Знаешь... А это интересно!", "Это не лень! Это культура!", "Бро, у нас уже DVD давно!" },
    { "Ну хоть красивый?" ,"Так точно!", "Скользкий тип", "Пахнет настойщим мужчиной!", "Ум - самое важное!" },
    { "Ты за этим в геймдев и пришёл?" ,"Я верю в тебя!", "Тут нам нужен Шерлок...", "Побольше компьютерных клубов!", "Главное участие, а не победа! Помни об этом!" },
    };
    [SerializeField]
    private string[,] ExpensiveMechanicComments = { { "Лол, где хомяк?" ,"Только не о любви, пожалуйста!", "Это будет весело...наверное...", "Знаешь… Это прекрасный жанр!", "Ну это по классике!" },
    { "Йо - хо - хо!" ,"Зеленый такие, смешные...", "Обтягивающие костюмы в студию!", "Желательно делать под крутой трек!", "Это слишком страшно!" },
    { "Ах ты читер…" , "Обожаю всякие мелочи!", "Ахахаха, удачи!", "Ме густа!", "Да, нет, убеждение, сарказм." },
    { "Дай угадаю, платные?" ,"Ох, холодненькое!", "Эм… я думал ты за идею…", "Я такого раньше не видел…", "Кто проживает на дне океана?" },
    { "Круто и молодёжно!" ,"Ну а зачем переплачивать?", "Кстати тут  нужен талант!", "Ох этот вайб…", "Сейчас шею буду разминать!" },
    { "Всё гениальное просто!" ,"Всё новое - хорошо забытое старое!", "Ого! Это будет интересно!", "Художники - гениальные люди!", "Как отдельный вид искусства!" },
    { "Интересно, что он умеет?" ,"Интересно, как его зовут?", "Прожженный жизнью герой!", "Эм… ок", "Полное погружение!" },
    { "Ради этого стоит жить!" ,"Ну тут без 100 грамм не разберёшься!", "Ну чисто человеческая проблема!", "Так можно лет на 100 пропасть!", "Про это чуть позже…" },
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

        //Debug.Log($"Ты выбрал ДЕШЕВКУ {currentMechanicLow}");
        YourGamePoints += 1;
        SpawnMechanicObject(MechanicType[typeid] + ": " + currentMechanicLow);
        LevelScript.GetInstance().KatanaOn();

    }
    private void PickMediumMechanic()
    {
        int typeid = currentVariationid - 1;
        levelScript.LoadMechanicDataAndCommentById(typeid, MechanicType[typeid] + ": " + currentMechanicMedium, MediumMechanicComments[typeid, currentMechanicIdMedium]);


        YourGamePoints += 2;
        //Debug.Log($"Ты выбрал СРЕДНЯЧОК {currentMechanicMedium}");
        SpawnMechanicObject(MechanicType[typeid] + ": " + currentMechanicMedium);
        LevelScript.GetInstance().KatanaOn();

    }
    private void PickExpensiveMechanic()
    {
        int typeid = currentVariationid - 1;
        levelScript.LoadMechanicDataAndCommentById(typeid, MechanicType[typeid] + ": " + currentMechanicExpensive, ExpensiveMechanicComments[typeid, currentMechanicIdExpensive]);


        //Debug.Log($"Ты выбрал ДОРОГОЕ УДОВОЛЬСТВИЕ {currentMechanicExpensive}");
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
