using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : UnitySingleton<MouseFollow>
{
    [SerializeField]
    [Tooltip("What layers define the world")]
    private LayerMask _rayMask = default;

    [SerializeField]
    [Tooltip("What layers define the world")]
    private LayerMask _placementMask = default;

    [SerializeField]
    [Tooltip("The offset that the visuals should have in regards to the inWorld mouse position")]
    private Vector3 _offset = default;

    [SerializeField]
    [Tooltip("Visuals used to indicate whether the current ground is placeble or not")]
    private GameObject _placeable = default, _notPlaceable = default;

    private bool _prevPlaceableGround = false;
    private bool _placeableGround = false;
    public bool PlaceableGround
    {
        get { return _placeableGround; }
    }

    private void Update()
    {
        if (GameStateManager.Instance.GameState == GameState.GameOver || GameStateManager.Instance.GameState == GameState.Paused)
        {
            _placeable.SetActive(false);
            _notPlaceable.SetActive(false);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        _placeableGround = false;
        if (Physics.Raycast(ray, out hit, 1000000, _rayMask))
        {
            transform.position = hit.point + _offset;
            if (Utils.IsInLayerMask(hit.collider.gameObject.layer, _placementMask))
                _placeableGround = true;
        }
        else
        {
            transform.position = Utils.GetPlaneIntersection(0);
        }

        if (_placeableGround != _prevPlaceableGround)
        {
            UpdateVisuals();
            _prevPlaceableGround = _placeableGround;
        }
    }

    private void UpdateVisuals()
    {
        if (_placeableGround)
        {
            _placeable.SetActive(true);
            _notPlaceable.SetActive(false);
        }
        else
        {
            _placeable.SetActive(false);
            _notPlaceable.SetActive(true);
        }
    }
}
