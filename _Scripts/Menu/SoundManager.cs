using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

	public Sprite music_on, music_off;
	public Sprite sfx_on, sfx_off;

	public Image setting_musix, setting_sfx;
	public Image pause_musix, pause_sfx;

	public AudioMixer m,s;

	int music, sfx;

	// Use this for initialization
	void Start () {
		music = PlayerPrefs.GetInt("music", 1);
		sfx = PlayerPrefs.GetInt("sfx", 1);

		if(music == 1)
		{
			setting_musix.sprite = music_on;
			pause_musix.sprite = music_on;
			m.SetFloat("musix_vol", 0f);
		}
		else if(music == 0)
		{
			setting_musix.sprite = music_off;
			pause_musix.sprite = music_off;
			m.SetFloat("musix_vol", -80f);
		}

		if(sfx == 1)
		{
			setting_sfx.sprite = sfx_on;
			pause_sfx.sprite = sfx_on;
			s.SetFloat("sfx_vol", 0f);
		}
		else if(sfx == 0)
		{
			setting_sfx.sprite = sfx_off;
			pause_sfx.sprite = sfx_off;
			s.SetFloat("sfx_vol", -80f);
		}
	}
	
	public void Music_toggle()
	{	
		music = PlayerPrefs.GetInt("music", 1);
		if(music == 1)
		{
			m.SetFloat("musix_vol", -80f);
			setting_musix.sprite = music_off;
			pause_musix.sprite = music_off;
			PlayerPrefs.SetInt("music", 0);
		}
		else if(music == 0)
		{
			m.SetFloat("musix_vol", 0f);
			setting_musix.sprite = music_on;
			pause_musix.sprite = music_on;
			PlayerPrefs.SetInt("music", 1);
		}
	}

	public void SFX_toggle()
	{	
		sfx = PlayerPrefs.GetInt("sfx", 1);
		if(sfx == 1)
		{
			s.SetFloat("sfx_vol", -80f);
			setting_sfx.sprite = sfx_off;
			pause_sfx.sprite = sfx_off;
			PlayerPrefs.SetInt("sfx", 0);
		}
		else if(sfx == 0)
		{
			s.SetFloat("sfx_vol", 0f);
			setting_sfx.sprite = sfx_on;
			pause_sfx.sprite = sfx_on;
			PlayerPrefs.SetInt("sfx", 1);
		}
	}

}
