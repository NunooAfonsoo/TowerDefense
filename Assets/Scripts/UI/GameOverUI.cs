using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameWonPanel;
    [SerializeField] private GameObject gameLostPanel;


    [SerializeField] private VoidEventSO gameWonEvent;
    [SerializeField] private VoidEventSO gameLostEvent;

    private void Awake()
    {
        gameWonEvent.OnEventRaised += GameWon;
        gameLostEvent.OnEventRaised += GameLost;
    }

    private void GameWon()
    {
        gameWonPanel.SetActive(true);
    }

    private void GameLost()
    {
        gameLostPanel.SetActive(true);
    }
}
