
using UnityEngine;
#if UNITY_EDITOR
using Tools.Scripts.Inspector.Multidimensional_Arrays;
using UnityEditor;
#endif

namespace Projects.Tile_Game.Scripts.Levels
{
#if (UNITY_EDITOR)
    [CustomEditor(typeof(Level))]
    public class TileLevelInspector: ButtonArrayInspector<TileState>
    {
        public Texture _enabledSquare;
        public Texture _disabledSquare;
        public Texture _unavailableSquare;
        
        protected override Texture GetTexture(TileState value)
        {
            return value switch
            {
                TileState.OFF => _disabledSquare,
                TileState.ON => _enabledSquare,
                TileState.INVISIBLE or _ => _unavailableSquare
            };
        }

        protected override TileState NextState(TileState value) =>  (TileState) NextIndex((int) value);
    }
#endif
}