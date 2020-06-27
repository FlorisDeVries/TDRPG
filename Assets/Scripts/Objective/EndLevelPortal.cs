using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelPortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameStateManager.Instance.SetGameState(GameState.Victory);
    }
}
