using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScalePowerUP : MonoBehaviour
{
    public float PowerupTime = 10;
    public float ScaleFactor = 1.33f;

    private Dictionary<GameObject, Vector3> scaledObjects = new Dictionary<GameObject, Vector3>();

    public void OnTriggerEnter(Collider col)
    {
        // Set scale to a bigger one
        var localScale = col.transform.localScale;
        col.transform.localScale = col.transform.localScale * ScaleFactor;
        // Revert the scale back after Time T.

        StartCoroutine(SetDefaultScale(col,PowerupTime));
        scaledObjects.Add(col.gameObject, localScale);

    }

    public IEnumerator SetDefaultScale(Collider col, float powerUpTime)
    {
        yield return new WaitForSeconds(powerUpTime);
        var item = scaledObjects[col.gameObject];
        col.transform.localScale = item;
        scaledObjects.Remove(col.gameObject);
    }


	
}
