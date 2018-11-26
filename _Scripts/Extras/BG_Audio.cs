using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BG_Audio : MonoBehaviour {

	AudioSource[] bg;
	public AudioClip[] bg_clip;

	public static BG_Audio Instance;
 
	private void Awake() {
		if(Instance == null)
		{	
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		bg = GetComponents<AudioSource>();
		bg[0].clip = bg_clip[0];  //cave ambient
		bg[1].clip = bg_clip[1];

		bg[0].Play();
		bg[1].Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
