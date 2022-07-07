using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeManager : MonoBehaviour
{
    public float forcaSlow = 0.01f;
    public float tempoSlow = 10f;
    public bool podeShockwave;
    public float tempo;
    private bool timeOut;
    [SerializeField][Header("Valor para contagem do Slow")] float contadorSlow;
    private float identitySlowCount;
    public Material shockwave;

    public GameObject player;

    public Volume volume;
    ColorAdjustments colorAdjustments;
    public bool colorOverTime;
    public float colorTimer;
    public float saturationForce;
    

    // Update is called once per frame

    private void Start()
    {
        identitySlowCount = contadorSlow;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Time.timeScale += (1f / tempoSlow) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        if (podeShockwave)
        {
            tempo += 1 * Time.fixedDeltaTime;
            shockwave.SetFloat("_TimeDistance", tempo);

            if (tempo >= 0.2f)
            {
                shockwave.SetFloat("_Size", 0f);
                shockwave.SetFloat("_TimeDistance", 0);
                shockwave.SetInt("_ConfirmWave", 0);
                tempo = 0;
                podeShockwave = false;
            }
        }

        if (colorOverTime)
        {
            if (!timeOut)
            {
                if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
                {
                    colorTimer += 1 * (Time.fixedDeltaTime * saturationForce);
                    colorAdjustments.saturation.value = -60;
                    colorAdjustments.saturation.value += colorTimer;
                }

                identitySlowCount -= 1 * (Time.fixedDeltaTime * saturationForce);
                if (identitySlowCount < 0)
                {
                    timeOut = true;
                }
            }
        }
    }

    public void BulletTime() 
    {
        Time.timeScale = forcaSlow;
        Time.fixedDeltaTime = Time.timeScale * 0.2f;
        colorOverTime = true;
    }

    public void FimBulletTime() 
    {
        timeOut = false;
        identitySlowCount = contadorSlow;
        Time.timeScale = 1f;
        shockwave.SetInt("_ConfirmWave", 1);
        shockwave.SetFloat("_Size", 0.05f);
        //shockwave.SetVector("_FocalPoint", new Vector4(player.transform.position.x, player.transform.position.y, 0, 0));
        podeShockwave = true;

        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorOverTime = false;
            colorTimer = 0;
            colorAdjustments.saturation.value = -20.95f;
        }
    }

    public bool TimeOut() 
    {
        return this.timeOut;
    }

}
