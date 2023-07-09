using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSequence : MonoBehaviour
{
    private string[] story;
    private float charWait = 0.1f, lineWait = 0.3f;
    private int line = 0, character = 0;

    // Start is called before the first frame update
    private void Start()
    {
        story = new string[] { "The British Museum has many things on display that belong elsewhere...",
                   "This is the story about how one person returned the items to their rightful places"};

        StartCoroutine(PrintLine());
    }

    private IEnumerator PrintLine()
    {
    }

    private IEnumerator PrintChar()
    {
    }
}