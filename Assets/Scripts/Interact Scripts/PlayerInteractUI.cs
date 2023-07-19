using TMPro;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public GameObject MachineManagerContainer;
    public GameObject StorageManagerContainer;
    public GameObject ShopManagerContainer; // Press P to Open ShopManager Panel
    public GameObject EmployeeManagerContainer; // Press M to Open EmployeeManager Panel
    public GameObject NpcManagerContainer;

    [SerializeField] private GameObject ButtonEContainer;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private TextMeshProUGUI interactTextMeshProGUI;

    [SerializeField] private bool isPanelOpening;
    [SerializeField] private bool canMove;

    private void Awake()
    {
        ButtonEContainer.SetActive(false);
        MachineManagerContainer.SetActive(false);
        ShopManagerContainer.SetActive(false);
        StorageManagerContainer.SetActive(false);
        EmployeeManagerContainer.SetActive(false);
        NpcManagerContainer.SetActive(false);   
    }
    private void Start()
    {
        isPanelOpening = false;
        canMove = true;
       
    }
    private void Update()
    {
        InteractableObjectUIOpening();
        ShopManagerUIOpening();
        EmployeeManagerUIOpening();
    }
    private void ShowButtonE(IInteractable interactable)
    {
        ButtonEContainer.SetActive(true);
        interactTextMeshProGUI.text = interactable.GetInteractText();
    }
    private void DisplayUI(GameObject ObjectToDisplay, bool ShowOrHide)
    {
        ObjectToDisplay.SetActive(ShowOrHide);
    }

    private void InteractableObjectUIOpening()
    {
        if (playerInteract.GetInteractableObject() != null) //Condition for InteractableObject UI Opening
        {
            ShowButtonE(playerInteract.GetInteractableObject()); //Show button E whenever player is neary to Object
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isPanelOpening == false)
                {
                    IInteractable interactable = playerInteract.GetInteractableObject();
                    interactable.DisplayUI(); // In Interact have DisplayUI Method
                    isPanelOpening = true;
                }
                else if(isPanelOpening == true)
                {
                    IInteractable interactable = playerInteract.GetInteractableObject();
                    interactable.TurnOffUI(); // In Interact have DisplayUI Method
                    isPanelOpening = false;
                }
                
            }
        }
        else
        {
            ButtonEContainer.SetActive(false);
        }
    }

    private void ShopManagerUIOpening()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPanelOpening == false)
            {
                DisplayUI(ShopManagerContainer, true);
                isPanelOpening = true;
            }
            else if (isPanelOpening == true)
            {
                DisplayUI(ShopManagerContainer, false);
                isPanelOpening = false;
                return;
            }
        }  
    }

    private void EmployeeManagerUIOpening()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isPanelOpening == false)
            {
                DisplayUI(EmployeeManagerContainer, true);
                isPanelOpening = true;
            }
            else if (isPanelOpening == true)
            {
                DisplayUI(EmployeeManagerContainer, false);
                isPanelOpening = false;
                return;
            }
        }
    }

    public bool FreezePlayerMovementByUIDisplaying()
    {
        
        if (isPanelOpening == true)
        {
            return true; // true means UI is Display then Player can't move
        }
        else
        {
            return false; // false means Ui isn't Display then Player can move
        }
    }
}
