using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControler : MonoBehaviour
{
    [SerializeField] GameObject[] screensMenu;
    [SerializeField] Animator transition;

    private int currentScreen;

    public void ChangeAnim(int i, string trigger)
    {
        transition = screensMenu[i].GetComponent<Animator>();
        transition.SetTrigger(trigger);
    }

    public void GameStart() 
    {
        StartCoroutine(LoadCharge());
    }

    IEnumerator LoadCharge() 
    {
        currentScreen = 3;
        ChangeAnim(currentScreen, "in");

        yield return new WaitForSeconds(2);

        currentScreen = 0;
        SceneManager.LoadScene(1);
    }

    public void SeeCredits() 
    {
        ChangeAnim(0, "out");
        currentScreen = 1;
        ChangeAnim(currentScreen, "in");
    }

    public void GoOptions() 
    {
        ChangeAnim(0, "out");
        currentScreen = 2;
        ChangeAnim(currentScreen, "in");
    }

    public void MenuExit() 
    {
        ChangeAnim(currentScreen, "out");
        ChangeAnim(0, "in");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
