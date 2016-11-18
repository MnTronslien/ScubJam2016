using UnityEngine;
using System.Collections;

public class PinField : MonoBehaviour
{
    public GameObject pinObject;

    public float rotationAngle;

    protected void Start()
    {
        Setup();
    }

    public void Setup()
    {
        for(int i = 1; i <= 4; i++)
        {
            for(int j = i; j <= 4; j++)
            {
                Instantiate(pinObject, new Vector3(
                                1.12f * i + transform.position.x, 0 + transform.position.y, 
                                1.12f * j + transform.position.z), Quaternion.identity, gameObject.transform );
            }
        }

        transform.Rotate(new Vector3(0, rotationAngle, 0));
    }

}
