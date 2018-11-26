using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public AudioClip death, jump, land;
	public AudioSource playerAudio;

	bool jump_bool, dead_bool;
	bool death_safty, jump_safty;
	UserInput ui;
	DeathManager dm;

	// Use this for initialization
	void Start () {
		ui = UserInput.Insatnce;
		dm = DeathManager.Instance;
		jump_bool = ui.Pass_choosed();
		dead_bool = dm.DeathStatus();
		death_safty = false;
		jump_safty = false;
	}
	
	// Update is called once per frame
	void Update () {
		jump_bool = ui.Pass_choosed();

		if(!dead_bool && !death_safty)
		{
			dead_bool = dm.DeathStatus();
		}

		if(dead_bool)
		{
			playerAudio.clip = death;
			playerAudio.Play();
			death_safty = true;
			dead_bool = false;
		}
		

		// if(jump_bool)
		// {
		// 	playerAudio.clip = jump;
		// 	playerAudio.Play();
		// }
	}

	private void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.gameObject.tag == "ground" || other.gameObject.tag == "moveable_ground" || other.gameObject.tag == "slippery_ground")
		{
			playerAudio.clip = land;
			playerAudio.Play();
		}	

		// if(other.gameObject.tag == "spikes")
		// {
		// 	playerAudio.clip = death;
		// 	playerAudio.Play();
		// }
	}

	private void OnCollisionExit2D(Collision2D other) 
	{
		if(other.gameObject.tag == "ground" || other.gameObject.tag == "moveable_ground" || other.gameObject.tag == "slippery_ground")
		{
			playerAudio.clip = jump;
			playerAudio.Play();
		}
	}

}
