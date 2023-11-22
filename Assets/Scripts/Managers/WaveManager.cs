using System.Linq;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    public int WavesNumber { get; private set; } = 3;

    [SerializeField] private ISpawner[] spawners;

    [SerializeField] private IntEventSO gameStartedEvent;
    [SerializeField] private VoidEventSO gameWonEvent;
    [SerializeField] private VoidEventSO gameLostEvent;
    [SerializeField] private IntEventSO waveStartedEvent;
    [SerializeField] private IntEventSO waveEndedEvent;
    [SerializeField] private IntEventSO creepKilledEvent;
    [SerializeField] private IntEventSO remainingCreepsChangedEvent;

    private int currentWaveNumber;
    private int remainingCreeps;
    private bool gameLost;

    protected override void Awake()
    {
        base.Awake();

        currentWaveNumber = 1;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gameStartedEvent.OnEventRaised += SetWavesNumber;
        creepKilledEvent.OnEventRaised += CreepKilled;
        gameLostEvent.OnEventRaised += StopSpawning;
    }

    private void SetWavesNumber(int wavesNumber)
    {
        WavesNumber = wavesNumber;
        spawners = FindObjectsOfType<MonoBehaviour>().OfType<ISpawner>().ToArray();

        StartSpawns();
    }

    private void StartSpawns()
    {
        remainingCreeps = currentWaveNumber * spawners.Length;
        waveStartedEvent.RaiseEvent(remainingCreeps);

        foreach (ISpawner spawner in spawners)
        {
            spawner.Spawn(currentWaveNumber);
        }
    }

    private void WaveEnded()
    {
        if (gameLost)
        {
            return;
        }

        currentWaveNumber++;

        if (currentWaveNumber > WavesNumber)
        {
            gameWonEvent.RaiseEvent();
        }
        else
        {
            waveEndedEvent.RaiseEvent(currentWaveNumber);
            StartSpawns();
        }
    }

    private void CreepKilled(int income)
    {
        remainingCreeps--;
        remainingCreepsChangedEvent.RaiseEvent(remainingCreeps);

        if (remainingCreeps == 0)
        {
            WaveEnded();
        }
    }

    private void StopSpawning()
    {
        gameLost = true;
    }
}
