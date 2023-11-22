using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour, ISpawner
{
    [SerializeField] private SpawnerConfig spawnerConfig;

    [SerializeField] private GameObject[] creeps;

    public void Spawn(int numberOfCreeps)
    {
        StartCoroutine(SpawnWithInterval(numberOfCreeps));
    }

    private IEnumerator SpawnWithInterval(int numberOfCreeps)
    {
        for (int i = 0; i < numberOfCreeps; i++)
        {
            int creepIndex = 0;
            float probability = Random.Range(0f, 1f);
            if (probability < 0.15f)
            {
                creepIndex = 1;
            }

            Instantiate(creeps[creepIndex], transform.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnerConfig.SpawnInterval);
        }
    }
}
