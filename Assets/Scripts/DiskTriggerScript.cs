using UnityEngine;

public class DiskTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            LevelScript.GetInstance().ChangeLocationToAltar();
        }
    }
}
