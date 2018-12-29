using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;
	public Vector3 rotationOffset;

	public bool rotation;
	
	public float smoothness;
	public float rotationSmoothness;

	private Vector3 velocity = Vector3.zero;
	
	void FixedUpdate()
	{
		Vector3 targetPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothness);
		transform.position = smoothedPosition;

		if (rotation)
		{
			Vector3 yRotation = Vector3.up * target.rotation.eulerAngles.y;
			Quaternion targetRotation = Quaternion.Euler(yRotation + rotationOffset);
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSmoothness);
		}
	}
}
