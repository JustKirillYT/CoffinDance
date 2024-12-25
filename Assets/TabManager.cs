using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{

    [SerializeField] private GameObject[] tabs;
    /*0 - �����
      1 - ���������
      2 - �����
      3 - ������ */
    public void OpenTab(int tabId)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(false);
        }

        tabs[tabId].SetActive(true);
    }

}
