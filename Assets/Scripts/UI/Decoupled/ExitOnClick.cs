using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExitOnClick : MonoBehaviour
{
    private Button _button = default;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
