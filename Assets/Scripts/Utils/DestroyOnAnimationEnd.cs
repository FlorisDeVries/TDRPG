using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple helper class that destroys a given gameobject when an attached animation ends
/// </summary>
public class DestroyOnAnimationEnd : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The gameObject to destroy")]
    private GameObject _toDestroy = default;

    void Start()
    {
        if (!_toDestroy)
            _toDestroy = this.gameObject;
    }

    public void AnimationEnd()
    {
        Destroy(_toDestroy);
    }    
}
