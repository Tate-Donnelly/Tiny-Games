using System;
using System.Collections.Generic;
using Tools.Scripts.Inspector.Multidimensional_Arrays;
using UnityEngine;

namespace Projects.Tile_Game.Scripts
{
    [CreateAssetMenu(fileName = "Level", menuName = "Assets/Tile Game/Level Data")]
    public class Level: SampleArray
    {
        public int SolutionTurnNum;
        public bool UseSpecificConfig;
    }
}