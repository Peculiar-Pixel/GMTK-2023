using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject[] items;
    public GameObject[] itemsGraphic;

    private bool canPlaceItem;

    private GameObject interactionArea;

    private bool placedAllItems = false;

    public void GetNewItem(GameObject item, GameObject graphics)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(isFull[i] == false)
            {
                //add item to inventory
                isFull[i] = true;
                items[i] = Instantiate(item, slots[i].transform, false);
                itemsGraphic[i] = graphics;
                break;
            }
        }
    }
    
    public void RemoveItem(int itemIndex)
    {
        isFull[itemIndex] = false;
        Destroy(items[itemIndex]);
        for (int i = 0; i < slots.Length; i++)
        {
            Debug.Log(isFull[i]);
            if (isFull[i] == true)
                return;
        }
        placedAllItems = true;
        Debug.Log("Placed all items: " + placedAllItems);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Interactable")
        {
            canPlaceItem = true;
            interactionArea = collision.gameObject;
            Debug.Log("error");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            canPlaceItem = false;
            interactionArea = null;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && canPlaceItem && interactionArea)
        {
            
            if (interactionArea.GetComponent<InteractionArea>().ExitStage == false)
            {
                int i = interactionArea.GetComponent<InteractionArea>().requiredItemIndex;                
                if (isFull[i])
                {
                    RemoveItem(i);
                    Instantiate(itemsGraphic[i], interactionArea.transform, false);
                }                
            }
            else
            {
                //change later
                if (placedAllItems) SceneManager.LoadSceneAsync(0);
            }
        }        
    }
}
