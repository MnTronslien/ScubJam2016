using UnityEngine;
using System.Collections.Generic;

public class Draggable : MonoBehaviour 
{
    protected Vector3 screenPoint;
    protected Vector3 offset;

    protected Rigidbody rb;

    public string releaseEvent;

    protected bool isDragged = false;

    protected virtual void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint( this.transform.position );
        offset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        this.isDragged = true;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        this.rb.MovePosition(curPosition);
    }

    virtual protected void Release()
    {

    }

    void OnMouseUp()
    {
        if( this.isDragged )
        {
            this.isDragged = false;
            if( this.releaseEvent != "" )
            {
                EventManager.TriggerEvent(this.releaseEvent);
            }

            this.Release();
        }
    }
}

