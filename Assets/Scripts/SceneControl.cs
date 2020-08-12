using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public Scene title;
    public Scene game;
    public Scene options;
    public Scene rules;
    public Scene loading;
    public Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        title = SceneManager.GetSceneByName("Title");
        game = SceneManager.GetSceneByName("Game");
        options = SceneManager.GetSceneByName("Options");
        rules = SceneManager.GetSceneByName("Rules");
        loading = SceneManager.GetSceneByName("Loading");
        currentScene = SceneManager.GetActiveScene();
    }

    public void TitleScene()
    {
        SceneManager.LoadScene("Title");
        currentScene = title;
    }

    public void OptionsScene()
    {
        SceneManager.LoadScene("Options");
        currentScene = options;
    }

    public void GameScene()
    {
        SceneManager.LoadScene("Game");
        currentScene = game;
    }

    public void RuleScene()
    {
        SceneManager.LoadScene("Rules");
        currentScene = rules;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Loading");
        currentScene = loading;
    }
}
