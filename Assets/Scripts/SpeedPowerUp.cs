using UnityEngine;
using System.Collections;

public class SpeedPowerUp : MonoBehaviour
{
    public float SpeedBoostFactor = 4;

    public void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Rigidbody>() != null)
        {
            var rigidBody = col.GetComponent<Rigidbody>();
            var vel = rigidBody.velocity;
            vel = vel * SpeedBoostFactor;

            rigidBody.velocity = vel;
        }
    }

}
