using System;
using System.Collections.Generic;
using UnityEngine;

namespace Projects.Tile_Game.Scripts
{
    [CreateAssetMenu(fileName = "Level", menuName = "Level Data")]
    public class Level: ScriptableObject
    {
        public int Rows=3;
        public int Columns=3;

        public bool UseSpecificConfig;
        //-1 uninteractable, 0 empty, 1 red
        public Wrapper<TileState>[] BoardConfiguration = new Wrapper<TileState>[3];
    
        public int SolutionTurnNum;

        public void 
            ResetGrid()
        {
            BoardConfiguration = new Wrapper<TileState>[Rows];
            for (int i = 0; i < Rows; i++)
            {
                BoardConfiguration[i] = new Wrapper<TileState>
                {
                    values = new TileState[Columns]
                };
            }
        }
    }
    
    [System.Serializable]
    public class Wrapper<T>
    {
        public T[] values;
    }
}