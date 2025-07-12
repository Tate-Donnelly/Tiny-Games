using System;
using System.Collections.Generic;
using Tools.Scripts.Inspector.Multidimensional_Arrays;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SampleArray))]
public class SampleArrayInspector : Editor
{
    SampleArray _sampleArray;

    private int _length;
    public Texture _enabledSquare;
    public Texture _disabledSquare;
    public Texture _unavailableSquare;
    
    void OnEnable()
    {
        _length = Enum.GetValues(typeof(Active)).Length;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        _sampleArray = (SampleArray)target;

        UpdateGridSizes();

        DrawGrid();
    }

    private void UpdateGridSizes()
    {
        if (CheckGrid())
        {
            CreateGrid();
        }
        else if(_sampleArray.Grid.Length != _sampleArray.RowsNum)
        {
            //resizing number of rows
            int oldHeight = _sampleArray.Grid.Length;
            bool growing = _sampleArray.RowsNum > oldHeight;
            System.Array.Resize(ref _sampleArray.Grid, _sampleArray.RowsNum);
            if (growing)
            {
                //Add new rows to array when growing array
                for (int i = oldHeight; i < _sampleArray.RowsNum; i++)
                {
                    _sampleArray.Grid[i] = new ArrayRow<Active>();
                    _sampleArray.Grid[i].ArrayColumns = new Active[_sampleArray.ColumnsNum];
                }
            }
        }else if (_sampleArray.Grid[0].ArrayColumns.Length != _sampleArray.ColumnsNum)
        {
            //resizing number of entries per row
            for (int i = 0; i < _sampleArray.Grid.Length; i++)
            {
                System.Array.Resize(ref _sampleArray.Grid[i].ArrayColumns, _sampleArray.ColumnsNum);
            }
        }
    }

    private bool CheckGrid()
    {
        return _sampleArray.Grid == null || _sampleArray.Grid.Length == 0 || _sampleArray.Grid[0]==null || _sampleArray.Grid[0].ArrayColumns.Length == 0;
    }

    private void CreateGrid()
    {
        _sampleArray.Grid = new ArrayRow<Active>[_sampleArray.RowsNum];
        for (int i = 0; i < _sampleArray.RowsNum; i++)
        {
            _sampleArray.Grid[i] = new ArrayRow<Active>();
            _sampleArray.Grid[i].ArrayColumns = new Active[_sampleArray.ColumnsNum];
        }
    }

    private void DrawGrid()
    {
        //Draw popups
        for (int i = 0; i < _sampleArray.Grid.Length; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < _sampleArray.Grid[i].ArrayColumns.Length; j++)
            {
                Active value = _sampleArray.Grid[i].ArrayColumns[j];
                if (GUILayout.Button(GetTexture(value), GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
                {
                    _sampleArray.Grid[i].ArrayColumns[j] = (Active) NextIndex((int) value);
                }
            }

            GUILayout.EndHorizontal();
        }
    }

    private Texture GetTexture(Active state)
    {
        return state switch
        {
            Active.Enabled => _enabledSquare,
            Active.Disabled => _disabledSquare,
            Active.Unavailable or _ => _unavailableSquare
        };
    }

    private int NextIndex(int index)
    {
        int result = ++index % _length;
        return result;
    }
}
