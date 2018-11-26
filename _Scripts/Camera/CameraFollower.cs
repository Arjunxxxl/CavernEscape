using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

	public Transform player;
	public Vector3 offset;
	Vector3 offset_temp, offset_p;

	public bool grounded;

	UserInput ui;
	public float bottomMargine;
	float margineOffset = 8f;

	public float fallingTime = 0;
	float maxTime = 3f;

	public float camSpeed_UP = 5f;
	public float camSpeed_Down = 2f;

	bool isDead = false;

	Vector3 newPos;

	DeathManager dm;
	bool spike_dead;

	bool danger;
	IndicateDanger id;


	// Use this for initialization
	void Start () {

		danger = false;
		id = IndicateDanger.Instance;

		if(!player)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		ui = UserInput.Insatnce;
		grounded = ui.isGrounded();
		Dead();
		isDead = false;
		fallingTime = 0;
		maxTime = 3f;

		offset_p = new Vector3(-2.2f, -1.75f, 10f);
		offset_temp = new Vector3(-2.2f, -3.5f, 10f);
	}
	
	// Update is called once per frame
	void Update () 
	{	
		Dead();

		if(isDead || spike_dead)
		{
			return;
		}

		if(transform.position.y <= -6.5f && player.position.y <= -9.5f)
		{
			offset = offset_temp;
			//newPos = player.position - offset;
			transform.position = new Vector3(transform.position.x, -6.5f, transform.position.z);
		}
		else
		{
			offset = offset_p;

			newPos = player.position - offset;
			newPos.x = 0f;
			if(newPos.y >= transform.position.y)
			{
				transform.position = Vector3.Lerp(transform.position, newPos, camSpeed_UP * Time.deltaTime);	
			}
			else
			{
				transform.position = Vector3.Lerp(transform.position, newPos, camSpeed_Down * Time.deltaTime);
			}
		}
	}

	void Dead()
	{	
		dm = DeathManager.Instance;
		spike_dead = dm.DeathStatus();

		grounded = ui.isGrounded();
		if(grounded && fallingTime <= maxTime)
		{
			bottomMargine = player.position.y - margineOffset;
			fallingTime = 0;
		}
		else
		{
			fallingTime += Time.deltaTime;
		}

		if(player.position.y <= bottomMargine)
		{	
			isDead = true;
		}

		if(fallingTime >= 2f)
		{
			danger = true;
		}
		else
		{
			danger = false;
		}

		id.isDanger(danger);

		 dm.get_DeathStatus(isDead);
	}


}
