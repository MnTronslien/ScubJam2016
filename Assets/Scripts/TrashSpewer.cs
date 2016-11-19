using UnityEngine;
using System.Collections.Generic;

public class TrashSpewer : MonoBehaviour
{
    public GameObject[] TrashItems;
    public int MaxSpewedItems = 100;

    public bool randomSpew = false;
    public Vector3 SpewForce = new Vector3(0, 1000, 0);

    private int currentSprewedItems = 0;
    private List<GameObject> itemsOnGround = new List<GameObject>();
    private bool shouldSpawnTrash = true;

    protected void Update()
    {
        if (!shouldSpawnTrash) return;
        if (currentSprewedItems > MaxSpewedItems)
        {
            Invoke("RemoveTrashFromWorld", 5.0f);
            shouldSpawnTrash = false;
        }

        var randomTrashItem = Random.Range(0, TrashItems.Length);
        var trashitem = (GameObject)Instantiate(TrashItems[randomTrashItem], transform.position, Quaternion.identity);

        itemsOnGround.Add(trashitem);

        var trashitemRigidBody = trashitem.GetComponent<Rigidbody>();
        if (randomSpew)
            trashitemRigidBody.AddForce(new Vector3(
                SpewForce.x * Random.Range(-10, 10), 
                SpewForce.y * Random.Range(0, 10), 
                SpewForce.z * Random.Range(-10, 10)));  
        else
            trashitemRigidBody.AddForce(SpewForce);

        currentSprewedItems++;
    }

    public void RemoveTrashFromWorld()
    {
        foreach(var item in itemsOnGround)
        {
            Destroy(item);
        }
    }

    public void Reset()
    {
        shouldSpawnTrash = true;
        currentSprewedItems = 0;
    }

}
