using System.Collections.Generic;
using Tools.Scripts;
using Tools.Scripts.Inspector.Multidimensional_Arrays;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Projects.Tile_Game.Scripts
{
    public class TileManager : GenericSingleton<TileManager>
    {
        [SerializeField] private RectTransform _tilesParent;
        [SerializeField] private Tile _tilePrefab;
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private List<List<Tile>> tiles = new List<List<Tile>>();

        public float MinTileWidth;
        public float MinTileHeight;
        public float MaxTileWidth;
        public float MaxTileHeight;

        public int MaxRows = 10;
        public int MaxCols = 10;
        
        private int _row;
        private int _col;

        public event UnityAction<int,int> OnTileClicked;
        public event UnityAction OnLevelCompleted;

        void Start()
        {
            Init();
        }

        void Init()
        {
            for (int r = 0; r < MaxRows; r++)
            {
                tiles.Add(new List<Tile>());
                for (int c = 0; c < MaxCols; c++)
                {
                    Tile tile = Instantiate(_tilePrefab, _tilesParent);
                    tile.Init(r,c);
                    tiles[r].Add(tile);
                    tile.gameObject.SetActive(false);
                }
            }
        }

        public void LoadLevels(Level level)
        {
            if (level.Grid == null)
            {
                _col = level.ColumnsNum;
                _row = level.RowsNum;
            }
            else
            {
                _col = level.Grid.Length;
                _row = level.Grid.Length;
            }
            
            SetupTiles(level);
        }

        private void SetupTiles(Level level)
        {
            float newWidth = Mathf.Clamp((_tilesParent.rect.width/_col) - 5,MinTileWidth,MaxTileWidth);
            float newHeight = Mathf.Clamp((_tilesParent.rect.height/_col) - 5, MinTileHeight,MaxTileHeight);

            _gridLayout.constraintCount = _col;
            _gridLayout.cellSize=new Vector3(newWidth,newHeight);
            
            for (int r = 0; r < MaxRows; r++)
            {
                for (int c = 0; c < MaxCols; c++)
                {
                    if (r >= _row || c >= _col)
                    {
                        tiles[r][c].gameObject.SetActive(false);
                    }
                    else
                    {
                        if (level.UseSpecificConfig)
                        {
                            tiles[r][c].SetState(GetState(level.Grid,r,c));
                        }
                        else
                        {
                            tiles[r][c].SetState(TileState.OFF);
                        }
                        tiles[r][c].gameObject.SetActive(true);
                    }
                }
            }
        }

        private TileState GetState(ArrayRow<TileState>[] boardConfig, int row, int col)
        {
            return boardConfig[row].ArrayColumns[col];
        }

        public void InvokeOnTileClicked(int row, int col)
        {
            OnTileClicked?.Invoke(row,col);
        
            if (CheckAllToggledOff())
            {
                OnLevelCompleted?.Invoke();
            }
        }

        private bool CheckAllToggledOff()
        {
            foreach (List<Tile> tileList in tiles)
            {
                foreach (Tile tile in tileList)
                {
                    if (tile.State == TileState.OFF && tile.gameObject.activeSelf) return false;
                }
            }
            return true;
        }
    }
}
