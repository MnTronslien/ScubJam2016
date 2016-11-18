using UnityEngine;
using System.Collections.Generic;

public class PinField : MonoBehaviour
{
    public GameObject pinObject;

    public float rotationAngle;

    public List<GameObject> pins = new List<GameObject>();

    protected void Start()
    {
        Setup();
    }

    public void Setup()
    {
        foreach(var pin in pins)
        {
            Destroy(pin);
            pins.Remove(pin);
        }

        for(int i = 1; i <= 4; i++)
        {
            for(int j = i; j <= 4; j++)
            {
              var obj = (GameObject)Instantiate(pinObject, new Vector3(
                                1.12f * i + transform.position.x, 0 + transform.position.y, 
                                1.12f * j + transform.position.z), Quaternion.identity, gameObject.transform );

                pins.Add(obj);
            }
        }

        transform.Rotate(new Vector3(0, rotationAngle, 0));
    }

}
