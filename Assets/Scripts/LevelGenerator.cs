using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour {

	public GameObject floorPrefab;
	public GameObject wallPrefab;
	public GameObject ceilingPrefab;

	public GameObject characterController;

	public GameObject floorParent;
	public GameObject wallsParent;

	public int holes = 3;//DECLARE THE NUMBER OF HOLES TO SPAWN
	
	public bool generateRoof = true;
	private bool characterPlaced = false;

	public int tilesToRemove = 50;
	public int mazeSize;

	public GameObject pickup;

	private bool[,] mapData;

	private int mazeX = 4, mazeY = 1;

	void Start () 
	{
		mapData = GenerateMazeData();

		//Creates a list that has a size of the value assigned to the variable holesToSpawn
		//and  will contain pair of keys and values of the coordinates of the location of the randomly generated hole location
		List<KeyValuePair<int, int>> holeLocation = new List<KeyValuePair<int, int>>(holes);

		//creates a list that contains pair of keys and values of the walkable path's coordinates
		List<KeyValuePair<int, int>> pathwayLocations = this.noWallPaths();

		for (int k = 0; k < holes; k++)
		{
			//Generates a random number between 0 and the total number of paths with no walls or total number of boolean with false value(walkableLocations)
			int nextHoleLocation = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(0, pathwayLocations.Count);

			//To make sure that the hole won't be generated on same location of the floor where the pickup is
			//and also not to be in the same location of another hole
			while (holeLocation.Contains(pathwayLocations[nextHoleLocation]) || pathwayLocations[nextHoleLocation].Key == mazeX && pathwayLocations[nextHoleLocation].Value == mazeY)
			{
			nextHoleLocation = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(0, pathwayLocations.Count);//continues to generate random number until the location is not  the same with the pickup's and the hole's. 
			}

			holeLocation.Add(pathwayLocations[nextHoleLocation]);
		}



		for (int z = 0; z < mazeSize; z++) 
		{
			for (int x = 0; x < mazeSize; x++) 
			{
				if (mapData[z, x]) 
				{
					CreateChildPrefab(wallPrefab, wallsParent, x, 1, z);
					CreateChildPrefab(wallPrefab, wallsParent, x, 2, z);
					CreateChildPrefab(wallPrefab, wallsParent, x, 3, z);
				} 
				//Only creates a floor if the coordinates are not included in the holeLocation List
				else if (!holeLocation.Contains(new KeyValuePair<int, int>(z,x)))
				{
					// create floor and ceiling
					CreateChildPrefab(floorPrefab, floorParent, x, 0, z);
					if (!characterPlaced)
					{
						// place the character controller on the first empty wall we generate
						characterController.transform.SetPositionAndRotation(
						new Vector3(x, 1, z), Quaternion.identity
					);

						// flag as placed so we never consider placing again
						characterPlaced = true;
					}
				}



				if (generateRoof) 
				{
					CreateChildPrefab(ceilingPrefab, wallsParent, x, 4, z);
				}
			}
		}
		var myPickup = Instantiate(pickup, new Vector3(mazeX, 1, mazeY), Quaternion.identity);
		myPickup.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
	}

	//creates a function that will return the coordinates of the paths with no walls or no walkable paths
	private List<KeyValuePair<int, int>> noWallPaths()
	{
		List<KeyValuePair<int, int>> result = new List<KeyValuePair<int, int>>();
		for (int z = 0; z < mazeSize; z++) 
		{
			for (int x = 0; x < mazeSize;x++)
            {
				if (!mapData[z, x]) result.Add(new KeyValuePair<int, int>(z, x));
            }
		}
		return result;
	}

	bool[,] GenerateMazeData() 
	{
		bool[,] data = new bool[mazeSize, mazeSize];

		for (int y = 0; y < mazeSize; y++) 
		{
			for (int x = 0; x < mazeSize; x++) 
			{
				data[y, x] = true;
			}
		}

		int tilesConsumed = 0;

		while (tilesConsumed < tilesToRemove) 
		{
			
			int xDirection = 0, yDirection = 0;

			if (Random.value < 0.5) 
			{
				xDirection = Random.value < 0.5 ? 1 : -1;
			} 
			else 
			{
				yDirection = Random.value < 0.5 ? 1 : -1;
			}

			int numSpacesMove = (int)(Random.Range(1, mazeSize - 1));

			for (int i = 0; i < numSpacesMove; i++) 
			{
				mazeX = Mathf.Clamp(mazeX + xDirection, 1, mazeSize - 2);
				mazeY = Mathf.Clamp(mazeY + yDirection, 1, mazeSize - 2);

				if (data[mazeY, mazeX]) 
				{
					data[mazeY, mazeX] = false;
					tilesConsumed++;
				}
			}
		}

		return data;
	}

	void CreateChildPrefab(GameObject prefab, GameObject parent, int x, int y, int z) 
	{
		var myPrefab = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
		myPrefab.transform.parent = parent.transform;
	}
}
