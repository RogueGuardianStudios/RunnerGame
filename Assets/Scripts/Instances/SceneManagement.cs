using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{
  
 [HideInInspector] public static SceneManagement instance;

 [SerializeField] private SceneData_SO LoadOnStartScene; 

  private void Awake()
  {
    if(instance)
      Destroy(gameObject);

    instance = this;
    DontDestroyOnLoad(gameObject);

    
  }

  private void Start()
  {
    LoadScene(LoadOnStartScene);
  }

  public void LoadScene(SceneData_SO loadOnStartScene)
  {
    StartCoroutine(nameof(LoadSceneAsync), loadOnStartScene);
  }

  private IEnumerator LoadSceneAsync(SceneData_SO sceneToLoad)
  {
    var operation = SceneManager.LoadSceneAsync(sceneToLoad.GetSceneIndex(), sceneToLoad.GetLoadSceneMode());
    operation.completed += asyncOperation => { Debug.Log(sceneToLoad.GetSceneName() + " Loaded"); };

    while (!operation.isDone)
    {
      yield return null;
    }

  }
  
  
}
