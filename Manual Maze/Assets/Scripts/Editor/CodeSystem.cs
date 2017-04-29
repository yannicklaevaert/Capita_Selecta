using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class CodeSystem : EditorWindow {
	int clones = 1;
	static int sizeMultiplier;

	[MenuItem("Examples/Clone Object")]
	static void Init()
	{
		EditorWindow window = GetWindow(typeof(CodeSystem));
		window.Show();
	}

	void OnGUI()
	{
		sizeMultiplier = EditorGUILayout.IntField("Number of clones:", clones);

		if (GUILayout.Button("Clone!"))
		{
			if (!Selection.activeGameObject)
			{
				Debug.Log("Select a GameObject first");
				return;
			}

			for (var i = 0; i < sizeMultiplier; i++)
				Instantiate(Selection.activeGameObject, Vector3.zero, Quaternion.identity);
		}
	}
}
