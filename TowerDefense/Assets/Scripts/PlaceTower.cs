using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceTower : MonoBehaviour
{
    [SerializeField] private Color normal, enter;
    [SerializeField] private MeshRenderer render;
    [SerializeField] private GameObject buyPanel;
    [SerializeField] private BuyOpen buyScr;

    private void OnMouseEnter()
    {
        render.material.color = enter;    
    }
    private void OnMouseExit()
    {
        render.material.color = normal;
    }
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if(!buyScr.gameObject.GetComponent<UpgradeTower>().upPan.activeSelf)
                buyScr.SetPlace(transform);
        }
    }
}
