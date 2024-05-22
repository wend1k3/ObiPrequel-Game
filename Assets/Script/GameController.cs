using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarController bottomBar;
    // Use this for initialization
    void Start()
    {
        bottomBar.PlayScene(currentScene);
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    if (currentScene.nextScene == null) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    else
                    {
                        currentScene = currentScene.nextScene;
                        bottomBar.PlayScene(currentScene);
                    }


                }
                else
                {
                    bottomBar.PlayNextSentence();
                }
            }


        }
            
    }
}
