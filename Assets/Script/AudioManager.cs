using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("OST List")]
    [EventRef] public string actionOST;
    [EventRef] public string mainOST;

    [Header("SFX List")]
    [EventRef] public string SFXGuerreiroMorrendo;
    [EventRef] public string SFXGuerreiroPulo;
    [EventRef] public string SFXPlayerFlutuando;
    [EventRef] public string SFXPlayerBengala;
    [EventRef] public string SFXPlayerPulo;

    /*[Header("Volumes")] 
    [Range(0.0f, 1.0f)] [SerializeField] public float masterVolume = 0.5f;
    [SerializeField] [Range(0.0f, 1.0f)] public float sfxVolume = 0.5f;
    [SerializeField] [Range(0.0f, 1.0f)] public float musicVolume = 0.5f;
    */
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }

    public void PlaySound(string eventRef, Vector3 position)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventRef, position);
    }
    public void PlaySound(string eventRef, GameObject gameObject)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(eventRef, gameObject);
    }

    void ChangeMusic(string eventRef)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventRef, gameObject.transform.position);
    }

    public void SetParameter(string parameter, float value)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(parameter, value);
    }

    public void SetMasterVolume(float volume)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MasterVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SFXVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicVolume", volume);
    }

    /*public void UpdateVolumes()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MasterVolume", masterVolume);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SFXVolume", sfxVolume);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicVolume", musicVolume);
    }

    public void SetVolume(string parameter, float volume)
    {
        switch (parameter)
        {
            case "MasterVolume":
                masterVolume = volume;
                break;
            case "SFXVolume":
                sfxVolume = volume;
                break;
            case "MusicVolume":
                musicVolume = volume;
                break;
        }

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MasterVolume", masterVolume);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SFXVolume", sfxVolume);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicVolume", musicVolume);
    }*/

    public float getVolume(string parameter)
    {
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName(parameter, out float volume).ToString();
        return volume;
    }
}
