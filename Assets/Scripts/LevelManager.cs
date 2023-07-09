using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] itemsThisLevel;
    [SerializeField] private GameObject player;

    void Start()
    {
        for (int i = 0; i < itemsThisLevel.Length; i++)
        {
            player.GetComponent<Inventory>().GetNewItem(itemsThisLevel[i]);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
