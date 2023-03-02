using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
   public static DataManager instance;
   [SerializeField] private CharacterType_SO characterTypes; 
   private const string PPK_LAST_SCORE = "LAST_SCORE";
   private const string PPK_HIGH_SCORE = "HIGH_SCORE";
   private const string PPK_CHARACTER = "CHARACTER";
   public int highScore
   {
      get;
      private set;
   }
   
   public int lastScore
   {
      get;
      private set;
   }

   public int character
   {
      get;
      private set;
   }
   
   private void Awake()
   {
      if(instance)
         Destroy(this.gameObject);
      
      instance = this;

      highScore = PlayerPrefs.GetInt(PPK_HIGH_SCORE);
      lastScore = PlayerPrefs.GetInt(PPK_LAST_SCORE);
      character = PlayerPrefs.GetInt(PPK_CHARACTER);
      DontDestroyOnLoad(gameObject);
   }

   private void SetHighScore(int score)
   {
      PlayerPrefs.SetInt(PPK_HIGH_SCORE,score);
      highScore = score;
   }
   
   private void SetLastScore(int score)
   {
      PlayerPrefs.SetInt(PPK_LAST_SCORE,score);
      lastScore = score;
   }

   public int SetCharacter(int passedIndex)
   {
      if (passedIndex == characterTypes.Count)
         passedIndex = 0;
      
      if (passedIndex < 0)
         passedIndex = characterTypes.Count-1;
      
      character = passedIndex;
      PlayerPrefs.SetInt(PPK_CHARACTER,passedIndex);
      PlayerPrefs.Save();
      return character;
   }

   public GameObject GetCharacter()
   {
      return characterTypes.GetCharacter(character);
   }
   
   public void ProcessScore(int passedScore)
   {
      if(passedScore > highScore)
         SetHighScore(passedScore);
      
      SetLastScore(passedScore);
      PlayerPrefs.Save();
   }
   
   
}
