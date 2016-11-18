using UnityEngine;
using System.Collections.Generic;

public class LaunchHandle : Draggable 
{
    [SerializeField]
    AnimationCurve feedbackCurve;

    public float maxDistance = 3f;

    public Color tenseColour = Color.red;

    Color originalColour;

    Vector3 dir;

    Material mat;
    AudioSource audioSource;

    float extent = 0f;

    private void Awake()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.mat = this.GetComponent<Renderer>().material;
        this.originalColour = this.mat.color;
    }

    private void Update()
    {
        if (this.isDragged)
        {
            Vector3 diff = LaunchBall.instance.target.gameObject.transform.position - this.transform.position;
            dir = diff.normalized;
            float dist = Mathf.Min(diff.magnitude, this.maxDistance);
            extent = dist / this.maxDistance;

            float t = this.feedbackCurve.Evaluate(this.extent);
            this.mat.color = Color.Lerp(this.originalColour, this.tenseColour, t);
            this.audioSource.volume = Mathf.Lerp(0f, 0.35f, t);
        
            if( !this.audioSource.isPlaying )
            {
                this.audioSource.Play();
            }
        }
        else
        {
            this.mat.color = this.originalColour;
            this.audioSource.Stop();
        }
    }

    protected override void Release()
    {
        LaunchBall.instance.Launch(dir * extent);
    }

}

