using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUp : MonoBehaviour
{
    public PowerUpType Type;
    public float Duration = 10f;
    public float ScaleFactor = 1.33f;

    private Dictionary<GameObject, Vector3> _scaledObjects = new Dictionary<GameObject, Vector3>();

    public void SetPowerUpTypeAndDuration(PowerUpType type, float duration)
    {
        Type = type;
        Duration = duration;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Player")
            return;

        if (Type == PowerUpType.ScaleUp)
        {
            ScaleObject(col);
            Destroy(gameObject);
        }
    }

    private void ScaleObject(Collider col)
    {
        // Set scale to a bigger one
        var localScale = col.transform.localScale;
        col.transform.localScale = col.transform.localScale * ScaleFactor;
        // Revert the scale back after Time T.

        StartCoroutine(SetDefaultScale(col));
        _scaledObjects.Add(col.gameObject, localScale);
    }

    public IEnumerator SetDefaultScale(Collider col)
    {
        yield return new WaitForSeconds(Duration);
        var item = _scaledObjects[col.gameObject];
        col.transform.localScale = item;
        _scaledObjects.Remove(col.gameObject);
    }
}
