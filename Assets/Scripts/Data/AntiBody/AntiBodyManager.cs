using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AntiBodyManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject antibodyPrefab;

    [Header("Hand")]
    [SerializeField] private Transform antibodyArea;
    [SerializeField] private List<AntiBodyData> knownAntiBodies;
    private HashSet<AntiBodyData> known;

    private void Start()
    {
        known = new HashSet<AntiBodyData>(knownAntiBodies);
    }

    public bool AddAntiBody(AntiBodyData antiBody)
    {
        if (antibodyArea.childCount < 4)
        {
            GameObject newIg = Instantiate(antibodyPrefab, antibodyArea);
            AntiBodyDisplay display = newIg.GetComponent<AntiBodyDisplay>();
            display.SetData(antiBody);
            display.UpdateVisuals();
            known.Add(antiBody);
            return true;
        }
        else
        {
            return false;
        }
    }

    public AntiBodyData GetKnown(AntigenData antigen)
    {
        foreach (AntiBodyData a in antigen.Matches)
        {
            if (known.Contains(a))
            {
                return a;
            }
        }

        return null;
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
