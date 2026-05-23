using UnityEngine;
using UnityEngine.UI;

public class AntiBodyDisplay : MonoBehaviour
{
    [SerializeField] private AntiBodyData data;
    [SerializeField] private Image artworkImage;

    public void SetData(AntiBodyData data)
    {
        this.data = data;
    }

    public void UpdateVisuals()
    {
        artworkImage.sprite = data.Sprite;
    }
}
