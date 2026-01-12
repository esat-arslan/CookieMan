using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private Player player;
    
    [SerializeField] private GameObject startPanel;
    [SerializeField] private Button startGameButton;
    
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverScore;
    [SerializeField] private Button restartOnGameOverButton;
    
    [SerializeField] private GameObject gameWonPanel;
    [SerializeField] private TMP_Text gameWonScore;
    [SerializeField] private Button restartOnGameWonButton;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private List<Image> liveSprites;
    
    private void OnEnable()
    {
        game.OnScoreUpdated += HandleScoreUpdate;
        player.OnLivesChanged += HandleLivesChanged;
        GameEvents.OnGameOver += HandleGameOver;
        GameEvents.OnGameWon += HandleGameWon;
        GameEvents.OnRestart += HandleRestart;
        startGameButton.onClick.AddListener(StartButtonClicked);
        restartOnGameOverButton.onClick.AddListener(RestartGameOverButtonClicked);
        restartOnGameWonButton.onClick.AddListener(RestartGameWonButtonClicked);
    }

    private void OnDisable()
    {
        player.OnLivesChanged -= HandleLivesChanged;
        game.OnScoreUpdated -= HandleScoreUpdate;
        GameEvents.OnGameOver -= HandleGameOver;
        GameEvents.OnGameWon -= HandleGameWon;
        GameEvents.OnRestart -= HandleRestart;
        startGameButton.onClick.RemoveListener(StartButtonClicked);
        restartOnGameOverButton.onClick.RemoveListener(RestartGameOverButtonClicked);
        restartOnGameWonButton.onClick.RemoveListener(RestartGameWonButtonClicked);
    }

    private void Start()
    {
        startPanel.SetActive(true);
    }

    private void HandleGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    private void RestartGameOverButtonClicked()
    {
        GameEvents.RestartGame();
        gameOverPanel.SetActive(false);
    }

    private void HandleGameWon()
    {
        gameWonPanel.SetActive(true);
    }

    private void RestartGameWonButtonClicked()
    {
        GameEvents.RestartGame();
        gameWonPanel.SetActive(false);
    }

    private void StartButtonClicked()
    {
        GameEvents.StartGame();
        startPanel.SetActive(false);
    }

    private void HandleScoreUpdate(int score)
    {
        scoreText.text = "Score: " + score.ToString();
        gameOverScore.text = "Your Score: " + score.ToString();
        gameWonScore.text = "Your Score: " + score.ToString();
    }

    private void HandleRestart()
    {
        foreach (Image icon in liveSprites)
        {
            icon.enabled = true;
        }
        
        scoreText.text = "Score: " + 0;
        gameOverScore.text = "Your Score: " + 0;
        gameWonScore.text = "Your Score: " + 0;
    }

    private void HandleLivesChanged(int lives)
    {
        for (int i = 2; i < liveSprites.Count; i--)
        {
            if (i > lives-1) liveSprites[i].enabled = false;
        }
    }
}



















