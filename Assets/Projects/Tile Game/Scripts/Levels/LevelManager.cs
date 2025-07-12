using System.Collections.Generic;
using Tools.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Projects.Tile_Game.Scripts
{
    public class LevelManager : GenericSingleton<LevelManager>
    {
        [SerializeField] private List<Level> _levels;
    
        //Internal
        private int _currentLevelIndex = 0;
        private int _turnCounter = 0;

        void Start()
        {
            SetupEvents();
        }

        void SetupEvents()
        {
            TileManager.Instance.OnTileClicked += (row, col) => IncrementTurnCounter();
            TileManager.Instance.OnLevelCompleted += NextLevel;
        }
    
        private void IncrementTurnCounter() => _turnCounter++;

        public void StartGame() => SetupLevel();
        
        private void NextLevel()
        {
            _currentLevelIndex++;
            if(_currentLevelIndex >= _levels.Count) GameManager.Instance.GameOver();
            else SetupLevel();
        }

        private void SetupLevel()
        {
            Level currentLevel = _levels[_currentLevelIndex];
            TileManager.Instance.LoadLevels(currentLevel);
        }

        public void ScoreLevel()
        {
            int optimalTurnNums = _levels[_currentLevelIndex].SolutionTurnNum;
            
            if (optimalTurnNums > _turnCounter)
            {
                //Exceeds best score
            }
            else if (optimalTurnNums == _turnCounter)
            {
                //Matches best score
            }
            else
            {
                //Fails level
            }
        }
    }
}
