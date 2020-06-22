using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private TextMesh _textMesh = default;

    private void OnEnable()
    {
        _textMesh = GetComponentInChildren<TextMesh>();
    }

    public void SetText(string text)
    {
        if (_textMesh)
            _textMesh.text = text;
    }

    public void SetColor(Color color)
    {
        if (_textMesh)
            _textMesh.color = color;
    }

    public void DestroyText()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        // transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
