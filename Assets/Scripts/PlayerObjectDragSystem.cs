using UnityEngine;

public class PlayerObjectDragSystem : MonoBehaviour
{
    [SerializeField] private float raySize;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private GameObject ESignObj;
    [SerializeField] private bool CanDragObject;
    [SerializeField] private Transform ObjectToDrag;
    [SerializeField] private bool isDraggingObject;
    [SerializeField] private Vector3 DragOffset;
    private void Start()
    {
        ESignObj.SetActive(false);
        CanDragObject = false;
    }
    private void Update()
    {
        RayCastToObject();

        if (CanDragObject && Input.GetKeyDown(KeyCode.E))
        {
            ObjectToDrag.GetComponent<Rigidbody>().isKinematic = true;
            isDraggingObject = true;
            DragOffset = PlayerCamera.position - ObjectToDrag.position;
            CanDragObject = false;
            ESignObj.SetActive(false);
            SoundController.GetInstance().PlayPickUp();

        }

        else if (isDraggingObject)
        {
            if (ObjectToDrag == null)
            {
                isDraggingObject = false;
                CanDragObject = true;
                return;
            }
            ObjectToDrag.position = PlayerCamera.position + (PlayerCamera.forward * 2f);
            if (Input.GetKeyDown(KeyCode.E))
            {
                isDraggingObject = false;
                CanDragObject = true;
                ObjectToDrag.GetComponent<Rigidbody>().isKinematic = false;
                SoundController.GetInstance().PlayPickUp();

            }
        }
    }
    private void RayCastToObject()
    {
        if (!isDraggingObject)
        {
            RaycastHit hit;
            //Debug.DrawRay(PlayerCamera.position, PlayerCamera.forward * raySize, Color.yellow);
            if (Physics.Raycast(PlayerCamera.position, PlayerCamera.forward, out hit, raySize))
            {
                if (hit.transform.CompareTag("DragObject"))
                {
                    //Debug.Log("Ray hit drag object!");
                    ESignObj.SetActive(true);
                    CanDragObject = true;
                    ObjectToDrag = hit.transform;
                }
                else
                {
                    ESignObj.SetActive(false);
                    CanDragObject = false;
                }
            }
            else
            {
                ESignObj.SetActive(false);
            }
        }
        else
        {
            CanDragObject = false;
        }

    }
    public void UndragDisk()
    {
        isDraggingObject = false;
        CanDragObject = true;
        ObjectToDrag.GetComponent<Rigidbody>().isKinematic = false;
        SoundController.GetInstance().PlayPickUp();
    }
}
