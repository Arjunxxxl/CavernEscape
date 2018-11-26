using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropingTile : MonoBehaviour {

	bool shaking = false;
	float dropTime = 3f;
	float currentTime = 0f;

	float speed = 5f;

	float rotationSpeed = 5f;

	float maxAngle = 1f, minAngle = -1f;
	Quaternion a,b;

	bool isRight = true;

	// Use this for initialization
	void Start () {
		a = Quaternion.Euler(0, 0, maxAngle);
		b = Quaternion.Euler(0, 0, minAngle); 
		isRight = true;
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log(currentTime);

		if(transform.rotation.eulerAngles.z == maxAngle)
		{
			isRight = true;
		}
		else if(transform.rotation.eulerAngles.z == 360f + minAngle)
		{
			isRight = false;
		}

		if(shaking)
		{
			currentTime += Time.deltaTime;
			StartCoroutine(shakeTile());
		}
		else if(currentTime < dropTime)
		{	
			currentTime = 0f;
		}

		if(currentTime >= dropTime)
		{
			DropTile();
		}

	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player")
		{
			shaking = true;
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.tag == "Player")
		{
			shaking = false;
		}
	}

	private void OnCollisionStay2D(Collision2D other) {
		if(other.gameObject.tag == "Player")
		{
			shaking = true;
		}	
	}

	void DropTile()
	{
		transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
	}

	IEnumerator shakeTile()
	{	
		if(isRight)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, b, Time.deltaTime * rotationSpeed);
		}
		else if(!isRight)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, a, Time.deltaTime * rotationSpeed);
		}


		yield return null;
	}

}
