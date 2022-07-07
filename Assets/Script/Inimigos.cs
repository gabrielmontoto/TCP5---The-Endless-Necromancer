using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigos : Individuo, ITomarDano
{
    [SerializeField] TipoInimigo tipo;
    public static Action<float,float, SpriteRenderer[], GameObject, TipoInimigo,Collider2D,Vector3, RuntimeAnimatorController> EventoDePossessao;
    
    [SerializeField] SpriteRenderer[] sprite;
   // [SerializeField] GameObject[] LuzesEfeitos;
    [SerializeField] GameObject pai;
    [SerializeField] string nome;
    [SerializeField] float contNome,contMaxNome,gravidade;
    [SerializeField] Collider2D colisorPossessao;

    [SerializeField] GameObject[] dissolve;
    [SerializeField] AudioClip audioClip;
    bool deleteThisIfIForget;

    #region movimentacao
    [SerializeField][Space][Header("Movimento")] EstadosIaInimigo estadosAcao;
    
    Teste_Player player;
    public float Velocidade { get { return velocidadeAndar; } }
    public EstadosIaInimigo EstadosInimigo { get { return estadosAcao; } set { estadosAcao = value; } }
    #endregion

    [SerializeField] int randomAtaqueOuEspecialMax;

    [Space] [Header("testes!")] [SerializeField] bool habilitarEspecial, habilitarAtaque,habilitarverificarColidido;
    [SerializeField] float contParado, contParadoMax;
    [SerializeField] Collider2D colisorInterno, colisorExterno;




    [SerializeField] RuntimeAnimatorController controlerOriginal;
    public float Vida { get { return vida; } set{ vida = value; }}
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Teste_Player>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        //sprite = GetComponent<SpriteRenderer>().sprite;
        Ataque(tipo);
        nome = pai.transform.name;
        contNome = contMaxNome;
        gravidade = rigidbody2d.gravityScale;
        controlerOriginal = GetComponentInChildren<Animator>().runtimeAnimatorController;
        _paiParticulas = GameObject.FindGameObjectWithTag("GerenciadorParticulas");
    }

    // Update is called once per frame
    void Update()
    {
        if(pai.transform.name != nome)
        {
            contNome -= Time.deltaTime;
            if(contNome <=0)
            {
                pai.transform.name = nome;
                this.GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<Rigidbody2D>().gravityScale = gravidade;
                contNome = contMaxNome;
                colisorPossessao.enabled = true;

            }
        }

       // print(estadosAcao);
       

     /*   if (contDuracaoAtaqueEspecial<=0)
        {
            habilitarEspecial = false;
            contDuracaoAtaqueEspecial = contDuracaoAtaqueEspecialMax;
            contAtaqueEspecial = contEspecialMax;
           // estadosAcao = EstadosIaInimigo.Patrulhando;
           
        }*/
        if(vida<=0)
        {
            estadosAcao = EstadosIaInimigo.Morrendo;
        }
        //se estiver proximo do player parar de andar e mudar de estado

        /*   else if(estadosAcao != EstadosIaInimigo.Morrendo) //if(rigidbody2d.transform.position.x - player.transform.position.x > raioAtaque && estadosAcao!=EstadosIaInimigo.Morrendo)
           {
               estadosAcao = EstadosIaInimigo.Patrulhando;
           }*/
        ControleEstados();
        PermissaoMudarEstado(estadosAcao);
    }

    private void PermissaoMudarEstado(EstadosIaInimigo estadoAtual)
    {
        if(estadosAcao==estadoAtual)
        {
            estadosAcao = estadoAtual;
        }
        else
        {
            if(estadoAtual == EstadosIaInimigo.Morrendo)
            {
                estadosAcao = EstadosIaInimigo.Morrendo;
            }
            else if(estadoAtual == EstadosIaInimigo.Parado)
            {
                estadosAcao = EstadosIaInimigo.Parado;
            }
            //else if()
            //se pode mudar
        }
        //se o estado for igual mantem
        //se for diferente ele ve se pode alterar
        //morte é se aconteceu deve mudar
        //se estiver em especial deve manter ate que termine
        //se atacar deve manter ate que termine
        //se nada disso, patrulhar
    }
    private void ControleEstados()
    {
        switch (estadosAcao)
        {
            case EstadosIaInimigo.Patrulhando:

                guerreiroAnimacoes.ChangeAnimationState(guerreiroAnimacoes.Andar_Guerreiro);

                if (habilitarEspecial)
                {
                    estadosAcao = EstadosIaInimigo.Especial;
                    habilitarEspecial = false;
                }
                else if (habilitarAtaque)
                {
                    estadosAcao = EstadosIaInimigo.Atacando;
                    habilitarAtaque = false;
                }

                #region descontinuado

                /*  if (Vector2.Distance(player.transform.position, rigidbody2d.transform.position) < raioAtaque * distanciaAtaque)
                  {//substituir por encostar no colisor
                      //se sair de 0 a 3 ele continua a chamar o ataque
                      //se nao ele chama o patrulhar, assim se durante o frame os valores baterem ele executa o novo ataque
                      //colocar uma corotina de meio segundo? pra esperar entre as açoes?

                      if (player.transform.position.y > rigidbody2d.transform.position.y - raioAtaque && player.transform.position.y < rigidbody2d.transform.position.y + raioAtaque && estadosAcao != EstadosIaInimigo.Morrendo)
                      {
                          estadosAcao = EstadosIaInimigo.Parado;

                         // contAtaqueEspecial -= Time.deltaTime; //isso sai daqui e pode ficar mais em cima no update?
                      }
                  }*/
                #endregion


                break;
            case EstadosIaInimigo.Atacando:

                if(contAtaque>=0)
                {
                    contAtaque -= Time.deltaTime;
                    guerreiroAnimacoes.ChangeAnimationState(guerreiroAnimacoes.Ataque_Guerreiro);
                }
                else
                {
                    
                    contAtaque = contAtaqueMax;
                  
                }
                Ataque(tipo);
                //  estadosAcao = EstadosIaInimigo.Atacando;
                rigidbody2d.velocity = Vector2.zero;
                
                //overlap?



                if (contAtaque == contAtaqueMax)
                {
                    habilitarAtaque = false;
                    habilitarEspecial = false;
                    estadosAcao = EstadosIaInimigo.Parado;
                }

                break;
            case EstadosIaInimigo.Especial:

                contDuracaoAtaqueEspecial -= Time.deltaTime;

                if (contDuracaoAtaqueEspecial <= 0)
                {
                    guerreiroAnimacoes.ChangeAnimationState(guerreiroAnimacoes.Habilidade_Guerreiro);
                }

                if (podeHabilidade) 
                {
                    
                     Especial(tipo);
                    print("teste 2");
                }

                if(guerreiroAnimacoes.animator.GetCurrentAnimatorStateInfo(0).IsName(guerreiroAnimacoes.Habilidade_Guerreiro))
                {
                    print("teste 1");
                    print(guerreiroAnimacoes.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                    if (guerreiroAnimacoes.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                    {
                        forcaAtribuida = false;
                        // rigidbody2d.velocity = Vector2.zero;
                        contDuracaoAtaqueEspecial = contDuracaoAtaqueEspecialMax;
                        habilitarEspecial = false;
                        habilitarAtaque = false;
                        estadosAcao = EstadosIaInimigo.Parado;
                    }
                }


                break;
            case EstadosIaInimigo.Morrendo:
                morrer();
                guerreiroAnimacoes.ChangeAnimationState(guerreiroAnimacoes.Idle_Guerreiro);

                break;
            case EstadosIaInimigo.Parado: //dar um delay

                guerreiroAnimacoes.ChangeAnimationState(guerreiroAnimacoes.Idle_Guerreiro);

                rigidbody2d.velocity = Vector2.zero;
                ataqueOuEspecialCausado = false;
                //  ataqueOuEspecialCausado = false;
                contParado -= Time.deltaTime;

                if (contParado <= 0 )
                {
                    habilitarverificarColidido = true;
                    //guerreiro
                    if (tipo == TipoInimigo.Gurreiro)
                    {
                        if (habilitarAtaque)
                        {
                            
                            estadosAcao = EstadosIaInimigo.Atacando;
                            
                            
                        }
                        else if (habilitarEspecial)
                        {
                           
                            estadosAcao = EstadosIaInimigo.Especial;
                        }

                    }
                    else if (tipo == TipoInimigo.Arqueiro)//arqueiro
                    {
                        if (habilitarEspecial)
                        {
                            estadosAcao = EstadosIaInimigo.Especial;
                        }
                        else if (habilitarAtaque)
                        {
                            estadosAcao = EstadosIaInimigo.Atacando;
                        }
                    }
                    
                    contParado = contParadoMax;

                    if (!habilitarAtaque && !habilitarEspecial && pai.transform.name == nome)
                    {
                        estadosAcao = EstadosIaInimigo.Patrulhando;
                    }

                }



                #region versao 1 ataque
                /*
                contParado -= Time.deltaTime;
                if (contParado <= 0)
                {
                    
                    int randomAtaqueOuEspecial = 0;
                    randomAtaqueOuEspecial = UnityEngine.Random.Range(0, randomAtaqueOuEspecialMax);



                    if (randomAtaqueOuEspecial > 7) //talvez ele precise manter que esta no especial ate que o especial termine
                    {
                        estadosAcao = EstadosIaInimigo.Especial;
                    }
                    else
                    {
                        estadosAcao = EstadosIaInimigo.Atacando;
                    }
                    contParado = contParadoMax;
                }

                */
                #endregion
                break;
            case EstadosIaInimigo.Ocupado:
                //realizando alguma animaçao? nao sei se vai precisar ser usado
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colisorPossessao.IsTouching(collision))
        {
            if (EventoDePossessao != null && collision.gameObject.CompareTag("Player"))
            {
                EventoDePossessao(vida,vidaMaxima, sprite, pai, tipo, colisorPossessao,transform.position,controlerOriginal);
                // this.gameObject.SetActive(false);
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colisorPrioritario(tipo, collision);
             habilitarverificarColidido = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (colisorInterno.IsTouching(collision))
            {
                habilitarAtaque = false;
            }
            if(colisorExterno.IsTouching(collision))
            {
                habilitarEspecial = false;
            }
        }
    }

    void colisorPrioritario(TipoInimigo tipo, Collider2D colidindo)
    {
        if(tipo==TipoInimigo.Arqueiro)
        {
            if(colisorInterno.IsTouching(colidindo))
            {
                habilitarEspecial = true;
                //especial
            }
             if(colisorExterno.IsTouching(colidindo))
            {
                habilitarAtaque = true;
                //ataque
            }

        }
        else if(tipo==TipoInimigo.Gurreiro)
        {
            if (colisorInterno.IsTouching(colidindo))
            {
                //ataque
                habilitarAtaque = true;
             
            }
            else if (colisorExterno.IsTouching(colidindo))
            {
                habilitarEspecial = true;
             
                //especial
            }
        }
    }

    public void ReceberDano(float dano)
    {
        vida -= dano;
        if(vida<=0)
        {
            estadosAcao = EstadosIaInimigo.Morrendo;
            AudioManager.instance.PlaySound(AudioManager.instance.SFXGuerreiroMorrendo, this.gameObject);
        }
    }
    private void morrer()
    {
        dissolve[0].GetComponent<Dissolve>().Drop();
        dissolve[0].GetComponent<Dissolve>().podeDissolver = true;
      //  dissolve[1].GetComponent<Dissolve>().podeDissolver = true;
    }
}


