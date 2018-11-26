using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRocks : MonoBehaviour {

    GameObject rock;
	public Sprite rock1, rock2, rock3;

	int x = 0;

	// Use this for initialization
	void Start () {
		x = (int)Random.Range(0f, 2.9999f);

		rock = gameObject;

		if(x == 0)
		{
			rock.GetComponent<SpriteRenderer>().sprite = rock1;
		}
		else if(x == 1)
		{
			rock.GetComponent<SpriteRenderer>().sprite = rock2;
		}
		else if(x == 2)
		{
			rock.GetComponent<SpriteRenderer>().sprite = rock3;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
