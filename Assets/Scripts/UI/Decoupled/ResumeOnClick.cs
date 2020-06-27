using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ResumeOnClick : MonoBehaviour
{
    private Button _button = default;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Resume);
    }

    private void Resume()
    {
        GameStateManager.Instance.OnPause();
    }
}