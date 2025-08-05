using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MechanicObject : MonoBehaviour
{
    [Header("Settings")]
    private string MechanichName;
    [SerializeField] UnityEvent MechanicOpenMetod;
    [Space]
    
    [Header("Other")]
    [Space]
    [SerializeField] private Vector3 textOffset;
    [SerializeField] private Transform TextObject;
    [SerializeField] private TextMeshProUGUI text;

    //private void Start()
    //{
    //    text.text = MechanichName;
    //}
    private void Update()
    {
        TextObject.transform.position = transform.position + textOffset;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Kotel"))
        {
            //col.GetComponent<KotelParticles>().PlayParticles();
            OpenMechanic();
            LevelScript.GetInstance().StartBugsWave();
        }
    }
    private void OpenMechanic() 
    {

        MechanicOpenMetod.Invoke();
        //Debug.Log($"{MechanichName} теперь в игре!");
        NotificationsScript.GetInstance().ShowMessage($"{MechanichName}");
        Destroy(gameObject);
    }
    public void UpdateName(string name) 
    {
        MechanichName = name;
        text.text = MechanichName;
    }
}
