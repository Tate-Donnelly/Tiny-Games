using Tools.Scripts;

namespace Projects.Tile_Game.Scripts
{
    public class GameManager : GenericSingleton<GameManager>
    {
        void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            LevelManager.Instance.StartGame();
        }

        public void GameOver()
        {
        
        }
    }
}
