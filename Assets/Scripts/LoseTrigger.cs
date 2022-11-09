using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
	public Rigidbody playerRB;
	private static float yOffset = -5;
	private Vector3 offset = new Vector3(0, yOffset, 0);

	void Update()
	{
		if (playerRB.velocity.y > 0)
		{
			transform.position = playerRB.position + offset;
		}
		DestroyPlatform();
	}
	private void OnTriggerEnter(Collider other)
	{
		Destroy(other.gameObject);
	}
	private void DestroyPlatform()
	{
		if (GameObject.FindGameObjectWithTag("Ground").transform.position.y < playerRB.position.y + yOffset)
		{
			Destroy(GameObject.FindGameObjectWithTag("Ground"));
		}
	}
}
