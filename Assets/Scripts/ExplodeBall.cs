using UnityEngine;
using System.Collections.Generic;

public class ExplodeBall : MonoBehaviour 
{
    public float explodeForce = 0f;

    public float radius = 10f;

    public LayerMask mask;

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] cols = Physics.OverlapSphere(this.transform.position, this.radius);
        foreach(Collider col in cols)
        {
            if( col.gameObject != this.gameObject && col.gameObject.layer != this.mask )
            {
                Rigidbody rb = col.GetComponent<Rigidbody>();

                if( rb != null )
                {
                    rb.AddExplosionForce(this.explodeForce, this.transform.position, this.radius, 2f);
                }
            }
        }
    }
}

