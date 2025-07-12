using System.Collections.Generic;
using Tools.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Projects.Tile_Game.Scripts
{
    public class LevelManager : GenericSingleton<LevelManager>
    {
        [SerializeField] private List<Level> _levels;
        public event UnityAction<int> OnNewLevel;
        public event UnityAction<int> OnTurnIncrement;
        
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

        private void IncrementTurnCounter()
        {
            _turnCounter++;
            OnTurnIncrement?.Invoke(_turnCounter);
        }

        public void StartGame() => SetupLevel();
        
        private void NextLevel()
        {
            _currentLevelIndex++;
            if(_currentLevelIndex >= _levels.Count) GameManager.Instance.GameOver();
            else SetupLevel();
        }

        private void SetupLevel()
        {
            _turnCounter = 0;
            Level currentLevel = _levels[_currentLevelIndex];
            TileManager.Instance.LoadLevels(currentLevel);
            OnNewLevel?.Invoke(_currentLevelIndex+1);
            OnTurnIncrement?.Invoke(_turnCounter);
        }

        public void ResetLevel() => SetupLevel();

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
