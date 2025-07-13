using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AsteroidWave", menuName = "Game/Asteroid Wave")]
public class AsteroidWaveData : ScriptableObject
{
    public List<float> spawnTimes = new List<float>();
    public List<GameObject> asteroidPrefabs = new List<GameObject>();


}