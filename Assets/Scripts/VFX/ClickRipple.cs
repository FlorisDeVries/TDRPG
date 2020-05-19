using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRipple : MonoBehaviour
{
    [SerializeField]
    private GameObject _ripplePrefab;

    void Start()
    {
        if(_ripplePrefab != null)
            InputManager.Instance.KeyEvents[KeyCode.Mouse0].AddListener(Ripple);
    }

    void Ripple(Vector3 pos)
    {
        GameObject ripple = Instantiate(_ripplePrefab);
        ripple.transform.position = pos;
    }
}
