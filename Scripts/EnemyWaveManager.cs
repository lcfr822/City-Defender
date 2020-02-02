using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    private bool waveRunning = false;
    private int totalEnemies = 3;
    private Coroutine waveRoutine = null;

    public int waveNumber = 0;
    public int kills = 0;
    public int shots = 0;
    public int hits = 0;
    public int existingEnemies = 0;
    public GameObject[] enemyAircraft = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        StartNewWave();
    }

    // Update is called once per frame
    void Update()
    {
        if(existingEnemies <= 0 && waveRunning)
        {
            waveRunning = false;
            StartNewWave();
        }
    }

    private void StartNewWave()
    {
        waveNumber++;
        totalEnemies = Mathf.RoundToInt(waveNumber * 2.0f);
        waveRoutine = StartCoroutine(Spawn(totalEnemies, 3.0f));
    }

    private IEnumerator Spawn(int count, float delay)
    {
        if (waveRunning) { StopCoroutine(waveRoutine); }

        for (int i = 0; i < count; i++)
        {
            GameObject newAircraft = Instantiate(enemyAircraft[ 0 /*Random.Range(0, 2)*/], transform.position, Quaternion.identity);
            existingEnemies++;
            newAircraft.name = newAircraft.name.Split('(')[0] + "_" + existingEnemies;
            yield return new WaitForSeconds(Random.Range(delay - (delay / 10.0f), delay +  (delay / 10.0f)));
        }

        waveRoutine = null;
        waveRunning = true;
        Debug.Log("Starting wave: " + waveNumber + "\nKills Required: " + totalEnemies);
    }
}
