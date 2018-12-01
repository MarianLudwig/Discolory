using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class LevelTransition : MonoBehaviour {

    public static LevelTransition Instance;
    private Animator animator;
    private int sceneToLoad = 1;

    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
    }

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            FadeToScene();
        }
	}

    public void FadeToScene()
    {
        animator.SetTrigger("In");
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            sceneToLoad = 1;
        }
        else
        {
            sceneToLoad = 0;
        }
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
