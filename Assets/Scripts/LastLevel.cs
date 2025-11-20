using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LastLevel : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayGame();
    }
    public void PlayGame()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("MainMenu");
    }
}
