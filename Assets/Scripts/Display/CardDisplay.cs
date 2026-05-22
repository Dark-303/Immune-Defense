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
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        if (data != null)
        {
            nameText.text = data.GetCardName();
            descText.text = data.GetDesc();
            costText.text = data.GetCost().ToString();
            if (data.GetImage() != null) artworkImage.sprite = data.GetImage();
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