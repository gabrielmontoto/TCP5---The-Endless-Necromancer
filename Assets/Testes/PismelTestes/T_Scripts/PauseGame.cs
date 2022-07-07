using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    
    [SerializeField] private InputAction pauseBtn;
    [SerializeField] private InputAction SuicideBtn;
    [SerializeField] GameObject[] screensMenu;
    [SerializeField] Animator transition;
    
    Volume volume;

    ChromaticAberration chromaticAber;

    private bool pause;
    private bool interactable;
    private int currentScreen;

    private void OnEnable()
    {
        pauseBtn.Enable();
        SuicideBtn.Enable();
    }

    private void OnDisable()
    {
        pauseBtn.Disable();
        SuicideBtn.Disable();
    }

    void Start()
    {
        pauseBtn.performed += ctx => Pausar();
        interactable = true;
    }

    public void Pausar() 
    {
        if (interactable)
        {
            pause = !pause;
            if (pause)
            {
                Time.timeScale = 0;

                ChangeAnim(0, "in");

                //if(volume.profile.TryGet<ChromaticAberration>(out chromaticAber)) 
                //{
                //    chromaticAber.active = false;
                //}
            }
            else
            {
                ExitGame();
                //if (volume.profile.TryGet<ChromaticAberration>(out chromaticAber))
                //{
                //    chromaticAber.active = true;
                //}
            }
        }
        else 
        {
            ChangeAnim(currentScreen, "out");
            interactable = true;
            //if (volume.profile.TryGet<ChromaticAberration>(out chromaticAber))
            //{
            //    chromaticAber.active = true;
            //}
        }
    }
    public void ChangeAnim(int i, string trigger)
    {
        transition = screensMenu[i].GetComponent<Animator>();
        transition.SetTrigger(trigger);
    }

    //public void WaitForIt() 
    //{

    //}

    public void BackToMenu() 
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame() 
    {
        ChangeAnim(currentScreen, "out");
        interactable = true;
    }

    public void GoToOptions() 
    {
        interactable = false;
        currentScreen = 1;
        ChangeAnim(0, "out");
        //enter courotine
        ChangeAnim(1, "in");
    }

    public void GoToKeyMenu() 
    {
        interactable = false;
        currentScreen = 2;
        ChangeAnim(1, "out");
        //enter courotine
        ChangeAnim(2, "in");
    }

}
