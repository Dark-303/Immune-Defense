using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DeckManager deck;
    [SerializeField] private PathogenManager pathogens;

    private void Update()
    {
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            deck.DrawHand();
        }

        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            pathogens.GetPathogen();
        }
    }
}
