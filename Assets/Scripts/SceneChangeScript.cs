using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeScript : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public void StartGame()
    {
        StartCoroutine(LoadScene(1));
    }

    // Called when the replay button is clicked
    public void ReplayButton()
    {
        StartCoroutine(LoadScene(1));
    }


    // Called when the quit button is clicked
    public void QuitButton()
    {
        StartCoroutine(LoadScene(0));
    }

    public IEnumerator LoadScene(int index)
    {
        animator.SetTrigger("fade");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }

}
