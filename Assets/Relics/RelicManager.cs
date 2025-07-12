using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    // Singleton instance - jednoduchý přístup odkudkoliv
    public static RelicManager Instance;

    // Seznam aktivních relikvií (např. IcyCoreRelic.asset, EmpowerRelic.asset, ...)
    public List<RelicData> activeRelics = new List<RelicData>();

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Zjistí, jestli je relikvie typu T aktivní a nezískaná
    public bool isRelicEnabled<T>() where T : RelicData
    {
        return activeRelics.Any(r => r is T && r.enabled && !r.obtained);
    }

    public T GetRelic<T>() where T : RelicData
    {
        return activeRelics.OfType<T>().FirstOrDefault();
    }


    public void obtainRelic<T>() where T : RelicData
    {
        var relic = GetRelic<T>();
        if (relic != null)
        {
            relic.obtained = true;
            Debug.Log($"Obtained relic: {relic.relicName}");
        }
        else
        {
            Debug.LogWarning($"No active relic of type {typeof(T).Name} found to obtain.");
        }
    }
    

}
