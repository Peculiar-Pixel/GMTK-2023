using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public List<GameObject> menus;

    public void SwitchMenu(GameObject menu)
    {
        //all menus off
        foreach (GameObject item in menus)
        {
            item.SetActive(false);
        }

        //current menu on
        menu.SetActive(true);
    }

    public void LoadScene(int buildindex)
    {
        SceneManager.LoadScene(buildindex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}