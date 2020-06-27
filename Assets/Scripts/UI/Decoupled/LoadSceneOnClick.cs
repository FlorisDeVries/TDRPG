using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadSceneOnClick : MonoBehaviour
{
    private Button _restartButton = default;
    [SerializeField]
    [Tooltip("What scene should be loaded onClick")]
    private string _sceneName = default;

    void Start()
    {
        _restartButton = GetComponent<Button>();
        _restartButton.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
