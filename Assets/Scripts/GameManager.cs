using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private DeckManager deck;
    [SerializeField] private PathogenManager pathogens;
    [SerializeField] private AntiBodyManager antibody;
    [Header("Health")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private int health = 500;
    [SerializeField] private TextMeshProUGUI healthText;
    [Header("Energy")]
    [SerializeField] private Slider energyBar;
    [SerializeField] private int energy = 10;
    [SerializeField] private TextMeshProUGUI energyText;

    private int initEnergy;
    private int initHealth;

    private void Start()
    {
        initEnergy = energy;
        initHealth = health;
        UpdateEnergyUI();
        UpdateHealthUI();
        pathogens.SpawnPathogen();
    }

    private void UpdateEnergyUI()
    {
        energyBar.maxValue = initEnergy;
        energyBar.value = energy;
        energyText.text = $"ATP: {energy}/10";
    }

    private void UpdateHealthUI()
    {
        healthBar.maxValue = initHealth;
        healthBar.value = health;
        healthText.text = $"Health: {health}/{initHealth}";
    }

    public void OnDiscard()
    {
        deck.DrawHand();
    }

    public void OnPlay()
    {
        SpawnAntibodies();
        int totalDamage = 0;
        int totalCost = 0;
        List<Transform> plasmacytes = new List<Transform>();
        for (int i = deck.GetHandArea().childCount - 1; i >= 0; i--)
        {
            Transform child = deck.GetHandArea().GetChild(i);
            if (child.GetComponent<CardDisplay>().IsSelected())
            {
                switch (child.GetComponent<CardDisplay>().GetData())
                {
                    case PlasmacyteData plasmacyte:
                        plasmacytes.Add(child);
                        totalCost += 7;
                        break;
                    case CardData card:
                        float finalDamage = card.Damage;
                        foreach (ReceptorType r in pathogens.GetPathogen().Detectable)
                        {
                            if (card.Unlocked.Contains(r))
                            {
                                finalDamage *= 1.2f;
                            }
                        }
                        foreach (Transform kid in antibody.GetAntibodyArea())
                        {
                            AntiBodyData data = kid.GetComponent<AntiBodyDisplay>().GetData();
                            foreach (ReceptorType r in data.Receptors)
                            {
                                if (card.Unlocked.Contains(r))
                                {
                                    if ((r == ReceptorType.FcγRIIA || r == ReceptorType.FcγRIIB) && (data.Type == AntiBodyType.fIgG1 || data.Type == AntiBodyType.fIgG3))
                                    {
                                        finalDamage *= 1.1f;
                                    }
                                    else if ((r == ReceptorType.FcγRIIIA || r == ReceptorType.FcγRIIIB) && (data.Type == AntiBodyType.fIgG1 || data.Type == AntiBodyType.fIgG3))
                                    {
                                        finalDamage *= 1.05f;
                                    }
                                    else
                                    {
                                        finalDamage *= 1.2f;
                                    }
                                }
                            }
                        }
                        totalCost += card.Cost;
                        totalDamage += (int)finalDamage;
                        break;
                }
            }
        }
        if (totalCost <= energy && (plasmacytes.Count > 0 || totalDamage > 0))
        {
            foreach (Transform child in plasmacytes)
            {
                if (deck.SpawnFactory((PlasmacyteData)child.GetComponent<CardDisplay>().GetData()))
                {
                    // DC check
                    child.SetParent(null);
                    Destroy(child.gameObject);
                }
            }
            energy -= totalCost;
            // Deal damage here
            deck.DrawHand();
            deck.IncreaseTime();
            UpdateEnergyUI();
            UpdateHealthUI();
        }
    }

    private void SpawnAntibodies()
    {
        for (int i = deck.GetFactoryArea().childCount - 1; i >= 0; i--)
        {
            Transform child = deck.GetFactoryArea().GetChild(i);
            PlasmacyteData card = (PlasmacyteData)child.GetComponent<CardDisplay>().GetData();
            if (card.CurrentTime > 3)
            {
                if (pathogens.GetPathogen() != null)
                {
                    if (antibody.AddAntiBody(card.GetAntiBody(pathogens.GetPathogen().Antigen)))
                    {
                        card.CurrentTime = 0;
                        deck.AddCard(card);
                        Destroy(child.gameObject);
                    }

                }
            }
        }
    }

    private void Update()
    {

    }
}
