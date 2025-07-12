using System;
using UnityEditor;
using UnityEngine;

namespace Projects.Tile_Game.Scripts
{
    [CustomEditor(typeof(Level))]
    public class LevelInspector: Editor
    {
        SerializedProperty grid;
        SerializedProperty array;
        SerializedProperty rows;
        SerializedProperty cols;
        
        Texture2D texOn;
        private Texture2D texOff;
        private Texture2D texInvisible;
        
        private Level level;
 
        int length;
 
        private void OnEnable()
        {
            grid = serializedObject.FindProperty("BoardConfiguration");
            length = Enum.GetValues(typeof(TileState)).Length;
        }
 
        public override void OnInspectorGUI()
        {
            GUILayout.BeginVertical();
            base.OnInspectorGUI();
            serializedObject.Update();
            texOff = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Projects/Tile Game/Art/black.png",
                typeof(Texture2D));
            texOn = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Projects/Tile Game/Art/red.png",
                typeof(Texture2D));
            texInvisible = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Projects/Tile Game/Art/gray.png",
                typeof(Texture2D));
 
            level = (Level)target;
            
            rows = serializedObject.FindProperty("Rows");
            cols = serializedObject.FindProperty("Columns");
 
            DrawGrid();
 
            GUILayout.EndVertical();
            if (GUILayout.Button("Reset"))
                level.ResetGrid();
 
            serializedObject.ApplyModifiedProperties();
        }
 
        private void DrawGrid()
        {
            if (level.Rows == 0) return;
            
            if (level.BoardConfiguration.Length == 0)
            {
                level.ResetGrid();
            }
            Debug.Log(grid.arraySize);
            
            for (int i = 0; i < 2; i++)
            {
                GUILayout.BeginHorizontal();
                array = grid.GetArrayElementAtIndex(i).FindPropertyRelative("values");
                for (int j = 0; j < 2; j++)
                {
                    var value = array.GetArrayElementAtIndex(j);
                    TileState element = (TileState)value.intValue;
                    
                    if (GUILayout.Button(new GUIContent(GetTexture(element)), GUILayout.MaxWidth(30), GUILayout.MaxHeight(30)))
                    {
                        value.intValue = NextIndex(value.intValue);
                    }
                }
                GUILayout.EndHorizontal();
            }
        }

        private GUIStyle GetButtonStyle(TileState tileState)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            Texture2D texture2D = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Projects/Tile Game/Art/red.png", typeof(Texture2D));
            texture2D.Apply();
            buttonStyle.normal.background = texture2D;
            buttonStyle.active.background = texture2D;
            buttonStyle.hover.background = texture2D;
            buttonStyle.focused.background = texture2D;
            return buttonStyle;
        }

        private Texture2D GetTexture(TileState tileState)
        {
            return tileState switch
            {
                TileState.OFF => texOff,
                TileState.ON => Texture2D.redTexture,
                TileState.INVISIBLE or _ => Texture2D.grayTexture
            };
        }
 
        private int NextIndex(int index)
        {
            int result = ++index % length;
            return result;
        }
    }
}