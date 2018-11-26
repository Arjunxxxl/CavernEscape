using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTile : MonoBehaviour {

	public float maxdisplacement;
	float xmin, xmax;
	Vector3 a,b;

	float speed = 1f;

	bool isRight = true;

	// Use this for initialization
	void Start () {
		xmin = transform.position.x - maxdisplacement;
		xmax = transform.position.x + maxdisplacement;

		isRight = true;

		a = new Vector3(xmin, transform.position.y, transform.position.z);
		b = new Vector3(xmax, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position == b)
		{
			isRight = false;
		}
		else if(transform.position == a)
		{

			isRight = true;
		}

		StartCoroutine(movetile());
	}

	IEnumerator movetile()
	{	
		if(isRight)
		{
			transform.position = Vector3.MoveTowards(transform.position, b, Time.deltaTime * speed);
		}
		else if(!isRight)
		{
			transform.position = Vector3.MoveTowards(transform.position, a, Time.deltaTime * speed);
		}

		yield return null;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player")
		{
			other.gameObject.transform.parent = this.gameObject.transform;
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			other.gameObject.transform.parent = this.gameObject.transform;
		}
	}

	private void OnCollisionExit2D(Collision2D other) 
	{
		if(other.gameObject.tag == "Player")
		{
			other.gameObject.transform.parent = null;
		}	
	}

}
