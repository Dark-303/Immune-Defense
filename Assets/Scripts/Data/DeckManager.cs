using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject cardPrefab;

    [Header("Hand")]
    [SerializeField] private Transform handArea;
    [SerializeField] private int handSize = 3;

    [Header("Deck")]
    [SerializeField] private List<CardData> deck = new List<CardData>();
    [SerializeField] private int redrawCost = 2;

    private List<CardData> masterDeck;

    private void Start()
    {
        masterDeck = new List<CardData>(deck);
        Shuffle();
        DrawHand();
    }

    private void Shuffle()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            CardData temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    private void ClearHand()
    {
        foreach (Transform child in handArea)
        {
            Destroy(child.gameObject);
        }
    }

    private void ClearPlayedHand()
    {
        foreach (Transform child in handArea)
        {
            if (child.GetComponent<CardDisplay>().IsSelected())
            {
                deck.Add(child.GetComponent<CardDisplay>().GetData());
                Destroy(child.gameObject);
            }
        }
    }

    private bool DrawCard()
    {
        if (deck.Count > 0)
        {
            CardData data = deck[0];
            deck.RemoveAt(0);

            GameObject newCard = Instantiate(cardPrefab, handArea);
            CardDisplay display = newCard.GetComponent<CardDisplay>();
            display.SetData(data);
            display.UpdateVisuals();
            return true;
        }
        return false;
    }

    public void DrawHand()
    {
        ClearPlayedHand();
        for(int i = 0; i < handSize - handArea.childCount; i++)
        {
            if (!DrawCard())
            {
                break;
            }
        }
    }

    
}
