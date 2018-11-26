using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour {

	bool isDead = false;
	bool fall_death = false;

	bool screenshot;
	int x;

	public static DeathManager Instance;
	private void Awake()
	{
		Instance = this;	
	}

	// Use this for initialization
	void Start () {
		isDead = false;
		fall_death = false;
		screenshot = false;
		x = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!screenshot)
		{
			screenshot = (isDead || fall_death);
		}

		if(screenshot && x == 0)
		{
			StartCoroutine(takeScreenshot());
			//Debug.Log("now");
			screenshot = false;
			x++;
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "spikes")
		{
			isDead= true;
			//Debug.Log("collision");
		}
	}

	public bool DeathStatus()
	{
		return (isDead || fall_death);
	}

	public void get_DeathStatus(bool deathstatus)
	{
		fall_death = deathstatus;
	}

	IEnumerator takeScreenshot()
	{
		yield return new WaitForSecondsRealtime(0.5f);
		ScreenCapture.CaptureScreenshot("share_screen.png", 2);
	}

}
