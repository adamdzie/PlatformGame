using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemy;
        public int count;
        public float rate;
    }
    public Text wave_string;
    public Wave[] waves;
    private int nextWave = 0;
    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    private float searchCountdown = 1f;
    public int wave_counter;

    
    public Transform[] spawnPoints;

    public SpawnState state = SpawnState.COUNTING;

    // Start is called before the first frame update
    void Start()
    {
        wave_string = GameObject.Find("Wave_info").GetComponent<Text>();
        
        wave_counter = 1;
        wave_string.text = "Wave " + wave_counter.ToString();
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points");
        }
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        wave_counter++;
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if(nextWave+1 > waves.Length - 1)
        {
            //ending screen
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
        WaveCounterUIUpdate();

    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave (Wave wave)
    {
        state = SpawnState.SPAWNING;

        for(int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy[Random.Range(0, wave.enemy.Length)]);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy (Transform enemy)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
    public void WaveCounterUIUpdate()
    {
        wave_string.text = "Wave " + wave_counter.ToString();
    }
}