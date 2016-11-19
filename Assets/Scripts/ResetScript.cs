using UnityEngine;
using System.Collections;
using NewtonVR;
public class ResetScript : MonoBehaviour
{
    public TrashSpewer trashSpewer;
    public PinField pinField;

    public bool manualTest;

    public void Start()
    {
    }

    public void Update()
    {
        if (GetComponent<NVRHand>().UseButtonDown || manualTest)
        {
            pinField.Setup();
            trashSpewer.Reset();
            manualTest = false;
        }
    }


}
