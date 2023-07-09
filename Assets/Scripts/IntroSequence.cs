using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSequence : MonoBehaviour
{
    private string[] story;
    private float charWait = 0.1f, lineWait = 0.8f;
    private int line = 0, character = 0;

    [SerializeField] private Text text;
    [SerializeField] private AudioSource thud;

    // Start is called before the first frame update
    private void Start()
    {
        story = new string[] { "The British Museum has many things on display that belong elsewhere...",
                   "This is the story about how one person returned the items to their rightful places"};

        StartCoroutine(PrintLines());
    }

    private IEnumerator PrintLines()
    {
        while (line < story.Length)
        {
            character = 0;
            while (character < story[line].Length)
            {
                text.text += story[line][character];
                thud.Play();
                yield return new WaitForSecondsRealtime(charWait);
                character++;
            }
            text.text += "\n\n";
            yield return new WaitForSeconds(lineWait);
            line++;
        }
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadSceneAsync(0);
    }
}