using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour {

	public TMP_Text best_score;
	public TMP_Text final_score;
	public TMP_Text score_txt;
	GameObject currentPlateform;

	float lastPosY;
	public int index = 0;
	int score = 0;
	int max_Score = 0;

	bool isDead;

	DeathManager dm;

	// Use this for initialization
	void Start () {
		dm = DeathManager.Instance;

		score_txt.text = "0";
		final_score.text = "0";
		currentPlateform = null;
		index = 0;
		score = 0;
		lastPosY = transform.position.y;

		isDead = dm.DeathStatus();

		max_Score = PlayerPrefs.GetInt("best_score", 0);
		best_score.text = "Best: " + max_Score + "";
	}
	
	// Update is called once per frame
	void Update () {

		isDead = dm.DeathStatus();
		if(isDead)
		{	
			final_score.text = score + "";
			if(score > max_Score)
			{
				max_Score = score;
				PlayerPrefs.SetInt("best_score", max_Score);
				best_score.text = "Best: " + max_Score + "";
			}
			return;
		}
		
		score_txt.text = score +"";
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if((other.gameObject.tag == "ground" || other.gameObject.tag == "moveable_ground" || other.gameObject.tag == "slippery_ground") 
				&& !isDead)
		{
			if(currentPlateform == null)
			{
				currentPlateform = other.gameObject;
			}

			if(currentPlateform == other.gameObject)
			{
				index++;
			}
			else
			{	if(transform.position.y > lastPosY)
				{
					currentPlateform = other.gameObject;
					score++;
					index = 0;
					lastPosY = transform.position.y;
				}
			}

		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.tag == "ground")
		{
			if(currentPlateform == other.gameObject)
			{
				//index = 0;
			}

		}
	}

}
