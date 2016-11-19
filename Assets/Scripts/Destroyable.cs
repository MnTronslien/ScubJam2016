using UnityEngine;
using System.Collections.Generic;

public class Destroyable : MonoBehaviour 
{
    public List<GameObject> debrisPrototypes;

    public List<GameObject> debris = new List<GameObject>();

    Collider col;

    bool broken = false;

    public float breakForce = 10;

	void Start () 
	{
        this.col = this.GetComponent<Collider>();

        this.Pop();
	}
	
	void Update () 
	{
	
	}

    private void OnCollisionEnter(Collision collision)
    {
        if( collision.rigidbody == null || broken )
        {
            return;
        }

        float impactForce = (collision.impulse.magnitude * collision.rigidbody.mass);

        Debug.Log("Impact Force: " + impactForce);

        if( impactForce >= this.breakForce )
        {
            this.Break();
        }
    }

    public void TakeForce( float force )
    {
        if( force >= this.breakForce )
        {
            this.Break();
        }
    }

    public void Break()
    {
        foreach( GameObject d in this.debris )
        {
            d.SetActive(true);
        }

        this.broken = true;

        Destroy(this.gameObject);
    }

    private void OnMouseDown()
    {
        //this.Pop();
    }

    public void Pop()
    {
        Debug.Log("Pop");

        Vector3 debrisDim = this.debrisPrototypes[0].GetComponent<Collider>().bounds.size;
        Vector3 selfDim = this.col.bounds.size;

        Debug.Log(debrisDim);
        Debug.Log(selfDim);

        float xStep = debrisDim.x;
        float yStep = debrisDim.y;
        float zStep = debrisDim.z;

        Debug.LogFormat("X:{0}, Y:{1}, Z:{2}", xStep, yStep, zStep);

        for (float x = 0f; x < selfDim.x; x += xStep)
        {
            for (float y = 0f; y < selfDim.y; y += yStep)
            {
                for (float z = 0f; z < selfDim.z; z += zStep)
                {
                    GameObject go = Instantiate(this.debrisPrototypes[0], this.transform.position, Quaternion.identity) as GameObject;
                    go.transform.position = this.transform.position + new Vector3(x, y, z) - this.col.bounds.extents;
                    this.debris.Add(go);
                    go.SetActive(false);
                }
            }
        }
    }

    float GetAverageDimension( Vector3 size )
    {
        return (size.x + size.y + size.z) / 3f;
    }
}

