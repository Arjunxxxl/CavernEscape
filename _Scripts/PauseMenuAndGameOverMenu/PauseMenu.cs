using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenu, gameMenu;
	string current;

	public PlayableDirector pause_anim;

	LoadLevel loader;

	public AudioClip click_clip;
	public AudioSource clickSource;


	

	// Use this for initialization
	void Start () {
		pauseMenu.SetActive(false);
		//gameMenu.SetActive(true);
		Time.timeScale = 1f;
		current = SceneManager.GetActiveScene().name;
		pause_anim.Stop();

		loader = LoadLevel.Instance;
		clickSource.clip = click_clip;		
	}

	public void Pause()
	{
		Time.timeScale = 0f;
		pauseMenu.SetActive(true);
		gameMenu.SetActive(false);
		pause_anim.Play();
		clickSource.Play();
	}

	public void Resume()
	{
		pauseMenu.SetActive(false);
		gameMenu.SetActive(true);
		Time.timeScale = 1f;
		pause_anim.Stop();
		clickSource.Play();
	}

	public void Restart()
	{	
		PlayerPrefs.SetString("isRestart" ,"true");
		clickSource.Play();
		loader.load(current);
	}

	public void Menu()
	{		
		clickSource.Play();
		PlayerPrefs.SetString("isRestart" ,"false");
		loader.load(current);
	}


}
