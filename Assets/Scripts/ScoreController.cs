using UnityEngine;

using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text score;

	void Start () 
	{
        Debris.instances.Clear();   
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

