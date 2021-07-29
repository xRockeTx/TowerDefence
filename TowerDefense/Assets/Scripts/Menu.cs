using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //   [SerializeField] private GameObject PanelWithSettings;
    [SerializeField] private GameObject sene;
    [SerializeField] private GameObject PanelWithLevels, menu, setings;
    [SerializeField] private AudioSource music;
    [SerializeField] private List<Toggle> toggles;

    private void Start()
    {
        toggles[0].isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Shadow"));
        toggles[1].isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Music")); 
        Openmenu();
        PlayerPrefs.SetInt("Lose", 0);
    }
    //меню
    public void Openmenu()
    {
        setings.SetActive(false);
        PanelWithLevels.SetActive(false);
        menu.SetActive(true);
        sene.SetActive(true);
    }
    //открытие настростроек
    public void OpenSeting()
    {
        setings.SetActive(true);
        menu.SetActive(false);
        PanelWithLevels.SetActive(false);
        sene.SetActive(false);
    }
     //выбор ур
    public void Openlevels()
    {
        PanelWithLevels.SetActive(true);
        setings.SetActive(false);
        menu.SetActive(false);
        sene.SetActive(false);
    }
    public void SettingToggle(int id)
    {
        switch (id)
        {
            case 1:
                music.mute = !music.mute;
                PlayerPrefs.SetInt("Music",Convert.ToInt32(music.mute));
                break;
            case 2:
                if(PlayerPrefs.GetInt("Shadow") == 1)
                    PlayerPrefs.SetInt("Shadow", 0);
                else
                    PlayerPrefs.SetInt("Shadow", 1);
                break;
        }
    }

    public void GoToLevel(int level)
    {
        SceneManager.LoadScene($"Lvl{level}");
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
