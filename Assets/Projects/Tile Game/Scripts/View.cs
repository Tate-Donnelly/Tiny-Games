using Projects.Tile_Game.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelTracker;
    [SerializeField] private TextMeshProUGUI _turnCounter;
    [SerializeField] private Button _resetButton;

    void Start()
    {
        LevelManager.Instance.OnNewLevel += UpdateLevelTracker;
        LevelManager.Instance.OnTurnIncrement += UpdateTurnCounter;
        _resetButton.onClick.AddListener(LevelManager.Instance.ResetLevel);
    }

    private void UpdateTurnCounter(int currentTurn)
    {
        _turnCounter.text = $"Turns: {currentTurn.ToString()}";
    }

    private void UpdateLevelTracker(int currentLevel)
    {
        _levelTracker.text = $"Level: {currentLevel.ToString()}";
    }
}
