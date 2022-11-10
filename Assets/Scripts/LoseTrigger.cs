using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
	private PlayerController playerControllerScript;
	public Rigidbody target;
	public Vector3 offset;

	void Start()
	{
		playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	private void Update()
	{
		if (target.velocity.y > 0.1)
		{
			transform.position = target.transform.position + offset;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			playerControllerScript.gameOver = true;
		}
		if (other.CompareTag("Ground") || other.CompareTag("SeedCollectable"))
		{
			Destroy(other.gameObject);
		}
	}
}
