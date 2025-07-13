using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int seed = 0;
    public string mission;

    void Start()
    {

        mission = "Vohnar";
        getMission();

        setSeed();

    }


    void setSeed()
    {
        if (seed == 0)
            seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);

        Debug.Log("Seed hry: " + seed);
        Random.InitState(seed);
    }
    

    void getMission()
    {
        if (!string.IsNullOrEmpty(mission))
        {
            Debug.Log("Aktuální mise: " + mission);
        }
        else
        {
            Debug.Log("Žádná mise nebyla nastavena.");
        }
    }

}
