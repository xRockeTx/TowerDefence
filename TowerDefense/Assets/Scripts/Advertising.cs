using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Advertising : MonoBehaviour
{
    [SerializeField] private BuyOpen buyOpen;
    [SerializeField] private Button addMoney;
    public void AddMoney()
    {
        ShowAdvertising(2);
        buyOpen.ChangeMoney(40);
        addMoney.gameObject.SetActive(false);
    }
    public void ShowAdvertising(int id)
    {
        switch (id)
        {
            case 1:
                Debug.Log("Видео реклама которую можно пропустить");
                break;
            case 2:
                Debug.Log("Видео реклама без возможности пропустить");
                break;

        }
    }
}
