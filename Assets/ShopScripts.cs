using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScripts : MonoBehaviour
{
    [SerializeField]
    public bool octive;
  
    void Update()
    {
        if (octive)
        {
            gameObject.SetActive(true);
        }

        else gameObject.SetActive(false);
    }

    public void SetActivePanel()
    {


    }
}
