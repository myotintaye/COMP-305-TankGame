using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void Btn_start(string newSelectLevel)
    {
        SceneManager.LoadScene(newSelectLevel);
    }

    public void Btn_help(string newHelp)
    {
        SceneManager.LoadScene(newHelp);
    }

    public void Btn_backHome(string newHome)
    {
        SceneManager.LoadScene(newHome);
    }

    public void Btn_level1(string newTankSelection)
    {
        SceneManager.LoadScene(newTankSelection);
    }

    public void Btn_level2(string newTankSelection)
    {
        SceneManager.LoadScene(newTankSelection);
    }

    public void Btn_level3(string newTankSelection)
    {
        SceneManager.LoadScene(newTankSelection);
    }

    public void Btn_tank1(string newLevelStart)
    {
        SceneManager.LoadScene(newLevelStart);
    }

    public void Btn_tank2(string newLevelStart)
    {
        SceneManager.LoadScene(newLevelStart);
    }

    public void Btn_tank3(string newLevelStart)
    {
        SceneManager.LoadScene(newLevelStart);
    }

    public void Btn_tankInfo(string newTankInfo)
    {
        SceneManager.LoadScene(newTankInfo);
    }

    public void Btn_exit()
    {
        Application.Quit();
    }
}
