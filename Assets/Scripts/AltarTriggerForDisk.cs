using UnityEngine;

public class AltarTriggerForDisk : MonoBehaviour
{
    [SerializeField] private Transform DiskPoint;
    [SerializeField] private PlayerObjectDragSystem dragSystem;
    [SerializeField] private GamedevGodDialog gamedevGodDialog;
    private bool diskIsOnPlace;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Disk" && !diskIsOnPlace)
        {
            diskIsOnPlace = true;
            dragSystem.UndragDisk();
            Debug.Log("Disk is here");
            col.GetComponent<Rigidbody>().isKinematic = true;
            col.transform.position = DiskPoint.position;
            gamedevGodDialog.StartDialogue();
            SoundController.GetInstance().PlayFinalDisk();

        }
    }
}
