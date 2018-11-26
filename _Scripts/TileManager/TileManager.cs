using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject[] tiles;
	public Transform player;
	float spawnY = -5f;
	float tileLength = 21.6f;
	private int amtOfTileOnScreen = 7;
	float safeZone = 41f;

	int lastTileIndex = 0;

	GameObject go;

	List<GameObject> activeTiles = new List<GameObject>();
	List<GameObject> platforms_list = new List<GameObject>();
	List<GameObject> spikes_wall = new List<GameObject>();
	List<GameObject> spikes_gnd = new List<GameObject>();
	List<GameObject> spikes_shooting = new List<GameObject>();
	List<GameObject> spikes_falling = new List<GameObject>();
	List<GameObject> horizontal_shooting_spikes = new List<GameObject>();


	public ColorManager cm;
	int color_index = 0;
	int startTile = 0;

	// Use this for initialization
	void Start () {
		startTile = 0;
		if(!player)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		activeTiles = new List<GameObject>();
		platforms_list = new List<GameObject>();
		spikes_wall = new List<GameObject>();
		spikes_gnd = new List<GameObject>();
		spikes_shooting = new List<GameObject>();
		spikes_falling = new List<GameObject>();
		horizontal_shooting_spikes = new List<GameObject>();

		for(int i = 0; i<amtOfTileOnScreen; i++)
		{	
			if(i == 0)
			{	
				startTile = Random.Range(0, 2);
				SpawnTile(startTile);
			}
			else if(i == 1)
			{
				SpawnTile(3);
			}
			else
			{
				SpawnTile();
			}
		}

		cm = ColorManager.Instance;
		color_index = cm.ColorIndex();
		//Debug.Log(index);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(player.position.y - safeZone > (spawnY - (amtOfTileOnScreen * tileLength)))
		{
			SpawnTile();
			RemoveTile();
		}

	}

	void SpawnTile(int prefabIndex = -1)
	{	
		int index = RandomPrefabIndex();
		if(prefabIndex == -1)
		{
			go = Instantiate(tiles[index]) as GameObject;
		}
		else
		{
			go = Instantiate(tiles[prefabIndex]) as GameObject;
		}
		go.transform.SetParent(transform);
		go.transform.position = Vector3.up * spawnY;
		spawnY += tileLength;
		activeTiles.Add(go);

		platforms_list = new List<GameObject>();
		spikes_wall = new List<GameObject>();
		spikes_gnd = new List<GameObject>();
		spikes_shooting = new List<GameObject>();
		spikes_falling = new List<GameObject>();

		color_index = cm.ColorIndex();

		if(go)
		{
			SetColor(go);
		}
		
	}

	void RemoveTile()
	{
		Destroy(activeTiles[0]);
		activeTiles.RemoveAt(0);
	}

	int RandomPrefabIndex()
	{
		if(tiles.Length<=1)
		{
			return 0;
		}

		int randomIndex = lastTileIndex;

		while(randomIndex == lastTileIndex)
		{
			randomIndex = Random.Range(3, tiles.Length);
		}

		return randomIndex;

	}

	void SetColor(GameObject o)
	{
		GameObject wallFront = o.transform.Find("walkable_front").gameObject;
		GameObject wallBack = o.transform.Find("walkable_back").gameObject;
		GameObject parallax_BG = o.transform.Find("parallax_BG").gameObject;
		GameObject parallax_BG2 = o.transform.Find("parallax_BG2").gameObject;


		GameObject platform_container = o.transform.Find("Platforms").gameObject;
		foreach(Transform child in platform_container.transform)
		{
			if(/*child.name == "platforms" ||*/ child.tag == "ground" || child.tag == "slippery_ground")
			{
				platforms_list.Add(child.gameObject);
			}
		}


		GameObject wall_spikes = o.transform.Find("Spikes_wall").gameObject;
		foreach(Transform wall_child in wall_spikes.transform)
		{
			if(wall_child.tag == "spikes")
			{
				spikes_wall.Add(wall_child.gameObject);
			}
		}
		GameObject gnd_spikes = o.transform.Find("Spikes_gnd").gameObject;
		foreach(Transform gnd_child in gnd_spikes.transform)
		{
			if(gnd_child.tag == "spikes")
			{
				spikes_gnd.Add(gnd_child.gameObject);
			}
		}


		if(o.tag == "Spikes_movement_shoot")
		{
			GameObject Shooting_Spikes = o.transform.Find("Shooting_Spike").gameObject;
			foreach(Transform shoot_child in Shooting_Spikes.transform)
			{
				if(shoot_child.tag == "spikes")
				{
					spikes_shooting.Add(shoot_child.gameObject);
				}
			}
		}

		if(o.tag == "Spikes_movement_fall")
		{
			GameObject fallinf_Spikes = o.transform.Find("FallingSpike").gameObject;
			foreach(Transform fall_child in fallinf_Spikes.transform)
			{
				if(fall_child.tag == "spikes")
				{
					spikes_falling.Add(fall_child.gameObject);
				}
			}
		}

		if(o.tag == "horizontal_shooting")
		{
			GameObject h_shooting = o.transform.Find("Horizontal_shooting_spikes").gameObject;
			foreach(Transform horizontal_child in h_shooting.transform)
			{
				if(horizontal_child.tag == "spikes")
				{
					horizontal_shooting_spikes.Add(horizontal_child.gameObject);
				}
			}
		}
		


		Color_tiles(wallFront, wallBack, parallax_BG, parallax_BG2);
		Color_platforms();
		Color_spikes();
	}

	public void Color_tiles(GameObject wallFront, GameObject wallBack, GameObject parallax_BG, GameObject parallax_BG2)
	{	
		//Debug.Log("color index = " + color_index);
		if(color_index == 0)
		{
			wallFront.GetComponent<SpriteRenderer>().color = new Color32(65, 0, 86, 255);
			wallBack.GetComponent<SpriteRenderer>().color = new Color32(42, 0, 56, 255);
			parallax_BG.GetComponent<SpriteRenderer>().color = new Color32(114, 35, 140, 255);
			parallax_BG2.GetComponent<SpriteRenderer>().color = new Color32(87, 26, 108, 255);
		}
		else if(color_index == 1)
		{	
			wallFront.GetComponent<SpriteRenderer>().color = new Color32(150, 0, 0, 255);
			wallBack.GetComponent<SpriteRenderer>().color = new Color32(82, 0, 0, 255);
			parallax_BG.GetComponent<SpriteRenderer>().color = new Color32(140, 51, 51, 255);
			parallax_BG2.GetComponent<SpriteRenderer>().color = new Color32(84, 29, 29, 255);
		}
		else if(color_index == 2)
		{	
			wallFront.GetComponent<SpriteRenderer>().color = new Color32(0, 77, 0, 255);
			wallBack.GetComponent<SpriteRenderer>().color = new Color32(0, 42, 0, 255);
			parallax_BG.GetComponent<SpriteRenderer>().color = new Color32(37, 151, 37, 255);
			parallax_BG2.GetComponent<SpriteRenderer>().color = new Color32(25, 105, 25, 255);
		}
		else if(color_index == 3)
		{	
			wallFront.GetComponent<SpriteRenderer>().color = new Color32(69, 58, 0, 255);
			wallBack.GetComponent<SpriteRenderer>().color = new Color32(27, 22, 0, 255);
			parallax_BG.GetComponent<SpriteRenderer>().color = new Color32(153, 136, 41, 255);
			parallax_BG2.GetComponent<SpriteRenderer>().color = new Color32(96, 84, 23, 255);
		}
	}
	
	void Color_platforms()
	{
		foreach(GameObject plat in platforms_list)
		{	
			if(plat)
			{	
				if(color_index == 0)
				{
					plat.GetComponent<SpriteRenderer>().color = new Color32(0, 33, 63, 255);
				}
				else if(color_index == 1)
				{
					plat.GetComponent<SpriteRenderer>().color = new Color32(15, 0, 45, 255);
				}
				else if(color_index == 2)
				{
					plat.GetComponent<SpriteRenderer>().color = new Color32(129, 2, 2, 255);
				}
				else if(color_index == 3)
				{
					plat.GetComponent<SpriteRenderer>().color = new Color32(203, 0, 71, 255);
				}
			}
		}
		
	}

	void Color_spikes()
	{
		foreach(GameObject w_spikes in spikes_wall)
		{	
			if(w_spikes)
			{	
				if(color_index ==0 )
				{
					w_spikes.GetComponent<SpriteRenderer>().color = new Color32(12, 33, 52, 255);
				}
				else if(color_index == 1)
				{
					w_spikes.GetComponent<SpriteRenderer>().color = new Color32(10, 7, 25, 255);
				}
				else if(color_index == 2)
				{
					w_spikes.GetComponent<SpriteRenderer>().color = new Color32(106, 0, 0, 255);
				}
				else if(color_index == 3)
				{
					w_spikes.GetComponent<SpriteRenderer>().color = new Color32(126, 0, 44, 255);
				}
			}
		}

		foreach(GameObject g_spikes in spikes_gnd)
		{	
			if(g_spikes)
			{	
				if(color_index == 0)
				{
					g_spikes.GetComponent<SpriteRenderer>().color = new Color32(12, 33, 52, 255);
				}
				else if(color_index == 1)
				{
					g_spikes.GetComponent<SpriteRenderer>().color = new Color32(10, 7, 25, 255);
				} 
				else if(color_index == 2)
				{
					g_spikes.GetComponent<SpriteRenderer>().color = new Color32(106, 0, 0, 255);
				}
				else if(color_index == 3)
				{
					g_spikes.GetComponent<SpriteRenderer>().color = new Color32(126, 0, 44, 255);
				}
			}
		}

		foreach(GameObject s_spikes in spikes_shooting)
		{	
			if(s_spikes)
			{	
				if(color_index ==0 )
				{
					s_spikes.GetComponent<SpriteRenderer>().color = new Color32(12, 33, 52, 255);
				}
				else if(color_index == 1)
				{
					s_spikes.GetComponent<SpriteRenderer>().color = new Color32(10, 7, 25, 255);
				}
				else if(color_index == 2)
				{
					s_spikes.GetComponent<SpriteRenderer>().color = new Color32(106, 0, 0, 255);
				} 
				else if(color_index == 3)
				{
					s_spikes.GetComponent<SpriteRenderer>().color = new Color32(126, 0, 44, 255);
				} 
				
			}
		}	
		foreach(GameObject f_spikes in spikes_falling)
		{	
			if(f_spikes)
			{	
				if(color_index ==0 )
				{
					f_spikes.GetComponent<SpriteRenderer>().color = new Color32(12, 33, 52, 255);
				}
				else if(color_index == 1)
				{
					f_spikes.GetComponent<SpriteRenderer>().color = new Color32(10, 7, 25, 255);
				}
				else if(color_index == 2)
				{
					f_spikes.GetComponent<SpriteRenderer>().color = new Color32(106, 0, 0, 255);
				}
				else if(color_index == 3)
				{
					f_spikes.GetComponent<SpriteRenderer>().color = new Color32(126, 0, 44, 255);
				}
			}
		}	
		foreach(GameObject h_shoot in horizontal_shooting_spikes)
		{	
			if(h_shoot)
			{	
				if(color_index ==0 )
				{
					h_shoot.GetComponent<SpriteRenderer>().color = new Color32(12, 33, 52, 255);
				}
				if(color_index == 1)
				{
					h_shoot.GetComponent<SpriteRenderer>().color = new Color32(10, 7, 25, 255);
				}
				if(color_index == 2)
				{
					h_shoot.GetComponent<SpriteRenderer>().color = new Color32(106, 0, 0, 255);
				}
				if(color_index == 3)
				{
					h_shoot.GetComponent<SpriteRenderer>().color = new Color32(126, 0, 44, 255);
				}
			}
		}
	}

}
