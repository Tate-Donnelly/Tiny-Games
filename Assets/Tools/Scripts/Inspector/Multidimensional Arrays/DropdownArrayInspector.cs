#if UNITY_EDITOR
using System;
using UnityEditor;
#endif
using UnityEngine;

namespace Tools.Scripts.Inspector.Multidimensional_Arrays
{
    #if UNITY_EDITOR
    public class DropdownArrayInspector<T>:ArrayInspector<T>
    {
        /// <summary>
        /// Draws dropdown for the grid's corresponding element
        /// </summary>
        /// <param name="row">Grid row</param>
        /// <param name="column">Grid column</param>
        protected override void DrawElement(int row, int column)
        {
            _arrayData.Grid[row].ArrayColumns[column]=GetEnumPopup(row, column);
        }

        protected virtual T GetEnumPopup(int row, int column)
        {
            return default(T);
            
            //Children should return the following
            //return (T) EditorGUILayout.EnumPopup(_arrayData.Grid[row].ArrayColumns[column]);
        }
    }
    #endif
}