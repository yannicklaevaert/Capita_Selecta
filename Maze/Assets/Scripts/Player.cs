using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Player : MonoBehaviour {

	private MazeCell currentCell;

	private MazeDirection currentDirection;

	public void SetLocation (MazeCell cell) {
		if (currentCell != null) {
			currentCell.OnPlayerExited();
		}
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
		currentCell.OnPlayerEntered();
	}

	private void Move (MazeDirection direction) {
		MazeCellEdge edge = currentCell.GetEdge(direction);
		if (edge is MazePassage) {
			MazeCell c = edge.otherCell ;
			if (c){
				if (c.GetEdge(direction) is MazeDoor){
				Debug.Log("enter");
				Debug.Log("check");
				var editorAsm = typeof(Editor).Assembly;
 				var inspWndType = editorAsm.GetType("UnityEditor.InspectorWindow");
				 var window = EditorWindow.GetWindow<EditorGUILayoutIntField>();
			}
				SetLocation(edge.otherCell);
			}
			else{
				print("exit");
				EditorUtility.DisplayDialog ("Victory", "Je bent gewonnen!", "Next Level", "Quit");
			}
			
		}
	}

	private void Rotate (MazeDirection direction) {
		transform.localRotation = direction.ToRotation();
		currentDirection = direction;
	}

	private void Update () {
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			Move(currentDirection);
		}
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			// Move(currentDirection.GetNextClockwise());
			Rotate(currentDirection.GetNextClockwise());
		}
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
			// Move(currentDirection.GetOpposite());
			Rotate(currentDirection.GetNextCounterclockwise());
			Rotate(currentDirection.GetNextCounterclockwise());
		}
		else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			// Move(currentDirection.GetNextCounterclockwise());
			Rotate(currentDirection.GetNextCounterclockwise());
		}
		else if (Input.GetKeyDown(KeyCode.Q)) {
			Rotate(currentDirection.GetNextCounterclockwise());
		}
		else if (Input.GetKeyDown(KeyCode.E)) {
			Rotate(currentDirection.GetNextClockwise());
		}
	}
}
