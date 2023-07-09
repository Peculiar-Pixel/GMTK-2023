using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject[] itemsThisLevel;
    [SerializeField] private Inventory player;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < itemsThisLevel.Length; i++)
        {
            player.GetNewItem(itemsThisLevel[i]);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
