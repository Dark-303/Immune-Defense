using System.Collections.Generic;
using UnityEngine;

class PathogenManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject pathogenPrefab;

    [Header("Areas")]
    [SerializeField] private Transform pathogenArea;
    [SerializeField] private Transform antibodyArea;

    [Header("Pathogens")]
    [SerializeField] private List<PathogenData> pathogens;
    private List<PathogenData> masterList;

    private void Start()
    {
        masterList = new List<PathogenData>(pathogens);
    }

    public PathogenDisplay GetPathogen()
    {
        if (pathogenArea.childCount > 0)
        {
            return pathogenArea.GetChild(0).GetComponent<PathogenDisplay>();
        }
        return null;
    }

    public bool HasPathogens => pathogens.Count > 0;

    public void SpawnPathogen()
    {
        if (!HasPathogens) return;
        foreach (Transform child in antibodyArea)
        {
            child.SetParent(null);
            Destroy(child.gameObject);
        }
        PathogenData pathogen = pathogens[0];
        pathogens.RemoveAt(0);
        GameObject newPathogen = Instantiate(pathogenPrefab, pathogenArea);
        PathogenDisplay display = newPathogen.GetComponent<PathogenDisplay>();
        display.SetData(pathogen);
        display.UpdateVisuals();
    }
}