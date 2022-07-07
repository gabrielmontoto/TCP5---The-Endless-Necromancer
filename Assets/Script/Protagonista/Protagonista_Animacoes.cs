using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Protagonista_Animacoes : MonoBehaviour
{

    public Animator animator;
    private string currentState;
    [SerializeField] StudioEventEmitter possessionEmitter;
    [SerializeField] StudioEventEmitter floatingEmitter;
    FMOD.Studio.EventInstance possessionEvent;

    // Strings das animações
    #region
    // Animações básicas
    const string P_Idle_D = "Protagonista_Idle_D";
    const string P_Idle_E = "Protagonista_Idle_E";
    const string P_Andar_D = "Protagonista_Andar_D";
    const string P_Andar_E = "Protagonista_Andar_E";

    // Animações de Possessão Direita
    const string P_PossessaoInicio_D = "Protagonista_PossessaoTransicao_D";
    const string P_PossessaoIdle_D = "Protagonista_PossessaoIdle_D";
    const string P_PossessaoTransf_D = "Protagonista_PossessaoTransformacao_D";
    const string P_PossessaoProjetil_D = "Protagonista_PossessaoProjetil_D";
    const string P_PossessaoDestrans_D = "Protagonista_PossessaoDestransformar_D";

    // Animações de Possessão Esquerda
    const string P_PossessaoInicio_E = "Protagonista_PossessaoTransicao_E";
    const string P_PossessaoIdle_E = "Protagonista_PossessaoIdle_E";
    const string P_PossessaoTransf_E = "Protagonista_PossessaoTransformacao_E";
    const string P_PossessaoProjetil_E = "Protagonista_PossessaoProjetil_E";
    const string P_PossessaoDestrans_E = "Protagonista_PossessaoDestransformar_E";
    #endregion

    // Variáveis de controle das animações
    #region
    // Movimento
    public bool movendo;
    public bool podeAnimMover;
    public bool direcaoMovimento; // esquerda = false | direita = true;

    // Possessão
    public bool podePossuir;
    public bool idlePossessao;
    public bool estaProjetil;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        ChangeAnimationState(P_Idle_D);
        podeAnimMover = true;
    }

    private void Update()
    {
        if (podeAnimMover)
        {
            if (direcaoMovimento)
            {
                if (movendo)
                {
                    ChangeAnimationState(P_Andar_D);
                }
                else
                {
                    ChangeAnimationState(P_Idle_D);
                }
            }
            else
            {
                if (movendo)
                {
                    ChangeAnimationState(P_Andar_E);
                }
                else
                {
                    ChangeAnimationState(P_Idle_E);
                }
            }
        }

        if (movendo)
        {
            if (!floatingEmitter.IsPlaying())
            {
                floatingEmitter.Play();
            }
        }
        else
        {
            floatingEmitter.Stop();

        }
        if (podePossuir && !idlePossessao && !estaProjetil)
        {
            podeAnimMover = false;

            if (direcaoMovimento)
            {
                ChangeAnimationState(P_PossessaoInicio_D);
                AudioManager.instance.SetParameter("SlowMotion", 1);
            }
            else
            {
                ChangeAnimationState(P_PossessaoInicio_E);
                AudioManager.instance.SetParameter("SlowMotion", 1);
            }
        }

        if (idlePossessao)
            PossessaoIdle();

        if (estaProjetil)
        {
            if (direcaoMovimento)
                ChangeAnimationState(P_PossessaoProjetil_D);
            else
                ChangeAnimationState(P_PossessaoProjetil_E);
        }

    }

    // Mudar animação atual
    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

    void InicioPossessaoIdle()
    {
        idlePossessao = true;
    }

    void PossessaoIdle()
    {
        if (direcaoMovimento)
            ChangeAnimationState(P_PossessaoIdle_D);

        else
            ChangeAnimationState(P_PossessaoIdle_E);
    }

    public void FimPossessaoIdle()
    {
        podePossuir = false;
        idlePossessao = false;
        estaProjetil = false;//mudei
        if (direcaoMovimento)
            ChangeAnimationState(P_PossessaoTransf_D);

        else
            ChangeAnimationState(P_PossessaoTransf_E);
    }

    void PossessaoProjetil()
    {
        estaProjetil = true;
    }

    public void FimPossessao()
    {
        estaProjetil = false;//mudei
        if (direcaoMovimento)
            ChangeAnimationState(P_PossessaoDestrans_D);

        else
            ChangeAnimationState(P_PossessaoDestrans_E);
    }

    void Destransformar()
    {
        podePossuir = false;
        idlePossessao = false;
        estaProjetil = false;
        podeAnimMover = true;
    }

    void CallSoundByEvent(string sfxPath)
    {
        possessionEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        possessionEvent = FMODUnity.RuntimeManager.CreateInstance(sfxPath);
        possessionEvent.start();
    }

    void CallSoundByEmitter(string sfxPath)
    {
        possessionEmitter.Event = sfxPath;
        possessionEmitter.Play();
    }

    public void StopSoundEvent()
    {
        possessionEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}

