using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
   [SerializeField] private SceneData_SO playLevel;
   [Header("Score Board")]
   [SerializeField] private TMP_Text highScoreText;
   [SerializeField] private TMP_Text lastScoreText;

   [Header("Character Select")]
   [SerializeField] private RenderFarmController myRenderFarmController;
   [SerializeField] private Button nextButton;
   [SerializeField] private Button lastButton;
   
   [Header("Navigation")]
   [SerializeField] private Button playButton;
   [SerializeField] private Button quitButton;
   
   
   private const string HIGH_SCORE = "High Score : ";
   private const string LAST_SCORE = "Last Score : ";
   private int activeCharacterIndex = -1;

   private void Awake()
   {
      if(nextButton == null)
         Debug.LogError("Next Button is Missing!! ",this);
      if(lastButton == null)
         Debug.LogError("Last Button is Missing!! ",this);
      if(playButton == null)
         Debug.LogError("Play Button is Missing!! ",this);
      if(quitButton == null)
         Debug.LogError("Quit Button is Missing!! ",this);
      
      nextButton.onClick.AddListener(OnNextClicked);
      lastButton.onClick.AddListener(OnLastClicked);
      playButton.onClick.AddListener(OnPlayClicked);
      quitButton.onClick.AddListener(OnQuitClicked);

      activeCharacterIndex = DataManager.instance.character;
      highScoreText.SetText( HIGH_SCORE + DataManager.instance.highScore);
      lastScoreText.SetText(LAST_SCORE+DataManager.instance.lastScore);

      myRenderFarmController.SetActiveTarget(DataManager.instance.GetCharacter());
   }

   private void OnDestroy()
   {
      nextButton.onClick.RemoveAllListeners();
      lastButton.onClick.RemoveAllListeners();
      playButton.onClick.RemoveAllListeners();
      quitButton.onClick.RemoveAllListeners();
      
   }


   private void OnQuitClicked()
   { 
      PlayerPrefs.Save(); 
     Application.Quit();
   }
   private void OnPlayClicked()
   {
      SceneManagement.instance.LoadScene(playLevel);
   }

   private void OnLastClicked()
   {
      activeCharacterIndex = DataManager.instance.SetCharacter(--activeCharacterIndex);
      
      myRenderFarmController.SetActiveTarget(DataManager.instance.GetCharacter());
     
   }


   private void OnNextClicked()
   {
      activeCharacterIndex = DataManager.instance.SetCharacter(++activeCharacterIndex);
      
      myRenderFarmController.SetActiveTarget(DataManager.instance.GetCharacter());
   }
}
