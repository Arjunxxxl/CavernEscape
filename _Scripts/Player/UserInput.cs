using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour {

	public GameObject particlesystem;

	public Vector2 startPos;

	public Vector2 dir;
	public float dis;
	float max_dist = 180f;
	float min_dist = 30f;
	bool dir_chooses = false;

	public bool Grounded = true;

	DeathManager dm;
	bool isDead = false;

	Vector3 originalCampos;
	Camera cam;
	float zoomsize = 7.5f;
	float zoomInSpeed = 0.3f;

	float camSize = 8.6f;

	bool zoom;

	bool isReady = false;

	public GameObject playMenu;

	public GameObject touch_indicator;

	public static UserInput Insatnce;
	private void Awake()
	{
		if(Insatnce == null)
		{
			Insatnce = this;
		}
		else
		{

		}
	}

	// Use this for initialization
	void Start () {
		dir = new Vector2(0, 0);
		dir_chooses = false;

		isReady = false;

		dm = DeathManager.Instance;
		isDead = dm.DeathStatus();

		particlesystem.SetActive(false);

		cam = Camera.main;
		camSize = cam.orthographicSize;

		zoom = false;

		StartCoroutine(WaittogetReady());
		touch_indicator.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		isDead = dm.DeathStatus();

		if(!playMenu.activeSelf)
		{
			return;
		}

		if(isDead || !isReady)
		{
			return;
		}
		
		GetTouchInput();
		if(!Grounded)
		{
			dir_chooses = false;
			dis = 0f;
			particlesystem.SetActive(false);
		}
		else
		{
			particlesystem.transform.position = new Vector3(transform.position.x, transform.position.y - 0.35f, transform.position.z);
			particlesystem.SetActive(true);
		}
		//Debug.Log(dis);

		if(zoom)
		{
			StartCoroutine(ZoomCam());
		}
		else
		{
			StartCoroutine(ZoomOutCam());
		}
	}


	void GetTouchInput()
	{
		if(Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			switch(touch.phase)
			{
				case TouchPhase.Began : 
						dir_chooses = false;
						startPos = touch.position;
						touch_indicator.SetActive(true);
						touch_indicator.transform.position = new Vector3(startPos.x, startPos.y, 0f);
						break;

				case TouchPhase.Moved : 
						dir = touch.position - startPos;
						dis = dir.magnitude;
						//Debug.Log(dir.x + "  "+ dir.y);
						if(dis>=max_dist)
						{
							dis = max_dist;
						}
						else if(dis < min_dist)
						{
							dis = 0f;
						}
						break;

				case TouchPhase.Ended:
						if(dis >= min_dist && Grounded)
						{
							dir_chooses = true;
						}
						else
						{
							dir_chooses = false;
						}
						touch_indicator.SetActive(false);
						break;
			}
		}
	}

	public Vector2 Pass_Direction()
	{
		return dir;
	}

	public bool Pass_choosed()
	{
		return dir_chooses;
	}

	public float Pass_Distance()
	{
		return dis;
	}

	public bool isGrounded()
	{
		return Grounded;
	}

	private void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.gameObject.tag == "ground" || other.gameObject.tag == "moveable_ground" || other.gameObject.tag == "slippery_ground")
		{
			Grounded = true;
			//ShakeCamera(2f, 1f);
			zoom = false;
		}	
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if(other.gameObject.tag == "ground" || other.gameObject.tag == "moveable_ground" || other.gameObject.tag == "slippery_ground")
		{
			Grounded = false;
			zoom = true;
		}		
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if(other.gameObject.tag == "ground" || other.gameObject.tag == "moveable_ground" || other.gameObject.tag == "slippery_ground")
		{
			Grounded = true;
		}
	}


	IEnumerator ShakeCamera(float duration, float mag)
	{
		originalCampos = cam.transform.localPosition;

		float timePassed = 0f;

		while(timePassed < duration)
		{
			float x = Random.Range(-0.5f, 0.5f) * mag;

			cam.transform.localPosition = new Vector3(x, originalCampos.y, originalCampos.z);
			timePassed += Time.deltaTime;

			yield return null;
		}
		cam.transform.localPosition = originalCampos;
	}

	IEnumerator ZoomCam()
	{	
		while(cam.orthographicSize > zoomsize)
		{
			cam.orthographicSize -= Time.deltaTime * zoomInSpeed; 
			yield return null;
		}
	}

	IEnumerator ZoomOutCam()
	{	
		while(cam.orthographicSize < camSize)
		{
			cam.orthographicSize += Time.deltaTime * zoomInSpeed / 2f;
			yield return null;
		}
	}

	IEnumerator WaittogetReady()
	{
		yield return new WaitForSeconds(2.25f);
		isReady = true;
	}

}
