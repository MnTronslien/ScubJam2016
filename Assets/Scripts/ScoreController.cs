using UnityEngine;

using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text score;
    GameController game;

    public float delayBeforeEndRound = 10f;

	void Start () 
	{
        this.game = FindObjectOfType<GameController>();

        Debris.instances.Clear();

        EventManager.StartListening("ball.finished", BallFinishedHandler);

        this.game.StartGame();
	}
	

    public void BallFinishedHandler()
    {
        Invoke("EndRound", this.delayBeforeEndRound);
    }

    void EndRound()
    {
        Debug.Log("Game ended");
        this.game.EndGame();
    }

	void Update () 
	{
        float total = 0f;
        foreach(Debris deb in Debris.instances )
        {
            total += deb.score;
        }
        this.score.text = total.ToString("n0");
	}
}

