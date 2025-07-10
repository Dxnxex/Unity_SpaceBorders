using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 1.5f;
    public float spawnMargin = 1f; // přesah mimo obrazovku

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnAsteroid();
            timer = 0f;
        }
    }

    void SpawnAsteroid()
    {
        // Levý a pravý okraj obrazovky ve světových souřadnicích
        Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f));
        Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        float randomX = Random.Range(left.x + spawnMargin, right.x - spawnMargin);
        float spawnY = left.y + spawnMargin;

        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
    }
}