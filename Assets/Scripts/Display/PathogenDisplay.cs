using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PathogenDisplay : MonoBehaviour
{
    [SerializeField] private PathogenData data;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Visuals")]
    [SerializeField] private Image artworkImage;
    [SerializeField] private Slider healthbar;
    private int currentHP;

    private void Start()
    {
        currentHP = data.MaxHP;
        healthbar.maxValue = data.MaxHP;
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        if (data != null)
        {
            nameText.text = data.PathogenName;
            if (data.Sprite != null) artworkImage.sprite = data.Sprite;
        }
        UpdateHealthUI();
    }

    public PathogenData GetData()
    {
        return data;
    }

    public void SetData(PathogenData data)
    {
        this.data = data;
    }

    public void UpdateHealthUI()
    {
        healthbar.value = currentHP;
    }

    public int GetAttack()
    {
        return Random.Range(0, data.MaxDamage + 1);
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        healthbar.value = currentHP;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}