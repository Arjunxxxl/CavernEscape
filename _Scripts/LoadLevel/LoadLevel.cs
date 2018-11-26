using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class LoadLevel : MonoBehaviour {

	public Slider slider;
	public GameObject loadingScreen;

	float progress;

	public PlayableDirector end_sceneTrans;
	public GameObject sceneTrans;

	public PlayableDirector start_sceneTrans;


	public static LoadLevel Instance;
	private void Awake() {
		Instance = this;
		end_sceneTrans.Stop();
	}

	// Use this for initialization
	void Start () {
		loadingScreen.SetActive(false);
		end_sceneTrans.Stop();
		sceneTrans.SetActive(true);
		//start_sceneTrans.Stop();
		StartCoroutine(StartTrans());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void load(string sceneName)
	{	
		StartCoroutine(SceneTransistion(sceneName));
	}

	IEnumerator SceneTransistion(string sceneName)
	{	
		sceneTrans.SetActive(true);
		end_sceneTrans.Play();
		yield return new WaitForSecondsRealtime(1.0f);
		StartCoroutine(startLoading(sceneName));
		end_sceneTrans.Stop();
	}

	IEnumerator startLoading(string name)
	{	
		loadingScreen.SetActive(true);

		AsyncOperation op = SceneManager.LoadSceneAsync(name);

		while(!op.isDone)
		{
			progress = Mathf.Clamp01(op.progress / 0.9f);
			slider.value = progress;

			yield return null;
		}
	}

	IEnumerator StartTrans()
	{	
		sceneTrans.SetActive(true);
		start_sceneTrans.Play();
		yield return new WaitForSeconds(2.25f);
		sceneTrans.SetActive(false);
		start_sceneTrans.Stop();
	}

}
