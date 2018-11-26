using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

	public GameObject rocks, gradient, sky;

	public ParticleSystem dust, bg_particle1, bg_particle2;

	int color_index = 0;

	Color dust_color, bg1, bg2;

	public static ColorManager Instance;
	private void Awake() {
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		color_index = (int)Random.Range(0f, 3.9999f);
		Color_background();
		Color_particle();
		//Debug.Log(color_index);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int ColorIndex()
	{
		return color_index;
	}

	void Color_background()
	{
		if(color_index == 0)
		{
			rocks.GetComponent<SpriteRenderer>().color = new Color32(181, 12, 235, 255);
			gradient.GetComponent<SpriteRenderer>().color = new Color32(167, 13, 217, 255);
			sky.GetComponent<SpriteRenderer>().color = new Color32(215, 103, 251, 255);
		}
		else if(color_index == 1)
		{
			rocks.GetComponent<SpriteRenderer>().color = new Color32(221, 40, 40, 255);
			gradient.GetComponent<SpriteRenderer>().color = new Color32(217, 32, 32, 255);
			sky.GetComponent<SpriteRenderer>().color = new Color32(255, 113, 113, 255);
		}
		else if(color_index == 2)
		{
			rocks.GetComponent<SpriteRenderer>().color = new Color32(0, 221, 0, 255);
			gradient.GetComponent<SpriteRenderer>().color = new Color32(0, 241, 0, 255);
			sky.GetComponent<SpriteRenderer>().color = new Color32(146, 255, 146, 255);
		}
		else if(color_index == 3)
		{
			rocks.GetComponent<SpriteRenderer>().color = new Color32(218, 187, 0, 255);
			gradient.GetComponent<SpriteRenderer>().color = new Color32(255, 224, 35, 255);
			sky.GetComponent<SpriteRenderer>().color = new Color32(255, 241, 156, 255);
		}
	}

	void Color_particle()
	{
		if(color_index == 0)
		{	
			var main = dust.main;
			dust_color = new Color32(0, 25, 45, 255);
			main.startColor = dust_color;
			var main2 = dust.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().main;
			main2.startColor = dust_color;

			bg1 = new Color32(255, 136, 246, 255);
			var main3 = bg_particle1.main;
			main3.startColor = bg1;

			bg2 = new Color32(115, 0, 120, 255);
			var main4 = bg_particle2.main;
			main4.startColor = bg2;
		}
		else if(color_index == 1)
		{	
			var main = dust.main;
			dust_color = new Color32(10, 0, 30, 255);
			main.startColor = dust_color;
			var main2 = dust.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().main;
			main2.startColor = dust_color;

			bg1 = new Color32(255, 79, 97, 255);
			var main3 = bg_particle1.main;
			main3.startColor = bg1;

			bg2 = new Color32(120, 00, 0, 255);
			var main4 = bg_particle2.main;
			main4.startColor = bg2;
		}

		else if(color_index == 2)
		{	
			var main = dust.main;
			dust_color = new Color32(65, 0, 0, 255);
			main.startColor = dust_color;
			var main2 = dust.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().main;
			main2.startColor = dust_color;

			bg1 = new Color32(64, 255, 64, 255);
			var main3 = bg_particle1.main;
			main3.startColor = bg1;

			bg2 = new Color32(0, 140, 0, 255);
			var main4 = bg_particle2.main;
			main4.startColor = bg2;
		}
		else if(color_index == 3)
		{	
			var main = dust.main;
			dust_color = new Color32(120, 10, 45, 255);
			main.startColor = dust_color;
			var main2 = dust.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().main;
			main2.startColor = dust_color;

			bg1 = new Color32(255, 240, 155, 255);
			var main3 = bg_particle1.main;
			main3.startColor = bg1;

			bg2 = new Color32(140, 120, 0, 255);
			var main4 = bg_particle2.main;
			main4.startColor = bg2;
		}

	}

}



/*
0 purple
1 red
2 green
*/

