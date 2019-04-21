using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap2 : MonoBehaviour {

	//ANOTHER GENERATION METHOUD USE THIS FOR GENERAL MAP GENERATION 
	public GameObject tile;
	public List<CustomTile> list = new List<CustomTile>();
	
	[Range (10,500)] public int MapWidth;
	[Range (10,500)] public int MapHeight;
	
	public CustomTile forSideCheck;
	public float InitialChance = 0.5f;

	public int Deadlimit = 4; //on first run its 3 second run change to 4 ++  
	public int BirthLimit = 2;
	public int cycles;

	

	public class CustomTile {
		public GameObject ob;
		public Vector3 position;
		public bool isAlive;
		//add sprite manager
		//THIS IS A FUCKING SUBCLASS 

		public CustomTile(Vector3 position , bool isAlive){
			this.position = position;
			this.isAlive = isAlive;
		}
		public void UpdateTile(){ //update tile info 
			ob.GetComponent<TileS>().isAlive = this.isAlive;
		}

	};


	void Start () {
		GenerateVectorMap(MapHeight,MapWidth);
		PlaceGameObject(list);
		mapCycle(Deadlimit,cycles);
		

	}
	
	
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space)){ //call this using a seperate and for loop with randomizer to do stuff
		/*	foreach(CustomTile a in list){
				SideCheckTile(a,Deadlimit);
				a.ob.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = a.isAlive;
			}
		}8*/
			
		
		}
	}

	public List<CustomTile> GenerateVectorMap(int height , int width){
		//classic two for loops to get a 2d vector map change later to use 2d arrays for better preformance
		for(float x = -width/2 ; x <= width/2; x++){
			for(float z= -height/2; z <= height/2 ; z++){
				Vector3 newV = new Vector3(x,0,z); 
				bool rand = (Random.value > InitialChance);
				CustomTile tile = new CustomTile(newV,rand);
				list.Add(tile);
			}
		}
		return list;
	}

	private GameObject CreateTile(Vector3 point){
		//get tiles from array
		if(tile.gameObject == null){
			Debug.LogError("NO TILE ENTERED");
		}
		GameObject newTile = Instantiate(tile,point,tile.transform.rotation);
		newTile.transform.parent = this.transform;
		return newTile;

	}
	public void PlaceGameObject(List<CustomTile> list){
		foreach(CustomTile a in list){
			GameObject newTile = CreateTile(a.position);
			a.ob = newTile;
			a.UpdateTile(); //to update the tile internal info for the "dead or alive" delete later too laggy 
			

		}
	}

	public void SideCheckTile(CustomTile tile,int Deadlimit){ //redo to get position in 2d array
		List<Vector3> newList = new List<Vector3>();
		List<GameObject> objDead = new List<GameObject>();
		List<GameObject> objAlive = new List<GameObject>();

		//loops through every x,y cord in square shape
		for(float x = -1; x <= 1; x++){
			for(float z= -1; z <= 1 ; z++){
				Vector3 temp = new Vector3(x,0,z);

				newList.Add(temp);
			}
		}

		//draw at all points directions 
		foreach(Vector3 a in newList){
			RaycastHit hit;
			Ray ray = new Ray(tile.ob.transform.position + a + Vector3.down , Vector3.up ); 
			if (Physics.Raycast(ray, out hit) && hit.collider.transform.tag == "Tile") { //change to include all objects or use layer
				//do stuff to object

				//check if alive or dead
				if(hit.collider.gameObject.transform.GetComponent<TileS>().isAlive){
					objAlive.Add(hit.collider.gameObject);
				} else {
					objDead.Add(hit.collider.gameObject);
				}
			}
		}

			if(objDead.Count <= Deadlimit && !tile.isAlive){
				tile.isAlive = true;
			} else {
				tile.isAlive = false;
			}
		}



	private void mapCycle(int rand,int cycles){
		for(int i =0; i <= cycles ;i++){
			rand = Random.Range(rand-1,rand+1);
			foreach(CustomTile a in list){ //check every tile in list 
				SideCheckTile(a,rand);
				//a.ob.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = a.isAlive;

				if(i == cycles){
				SideCheckTile(a,Deadlimit);
				//a.ob.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = a.isAlive;
				if(MapWidth * MapHeight < 5000){ //temporary preformance solution
					Destroy(a.ob.transform.GetComponent<Rigidbody>(),0f);
				} 

				}
			}

		}

		}
		
	}

