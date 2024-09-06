using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;

    public static SceneController Instance { get { return instance; } }

    [SerializeField] GameObject transition;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        transition.SetActive(true); 
        StartCoroutine(SceneLoad(sceneName));
    }

    IEnumerator SceneLoad(string sceneName)
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        transition.SetActive(false);
    }

    public void SetActiveTransition()
    {
        transition.SetActive(false);
    }
}
