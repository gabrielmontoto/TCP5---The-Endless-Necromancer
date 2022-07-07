using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera CVC;
    private float shakeTime;

    public static CameraShake Instancia { get; private set; }
    private void Awake()
    {
        Instancia = this;
       CVC = GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin cinemachine = CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachine.m_AmplitudeGain = 0f;
    }
    public void ShakeDaCAmera(float intensidade, float timer)
    {
        CinemachineBasicMultiChannelPerlin cinemachine = CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachine.m_AmplitudeGain = intensidade;
        shakeTime = timer;

    }
    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachine = CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachine.m_AmplitudeGain = 0f;
            }
        }
    }
}
