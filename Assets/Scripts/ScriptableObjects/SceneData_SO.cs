using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/SceneData",fileName = "new SceneData_SO")]
public class SceneData_SO : ScriptableObject
{
   [SerializeField]private string sceneName;
   [SerializeField]private int sceneIndex = -1;
   [SerializeField] private LoadSceneMode myLoadSceneMode = LoadSceneMode.Additive;
   public string GetSceneName()
   {
      if(string.IsNullOrEmpty(sceneName))
         Debug.LogError("Null Scene Name!!",this);
      
      return sceneName;
   }

   public int GetSceneIndex()
   {
      if(sceneIndex < 0)
         Debug.LogError("Null Scene Index!!",this);
      
      return sceneIndex;
   }

   public LoadSceneMode GetLoadSceneMode()
   {
      return myLoadSceneMode;
   }
}