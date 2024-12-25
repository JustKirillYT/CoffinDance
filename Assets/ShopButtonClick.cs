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
    private bool isOpen = false; // ��������� ��������
    private Vector2 targetPosition; // ������� ������� ������
    [SerializeField]
    private GameObject BackGround;
    private DimBackgroundController dimController;


    private void Start()
    {
        // ������������� ��������� �������
        targetPosition = hiddenPosition;
        shopPanel.anchoredPosition = hiddenPosition;
        dimController = BackGround.GetComponent<DimBackgroundController>();
    }

    private void Update()
    {
        // ������� ����������� ������ � ������� �������
        shopPanel.anchoredPosition = Vector2.Lerp(shopPanel.anchoredPosition, targetPosition, Time.deltaTime * animationSpeed);
    }
    private void OnMouseDown()
    {

        isOpen = !isOpen;
        targetPosition = isOpen ? visiblePosition : hiddenPosition;

        if (isOpen)
        {
            dimController.FadeIn(0.5f); // ������� ��������� �� 0.5 �������
        }
        else
        {
            dimController.FadeOut(0.5f); // ������� ������������ �� 0.5 �������
        }

        
    }
}
