using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RateUs : MonoBehaviour {

	bool isProcessing = false;
	public DeathManager dm;
	bool death;

	// Use this for initialization
	void Start () {
		isProcessing = false;
		dm = DeathManager.Instance;
		death = dm.DeathStatus();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Rate()
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.ImagineStudio.caverncube");
	}

    public void ShareScore()
    {
        Debug.Log("working -1");
        if (!isProcessing)
        {	
             Debug.Log("working 0");
			death = dm.DeathStatus();
            StartCoroutine(ShareScreenshot());
        }
    }

	public void ShareBtnPress()
    {   
        Debug.Log("working -1");
        if (!isProcessing)
        {	
             Debug.Log("working 0");
			death = dm.DeathStatus();
            StartCoroutine(ShareScreenshot());
        }
    }
 
    IEnumerator ShareScreenshot()
    {
        isProcessing = true;
 
        //yield return null;
		string destination = "";
		Debug.Log("working 1 ");
		if(death)
		{
        	destination = Path.Combine(Application.persistentDataPath, "share_screen.png");
		}
		else
		{	
			Debug.Log("working 2 ");
			ScreenCapture.CaptureScreenshot("share_screen.png", 2);
			destination = Path.Combine(Application.persistentDataPath, "share_screen.png");
		}
		Debug.Log("working 3 ");
        yield return new WaitForSecondsRealtime(0.3f);
 
        if (!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"),
                uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
                "Can you beat my score?");
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
                intentObject, "Share your new score");
            currentActivity.Call("startActivity", chooser);
 
            yield return new WaitForSecondsRealtime(1);
        }
 
        isProcessing = false;
    }



}
