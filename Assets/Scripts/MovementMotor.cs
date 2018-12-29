using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementMotor : MonoBehaviour {

	public float speed = 7.5f;
	public float rotationSpeed = 12f;
	public float increaseInSpeed;
	private Rigidbody rb;

	private Vector3 dir;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		speed = GameManager.Instance.startingPlayerSpeed;
		increaseInSpeed = GameManager.Instance.increaseInSpeed;
	}

	public void Move(float xMovement, float zMovement)
	{
		dir = Vector3.zero;
		dir.x = xMovement;
		dir.z = zMovement;
		dir = dir.normalized;
		//dir = new Vector3(xMovement, 0, zMovement).normalized;
		
		if (dir.Equals(Vector3.zero))
		{
			dir = transform.forward;
		}
		
		//Vector3 targetPosition = rb.position + transform.forward * speed * Time.fixedDeltaTime;
		Vector3 targetPosition = rb.position + dir * speed * Time.fixedDeltaTime;
		rb.MovePosition(Vector3.Slerp(rb.position, targetPosition, 50f * Time.fixedDeltaTime));
		
		Quaternion toRotation = Quaternion.LookRotation(dir, transform.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);

	}

	public void IncreaseSpeed()
	{
		speed += increaseInSpeed;
		transform.DOScale(transform.localScale + Vector3.one * 0.2f, 0.75f).SetEase(Ease.OutElastic);
		transform.DOMoveY(transform.position.y + 0.1f, 0.75f).SetEase(Ease.OutElastic);
	}
	
	void LateUpdate()
	{
		if (GameManager.Instance.gameIsOver)
		{
			return;
		}
		
		Vector3 rot = transform.localEulerAngles;
		rot.z = 0;
		rot.x = 0;
		transform.localEulerAngles = rot;
		rb.velocity = Vector3.zero + Physics.gravity;
	}
}
