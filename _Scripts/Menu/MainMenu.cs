using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour {

	public GameObject playMenu, setting, highscore;
	public GameObject main_menu;

	public PlayableDirector startLevel, endLevel;
	public GameObject sceneTrans;

	string isRestart;

	public AudioClip click;
	public AudioSource click_Audio_Manager;

	public TMP_Text highscore_txt;
	int best;

	public static MainMenu instance;
	private void Awake() {
		instance = this;
	}

	private void Start() {
		Restart_startus();
		SetHighscore();
		if(isRestart == "false")
		{
			playMenu.SetActive(false);
			main_menu.SetActive(true);
			setting.SetActive(false);
			highscore.SetActive(false);
		}
		else
		{
			playMenu.SetActive(true);
			main_menu.SetActive(false);
			setting.SetActive(false);
			highscore.SetActive(false);
		}

		click_Audio_Manager.clip = click;
	}

	public void Play()
	{
		StartCoroutine(PlayMenu());
		click_Audio_Manager.Play();
	}

	public void ShowMain_Menu()
	{
		StartCoroutine(MenuMain());
		click_Audio_Manager.Play();
	}
	
	IEnumerator PlayMenu()
	{	
		sceneTrans.SetActive(true);
		endLevel.Play();
		yield return new WaitForSecondsRealtime(1.5f);
		endLevel.Stop();
		startLevel.Play();

		playMenu.SetActive(true);
		main_menu.SetActive(false);

		
		yield return new WaitForSecondsRealtime(2.5f);
		sceneTrans.SetActive(false);
		startLevel.Stop();
	}

	IEnumerator MenuMain()
	{	
		sceneTrans.SetActive(true);
		endLevel.Play();
		yield return new WaitForSecondsRealtime(1.5f);
		endLevel.Stop();
		startLevel.Play();

		playMenu.SetActive(false);
		main_menu.SetActive(true);

		
		yield return new WaitForSecondsRealtime(2.5f);
		sceneTrans.SetActive(false);
		startLevel.Stop();
	}

	public void ShowSetting()
	{
		StartCoroutine(Play_start(setting));
		click_Audio_Manager.Play();
	}

	public void BackSettings()
	{
		StartCoroutine(Play_end(setting));
		click_Audio_Manager.Play();
	}

	public void ShowHighscore()
	{
		StartCoroutine(Play_start(highscore));
		SetHighscore();
		click_Audio_Manager.Play();
	}

	public void BackHighscore()
	{
		StartCoroutine(Play_end(highscore));
		click_Audio_Manager.Play();
	}

	IEnumerator Play_start(GameObject o)
	{
		sceneTrans.SetActive(true);
		endLevel.Play();
		yield return new WaitForSecondsRealtime(1.5f);
		endLevel.Stop();
		startLevel.Play();

		o.SetActive(true);
		main_menu.SetActive(false);

		
		yield return new WaitForSecondsRealtime(2.5f);
		sceneTrans.SetActive(false);
		startLevel.Stop();
	}

		IEnumerator Play_end(GameObject o)
	{
		sceneTrans.SetActive(true);
		endLevel.Play();
		yield return new WaitForSecondsRealtime(1.5f);
		endLevel.Stop();
		startLevel.Play();

		o.SetActive(false);
		main_menu.SetActive(true);

		
		yield return new WaitForSecondsRealtime(2.5f);
		sceneTrans.SetActive(false);
		startLevel.Stop();
	}

	void SetHighscore()
	{	
		best = PlayerPrefs.GetInt("best_score", 0);
		highscore_txt.text = best + "";
	}

	void Restart_startus()
	{
		isRestart = PlayerPrefs.GetString("isRestart" ,"false");
	}
}
