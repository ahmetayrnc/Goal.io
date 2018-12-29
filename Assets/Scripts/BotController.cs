using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementMotor))]
public class BotController : MonoBehaviour {

	private MovementMotor motor;
	private float xMovement;
	private float zMovement;

	private Transform target;
	private Vector3 velocity = Vector3.zero;
	
	void Start()
	{
		motor = GetComponent<MovementMotor>();
		target = BotInformation.Instance.GetClosestBallOutside(transform.position);
		StartCoroutine(CalculateMovement());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.Instance.gameIsOver)
		{
			return;
		}
		
		if (target == null)
		{
			target = BotInformation.Instance.GetClosestBallOutside(transform.position);
		}
		
		Vector3 targetDir = (target.position - transform.position).normalized;
		Vector3 dir = Vector3.SmoothDamp(transform.forward, targetDir, ref velocity, 0.1f);
		
		xMovement = dir.x;
		zMovement = dir.z;
	}
	
	void FixedUpdate()
	{
		if (GameManager.Instance.gameIsOver)
		{
			return;
		}
		
		motor.Move(xMovement, zMovement);
	}

	IEnumerator CalculateMovement()
	{
		float i = 0;
		while (gameObject.activeSelf)
		{
			yield return new WaitForSeconds(0.75f);
			target = BotInformation.Instance.GetClosestBallOutside(transform.position);
			i++;

			if (i >= 2f)
			{
				target = BotInformation.Instance.GetRandomBallOutside();
				i = 0;
			}
		}
	}
}
