using System;
using System.Collections.Generic;
using Tools.Scripts.Inspector.Multidimensional_Arrays;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SampleArray))]
public class SampleArrayInspector : ArrayInspector<State>
{
    protected override void OnButtonClicked(int row, int column, State value)
    {
        _arrayData.Grid[row].ArrayColumns[column] = (State) NextIndex((int) value);
    }
}
