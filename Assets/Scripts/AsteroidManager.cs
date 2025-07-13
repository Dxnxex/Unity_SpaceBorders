using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public AsteroidWaveData waveData;
    private float timer = 0f;
    private int nextSpawnIndex = 0;
    private float spawnMargin = 1f;

    void Start()
    {
        startMission();
    }



    void Update()
    {
        timer += Time.deltaTime;

        if (nextSpawnIndex < waveData.spawnTimes.Count && timer >= waveData.spawnTimes[nextSpawnIndex])
        {
            SpawnAsteroid(nextSpawnIndex);
            nextSpawnIndex++;
        }
    }

    void SpawnAsteroid(int index)
    {
        // Levý a pravý okraj obrazovky ve světových souřadnicích
        Vector3 left =  Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f));
        Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        // Náhodná X souřadnice v rozsahu s přesahem mimo obrazovku
        float randomX = Random.Range(left.x + spawnMargin, right.x - spawnMargin);
        float spawnY = left.y + spawnMargin;
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        // Vytvoření asteroidu
        Instantiate(waveData.asteroidPrefabs[index], spawnPosition, Quaternion.identity);
    }


    void startMission()
    {
        
        //Nalézt data
        string missionName = FindFirstObjectByType<GameManager>().mission;
        waveData = Resources.Load<AsteroidWaveData>("Waves/AsteroidWave_" + missionName);

        // Kontrola, zda byla data nalezena
        if (waveData == null) { Debug.LogError("Wave data pro misi '" + missionName + "' nebyla nalezena!"); }
        else
        {
            Debug.Log("Wave data pro misi '" + missionName + "' byla úspěšně načtena.");
            timer = 0f;
            nextSpawnIndex = 0;
        }
    }

}