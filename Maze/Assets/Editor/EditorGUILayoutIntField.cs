 using UnityEngine;
 using System.Collections;
 using UnityEditor;
 
 // Editor Script that clones the selected GameObject a number of times.
 
 class EditorGUILayoutIntField : EditorWindow {
     
     int clones = 1;
     
     [MenuItem("Examples/Clone Object")]
     
     static void Init() {
         EditorWindow window = GetWindow<EditorGUILayoutIntField>();
         window.Show();
     }
     
     void OnGUI() {
         int sizeMultiplier = EditorGUILayout.IntField("Number of clones:", clones);
         if(GUILayout.Button("Clone!"))
             for(var i = 0; i < clones; i++)
                 Instantiate(Selection.activeGameObject, Vector3.zero, Quaternion.identity);
     }
 }