using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
	private Rigidbody playerRB;

	public bool gameOver = false;

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

	//Score
	public TextMeshProUGUI scoreText;
	private int score = 0;
	private int highScore = 0; //Will read high score that they have from save file

	//Seed Count
	public TextMeshProUGUI seedCountText;
	private int seedCount = 0; //Will read how many seeds they have from a save file

	void Start()
	{
		playerRB = GetComponent<Rigidbody>();
		seedCountText.text = "Seeds: " + seedCount;
		scoreText.text = "Score: " + score;
	}
	void Update()
	{
		Move();
		Jump();
		if (gameOver)
		{
			Debug.Log("Game Over");
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isOnGround = true;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("SeedCollectable"))
		{
			seedCount++;
			seedCountText.text = "Seeds: " + seedCount;
			Destroy(other.gameObject);
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
		Vector3 movement = speed * Time.deltaTime * new Vector3(Input.GetAxis("Horizontal"), 0, 0);
		transform.Translate(movement, Space.World);

		if (transform.position.x > 3.5f)
		{
			transform.position = new Vector3(3.5f, transform.position.y, transform.position.z);
		}
		if (transform.position.x < -3.5f)
		{
			transform.position = new Vector3(-3.5f, transform.position.y, transform.position.z);
		}

		//Moving Character Rotation
		if (movement != Vector3.zero)
		{
			Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
		}

		//Score
		if (score < (int)transform.position.y)
		{
			score = (int)transform.position.y;
			scoreText.text = "Score: " + score;
		}
		if (gameOver && highScore < score)
		{
			highScore = score;
		}
	}
}