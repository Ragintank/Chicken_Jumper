using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody playerRB;

	//Move
	public float speed;
	public float rotationSpeed;

	//Jump
	private bool isOnGround = true;
	private float? lastGroundedTime;
	private float? jumpButtonPressedTime;
	public float jumpGracePeriod;
	public float fallMuliplier;
	public float jumpForce;

	void Start()
	{
		playerRB = GetComponent<Rigidbody>();
	}
	void Update()
	{
		Move();
		Jump();
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isOnGround = true;
		}
	}
	private void Jump()
	{
		//Jump
		if (Time.time - lastGroundedTime <= jumpGracePeriod)
		{
			if (Time.time - jumpButtonPressedTime <= jumpGracePeriod)
			{
				playerRB.velocity = new Vector3(playerRB.velocity.x, 0, 0);
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

		//Jump Buffer
		if (isOnGround)
		{
			lastGroundedTime = Time.time;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			jumpButtonPressedTime = Time.time;
		}

		//No Collider when platforms are above you
		if (playerRB.velocity.y > 0)
		{
			GetComponent<MeshCollider>().enabled = false;
		}
		else if (playerRB.velocity.y < 0)
		{
			GetComponent<MeshCollider>().enabled = true;
		}
	}
	private void Move()
	{
		//Movement
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed * Time.deltaTime;
		transform.Translate(movement, Space.World);

		//Moving Character Rotation
		if (movement != Vector3.zero)
		{
			Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
		}
	}
}
