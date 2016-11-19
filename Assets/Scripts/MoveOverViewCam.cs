using UnityEngine;
using System.Collections;

public enum CamMovement
{
    Auto,
    Manual
}

public class MoveOverViewCam : MonoBehaviour
{
    public Vector3 LookAtPosition = Vector3.zero;
    public Transform[] Nodes;
    public float MovementSpeed = 5f;

    private Vector3 _dir = Vector3.left;
    private CamMovement _currentMovement = CamMovement.Auto;
    private int _currentIndex;
    private readonly float _left = -1f;
    private readonly int _startIndex = 0;

    void Start()
    {
        _currentIndex = _startIndex;
    }

	void Update ()
	{
	    if (Nodes == null || Nodes.Length <= 0)
	        return;

	    if (_currentMovement == CamMovement.Auto && _dir == Vector3.zero)
	    {
            SetMoveDir(_left);
        }

	    if (transform.position != Nodes[_currentIndex].position)
	    {
            MoveCam();
        }

        RotateCamera();
	    HandleIndex();
	}

    public void SetMovementType(CamMovement movement)
    {
        _currentMovement = movement;
    }

    public void SetMoveDir(float dirX)
    {
        _dir = new Vector3(dirX, 0, 0);
    }

    private void MoveCam()
    {
        transform.position = Vector3.MoveTowards(transform.position, Nodes[_currentIndex].position, 1f * MovementSpeed * Time.deltaTime);
        // * MovementSpeed * Time.deltaTime;
    }

    private void RotateCamera()
    {
        transform.LookAt(LookAtPosition);
    }

    private void HandleIndex()
    {
        if (transform.position == Nodes[_currentIndex].position)
        {
            if (_currentIndex == Nodes.Length - 1)
            {
                _currentIndex = 0;
            }
            else
            {
                _currentIndex++;
            }
        }
    }
}
