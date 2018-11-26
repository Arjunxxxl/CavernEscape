using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Audio;

public class GameOver : MonoBehaviour {

	public GameObject gameover_menu;
	public PlayableDirector gameover_anim;

	DeathManager dm;
	public bool isDead;
	public bool showGameOver = false;

	string current;

	LoadLevel loader;
	ObjectPooler pooler;

	Transform player;

	GameObject particle;

	MainMenu m_menu;

	public AudioClip click_clip;
	public AudioSource clickSource;

	int index;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	// Use this for initialization
	void Start () {
		dm = DeathManager.Instance;
		m_menu = MainMenu.instance;


		clickSource.clip = click_clip;


		gameover_anim.Stop();
		gameover_menu.SetActive(false);
		isDead = dm.DeathStatus();
		current = SceneManager.GetActiveScene().name;

		loader = LoadLevel.Instance;

		pooler = ObjectPooler.Instance;
		showGameOver = false;

		index = 0;
		index = Random.Range(0, 6);
	}

	void Update() {
		isDead = dm.DeathStatus();
		
		if(isDead)
		{
			StartCoroutine(Delay_death());
			player.GetComponent<SpriteRenderer>().enabled = false;
			player.GetComponent<BoxCollider2D>().enabled = false;
			Time.timeScale = 0f;

			particle = pooler.SwapnfromPool(index, player.position, Quaternion.identity);
			//particle.GetComponent<ParticleSystem>().emi
		}

		if(isDead && showGameOver)
		{
			gameover_menu.SetActive(true);
			gameover_anim.Play();
			Time.timeScale = 0f;
		}

	}

	public void Restart()
	{	
		PlayerPrefs.SetString("isRestart" ,"true");
		clickSource.Play();
		loader.load(current);
	}

	public void Menu()
	{	
		PlayerPrefs.SetString("isRestart" ,"false");
		clickSource.Play();
		loader.load(current);
	}

	IEnumerator Delay_death()
	{	
		yield return new WaitForSecondsRealtime(1.5f);
		showGameOver = true;
		
	}

	private void OnApplicationQuit() {
		PlayerPrefs.SetString("isRestart" ,"false");
	}
	
}
