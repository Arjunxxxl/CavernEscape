using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalShooting : MonoBehaviour {

	ParticleSystem boost;

	int isRight;
	Transform player;
	float speed_shoot = 3.5f;

	Vector3 dxn, dxn2;

	bool shoot;

	ColorManager cm;
	int colorindex;

	Color partile_color;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		dxn = new Vector3(1f, 0f, 0);
		dxn2 = new Vector3(-1f, 0f, 0);
		shoot = false;
	}

	// Use this for initialization
	void Start () {
		
		boost = transform.GetChild(0).GetComponent<ParticleSystem>();
		boost.Stop();

		cm = ColorManager.Instance;
		colorindex = cm.ColorIndex();

		if(transform.position.x <= 0)
		{
			isRight = 1;
		}
		else
		{
			isRight = -1;
		}

		//Debug.Log(isRight);
		if(colorindex == 0)
		{
			partile_color = new Color32(255, 50, 0, 255);
		}
		else if(colorindex == 1)
		{
			partile_color = new Color32(0, 200, 150, 255);
		}
		else if(colorindex == 2)
		{
			partile_color = new Color32(255, 130, 0, 255);
		}
		else if(colorindex == 3)
		{
			partile_color = new Color32(170, 240, 210, 255);
		}
		var main = boost.main;
		main.startColor = partile_color;
	}
	
	// Update is called once per frame
	void Update () {
		if(player.position.y >= transform.position.y)
		{		
			shoot = true;
			boost.Play();
		}

		if(shoot && isRight == 1)
		{
			transform.Translate(dxn * speed_shoot * Time.deltaTime, Space.World);
			//Debug.Log("shoot _right");
		}
		else if(shoot && isRight == -1)
		{
			transform.Translate(dxn2 * speed_shoot * Time.deltaTime, Space.World);
			//Debug.Log("shoot_left");
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player")
		{
			gameObject.SetActive(false);
		}
	}
	
}
