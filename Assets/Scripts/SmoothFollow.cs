using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{

		// The target we are following
		[SerializeField]
		private Transform target;
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 10.0f;
		// the height we want the camera to be above the target
		[SerializeField]
		private float height = 8.0f;

		[SerializeField]
		private float rotationDamping;
		[SerializeField]
		private float heightDamping;

	    private float _minDistance = 8f;
	    private float _defaultHeight;

	    void Start()
	    {
	        _minDistance = _defaultHeight = height;
	    }

	    void Update()
	    {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 40f))
            {
                float hitDistance = Vector3.Distance(transform.position, hit.point);

                if (hitDistance < _minDistance)
                {
                    height++;
                }
                else if(hitDistance > _minDistance && height > _minDistance)
                {
                    height-=0.25f;
                }
            }
        }

		void LateUpdate()
		{
			// Early out if we don't have a target
			if (!target)
				return;

			// Calculate the current rotation angles
			var wantedRotationAngle = target.eulerAngles.y;
			var wantedHeight = target.position.y + height;

			var currentRotationAngle = transform.eulerAngles.y;
			var currentHeight = transform.position.y;

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);

			// Always look at the target
			transform.LookAt(target);
		}
	}
}