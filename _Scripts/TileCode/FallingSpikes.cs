using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikes : MonoBehaviour {

	Transform player;

	ParticleSystem boost;

	float trigger_height = 2.8f;
	float speed = 5f;

	ColorManager cm;
	int colorindex;

	Color partile_color;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		cm = ColorManager.Instance;
		colorindex = cm.ColorIndex();
		boost = transform.GetChild(0).GetComponent<ParticleSystem>();
		boost.Stop();

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
		if(player.position.y >= transform.position.y - trigger_height)
		{
			transform.Translate(Vector3.up * speed * Time.deltaTime);
			speed += Time.deltaTime;
			boost.Play();
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player")
		{
			gameObject.SetActive(false);
		}
	}

}
