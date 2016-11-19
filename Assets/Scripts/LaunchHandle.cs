using UnityEngine;
using System.Collections.Generic;

public class LaunchHandle : MonoBehaviour
{
    [SerializeField]
    AnimationCurve feedbackCurve;

    public float maxDistance = 3f;

    public Color tenseColour = Color.red;

    [SerializeField] //so it is private but visible in unity
    private float contractionSpeed = 100;

    private bool isDragged = false;

    private Vector3 startPosOfBAll;

    Color originalColour;

    Vector3 dir;

    Material mat;
    AudioSource audioSource;

    private bool hasBeenFired = false;

    float extent = 0f;
    float extentAtRelease;

    private void Awake()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.mat = this.GetComponent<Renderer>().material;
        this.originalColour = this.mat.color;


        startPosOfBAll = LaunchBall.instance.target.gameObject.transform.position;
    }

    private void Update()
    {
        float t = this.feedbackCurve.Evaluate(this.extent);
        if (this.isDragged)
        {

            
            // Calculating force of the launch
            Vector3 diff = LaunchBall.instance.target.gameObject.transform.position - this.transform.position;
            dir = diff.normalized;
            float dist = Mathf.Min(diff.magnitude, this.maxDistance);
            extent = dist / this.maxDistance;

            //Cosmetics
            this.mat.color = Color.Lerp(this.originalColour, this.tenseColour, t);



            // Audio
            this.audioSource.volume = Mathf.Lerp(0f, 0.35f, t);
            this.audioSource.pitch = Mathf.Lerp(0f, 2f, t);
            if ( !this.audioSource.isPlaying )
            {
                this.audioSource.Play();
            }
        }
        else
        {
            //resetting cosmetics
            this.audioSource.Stop();

            if (hasBeenFired) { this.transform.position = Vector3.MoveTowards(transform.position, startPosOfBAll, Time.deltaTime * contractionSpeed ); } 
        }
        //Line Renderer cosmetics
        this.GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
        this.GetComponent<LineRenderer>().SetPosition(1, startPosOfBAll);
        this.GetComponent<LineRenderer>().SetWidth(0.5f, Mathf.Lerp(0.5f, 4.5f, t));
    }




    public void FlipIsDragged()
    {
        isDragged = !isDragged;
    }

    // There needs to be a delay between the sling is realeased and the ball is fired;
    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player" && hasBeenFired)
        {
            LaunchBall.instance.Launch(dir * extentAtRelease);
        }
    }
        

    public void Release()
    {
        extentAtRelease = extent;
        hasBeenFired = true;
    }
}

