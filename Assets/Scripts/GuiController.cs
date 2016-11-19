using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuiController : MonoBehaviour
{
    public GameObject GuiPanel;
    public GameObject HighScorePanel;

    public Text[] HighScoreTexts;
    public Text SpeedText;
    public Text ScoreText;

    private readonly int _initScoreValue = 0;
    private readonly Color _normalColorBlue = new Color(0f,68/255f,146/255f,255/255f);
    private readonly string _speedTxt = "Speed: ";
    private readonly string _scoreTxt = "Score: ";

    void Start()
    {
        InitHighScore();
    }

    public void UpdateCurrentScore(int score)
    {
        ScoreText.text = _scoreTxt + score;
    }

    public void UpdateCurrentSpeed(int speed)
    {
        SpeedText.text = _speedTxt + speed;
    }

    public void GameEnd(int playerScore)
    {
        int currentScore = playerScore;
        int[] scores = new int[HighScoreTexts.Length];
        bool newHighScore = false;
        int newHighScorePos = 10;

        for (int i = 0; i < scores.Length; i++)
        {
            string index = GetPosIndex(i + 1);
            scores[i] = PlayerPrefs.GetInt(index);

            if (scores[i] < currentScore && !newHighScore)
            {
                newHighScore = true;
                newHighScorePos = i;
            }
        }

        if (newHighScore)
        {
            scores = AddNewScore(scores, currentScore, newHighScorePos);
            UpdateHighScoreBoard(scores, newHighScorePos);
        }
    }

    private void UpdateHighScoreBoard(int[] scores, int newHighScorePos)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            string index = GetPosIndex(i + 1);
            HighScoreTexts[i].text = index + ": " + PlayerPrefs.GetInt(index);
            HighScoreTexts[i].GetComponent<Text>().color = (i == newHighScorePos) ? Color.green : _normalColorBlue;
        }
    }
    
    private int[] AddNewScore(int[] currentHighScores, int newScore, int pos)
    {
        int[] newScores = currentHighScores;

        for (int i = currentHighScores.Length - 1; i >= pos; i--)
        {
            // Current key
            string index = GetPosIndex(i + 1);

            // Move highscore at next pos to current pos
            if (i == pos)
            {
                PlayerPrefs.SetInt(index, newScore);
                newScores[i] = newScore;
                break;
            }

            PlayerPrefs.SetInt(index, currentHighScores[i - 1]);
            newScores[i] = newScores[i - 1];
        }
        return newScores;
    }

    private string GetPosIndex(int i)
    {
        return (i < 10) ? ("0" + i) : "" + i;
    }

    public void ChangePanel(GameState newState)
    {
        if (newState == GameState.Running)
        {
            SetActivePanel(true, false);  
        }
        else if(newState == GameState.Ended)
        {
            SetActivePanel(false, true);
        }
    }

    private void SetActivePanel(bool gui, bool highScore)
    {
        GuiPanel.SetActive(gui);
        HighScorePanel.SetActive(highScore);
    }

    private void InitHighScore()
    {
        for (int i = 0; i < 10; i++)
        {
            string index = GetPosIndex(i + 1);
            HighScoreTexts[i].GetComponent<Text>().color = _normalColorBlue;

            if (!PlayerPrefs.HasKey(index))
            {
                PlayerPrefs.SetInt(index, _initScoreValue);
                HighScoreTexts[i].text = index + ": " + _initScoreValue;
            }
            else
            {

                HighScoreTexts[i].text = index + ": " + PlayerPrefs.GetInt(index);
            }
        }
    }
}
