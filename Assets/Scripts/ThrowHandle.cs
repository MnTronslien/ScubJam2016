using UnityEngine;
using System.Collections.Generic;

public class ThrowHandle : Draggable 
{
    Vector3 lastPos;

    public float forceScale = 1f;

    public float velocitySmoothing = 0.1f;

    Vector3 velocity;

	protected override void Start () 
	{
        this.lastPos = this.transform.position;
        base.Start();
    }
	
	void Update () 
	{
        Vector3 nowPos = this.transform.position;

        Vector3 diff = nowPos - this.lastPos;

        float speed = diff.magnitude / Time.deltaTime;
        
        this.lastPos = this.transform.position;
        this.velocity = diff.normalized * speed * this.forceScale;
	}

    protected override void Release()
    {
        this.rb.velocity = this.velocity * this.forceScale;
    }
}

