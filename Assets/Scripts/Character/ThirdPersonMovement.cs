using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

	private Vector3 movement;
	
	public float moveSpeed = 2.0f;
	public float rotSpeed = 15.0f;
	public float jumpSpeed = 9.0f;
	public float gravity = -9.8f;
	public float terminalVelocity = -10.0f;
	public float minFall = -1.5f; //some downward force is required in case of running up and down on uneven terrain
	

	[SerializeField] private float leapCooldown = 2f;
	private float nextLeapTime = 0f;

	private float vertSpeed;

	private CharacterController charController;

    private void Start()
    {
        charController = GetComponent<CharacterController>();

		vertSpeed = minFall;
    }

    private void Update()
    {
		MovementCalculations();

		LeapCalculations();

		movement *= Time.deltaTime;
		charController.Move(movement);
    }

	private void MovementCalculations()
    {
		movement = Vector3.zero; // it's important to have a default value in case there isn't any input

		float horInput = Input.GetAxis("Horizontal");
		float vertInput = Input.GetAxis("Vertical");

		bool movingInput = horInput != 0 || vertInput != 0;
		if (movingInput)
		{
			Vector3 right = cameraTransform.right;
			Vector3 forward = Vector3.Cross(right, Vector3.up);
			movement = (right * horInput) + (forward * vertInput);
			movement *= moveSpeed;
			movement = Vector3.ClampMagnitude(movement, moveSpeed); // limiting diagonal movement to one speed

			Quaternion direction = Quaternion.LookRotation(movement);
			transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
		}
	}
	private void LeapCalculations()
    {
		if (charController.isGrounded)
		{
			if (Input.GetButtonDown("Jump") && Time.time >= nextLeapTime)
			{
				vertSpeed = jumpSpeed;
				moveSpeed = 10f;
				nextLeapTime = Time.time + leapCooldown;
				EventSystem.InvokeOnLeapCooldown(leapCooldown);
			}
			else
			{
				vertSpeed = minFall;
				moveSpeed = 2f;
			}
		}
		else
		{
			vertSpeed += gravity * 5 * Time.deltaTime;
			if (vertSpeed < terminalVelocity)
			{
				vertSpeed = terminalVelocity;
			}
		}
		movement.y = vertSpeed;
	}
}
