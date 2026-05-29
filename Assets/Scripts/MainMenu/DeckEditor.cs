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
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject menuItemPrefab;

    [Header("Deck")]
    [SerializeField] private List<CardBase> playingDeck = new List<CardBase>();
    [SerializeField] private Transform deckArea;

    [Header("Passive Deck")]
    [SerializeField] private List<PassiveCardBase> passiveDeck = new List<PassiveCardBase>();
    [SerializeField] private Transform passiveDeckArea;

    [Header("Menu")]
    [SerializeField] private Transform menuArea;
    [SerializeField] private TextMeshProUGUI menuText;

    private CardDisplay currCard;

    public static List<CardBase> savedDeck;
    public static List<PassiveCardBase> savedPassiveDeck;

    private void Start()
    {
        if (savedDeck != null)
        {
            playingDeck = new List<CardBase>(savedDeck);
        }
        if (savedPassiveDeck != null)
        {
            passiveDeck = new List<PassiveCardBase>(savedPassiveDeck);
        }
        SpawnDeckMenu();
        UpdateDeckUI();
    }

    private void AddCard(CardBase card)
    {
        if (card is PassiveCardBase passive)
        {
            if (passiveDeck.Count >= 3)
            {
                return;
            }
            PassiveCardBase newCard = Instantiate(passive);
            passiveDeck.Add(newCard);
        }
        else
        {
            if (playingDeck.Count >= 12)
            {
                return;
            }
            CardBase newCard = Instantiate(card);
            playingDeck.Add(newCard);
        }
        UpdateDeckUI();
    }

    private void RemoveCard(CardBase card)
    {
        if (playingDeck.Contains(card))
        {
            playingDeck.Remove(card);
        }
        else if (card is PassiveCardBase passive && passiveDeck.Contains(passive))
        {
            passiveDeck.Remove(passive);
        }
        UpdateDeckUI();
    }

    private void UpdateDeckUI()
    {
        Clear(deckArea);
        Clear(passiveDeckArea);

        foreach (CardBase card in playingDeck)
        {
            CreateCard(card, deckArea);
        }

        foreach (PassiveCardBase card in passiveDeck)
        {
            CreateCard(card, passiveDeckArea);
        }
    }

    private void CreateCard(CardBase card, Transform area)
    {
        GameObject newCard = Instantiate(cardPrefab, area);
        CardDisplay display = newCard.GetComponent<CardDisplay>();
        display.SetData(card);
        display.UpdateVisuals();
        newCard.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            if (currCard != null && currCard == display)
            {
                SpawnDeckMenu();
            }
            else
            {
                SpawnReceptorMenu(display);
            }
        });
    }

    private void SpawnDeckMenu()
    {
        if (currCard != null && currCard.IsSelected()) currCard.OnCardClicked();
        currCard = null;
        Clear(menuArea);
        menuText.text = "Deck Editor";

        foreach (CardBase card in baseDeck)
        {
            GameObject newItem = Instantiate(menuItemPrefab, menuArea);
            CardDisplay itemDisplay = newItem.GetComponentInChildren<CardDisplay>();
            itemDisplay.SetData(card);
            itemDisplay.UpdateVisuals();
            newItem.GetComponentInChildren<Button>().onClick.AddListener(() => AddCard(itemDisplay.GetData()));
        }
    }

    private void ToggleReceptor(GameObject toggle)
    {
        ReceptorType r = toggle.GetComponentInParent<ReceptorData>().rec;
        currCard.GetData().toggleReceptor(r);
    }

    private void SpawnReceptorMenu(CardDisplay card)
    {
        if (currCard != null && currCard.IsSelected()) currCard.OnCardClicked();
        currCard = card;

        Clear(menuArea);

        menuText.text = "Receptor Editor";

        GameObject delete = Instantiate(buttonPrefab, menuArea);
        Button button = delete.GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            RemoveCard(card.GetData());
            SpawnDeckMenu();
        });

        foreach (ReceptorType r in Enum.GetValues(typeof(ReceptorType)))
        {
            if (card.GetData().Allowed != null && card.GetData().Allowed.Contains(r))
            {
                GameObject newToggle = Instantiate(TogglePrefab, menuArea);
                newToggle.GetComponent<ReceptorData>().rec = r;
                newToggle.GetComponentInChildren<TextMeshProUGUI>().text = r.ToString();
                newToggle.GetComponentInChildren<Toggle>().SetIsOnWithoutNotify(card.GetData().Unlocked.Contains(r));
                newToggle.GetComponentInChildren<Toggle>().onValueChanged.AddListener((isOn) => ToggleReceptor(newToggle));
            }
        }
    }

    private void Clear(Transform obj)
    {
        for (int i = obj.childCount - 1; i >= 0; i--)
        {
            Destroy(obj.GetChild(i).gameObject);
        }
    }

    public void SaveDeck()
    {
        savedDeck = new List<CardBase>(playingDeck);
        savedPassiveDeck = new List<PassiveCardBase>(passiveDeck);
        SceneManager.LoadScene("Scene_01");
    }
}