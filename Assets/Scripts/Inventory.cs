using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject[] items;

    public void GetNewItem(GameObject item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(isFull[i] == false)
            {
                //add item to inventory
                isFull[i] = true;
                items[i] = Instantiate(item, slots[i].transform, false);
                break;
            }
        }
    }
    
    public void RemoveItem(int itemIndex)
    {

    }
}
