using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class RelicUIManager : MonoBehaviour
{
    public static RelicUIManager Instance;
    public Transform relicPanel;
    public GameObject relicIconPrefab;

    void Start()
    {

        // Inicializace UI s relikviemi
        UpdateRelicUI(RelicManager.Instance.activeRelics.Where(r => r.enabled).ToList());

    }

    private void Awake()
    {

        //Singleton
        if (Instance == null) { Instance = this; } else { Destroy(gameObject); }
        
    }


    void Update()
    {

    }

    public void UpdateRelicUI(List<RelicData> enabledRelics)
    {

        // Vyčištění panelu relikvií
        foreach (Transform child in relicPanel) {Destroy(child.gameObject); }

        // Kontrola, zda existují nějaké relikvie k zobrazení
        foreach (var relic in enabledRelics)
        {
            // Kontrola, zda relikvie má obrázek
            if (relic.image == null) { Debug.LogWarning($"Relic {relic.relicName} nemá obrázek!"); }
            if (relic.relicName == null) { Debug.LogWarning($"Relic {relic.relicName} nemá jméno!"); }

            // Vytvoření ikony relikvie
            GameObject icon = Instantiate(relicIconPrefab, relicPanel);

            // Nastavení obrázku a názvu relikvie v ikoně
            icon.GetComponent<Image>().sprite = relic.image;
            icon.GetComponentInChildren<TMP_Text>().text = relic.relicName;


        }

    }
    
    

}