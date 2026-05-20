using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] private CardData data;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private TextMeshProUGUI costText;

    [Header("Visuals")]
    [SerializeField] private Image artworkImage;
    [SerializeField] private bool isSelected = false;

    private void Start()
    {
        if (data != null)
        {
            nameText.text = data.cardName;
            descText.text = data.desc;
            costText.text = data.cost.ToString();
            artworkImage.sprite = data.cardImage;
        }
    }

    public void UpdateVisuals()
    {
        if (data != null)
        {
            nameText.text = data.cardName;
            descText.text = data.desc;
            costText.text = data.cost.ToString();
            if (data.cardImage != null) artworkImage.sprite = data.cardImage;
        }
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public CardData GetData()
    {
        return data;
    }

    public CardData SetData(CardData data)
    {
        CardData prev = this.data;
        this.data = data;
        return prev;
    }

    private void OnCardClicked()
    {
        isSelected = !isSelected;
        transform.localPosition += isSelected ? new Vector3(0, 20, 0) : new Vector3(0, -20, 0);
    }
}