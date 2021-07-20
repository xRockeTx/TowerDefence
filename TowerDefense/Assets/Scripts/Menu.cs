using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject PanelWithLevels,PanelWithSettings;

    public void OpenOrCloseLevelPan()
    {
        PanelWithLevels.SetActive(!PanelWithLevels.activeSelf);
    }
    public void OpenOrCloseSetting()
    {
        PanelWithSettings.SetActive(!PanelWithSettings.activeSelf);
    }

    public void GoToLevel(int level)
    {
        SceneManager.LoadScene($"Lvl{level}");
    }
}
