using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody playerRB;
	private bool isOnGround = true;
	private float? lastGroundedTime;
	private float? jumpButtonPressedTime;

	public float speed;
	public float jumpForce;
	public float fallMuliplier;
	public float rotationSpeed;
	public float jumpGracePeriod;

	void Start()
	{
		playerRB = GetComponent<Rigidbody>();
	}
	void Update()
	{
		//Movement
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed * Time.deltaTime;
		transform.Translate(movement, Space.World);

		//Jump Buffer
		if (isOnGround)
		{
			lastGroundedTime = Time.time;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			jumpButtonPressedTime = Time.time;
		}
		//Jump
		if (Time.time - lastGroundedTime <= jumpGracePeriod)
		{
			if (Time.time - jumpButtonPressedTime <= jumpGracePeriod)
			{
				playerRB.velocity += Vector3.up * jumpForce;
				isOnGround = false;
				jumpButtonPressedTime = null;
				lastGroundedTime = null;
			}
		}
		if (playerRB.velocity.y < 0)
		{
			playerRB.velocity += (fallMuliplier - 1) * Physics.gravity.y * Time.deltaTime * Vector3.up;
		}

		//When Moving Character Rotation
		if (movement != Vector3.zero)
		{
			Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isOnGround = true;
		}
	}
}
