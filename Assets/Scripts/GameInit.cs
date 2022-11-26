using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInit : MonoBehaviour
{
    private List<AsyncOperation> _ops;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        LoadFirstScenes();
    }

    private void LoadFirstScenes()
    {
        _ops = new List<AsyncOperation>(){
            SceneManager.LoadSceneAsync("CityLevel", LoadSceneMode.Additive),
            SceneManager.LoadSceneAsync("GameplayScene", LoadSceneMode.Additive),
        };
        StartCoroutine(UnloadInitScene());
    }

    private IEnumerator UnloadInitScene()
    {
        for (int i = 0; i < _ops.Count; i++)
        {
            if (!_ops[i].isDone)
            {
                yield return null;
            }
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("CityLevel"));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(0));
    }
}
