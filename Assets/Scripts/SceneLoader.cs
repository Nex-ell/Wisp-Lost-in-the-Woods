using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    [SerializeField] bool isFinalBoss = false;
    public float transitionTime = 1f;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFinalBoss)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }


}
