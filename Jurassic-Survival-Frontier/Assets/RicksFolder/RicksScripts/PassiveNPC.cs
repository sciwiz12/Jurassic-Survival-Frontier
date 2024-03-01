using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveNPC : MonoBehaviour
{
    public string[] dialogues; // Array of dialogues for the NPC
    public bool canTrade; // Determines if this NPC can trade
    public Item[] items; // Items the NPC has for sale
    private Inventory playerInventory; // Reference to the player's inventory
    public GameObject inventorySlot; // Reference to the inventory slot
    private Dictionary<Item, InventorySlot> itemSlotMapping = new Dictionary<Item, InventorySlot>(); // Maps items to inventory slots
    public GameObject dialogueUI; // UI panel for dialogues
    public Text dialogueText; // Text component to display dialogues
    public GameObject tradingUI; // UI panel for trading
    public GameObject inventoryUI; // UI panel for the player's inventory
    private bool playerInRange = false; // Tracks if the player is in range
    private int dialogueIndex = 0; // Tracks the current dialogue

    void Awake()
    {
        CreateItemSlots();
    }

    void Start()
    {
        tradingUI.SetActive(false);
        dialogueUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogueUI.activeInHierarchy)
            {
                StartDialogue();
            }
            else
            {
                ContinueDialogue();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            EndDialogue();
            tradingUI.SetActive(false);
            inventoryUI.SetActive(false);
        }
    }

    void StartDialogue()
    {
        dialogueUI.SetActive(true);
        dialogueIndex = 0;
        dialogueText.text = dialogues[dialogueIndex];
    }

    void ContinueDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[dialogueIndex];
        }
        else
        {
            EndDialogue();
            if (canTrade)
            {
                StartTrading();
            }
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
    }

    void StartTrading()
    {
        tradingUI.SetActive(true);
        inventoryUI.SetActive(true);
        // Initialize trading logic here
    }

    void CreateItemSlots()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Item item = items[i];
            GameObject newSlot = Instantiate(inventorySlot, tradingUI.transform);
            newSlot.GetComponent<InventorySlot>().SetParent(false);
            newSlot.GetComponent<InventorySlot>().item = item;
            itemSlotMapping.Add(item, newSlot.GetComponent<InventorySlot>());
            // Set the slot's "isInventorySlot" property to false
            newSlot.GetComponent<InventorySlot>().isInventorySlot = false;
            // Set the item's quantity in the slot
            newSlot.GetComponent<InventorySlot>().UpdateSlot(item.itemName, item.quantity);
            // Set the item's image in the slot
            newSlot.GetComponent<InventorySlot>().itemImage.sprite = item.icon;
            // set the slot's position and size
            RectTransform slotTransform = newSlot.GetComponent<RectTransform>();
            slotTransform.anchoredPosition = new Vector2(50 + (i * 100), -50);
            slotTransform.sizeDelta = new Vector2(30, 30);
            // If there are more than three items, start a new row of slots on the 4th item
            if (i > 2)
            {
                slotTransform.anchoredPosition = new Vector2(50 + ((i - 3) * 100), -150);
            }



        }
    }


    /*
    public void BuyItem(Item item)
    {
        if (playerInventory.money >= item.price)
        {
            playerInventory.AddItem(item);
            playerInventory.money -= item.price;
            money += item.price;
            moneyText.text = "Money: " + money.ToString();
        }
        else
        {
            Debug.Log("Not enough money to buy this item.");
        }
    }

    public void SellItem(Item item)
    {
        if (playerInventory.HasItem(item))
        {
            playerInventory.RemoveItem(item);
            playerInventory.money += item.price;
            money -= item.price;
            moneyText.text = "Money: " + money.ToString();
        }
        else
        {
            Debug.Log("Player does not have this item to sell.");
        }
    */
}
