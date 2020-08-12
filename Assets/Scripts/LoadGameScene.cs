using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameScene : MonoBehaviour
{
    SceneControl scene;
    AsyncOperation async;
    ObjectReference box;
    // Start is called before the first frame update
    void Start()
    {
        scene = GetComponent<SceneControl>();
        box = GetComponent<ObjectReference>();
        StartCoroutine(LoadSceneInBG());
    }

    // Update is called once per frame
    void Update()
    {
        // if (i < 200) 
        // {
        //     DontDestroyOnLoad(Instantiate(box.Selection, new Vector3(0, -1, 0), Quaternion.identity));
        //     i++;
        // }
        // else StartCoroutine(LoadScene());
        Debug.Log("meh");
    }

    IEnumerator LoadSceneInBG()
    { 
        async = SceneManager.LoadSceneAsync(2);
        while (!async.isDone) yield return null;
    }

    public float LoadingProgress()
    {
        // float blah = ((float)i / ((float)Options.width * (float)Options.height)) * 100f;
        // return blah;
        return async.progress;
    }
}
