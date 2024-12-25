using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UIElements;

public class ShopButtonClick : MonoBehaviour
{
    public RectTransform shopPanel;
    public Vector2 hiddenPosition; 
    public Vector2 visiblePosition; 
    public float animationSpeed = 5f;
    private bool isOpen = false; // Состояние магазина
    private Vector2 targetPosition; // Целевая позиция панели
    [SerializeField]
    private GameObject BackGround;
    private DimBackgroundController dimController;


    private void Start()
    {
        // Устанавливаем начальную позицию
        targetPosition = hiddenPosition;
        shopPanel.anchoredPosition = hiddenPosition;
        dimController = BackGround.GetComponent<DimBackgroundController>();
    }

    private void Update()
    {
        // Плавное перемещение панели к целевой позиции
        shopPanel.anchoredPosition = Vector2.Lerp(shopPanel.anchoredPosition, targetPosition, Time.deltaTime * animationSpeed);
    }
    private void OnMouseDown()
    {

        isOpen = !isOpen;
        targetPosition = isOpen ? visiblePosition : hiddenPosition;

        if (isOpen)
        {
            dimController.FadeIn(0.5f); // Плавное появление за 0.5 секунды
        }
        else
        {
            dimController.FadeOut(0.5f); // Плавное исчезновение за 0.5 секунды
        }

        
    }
}
