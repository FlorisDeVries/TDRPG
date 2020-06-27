using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic class that makes an animator fadeOut on given gameState
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class FadeOnGameState : MonoBehaviour
{
    private Animator _animator = default;

    [SerializeField]
    [Tooltip("The game state on which to fade this element out")]
    private GameState _gameState = GameState.GameOver;

    [SerializeField]
    [Tooltip("Whether we should fade in or out")]
    private bool _fadeOut = true;

    private CanvasGroup _canvasGroup = default;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _canvasGroup = GetComponent<CanvasGroup>();

        GameStateManager.Instance.GameStateEvents[_gameState].AddListener(Fade);
    }

    private void Fade()
    {
        if (_fadeOut)
            _animator.SetTrigger("FadeOut");
        else
            _animator.SetTrigger("FadeIn");

        if (_canvasGroup)
        {
            _canvasGroup.interactable = !_fadeOut;
            _canvasGroup.blocksRaycasts = !_fadeOut;
        }
    }
}
