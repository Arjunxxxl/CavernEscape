using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Direction : MonoBehaviour {

	public GameObject Dxn;
	public TMP_Text PowerIndiacator;
	public Vector3 power_text_offset;
	public float power;
	public float min_length = 0.02f;
	public float max_length = 0.12f;

	public float min_pow = 30f;
	public float max_pow = 180f;

	UserInput input;
	Vector2 dir;
	float dis;
	Vector3 rot;

	Vector3 localS;

	bool show = false;

	Camera cam;

	// Use this for initialization
	void Start () {
		input = UserInput.Insatnce;
		dir = input.Pass_Direction();
		dis = dir.magnitude;

		show = false;
		Dxn.SetActive(false);
		PowerIndiacator.transform.gameObject.SetActive(false);

		cam = Camera.main;
		power_text_offset = new Vector3(0f, 0.5f, 0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		dir = input.Pass_Direction();
		dis = input.Pass_Distance();

		CheckTouchInput();

		if(show)
		{
			SetDxn();
		}
		else
		{
			ResetDxn();
		}
	}

	void CheckTouchInput()
	{
		if(Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			switch(touch.phase)
			{
				case TouchPhase.Moved :
						if(dis>=30)
						{
							show = true;
						}
						break;

				case TouchPhase.Ended :
						show = false;
						break;		
			}
		}
	}

	void SetDxn()
	{
		Dxn.SetActive(true);

		float rot_z = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg ;
		//Debug.Log(rot_z);
        Dxn.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

		SetPower();
		DisplayPower();
	}

	void ResetDxn()
	{	
		localS = Dxn.transform.localScale;
		localS.y = 0f;
		Dxn.transform.localScale = localS;

		Dxn.SetActive(false);

		Dxn.transform.rotation = Quaternion.Euler(0f , 0f, 0f);

		PowerIndiacator.transform.gameObject.SetActive(false);
	}

	void SetPower()
	{
		localS = Dxn.transform.localScale;
		
		localS.y = ( ( (dis - min_pow) / (max_pow - min_pow) ) * (max_length - min_length) ) + min_length;

		Dxn.transform.localScale = localS;
	}

	void DisplayPower()
	{	

		power = ((dis - min_pow) / (max_pow - min_pow)) * 100f;
		PowerIndiacator.text = (int)power + "";

		PowerIndiacator.transform.position = cam.WorldToScreenPoint(transform.position + power_text_offset);
		PowerIndiacator.transform.gameObject.SetActive(true);
	}

}
