using System;
using UnityEditor;
using UnityEngine;

namespace Tools.Scripts.Inspector.Multidimensional_Arrays
{
    public class ArrayInspector<T>:Editor
    {
        protected ArrayData<T> _arrayData;
        private int _length;
        public Texture _enabledSquare;
        public Texture _disabledSquare;
        public Texture _unavailableSquare;
        
        void OnEnable()
        {
            SetEnumLength();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SetArrayTarget();

            UpdateGridSizes();

            DrawGrid();
        }
        
        private void SetArrayTarget() => _arrayData = (ArrayData<T>)target;
        
        private void SetEnumLength() => _length = Enum.GetValues(typeof(T)).Length;
        
        #region Grid Setup
        private void UpdateGridSizes()
        {
            if (IsGridEmpty())
            {
                CreateGrid();
            }
            else if(ShouldResizeRows())
            {
                ResizeRows();
            }
            else if (ShouldResizeColumns())
            {
                ResizeColumns();
            }
        }
        
        #region Rows
        private bool ShouldResizeRows() => _arrayData.Grid.Length != _arrayData.RowsNum;

        /// <summary>
        /// Changes the number of rows to match requested amount
        /// </summary>
        private void ResizeRows()
        {
            int height = _arrayData.Grid.Length;
            bool growing = _arrayData.RowsNum > height;
            Array.Resize(ref _arrayData.Grid, _arrayData.RowsNum);
            if (growing) IncreaseRows();
        }

        /// <summary>
        /// Add more rows and set up the columns in that row.
        /// </summary>
        private void IncreaseRows()
        {
            //Add new rows to array when growing array
            for (int i = _arrayData.Grid.Length; i < _arrayData.RowsNum; i++)
            {
                CreateRow(i);
            }
        }

        private void CreateRow(int row) => _arrayData.Grid[row] = new ArrayRow<T>(_arrayData.ColumnsNum);
        
        #endregion
        
        #region Columns
        
        private bool ShouldResizeColumns() => _arrayData.Grid[0].ArrayColumns.Length != _arrayData.ColumnsNum;

        /// <summary>
        /// Resizing number of columns per row
        /// </summary>
        private void ResizeColumns()
        {
            for (int i = 0; i < _arrayData.Grid.Length; i++)
            {
                Array.Resize(ref _arrayData.Grid[i].ArrayColumns, _arrayData.ColumnsNum);
            }
        }

        private void CreateColumn(int row) => _arrayData.Grid[row].ArrayColumns = new T[_arrayData.ColumnsNum];
        
        #endregion

        private bool IsGridEmpty()
        {
            return _arrayData.Grid == null || _arrayData.Grid.Length == 0 || _arrayData.Grid[0]==null || _arrayData.Grid[0].ArrayColumns.Length == 0;
        }

        /// <summary>
        /// Creates grid with the array's dimensions
        /// </summary>
        private void CreateGrid()
        {
            _arrayData.Grid = new ArrayRow<T>[_arrayData.RowsNum];
            for (int i = 0; i < _arrayData.RowsNum; i++)
            {
                CreateRow(i);
            }
        }
        #endregion

        #region Draw

        /// <summary>
        /// Draws the grid
        /// </summary>
        private void DrawGrid()
        {
            for (int i = 0; i < _arrayData.Grid.Length; i++)
            {
                GUILayout.BeginHorizontal();
                UpdateRow(i);
                for (int j = 0; j < _arrayData.Grid[i].ArrayColumns.Length; j++)
                {
                    DrawButton(i, j);
                }
                GUILayout.EndHorizontal();
            }
        }

        /// <summary>
        /// Updates row sizing if something doesn't match up
        /// </summary>
        /// <param name="row">Row index</param>
        private void UpdateRow(int row)
        {
            if (_arrayData.Grid[row] == null)
            {
                CreateRow(row);
            }
            if (_arrayData.Grid[row].ArrayColumns == null)
            {
                CreateColumn(row);
            }
        }

        /// <summary>
        /// Draws buttons for the grid's corresponding element
        /// </summary>
        /// <param name="row">Grid row</param>
        /// <param name="column">Grid column</param>
        private void DrawButton(int row, int column)
        {
            T value = _arrayData.Grid[row].ArrayColumns[column];
            if (GUILayout.Button(GetTexture(value), GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
            {
                OnButtonClicked(row,column,value);
            }
        }
        
        protected virtual void OnButtonClicked(int row, int column, T value){}
        
        /// <summary>
        /// Gets the state's corresponding texture
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected virtual Texture GetTexture(T state)
        {
            return state switch
            {
                State.Enabled => _enabledSquare,
                State.Disabled => _disabledSquare,
                State.Unavailable or _ => _unavailableSquare
            };
        }
        
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
        #endregion
    }
}