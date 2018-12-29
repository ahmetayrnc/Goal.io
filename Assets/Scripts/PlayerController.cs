using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[RequireComponent(typeof(MovementMotor))]
public class PlayerController : MonoBehaviour
{
	public VirtualJoystick joystickInput;
	
	private MovementMotor motor;
	private float xMovement;
	private float zMovement;
	
	void Start()
	{
		motor = GetComponent<MovementMotor>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		xMovement = joystickInput.Horizontal();
		zMovement = joystickInput.Vertical();
	}

	void FixedUpdate()
	{
		if (GameManager.Instance.gameIsOver)
		{
			return;
		}
		
		motor.Move(xMovement, zMovement);
	}
}
