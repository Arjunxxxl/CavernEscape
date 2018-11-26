using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IndicateDanger : MonoBehaviour {

	bool danger;
	public GameObject DamageHolder;

	public PlayableDirector indicator;

	public static IndicateDanger Instance;
	private void Awake() {
		Instance = this;
	}


	// Use this for initialization
	void Start () {
		danger = false;
		indicator.Stop();
		DamageHolder.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(danger)
		{
			indicator.Play();
			DamageHolder.SetActive(true);
		}
		else
		{
			indicator.Stop();
			DamageHolder.SetActive(false);
		}
	}

	public void isDanger(bool d)
	{
		danger = d;
	}

}
