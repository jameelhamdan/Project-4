using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {
		


	//USE THIS FOR RANDOM ORE GENERATION
	public GameObject tile;
	public List<GameObject> listoftiles  = new List<GameObject>();

	public List<GameObject> listofrand = new List<GameObject>();

	public List<GameObject> listofclosetoRand = new List<GameObject>();
	

	//dont ask what all this shit does i don't know either 
	void Start(){
		//add procedural generation later
		GenerateTiles(50);
		throwRandom(5,listoftiles,listofrand);
		getTilesBetweenRand2(3,listofrand,listofclosetoRand);

	}

	void Update(){
		foreach(GameObject a in listofrand){
			if(a != null) Debug.DrawLine(a.transform.position,a.transform.position + new Vector3(0,5,0),Color.black);
		}

		foreach(GameObject a in listofclosetoRand){
			if(a != null) Debug.DrawLine(a.transform.position,a.transform.position + new Vector3(0,1,0),Color.magenta);
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			foreach(GameObject a in listofclosetoRand){
				if(a != null) Destroy(a,0.5f);
			}
		}
	}

	private void GenerateTiles(int Range){
		Vector3 ZeroPoint = new Vector3(0,-0.5f,0);
		
		
		for(int z = -Range/2; z <= Range/2 ; z++){
			
			for(int x = -Range/2; x <= Range/2 ;x++ ){
				GameObject currentTile = CreateTile(new Vector3(x,ZeroPoint.y,z));
					

			}
		}


	}

	private GameObject CreateTile(Vector3 point){
		//get tiles from array
		if(tile.gameObject == null){
			Debug.Log("NO TILE ENTERED");
		}
		GameObject newTile = Instantiate(tile,point,tile.transform.rotation);
		newTile.transform.parent = this.transform;
		listoftiles.Add(newTile);
		return newTile;

	}
	
	//get a list of random tiles from the ready made all tiles
	private void throwRandom(int amount,List<GameObject> input, List<GameObject> output){
		int range = listoftiles.Count;
		int[] points = new int[amount];

		for(int i =0; i < amount;i++){
			points[i] = Random.Range(0,range);
			output.Add(input[points[i]]);
		}
		
	}

	private void getTilesBetweenRand2(int radius,List<GameObject> input , List<GameObject> output){

		//loops through each base tile object
		foreach(GameObject a in input) {
			for(int i = 0;i < input.Count; i++){
				List<GameObject> recent = drawRays(input[i].transform.position,getVector3sInRange(radius,true));

				foreach(GameObject n in recent) {
					if(!output.Contains(n)) output.Add(n);
				}
				
			}
		}

	}

	//gets points around zeropoints use it as an offset
	//this gets points around the base tile with radius
	//might need some more optimization for % double and single to make more even range
	private List<Vector3> getVector3sInRange(float radius , bool randomize = false){
		List<Vector3> newList = new List<Vector3>();
		if(randomize){ //makes a random range
			float min;
			if(radius < 2){
				min = radius;
			} else {
				min = radius - 1;
			}
			float max = radius + 2;

			radius = Random.Range(min,max);
		}

		//loops through every x,y cord in square shape
		for(float x = -radius; x <= radius; x++){
			for(float z= -radius; z <= radius ; z++){
				Vector3 temp = new Vector3(x,0,z);
				float dist = Vector3.Distance(temp,Vector3.zero);

				//this kinda circulized it
				dist = Mathf.Ceil(dist); 
				if(dist <= radius -1 ){
					newList.Add(temp);
				} else {
					newList.Remove(temp);
				}
			}
		}
		return newList;
	}
	
	
	private List<GameObject> drawRays(Vector3 tempPos , List<Vector3> PointstoSearch){
		List<GameObject> objnear = new List<GameObject>();
		//draw at all points directions 
		foreach(Vector3 a in PointstoSearch){
			//tempPos change position according to array
			RaycastHit hit;
			Ray ray = new Ray(tempPos + a + Vector3.down , Vector3.up ); 
			if (Physics.Raycast(ray, out hit) && hit.collider.transform.tag == "Tile") { //change to include all objects or use layer
				objnear.Add(hit.collider.gameObject);
			}
		}
		return objnear;
	}

	private void ChangeTile(GameObject from , GameObject to){

	}


}


 

	
