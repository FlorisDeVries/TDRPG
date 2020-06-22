using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationField : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The prefab for the floating text used to display info")]
    private GameObject _floatingTextPrefab = default;

    [SerializeField]
    [Tooltip("The prefab for the floating text used to display info")]
    private Transform _textPosition = default;

    [SerializeField]
    [Tooltip("What information this board should display")]
    private string _informationText = default;

    [SerializeField]
    [Tooltip("The exclamation mark")]
    private GameObject _exclamationMark = default;

    private Transform target = default;
    private FloatingText _floatingText = default;
    private SphereCollider _collider = default;

    public float scalar = 0;

    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _exclamationMark.SetActive(false);
        target = other.transform;
        _floatingText = Instantiate(_floatingTextPrefab, _textPosition.position, Quaternion.identity).GetComponent<FloatingText>();
        _floatingText.SetText(_informationText);
        _floatingText.SetColor(Color.white);
    }

    private void OnTriggerExit(Collider other)
    {
        _exclamationMark.SetActive(true);
        Destroy(_floatingText.gameObject);
        target = null;
    }

    private void Update()
    {
        if (_floatingText)
        {
            float dist = Vector3.Distance(transform.position, target.position);
            scalar = (6 - dist) / 5;
            _floatingText.transform.localScale = new Vector3(scalar, scalar, 0);
        }
    }
}
