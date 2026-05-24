using UnityEngine;

public class AntiBodyManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject antibodyPrefab;

    [Header("Hand")]
    [SerializeField] private Transform antibodyArea;

    public bool AddAntiBody(AntiBodyData antiBody)
    {
        if (antibodyArea.childCount < 4)
        {
            GameObject newIg = Instantiate(antibodyPrefab, antibodyArea);
            AntiBodyDisplay display = newIg.GetComponent<AntiBodyDisplay>();
            display.SetData(antiBody);
            display.UpdateVisuals();
            return true;
        }
        else
        {
            return false;
        }
    }

    public Transform GetAntibodyArea()
    {
        return antibodyArea;
    }

    public void Clear()
    {
        foreach (Transform child in antibodyArea)
        {
            Destroy(child.gameObject);
        }
    }
}
