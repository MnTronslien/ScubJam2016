using UnityEngine;
using System.Collections.Generic;

public class LaunchBall : MonoBehaviour {

    public float force = 0f;
    
    public Transform origin;
    public Rigidbody target;

    public string launchEvent = "launchBall";

    public static LaunchBall instance;

	void Awake ()
    {
        if( instance == null )
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
	}

	void Update ()
    {
	
	}

    public void Launch( Vector3 velocity )
    {
        this.target.AddForce(this.force * velocity);
    }
}
