using UnityEngine;

public class AntiBodyManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject antibodyPrefab;

    [Header("Hand")]
    [SerializeField] private Transform antibodyArea;

    public void AddAntiBody(AntiBodyData antiBody)
    {
        GameObject newIg = Instantiate(antibodyPrefab, antibodyArea);
        AntiBodyDisplay display = newIg.GetComponent<AntiBodyDisplay>();
        display.SetData(antiBody);
        display.UpdateVisuals();
    }
}
