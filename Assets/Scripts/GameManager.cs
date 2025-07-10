using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int seed = 0;

    void Start()
    {

        setSeed();

    }


    void setSeed()
    {
        if (seed == 0)
            seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);

        Debug.Log("Seed hry: " + seed);
        Random.InitState(seed);        
    }

}
