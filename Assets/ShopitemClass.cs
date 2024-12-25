using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemClass : MonoBehaviour
{
    [SerializeField]
    private float Cost;
    [SerializeField]
    private string Quanity;
    [SerializeField]
    private string Name;
    [SerializeField]
    private TextMeshProUGUI Description;
    private GameObject obj;

    public void Start()
    {
        var img = GetComponent<Image>();
        obj = GetComponent<GameObject>();
        Description.text = $"Название: {Name}\n" +
            $"Цена: {Cost}\n " +
            $"Качество: {Quanity}\n";
    }
}
