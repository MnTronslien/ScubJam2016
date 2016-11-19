﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GuiController))]
public class GameController : MonoBehaviour
{
    public GameState CurrentState;
    public int CurrentScore;

    private GuiController _guiController;
    private int _lastScore;

    private readonly int _startScore = 0;

    void Awake()
    {
        CurrentState = GameState.Paused;
        _guiController = GetComponent<GuiController>();
        _lastScore = CurrentScore = _startScore;
    }
	
	void Update ()
	{
	    if (CurrentState == GameState.Running)
	    {
            HandleScore();
        }
        else if (CurrentState == GameState.Quit)
	    {
	        Application.Quit();
	    }
    }

    public void StartGame()
    {
        ChangeGameState(GameState.Running);
    }

    public void QuitGame()
    {
        ChangeGameState(GameState.Quit);
    }

    public void AddScore(int points)
    {
        CurrentScore += points;
    }

    private void ChangeGameState(GameState newState)
    {
        CurrentState = newState;
    }

    private void HandleScore()
    {
        if (CurrentScore != _lastScore && CurrentScore > 0)
            UpdateScore();
    }

    private void UpdateScore()
    {
        _lastScore = CurrentScore;
        _guiController.UpdateCurrentScore(_lastScore);
    }
}
