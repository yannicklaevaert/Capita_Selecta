using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Maze : MonoBehaviour {

	public IntVector2 size;

	public MazeCell cellPrefab;
	public MazePassage passagePrefab;
	public MazeWall[] wallPrefabs;
	public MazeDoor doorPrefab;
	public MazeRoomSettings[] roomSettings;
	private MazeCell[,] cells;
	private List<MazeRoom> rooms = new List<MazeRoom> ();
	public float generationStepDelay;
	



	[Range(0f, 1f)]
	public float doorProbability;

	public IEnumerator Generate () {
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
		cells = new MazeCell[size.x, size.z];
		IntVector2 coordinates = RandomCoordinates;
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep(activeCells);
		}
		CreateExit();
	}

	private void DoFirstGenerationStep (List<MazeCell> activeCells) {
		MazeCell newCell = CreateCell (RandomCoordinates);
		newCell.Initialize (CreateRoom (-1));
		activeCells.Add(newCell);
	}

	private void DoNextGenerationStep (List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt(currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
		if (ContainsCoordinates(coordinates)) {
			MazeCell neighbor = GetCell(coordinates);
			if (neighbor == null) {
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else if (currentCell.room.settingsIndex == neighbor.room.settingsIndex) {
				CreatePassageInSameRoom (currentCell, neighbor, direction);
			}
			else {
				CreateWall(currentCell, neighbor, direction);
			}
		}
		else {
			CreateWall(currentCell, null, direction);
		}
	}

	private MazeCell CreateCell (IntVector2 coordinates) {
		MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
		cells[coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}

	private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
		MazePassage passage = Instantiate(prefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(prefab) as MazePassage;
		if (passage is MazeDoor) {
			otherCell.Initialize (CreateRoom (cell.room.settingsIndex));
		} else {
			otherCell.Initialize (cell.room);
		}
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}



	private void CreateExitPassage(MazeCell cell, MazeDirection direction){
		//Maak passage
		MazePassage passage = Instantiate(passagePrefab) as MazePassage;
		//Maak passage in huidige cell
		passage.Initialize(cell, null, direction);

	}


	private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazeWall wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as MazeWall;
		wall.Initialize(cell, otherCell, direction);
		if (otherCell != null) {
			wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as MazeWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}

	public IntVector2 RandomCoordinates {
		get {
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}

	public IntVector2 StartCoordinates{
		get {
			return new IntVector2(size.x/2, size.z/2);
		}
	}

	public IntVector2 CreateExit() {
		//Kies tussen horizontaal (0) of verticaal (1)
		int random_edge = Random.Range(0,2);
		//Kies boven-onder of links-rechts
		int random_place = Random.Range(0,2);
		IntVector2 coordinates =  new IntVector2(Random.Range(0, size.x), 0);
		MazeDirection direction = MazeDirection.North;

		if (random_edge == 0){
			if (random_place == 0){
				//Onderste rij
				coordinates =  new IntVector2(Random.Range(0, size.x), 0);
				direction = MazeDirection.South;
			}
			else{
				//Bovenste rij
				coordinates =  new IntVector2(Random.Range(0, size.x), size.z-1);
				direction = MazeDirection.North;
			}
		}
		else{
			if (random_place == 0){
				//linker kolom
				coordinates =  new IntVector2(0, Random.Range(0, size.z));
				direction = MazeDirection.West;
			}
			else{
				//Rechter kolom
				coordinates =  new IntVector2(size.x - 1, Random.Range(0, size.z));
				direction = MazeDirection.East;
			}
		}
		//Get cell at this coordinates
		MazeCell currentCell = cells[coordinates.x, coordinates.z];
		print("x-as op de onderkant van de kaart, z-as is de linkerkant");
		print("Uitgang op: " + coordinates.x +","+ coordinates.z);
		CreateExitPassage(currentCell, direction);
		return coordinates;
	}

	public bool ContainsCoordinates (IntVector2 coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}

	public MazeCell GetCell (IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.z];
	}

	private MazeRoom CreateRoom (int indexToExclude) {
		MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom> ();
		newRoom.settingsIndex = Random.Range (0, roomSettings.Length);
		if (newRoom.settingsIndex == indexToExclude) {
			newRoom.settingsIndex = (newRoom.settingsIndex + 1) % roomSettings.Length;
		}
		newRoom.settings = roomSettings [newRoom.settingsIndex];
		rooms.Add (newRoom);
		return newRoom;
	}

	private void CreatePassageInSameRoom (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
		if (cell.room != otherCell.room) {
			MazeRoom roomToAssimilate = otherCell.room;
			cell.room.Assimilate(roomToAssimilate);
			rooms.Remove(roomToAssimilate);
			Destroy(roomToAssimilate);
		}
	}
}


