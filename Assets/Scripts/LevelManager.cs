using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SceneData_SO mainMenuScene;
    [SerializeField] private Platform platformPrefab;
    [SerializeField] private Platform activePlatform;
    [HideInInspector] public static LevelManager instance;
    private const string SCORE_TEXT = "Score : ";
    [SerializeField] private TMP_Text ScoreText;
    private int score = 0;
    
    
    
    
    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        updateScoreText();
        SpawnPlatform(3);
    }

    private void GeneratePlatforms(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Quaternion platformRotation = activePlatform.transform.rotation;
            int choice = Random.Range(0, 4);
            switch (choice)
            {
                case 0 :
                {
                    platformRotation *= Quaternion.Euler(90,0,0);
                    break;
                }
                case 1 :
                {
                    platformRotation *= Quaternion.Euler(-90,0,0);
                    break;
                }
                case 2 :
                {
                    platformRotation *= Quaternion.Euler(0,90,0);
                    break;
                }
                case 3 :
                {
                    platformRotation *= Quaternion.Euler(0,-90,0);
                    break;
                }
            }
            
            Platform nextPlatform = Instantiate(platformPrefab, activePlatform.GetSocketPosition(), platformRotation);
            activePlatform = nextPlatform;
        }
    }

    public void AddToScore(int passedIncrease)
    {
        score += passedIncrease;
        updateScoreText();
    }

    private void updateScoreText()
    {
        ScoreText.SetText(SCORE_TEXT+score);
    }

    public void GameOver()
    {
        DataManager.instance.ProcessScore(score);
        SceneManagement.instance.LoadScene(mainMenuScene);
    }

    public void SpawnPlatform(int amount)
    {
        GeneratePlatforms(amount);
    }
}
