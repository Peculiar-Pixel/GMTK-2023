using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] itemsThisLevelUI;
    public GameObject[] itemsThisLevel;
    [SerializeField] private GameObject player;

    void Start()
    {
        for (int i = 0; i < itemsThisLevelUI.Length; i++)
        {
            player.GetComponent<Inventory>().GetNewItem(itemsThisLevelUI[i], itemsThisLevel[i]);
        }        
    }

    public void EndLevel()
    {
        Time.timeScale = 0;
        //Popup UI
    }
}
