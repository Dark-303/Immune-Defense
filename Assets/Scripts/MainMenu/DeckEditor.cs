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

    [Header("Menu")]
    [SerializeField] private Transform menuArea;
    [SerializeField] private TextMeshProUGUI menuText;

    private CardDisplay currCard;

    public static List<CardBase> savedDeck;

    private void Start()
    {
        if (savedDeck != null)
        {
            playingDeck = new List<CardBase>(savedDeck);
        }
        SpawnDeckMenu();
    }

    private void AddCard(CardBase card)
    {
        if (playingDeck.Count >= 12)
        {
            return;
        }

        CardBase newCard = Instantiate(card);
        playingDeck.Add(newCard);
        UpdateDeckUI();
    }

    private void RemoveCard(CardBase card)
    {
        playingDeck.Remove(card);
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
    }

    private void SpawnDeckMenu()
    {
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
                newToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(card.GetData().Unlocked.Contains(r));
                newToggle.GetComponent<Toggle>().onValueChanged.AddListener((isOn) => ToggleReceptor(newToggle));
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
        SceneManager.LoadScene("Scene_01");
    }
}