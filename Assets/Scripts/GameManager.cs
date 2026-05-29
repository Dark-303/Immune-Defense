using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [Header("Game Over")]
    [SerializeField] private Button Respawn;
    [SerializeField] private TextMeshProUGUI RespawnText;
    [SerializeField] private Image Overlay;
    [SerializeField] private TextMeshProUGUI GameOver;

    private int initEnergy;
    private int initHealth;
    private int maxHealth;

    private void Start()
    {
        initEnergy = energy;
        initHealth = health;
        pathogens.SpawnPathogen();
        foreach (Transform child in deck.GetPassiveArea())
        {
            CardBase card = child.GetComponent<CardDisplay>().GetData();
            if (card is SkinData skin)
            {
                health = skin.GetHealth(health);
            }
        }
        maxHealth = health;
        UpdateEnergyUI();
        UpdateHealthUI();
    }

    private void UpdateEnergyUI()
    {
        energyBar.maxValue = initEnergy;
        energyBar.value = energy;
        energyText.text = $"ATP: {energy}/{initEnergy}";
    }

    private void UpdateHealthUI()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        healthText.text = $"Health: {health}/{maxHealth}";
    }

    public void OnDiscard()
    {
        bool selected = false;
        foreach (Transform child in deck.GetHandArea())
        {
            if (child.GetComponent<CardDisplay>().IsSelected())
            {
                selected = true;
                break;
            }
        }
        if (deck.GetRedrawCost() <= energy && selected)
        {
            energy = Mathf.Max(0, energy - deck.GetRedrawCost());
            UpdateEnergyUI();
            deck.DrawHand();
        }
    }

    public void OnPlay()
    {
        if (pathogens.GetPathogen() == null)
        {
            ResetGame();
            return;
        }
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
                        foreach (ReceptorType r in pathogens.GetPathogen().GetData().Detectable)
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
                        foreach (Transform kid in deck.GetPassiveArea())
                        {
                            CardBase passive = kid.GetComponent<CardDisplay>().GetData();
                            if (passive is DCData dendritic)
                            {
                                foreach (ReceptorType r in dendritic.Unlocked)
                                {
                                    if (card.Unlocked.Contains(r))
                                    {
                                        finalDamage *= dendritic.Bonus;
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
            bool DCBoost = false;
            foreach (Transform child in plasmacytes)
            {
                if (deck.SpawnFactory((PlasmacyteData)child.GetComponent<CardDisplay>().GetData()))
                {
                    DCBoost = true;
                    child.SetParent(null);
                    Destroy(child.gameObject);
                }
            }
            foreach (Transform child in deck.GetPassiveArea())
            {
                CardBase card = child.GetComponent<CardDisplay>().GetData();
                if (card is PlateletData platelet)
                {
                    health = Mathf.Min(maxHealth, health + platelet.Heal);
                }
                if (DCBoost)
                {
                    if (card is DCData dendritic)
                    {
                        deck.IncreaseTime();
                    }
                }
            }
            energy = Mathf.Max(0, energy - totalCost);
            pathogens.GetPathogen().ApplyDamage(totalDamage);
            deck.DrawHand();
            SpawnAntibodies();
            deck.IncreaseTickTime();
            UpdateEnergyUI();
            UpdateHealthUI();
            pathogens.GetPathogen().UpdateHealthUI();
            CheckHealth();
            RunEnemyTurn();
        }
    }

    private void RunEnemyTurn(int n)
    {
        StartCoroutine(Util.Wait(2.0f));
        if (pathogens.GetPathogen().IsAlive)
        {
            health = Mathf.Max(0, health - pathogens.GetPathogen().GetAttack());
        }
        StartCoroutine(Util.Wait(2.0f));
        energy = Mathf.Min(initEnergy, energy + n);
        CheckHealth();
        UpdateHealthUI();
        UpdateEnergyUI();

    }

    private void CheckHealth()
    {
        if (health == 0)
        {
            Respawn.image.enabled = true;
            RespawnText.enabled = true;
            GameOver.enabled = true;
            Overlay.enabled = true;
        }
    }
    
    private void RunEnemyTurn()
    {
        RunEnemyTurn(2);
    }

    private void SpawnAntibodies()
    {
        for (int i = deck.GetFactoryArea().childCount - 1; i >= 0; i--)
        {
            Transform child = deck.GetFactoryArea().GetChild(i);
            PlasmacyteData card = (PlasmacyteData)child.GetComponent<CardDisplay>().GetData();
            if (pathogens.GetPathogen() != null && antibody.GetKnown(pathogens.GetPathogen().GetData().Antigen) != null && card.TickTime >= 1)
            {
                AntiBodyData a = antibody.GetKnown(pathogens.GetPathogen().GetData().Antigen);
                if (antibody.AddAntiBody(a))
                {
                    pathogens.GetPathogen().ApplyDamage(a.Damage);
                    card.TickTime = 0;
                    card.CurrentTime = 0;
                    deck.AddCard(card);
                    child.SetParent(null);
                    Destroy(child.gameObject);
                }
            }
            else if (pathogens.GetPathogen() != null && antibody.GetKnown(pathogens.GetPathogen().GetData().Antigen) == null && card.CurrentTime >= 3)
            {
                AntiBodyData a = card.GetAntiBody(pathogens.GetPathogen().GetData().Antigen);
                if (antibody.AddAntiBody(a))
                {
                    pathogens.GetPathogen().ApplyDamage(a.Damage);
                    card.TickTime = 0;
                    card.CurrentTime = 0;
                    deck.AddCard(card);
                    child.SetParent(null);
                    Destroy(child.gameObject);
                }

            }
        }
    }

    public void Skip()
    {
        SpawnAntibodies();
        deck.IncreaseTickTime();
        UpdateEnergyUI();
        UpdateHealthUI();
        CheckHealth();
        RunEnemyTurn(1);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Scene_Main");
    }

    private void Update()
    {

    }
}
