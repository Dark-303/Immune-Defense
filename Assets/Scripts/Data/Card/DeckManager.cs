using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject cardPrefab;

    [Header("Hand")]
    [SerializeField] private Transform handArea;
    [SerializeField] private int handSize = 3;

    [Header("Factory")]
    [SerializeField] private Transform factoryArea;

    [Header("Deck")]
    [SerializeField] private List<CardBase> deck = new List<CardBase>();
    [SerializeField] private int redrawCost = 2;

    private List<CardBase> masterDeck;

    private void Start()
    {
        masterDeck = new List<CardBase>(deck);
        Shuffle();
        DrawHand();
    }

    private void Shuffle()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            CardBase temp = deck[i];
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
            CardBase data = deck[0];
            deck.RemoveAt(0);

            GameObject newCard = Instantiate(cardPrefab, handArea);
            CardDisplay display = newCard.GetComponent<CardDisplay>();
            display.SetData(data);
            display.UpdateVisuals();
            return true;
        }
        return false;
    }

    public List<PlasmacyteData> GetPlasmacytes()
    {
        List<PlasmacyteData> l = new List<PlasmacyteData>();
        foreach (Transform child in handArea)
        {
            if (child.GetComponent<CardDisplay>().GetData().GetType().Equals(typeof(PlasmacyteData)))
            {
                l.Add((PlasmacyteData)(child.GetComponent<CardDisplay>().GetData()));
            }
        }
        return l;
    }

    public Transform GetHandArea()
    {
        return handArea;
    }

    public Transform GetFactoryArea()
    {
        return factoryArea;
    }

    public bool SpawnFactory(PlasmacyteData card)
    {
        if (factoryArea.childCount <= 0)
        {
            GameObject newCard = Instantiate(cardPrefab, factoryArea);
            CardDisplay display = newCard.GetComponent<CardDisplay>();
            display.SetData(card);
            display.UpdateVisuals();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DrawHand()
    {
        ClearPlayedHand();
        Shuffle();
        int handCount = 0;
        foreach (Transform child in handArea)
        {
            CardDisplay display = child.GetComponent<CardDisplay>();
            if (display != null && !display.IsSelected())
            {
                handCount++;
            }
        }

        int draw = handSize - handCount;
        for (int i = 0; i < draw; i++)
        {
            if (!DrawCard())
            {
                break;
            }
        }
    }


}
