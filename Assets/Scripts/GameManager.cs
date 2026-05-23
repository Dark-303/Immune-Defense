using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DeckManager deck;
    [SerializeField] private PathogenManager pathogens;
    [SerializeField] private AntiBodyManager antibody;

    private void Update()
    {
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            deck.DrawHand();
        }

        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            pathogens.SpawnPathogen();
        }

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            foreach (PlasmacyteData p in deck.GetPlasmacytes())
            {
                if (pathogens.GetPathogen() != null)
                {
                    antibody.AddAntiBody(p.GetAntiBody(pathogens.GetPathogen().Antigen));
                }
            }
        }
    }
}
