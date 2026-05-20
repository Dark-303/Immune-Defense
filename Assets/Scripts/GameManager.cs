using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DeckManager deck;

    private void Update()
    {
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            deck.DrawHand();
        }
    }
}
