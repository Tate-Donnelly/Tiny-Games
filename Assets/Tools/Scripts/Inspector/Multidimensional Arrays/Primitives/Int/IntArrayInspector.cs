#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Tools.Scripts.Inspector.Multidimensional_Arrays.Primitives
{
#if UNITY_EDITOR
    [CustomEditor(typeof(IntArray))]
    public class IntArrayInspector:ArrayInspector<int>
    {
        /// <summary>
        /// Draws int for the grid's corresponding element
        /// </summary>
        /// <param name="row">Grid row</param>
        /// <param name="column">Grid column</param>
        protected override void DrawElement(int row, int column)
        {
            _arrayData.Grid[row].ArrayColumns[column]=EditorGUILayout.IntField(_arrayData.Grid[row].ArrayColumns[column]);
        }
    }
#endif
}