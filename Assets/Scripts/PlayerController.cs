using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody playerRB;
	private bool isOnGround = true;

	public float speed;
	public float jumpForce;
	public float fallMuliplier;

	void Start()
	{
		playerRB = GetComponent<Rigidbody>();
	}
	void Update()
	{
		float movementX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		transform.position += new Vector3(movementX, 0, 0);
		
		if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
		{
			playerRB.velocity += Vector3.up * jumpForce;
			isOnGround = false;
		}
		if (playerRB.velocity.y < 0)
		{
			playerRB.velocity += (fallMuliplier - 1) * Physics.gravity.y * Time.deltaTime * Vector3.up;
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
