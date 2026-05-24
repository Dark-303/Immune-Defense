using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] private CardBase data;

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
            nameText.text = data.Name;
            descText.text = data.Desc;
            costText.text = data.Cost.ToString();
            isSelected = false;
            if (data.Sprite != null) artworkImage.sprite = data.Sprite;
        }
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public CardBase GetData()
    {
        return data;
    }

    public CardBase SetData(CardBase data)
    {
        CardBase prev = this.data;
        this.data = data;
        return prev;
    }

    public void OnCardClicked()
    {
        if (!(data is PassiveCardBase))
        {
            isSelected = !isSelected;
            transform.localPosition += isSelected ? new Vector3(0, 20, 0) : new Vector3(0, -20, 0);
        }
    }
}