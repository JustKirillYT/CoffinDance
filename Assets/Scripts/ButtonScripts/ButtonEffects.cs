using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonEffects : MonoBehaviour
{
    private Vector2 originalSize;


    private void Start()
    {
        originalSize = transform.localScale;
    }

    private void OnMouseEnter()
    {
        Debug.Log($"Навел мышкой на {transform.name} ");
        transform.localScale = originalSize * 1.1f;
    }

    private void OnMouseExit()
    {
       transform .localScale = originalSize;
    }
}
