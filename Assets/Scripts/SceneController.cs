using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{
	void Start () 
	{
	
	}
	
	void Update () 
	{
	    if( Input.GetKeyDown( KeyCode.R ) )
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}
}

