using UnityEngine;
using System.Collections.Generic;

public class Debris : MonoBehaviour 
{
    Vector3 startPosition;

    public float maxScore = 100f;

    float bestDistance = 0f;

    public float score { get { return Mathf.Min( this.bestDistance / 1000, this.maxScore ); } }

    static List<Debris> m_instances;

    public static List<Debris> instances
    {
        get
        {
            if (m_instances == null)
            {
                m_instances = new List<Debris>();
            }
            return m_instances;
        }
    }

	void Start () 
	{
        this.startPosition = this.transform.position;
        instances.Add(this);
	}
	
	void Update () 
	{
        float newDist = (this.transform.position - this.startPosition).sqrMagnitude;
        this.bestDistance = Mathf.Max(this.bestDistance, newDist);
	}
}

