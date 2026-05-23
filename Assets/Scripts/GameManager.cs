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
        for(int i = deck.GetHandArea().childCount - 1; i >= 0; i--)
        {
            Transform child = deck.GetHandArea().GetChild(i);
            if (child.GetComponent<CardDisplay>().IsSelected())
            {
                switch (child.GetComponent<CardDisplay>().GetData())
                {
                    case PlasmacyteData plasmacyte:
                        if (deck.SpawnFactory(plasmacyte))
                        {
                            // DC check
                            child.SetParent(null);
                            Destroy(child.gameObject);
                        }
                        break;
                    case CardData card:
                        // Check receptors
                        break;
                }
            }
        }
        deck.DrawHand();
    }

    private void Update()
    {
        
    }
}
