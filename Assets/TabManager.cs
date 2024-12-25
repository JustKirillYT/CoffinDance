using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{

    [SerializeField] private GameObject[] tabs;
    /*0 - гробы
      1 - украшения
      2 - цветы
      3 - другое */
    public void OpenTab(int tabId)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(false);
        }

        tabs[tabId].SetActive(true);
    }

}
