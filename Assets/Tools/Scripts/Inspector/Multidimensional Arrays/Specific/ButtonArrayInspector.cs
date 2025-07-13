using System;
using UnityEngine;

namespace Tools.Scripts.Inspector.Multidimensional_Arrays
{
#if UNITY_EDITOR
    public class ButtonArrayInspector<T>:ArrayInspector<T>
    {
        private int _length;
        
        void OnEnable() => SetEnumLength();
        
        private void SetEnumLength() => _length = Enum.GetValues(typeof(T)).Length;
        
        /// <summary>
        /// Draws buttons for the grid's corresponding element
        /// </summary>
        /// <param name="row">Grid row</param>
        /// <param name="column">Grid column</param>
        protected override void DrawElement(int row, int column)
        {
            T value = _arrayData.Grid[row].ArrayColumns[column];
            if (GUILayout.Button(GetTexture(value), GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
            {
                _arrayData.Grid[row].ArrayColumns[column] = NextState(value);
            }
        }
        
        /// <summary>
        /// Gets the state's corresponding texture
        /// Should be replaced by inheritors
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected virtual Texture GetTexture(T state)
        {
            return Texture2D.blackTexture;
        }

        /// <summary>
        /// The next state of T.
        /// Should be replaced by inheritors
        /// </summary>
        /// <param name="value">Enum</param>
        /// <returns></returns>
        protected virtual T NextState(T value) => value;
        
        /// <summary>
        /// Gets the next index of the enum state
        /// </summary>
        /// <param name="index">current index</param>
        /// <returns></returns>
        protected int NextIndex(int index)
        {
            int result = ++index % _length;
            return result;
        }
    }
    #endif
}