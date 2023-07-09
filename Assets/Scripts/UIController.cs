using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelect;
    public GameObject credits;
    public GameObject controls;

    public void SwitchMenu(GameObject menu)
    {
        //all menus off
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        credits.SetActive(false);
        controls.SetActive(false);

        //current menu on
        menu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}