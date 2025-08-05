using UnityEngine;

public class PlayerShop : MonoBehaviour
{
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private float raySize;
    [SerializeField] private GameObject ESignObj;
    [SerializeField] private GameObject PlayerCanvas;
    [SerializeField] private GameObject ShopCanvas;

    private FirstPersonController fpsController;
    private bool isInShop;
    private bool canEnterShop;
    private void Start()
    {
        isInShop = false;
        fpsController = GetComponent<FirstPersonController>();
        ShopCanvas.SetActive(false);
    }
    private void Update()
    {
        RayCastToObject();

        if (canEnterShop && !isInShop && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Enter the SHOP!");
            OpenShop();
        }
    }
    private void RayCastToObject()
    {
        if (!isInShop)
        {
            RaycastHit hit;
            Debug.DrawRay(PlayerCamera.position, PlayerCamera.forward * raySize, Color.yellow);
            if (Physics.Raycast(PlayerCamera.position, PlayerCamera.forward, out hit, raySize))
            {
                if (hit.transform.CompareTag("Seller"))
                {
                    ESignObj.SetActive(true);
                    canEnterShop = true;
                }
                else
                {
                    ESignObj.SetActive(false);
                    canEnterShop = false;
                }
            }
            else
            {
                ESignObj.SetActive(false);
            }
        }
        else
        {
            canEnterShop = false;
        }

    }
    private void OpenShop() 
    {
        fpsController.enabled = false;
        PlayerCanvas.SetActive(false);
        ShopCanvas.SetActive(true);
        canEnterShop = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CloseShop() 
    {
        PlayerCanvas.SetActive(true);
        fpsController.enabled = true;
        ShopCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
