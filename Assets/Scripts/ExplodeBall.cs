using UnityEngine;
using System.Collections.Generic;

public class ExplodeBall : MonoBehaviour 
{
    public float collideExplodeForce = 0f;
    public float finishExplodeForce = 0f;

    public float collideRadius = 10f;
    public float finishRadius = 50f;

    public float finishTriggerVelocity = 8f;

    public LayerMask mask;

    public GameObject explodeFx;

    bool isShot = false;
    bool isFinished = false;

    Rigidbody rb;

	void Start () 
	{
        this.rb = this.GetComponent<Rigidbody>();
	}
	
	void Update () 
	{
	    if( !isShot )
        {
            if( this.rb.velocity.magnitude > 0.1f )
            {
                this.isShot = true;
            }
        }
        else
        {
            if (this.rb.velocity.magnitude < this.finishTriggerVelocity)
            {
                this.CheckDestroyables(this.finishExplodeForce, this.finishRadius);
                this.Explode(this.finishExplodeForce, this.finishRadius);
                EventManager.TriggerEvent("ball.finished");
                Instantiate(this.explodeFx, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        this.Explode(this.collideExplodeForce, this.collideRadius);
    }

    void CheckDestroyables(float force, float radius)
    {
        Collider[] cols = Physics.OverlapSphere(this.transform.position, radius);
        foreach (Collider col in cols)
        {
            if (col.gameObject != this.gameObject)
            {
                Destroyable des = col.gameObject.GetComponent<Destroyable>();
                if (des != null)
                {
                    des.TakeForce(force);
                }
            }
        }
    }

    public void Explode( float force, float radius )
    {
        Collider[] cols = Physics.OverlapSphere(this.transform.position, radius);
        foreach (Collider col in cols)
        {
            if (col.gameObject != this.gameObject )
            {
                if (this.mask == (this.mask | (1 << col.gameObject.layer)))
                {
                    Rigidbody rb = col.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.AddExplosionForce(force, this.transform.position, radius, 2f);
                    }
                }
            }
        }
    }
}

