using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeckEditor : MonoBehaviour
{
    [SerializeField] private List<CardBase> baseDeck;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject TogglePrefab;

    [Header("Deck")]
    [SerializeField] private List<CardBase> playingDeck = new List<CardBase>();
    [SerializeField] private Transform deckArea;

    [Header("Menu")]
    [SerializeField] private Transform menuArea;
    [SerializeField] private TextMeshProUGUI menuText;

    private CardDisplay currCard;

    public static List<CardBase> savedDeck;

    public void AddCard(CardBase card)
    {
        if (playingDeck.Count >= 12)
        {
            return;
        }

        CardBase newCard = Instantiate(card);
        playingDeck.Add(newCard);
        UpdateDeckUI();
    }

    public void RemoveCard(Transform card)
    {
        playingDeck.Remove(card.GetComponent<CardDisplay>().GetData());
        card.SetParent(null);
        Destroy(card);
        UpdateDeckUI();
    }

    private void UpdateDeckUI()
    {
        foreach (Transform child in deckArea)
        {
            Destroy(child.gameObject);
        }

        foreach (CardBase card in playingDeck)
        {
            GameObject newCard = Instantiate(cardPrefab, deckArea);
            newCard.GetComponent<CardDisplay>().SetData(card);
            newCard.GetComponent<CardDisplay>().UpdateVisuals();
        }
    }

    public void ToggleReceptor(GameObject toggle)
    {
        ReceptorType r = toggle.GetComponentInParent<ReceptorData>().rec;
        currCard.GetData().toggleReceptor(r);
    }

    public void SpawnReceptorMenu(CardDisplay card)
    {
        currCard = card;

        Clear(menuArea);

        menuText.text = "Receptor Editor";
        
        foreach (ReceptorType r in Enum.GetValues(typeof(ReceptorType)))
        {
            if (card.GetData().Allowed.Contains(r))
            {
                GameObject newToggle = Instantiate(TogglePrefab, menuArea);
                newToggle.GetComponent<ReceptorData>().rec = r;
                newToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(card.GetData().Unlocked.Contains(r));
                newToggle.GetComponent<Toggle>().onValueChanged.AddListener((isOn) => ToggleReceptor(newToggle));
            }
        }
    }

    public void Clear(Transform obj)
    {
        foreach(Transform child in obj)
        {
            child.SetParent(null);
            Destroy(child);
        }
    }

    public void SaveDeck()
    {
        savedDeck = new List<CardBase>(playingDeck);
        SceneManager.LoadScene("Scene_01");
    }
}