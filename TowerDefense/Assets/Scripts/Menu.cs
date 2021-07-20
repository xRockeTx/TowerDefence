using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
 //   [SerializeField] private GameObject PanelWithSettings;
    [SerializeField] private GameObject PanelWithLevels, menu, setings;
    /*
      public void OpenOrCloseLevelPan()
      {
          PanelWithLevels.SetActive(!PanelWithLevels.activeSelf);
      }
      public void OpenOrCloseSetting()
      {
          PanelWithSettings.SetActive(!PanelWithSettings.activeSelf);
      }
      */
    //открытие меню

    private void Start()
    {
        Openmenu();
    }
    //меню
    public void Openmenu()
    {
        setings.SetActive(false);
        PanelWithLevels.SetActive(false);
        menu.SetActive(true);
    }
    //открытие настростроек
    public void OpenSeting()
    {
        setings.SetActive(true);
        menu.SetActive(false);
        PanelWithLevels.SetActive(false);
    }
     //выбор ур
    public void Openlevels()
    {
        PanelWithLevels.SetActive(true);
        setings.SetActive(false);
        menu.SetActive(false);
    }
    public void GoToLevel(int level)
    {
        SceneManager.LoadScene($"Lvl{level}");
    }
}
