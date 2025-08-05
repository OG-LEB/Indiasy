using UnityEngine;

public class PickUpSwordScript : MonoBehaviour
{
    [SerializeField] GameObject LoadingScreen;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            LoadingScreen.SetActive(false);
            LevelScript.GetInstance().PickUpSword();
        }
    }
}
