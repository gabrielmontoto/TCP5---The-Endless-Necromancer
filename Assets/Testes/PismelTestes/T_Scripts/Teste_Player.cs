using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Teste_Player : Individuo, ITomarDano
{
    [SerializeField] [Header("Especificas do Player")] [Space] float contDashPossessao;
    [SerializeField] float DashPossessaoMaximo;
    [SerializeField] float forcaDashPossessao;
    [SerializeField] bool dashEmAcao, cooldownDashBool, cooldownPossessaoBool;
    [SerializeField] float cooldownDash, cooldownDashMax, cooldownPossessao;
    [SerializeField] SpriteRenderer[] spriteRenderer;
    [SerializeField] Sprite spriteOriginal;
    //   [SerializeField] Rigidbody2D rigidbody2d;
    [SerializeField] Slider sliderVida, sliderPossessao, sliderVidaInimigoPossuido, sliderExperiencia1, sliderExperiencia2;

    #region salto
    [SerializeField] bool podePular, oSaltoJaFoiDado;
    [Space] [SerializeField] Transform posPes;
    [SerializeField] float raioPes;
    [SerializeField] LayerMask oQueChao;
    [SerializeField] float saltoCont, SaltoMaxCont;
    float gravidade;

    public GameObject poeiraQueda;
    public GameObject poeiraSalto;
    [SerializeField] bool enconstouChao;
    #endregion

    #region angulo do dash e variaveis utilizadas na movimentaçao do dash
    Gamepad_Input controls;
    [SerializeField] Vector2 moveDash, ultimaPosDash;   // ***************************************** pismel <- estou usando o move do controle esquerdo pra fazer o dash pq 
    //ainda nao tinha visto o do lado direito funcionando pra servir, ai quando tiver é so fazer as substituiçoes, me avisa que eu faço
    [SerializeField] float anguloDash, ultimoAnguloDash;
    [SerializeField] GameObject objetoQueVaiGirar;

    #endregion

    #region variaveis avulsas
    [SerializeField] [Space] Vector2 move;
    public Vector2 Move { get { return move; } }
    //[SerializeField] Animator animator;
    #endregion


    [SerializeField] [Header("Info do Possuido")] [Space] float vidaDoPossuido;
    [SerializeField] GameObject inimigoPossuido;
    [SerializeField] TipoInimigo tipodeInimigoPossuido;
    [SerializeField] GameObject[] luzesEfeitos;
    [SerializeField] string InimigoPossuido = "possuido";
    [SerializeField] bool inimigoFoiPossuido;
    float conttemp = 0.5f;
    [SerializeField] Material fogoPossessao;

    [Header("Utilizado pelo Mouse")]
    [Space] [SerializeField] Camera cam;
    [SerializeField] bool UsandoMouse;
    public bool UtilizandoMouse { get { return UsandoMouse; } }

    [Header("Slow Timer")]
    [Space] [SerializeField] TimeManager bulletTime;

    [SerializeField] GameObject poeiraMovimento;
    [SerializeField] GameObject paiParticulas;

    Animator animator;
    Protagonista_Animacoes P_animacoes;

    [SerializeField] PlayerDeath playerDeath;

    [SerializeField] GameObject deathPortal;
    [SerializeField] GameObject efeitoFogo;
    [SerializeField] public bool renasceu;

    public bool reiniciar;
    float tempoReiniciar;
    public bool podeReiniciar;

    [SerializeField] Material materialOriginal;
    [SerializeField] Material materialGuerreiro;

    private PlayerInput playerInput;

    [Space] [Header("outros")] [SerializeField] Protagonista_Animacoes protagonista_Animacoes;
    [SerializeField] RuntimeAnimatorController controllerOriginal;
    [SerializeField] BoxCollider2D colisorBruxa, colisorPossuido;
    [SerializeField] GameObject[] efeitosInimigoPossuido;
    //  GameObject possuido;
    // Start is called before the first frame update
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions["DashControl"].performed += ctx => DashStickMira(ctx);
        playerInput.actions["DashControl"].canceled += ctx => moveDash = Vector2.zero;

        playerInput.actions["MoveControl"].performed += ctx => move = ctx.ReadValue<Vector2>();
        playerInput.actions["MoveControl"].canceled += ctx => move = Vector2.zero;

        playerInput.actions["DashTrigger"].performed += ctx => DashSlowtime();
        playerInput.actions["DashTrigger"].canceled += ctx => DashAction();

        playerInput.actions["Jump"].performed += ctx => ClickSalto();
        playerInput.actions["Jump"].canceled += ctx => SaltoReset();

        //playerInput.actions["NormalAttack"].performed += ctx => ;
        //playerInput.actions["NormalAttack"].canceled += ctx => ;

        //playerInput.actions["SpecialAttack"].performed += ctx => ;
        //playerInput.actions["SpecialAttack"].canceled += ctx => ;

        animator = this.GetComponentInChildren<Animator>();
        P_animacoes = this.GetComponentInChildren<Protagonista_Animacoes>();
    }
    void Start()
    {
        colisorPossuido.enabled = false;
        this.spriteOriginal = GetComponentInChildren<SpriteRenderer>().sprite;
        objetoQueVaiGirar.SetActive(false);
        gravidade = rigidbody2d.gravityScale;
        vida = vidaMaxima;
        if (sliderVida != null)
        {
            sliderVida.maxValue = vidaMaxima;
            sliderVida.value = vida;
        }
        cooldownDash = cooldownDashMax;
        if (sliderPossessao != null)
        {
            sliderPossessao.maxValue = cooldownDashMax;
            sliderPossessao.value = cooldownDash;
        }
        if (sliderExperiencia1 != null)
        {
            sliderExperiencia1.maxValue = experienciaPorNivel;
            sliderExperiencia2.maxValue = sliderExperiencia1.maxValue;
            sliderExperiencia1.value = 0;
            sliderExperiencia2.value = sliderExperiencia1.value;
        }
        if (sliderVidaInimigoPossuido != null)
        {
            sliderVidaInimigoPossuido.maxValue = vidaDoPossuido;
            sliderVidaInimigoPossuido.value = 0;
        }

        cooldownPossessao = 1f;
        fogoPossessao.SetFloat("_DissolveAmount", cooldownPossessao);

        if (tipodeInimigoPossuido == TipoInimigo.Vazio)
        {
            guerreiroAnimacoes.enabled = false;
            protagonista_Animacoes.enabled = true;
            controllerOriginal = animator.runtimeAnimatorController;

        }


    }

    // Update is called once per frame
    void Update()
    {

        if (!reiniciar)
        {
            tempoReiniciar += Time.deltaTime;

            if (tempoReiniciar >= 1)
            {
                Instantiate(deathPortal, new Vector3(this.transform.position.x, this.transform.position.y,
                this.transform.position.z), Quaternion.identity);
                tempoReiniciar = 0;
                reiniciar = true;
            }

        }


        MovimentacaoDoPlayer();


        if (inimigoFoiPossuido)
        {
            if (playerInput.actions["NormalAttack"].triggered)
            {
                rigidbody2d.velocity = Vector2.zero;
            }
            if (playerInput.actions["NormalAttack"].triggered && contAtaque <= 0)
            {

                print("tentando atacar");
                guerreiroAnimacoes.ChangeAnimationState(guerreiroAnimacoes.Ataque_Guerreiro);
                
                contAtaque = contAtaqueMax;
            }
            else
            {
                contAtaque -= Time.deltaTime;

            }
            Ataque(tipodeInimigoPossuido);
            if (podeHabilidade)
                Especial(tipodeInimigoPossuido);
            if (playerInput.actions["SpecialAttack"].triggered)
            {
                HabilidadeEspecial();
            }
            else
            {
                contAtaqueEspecial -= Time.deltaTime;
            }


            if (guerreiroAnimacoes.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                print(guerreiroAnimacoes.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

                if (rigidbody2d.velocity.x != 0)
                    guerreiroAnimacoes.ChangeAnimationState(guerreiroAnimacoes.Andar_Guerreiro);
                else
                    guerreiroAnimacoes.ChangeAnimationState(guerreiroAnimacoes.Idle_Guerreiro);

            }


        }
        //if botaoAtaque pressionado, chamar ataque
        if(oSaltoJaFoiDado)
        Salto();

        if (cooldownPossessaoBool)
        {
            if (cooldownPossessao > 1f)
            {
                cooldownPossessao -= 10f * Time.deltaTime;
                fogoPossessao.SetFloat("_DissolveAmount", cooldownPossessao);
            }
            else
            {
                cooldownPossessao = 1f;
                cooldownPossessaoBool = false;
            }
        }
        if (dashEmAcao)
        {
            if (contDashPossessao <= 0)
            {

                rigidbody2d.velocity = Vector2.zero;
                contDashPossessao = DashPossessaoMaximo;
                P_animacoes.FimPossessao();
                dashEmAcao = false;

            }
        }
    }

    public void SetDevice(bool setado) 
    {
        UsandoMouse = setado;
    }

    private void HabilidadeEspecial()
    {
        if (contAtaqueEspecial <= 0)
        {
            guerreiroAnimacoes.ChangeAnimationState(guerreiroAnimacoes.Habilidade_Guerreiro);



            contAtaqueEspecial = contEspecialMax;
            forcaAtribuida = false;
        }
    }

    private void FixedUpdate()
    {
        if (dashEmAcao)
        {

            if (!inimigoFoiPossuido)
            {

                rigidbody2d.velocity = ultimaPosDash * forcaDashPossessao;

            }
            else
            {
                StartCoroutine("DeleyPossessao");
            }

        }

        /*   if (this.inimigoPossuido != null)
           {
               this.inimigoPossuido.transform.position = this.transform.position;
           }*/
    }
    private void OnEnable()
    {
        Inimigos.EventoDePossessao += EventoDePossessao;
        //controls.InputCalls.Enable();
        

    }
    private void OnDisable()
    {
        //  Inimigos.EventoDePossessao -= EventoDePossessao;
        //controls.InputCalls.Disable();
    }
    IEnumerator DeleyPossessao()
    {
        yield return new WaitForSeconds(0.1f);
        rigidbody2d.velocity = Vector2.zero;
    }

    private void MovimentacaoDoPlayer()
    {
        Basica();
        DashCalculos();
        if (UsandoMouse)
        {
            //  UsandoMouse = true;
            MouseMovimentacao();
        }


    }
    private void MouseMovimentacao()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookPos = mousePos - rigidbody2d.position;
        //print(lookPos);

        ultimaPosDash = new Vector2(lookPos.x, lookPos.y).normalized;
        anguloDash = (Mathf.Atan2(lookPos.x, lookPos.y) * Mathf.Rad2Deg) + 90f;
    }
    private void DashCalculos()
    {
        if (dashEmAcao)
        {
            contDashPossessao -= Time.deltaTime;

        }

        if (cooldownDashBool == false)
        {
            cooldownDash += Time.deltaTime;
            AtualizarSliderDash(cooldownDash);

            if (cooldownDash >= cooldownDashMax)
            {
                cooldownDashBool = true;
                cooldownDash = 0;
            }
        }


        #region pra onde o dash vai se mover

        if (!UsandoMouse)
        {
            anguloDash = (Mathf.Atan2(moveDash.x, moveDash.y) * Mathf.Rad2Deg) + 90;
        }


        if (anguloDash != ultimoAnguloDash)
        {
            if (moveDash.x != 0 || moveDash.y != 0 || UsandoMouse)
            {
                ultimoAnguloDash = anguloDash;
                if (!UsandoMouse)
                    ultimaPosDash = new Vector2(moveDash.x, moveDash.y);
                // objetoQueVaiGirar.transform.rotation = new Quaternion(0,0,move.x * move.y,0);// = new Quaternion(move.x,move.y,0,0) ;
                objetoQueVaiGirar.transform.eulerAngles = new Vector3(0, 180, anguloDash);

            }
        }

        if (moveDash != Vector2.zero || UsandoMouse)
        {
            objetoQueVaiGirar.SetActive(true);
        }
        else
            objetoQueVaiGirar.SetActive(false);

        #endregion
    }
    private void DashStickMira(InputAction.CallbackContext ctx)
    {
        UsandoMouse = false;
        moveDash = ctx.ReadValue<Vector2>();
    }

    private void Dash()
    {
        if (cooldownDashBool)
        {

            dashEmAcao = true;
            cooldownDashBool = false;

            if (this.inimigoPossuido != null)
            {
                SairPossessao();
            }
        }
        P_animacoes.FimPossessaoIdle();
    }

    private void DashSlowtime()
    {
        P_animacoes.podePossuir = true;

        bulletTime.BulletTime();
        objetoQueVaiGirar.SetActive(true);
    }

    private void DashAction()
    {
        AudioManager.instance.SetParameter("SlowMotion", 0);
        P_animacoes.StopSoundEvent();

        if (!bulletTime.TimeOut())
        {
            Dash();
            cooldownPossessao = 10;
            fogoPossessao.SetFloat("_DissolveAmount", cooldownPossessao);
            cooldownPossessaoBool = true;
        }
        else
            P_animacoes.FimPossessao();

        bulletTime.FimBulletTime();
        objetoQueVaiGirar.SetActive(false);
    }


    private void Basica()
    {
        //   Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime * this.velocidadeAndar;
        //  transform.Translate(m, Space.World);
        if (!UsandoMouse)
            rigidbody2d.velocity = new Vector2(move.x * velocidadeAndar, rigidbody2d.velocity.y) ;
        else
        {
            rigidbody2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * velocidadeAndar, rigidbody2d.velocity.y) ;
        }

        objetoQueVaiGirar.transform.position = this.transform.position;
        if (rigidbody2d.velocity.x < 0)
        {
            this.transform.rotation = new Quaternion(0, 180, 0, 0);
            if (!inimigoFoiPossuido)
                this.GetComponentInChildren<SpriteRenderer>().flipX = true;
            viradoDireita = false;
            P_animacoes.movendo = true;
            P_animacoes.direcaoMovimento = false;

           // animator.speed = GameManager.instance.VelocidadeDoJogo;
        }
        else if (rigidbody2d.velocity.x == 0)
        {
            //this.transform.rotation = new Quaternion(0, 180, 0, 0);

            P_animacoes.movendo = false;
            if (this.GetComponentInChildren<SpriteRenderer>().flipX == true)
                P_animacoes.direcaoMovimento = false;
            else if (this.GetComponentInChildren<SpriteRenderer>().flipX == false)
                P_animacoes.direcaoMovimento = true;

           // animator.speed = GameManager.instance.VelocidadeDoJogo;
        }
        else
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            if (!inimigoFoiPossuido)
                this.GetComponentInChildren<SpriteRenderer>().flipX = false;
            viradoDireita = true;
            P_animacoes.movendo = true;
            P_animacoes.direcaoMovimento = true;

          //  animator.speed = GameManager.instance.VelocidadeDoJogo;
        }
    }

    private void EventoDePossessao(float vidaInimigo, float vidaMaximaInimigo, SpriteRenderer[] sprite, GameObject possuido, TipoInimigo tipoInimigoPossuido, Collider2D collider, Vector3 posicao, RuntimeAnimatorController controllerAnimacao)
    {

        if (dashEmAcao && possuido.transform.name != InimigoPossuido)
        {

            this.spriteRenderer[0].material = materialGuerreiro;

            transform.position = posicao;
            vidaDoPossuido = vidaInimigo;
            //spriteRenderer.sprite = sprite;
            inimigoFoiPossuido = true;
            possuido.transform.name = InimigoPossuido;
            collider.enabled = false;
            this.spriteRenderer[0].sprite = sprite[0].sprite;
           // this.spriteRenderer[1].sprite = sprite[1].sprite;
            this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = null;
            //this.possuido = possuido;
            #region adicionando feedback Glow Teste
            //mudanças no possuido
            //  possuido.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
            possuido.transform.position = this.transform.position;

            //  possuido.transform.GetChild(0).GetComponent<GlowPlayer>().BrilhaPorra(true);
            //possuido


            #endregion
            foreach (GameObject item in this.luzesEfeitos)
            {
                item.SetActive(false);
            }
            #region Parametros anteriores    
            this.inimigoPossuido = possuido;
            // possuido.transform.position = Vector3.zero;
            possuido.transform.SetParent(this.transform);

            possuido.transform.GetChild(0).position = this.transform.position;

            //  print(possuido.transform.GetChild(0).name);
            tipodeInimigoPossuido = tipoInimigoPossuido;
            // inimigoPossuido.transform.GetChild(0).transform.position = new Vector3(0,0,0);
            possuido.transform.GetChild(0).gameObject.SetActive(false);
            //   possuido.transform.GetChild(1).gameObject.SetActive(true);


            possuido.transform.rotation = new Quaternion(0, 0, 0, 0);
            #endregion
            P_animacoes.FimPossessao();
            animator.enabled = false;

            if (sliderVidaInimigoPossuido != null)
            {
                sliderVidaInimigoPossuido.maxValue = vidaMaximaInimigo;
                sliderVidaInimigoPossuido.value = vidaDoPossuido;
            }

            // if (tipodeInimigoPossuido == TipoInimigo.Vazio)
            {
                guerreiroAnimacoes.enabled = true;
                protagonista_Animacoes.enabled = false;
                animator.enabled = true;
                animator.runtimeAnimatorController = controllerAnimacao;
            }

            colisorBruxa.enabled = false;
            colisorPossuido.enabled = true;
            this.GetComponentInChildren<SpriteRenderer>().flipX = false;
            foreach (GameObject item in efeitosInimigoPossuido)
            {
                item.SetActive(true);
            }
            posPes.position = new Vector3(posPes.position.x, rigidbody2d.transform.position.y - 4.5f, posPes.transform.position.z);
        }
    }
    private void SairPossessao()
    {

        this.spriteRenderer[0].material = materialOriginal;

        inimigoPossuido.transform.GetChild(0).GetComponent<Inimigos>().Vida = vidaDoPossuido;
        print(vidaDoPossuido);
        //adicionado
        // inimigoPossuido.GetComponent<GlowPlayer>().BrilhaPorra(false);
        inimigoPossuido.transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = 0;
        // inimigoPossuido.transform.GetChild(0).transform.position = new Vector3(0, 0, 0);
        this.spriteRenderer[0].sprite = null;
    //    this.spriteRenderer[1].sprite = null;
        inimigoPossuido.transform.name = InimigoPossuido;
        /*    foreach (GameObject item in luzesEfeitos)
            {
                Destroy(item);
            }*/

        //anterior
        // inimigoPossuido.transform.GetChild(1).gameObject.SetActive(false);
        inimigoPossuido.transform.GetChild(0).gameObject.SetActive(true);
        inimigoPossuido.transform.SetParent(null);
        inimigoPossuido.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        inimigoPossuido.SetActive(true);
        inimigoPossuido.transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        inimigoPossuido.transform.GetChild(0).GetComponent<Inimigos>().EstadosInimigo = EstadosIaInimigo.Parado;
        vidaDoPossuido = 0;

        this.GetComponentInChildren<SpriteRenderer>().sprite = spriteOriginal;
        // Inimigos.EventoDePossessao -= EventoDePossessao;
        tipodeInimigoPossuido = TipoInimigo.Vazio;

        animator.enabled = true;
        foreach (GameObject item in this.luzesEfeitos)
        {
            item.SetActive(true);
        }
        inimigoFoiPossuido = false;

        sliderVidaInimigoPossuido.value = 0;

        guerreiroAnimacoes.enabled = false;
        protagonista_Animacoes.enabled = true;
        animator.runtimeAnimatorController = controllerOriginal;
        colisorBruxa.enabled = true;
        colisorPossuido.enabled = false;
        foreach (GameObject item in efeitosInimigoPossuido)
        {
            item.SetActive(false);
        }
        posPes.position = new Vector3(posPes.position.x, rigidbody2d.transform.position.y-3.5f, posPes.transform.position.z);
        inimigoPossuido = null;
    }

    public void ReceberDano(float valor)
    {
        if (inimigoFoiPossuido)
        {
            vidaDoPossuido -= valor;
            AtualizarSliderVidaInimigoPossuido(vidaDoPossuido);
            vida -= valor * 0.5f;
        }
        else
        {
            vida -= valor;
        }
        // HitEffect();
        AtualizarSliderVidaPlayer(vida);
        if (vida <= 0)
        {
            playerDeath.PlayerMorreu();
        }
    }

    private void ClickSalto()
    {
        oSaltoJaFoiDado = true;
    }
    private void Salto()
    {
        podePular = Physics2D.OverlapCircle(posPes.position, raioPes, oQueChao);

        if (podePular == true)
        {
            if (enconstouChao)
            {
                GameObject go = Instantiate(poeiraQueda, new Vector3(this.posPes.position.x, this.posPes.position.y + 0.3f,
                    this.posPes.position.z), Quaternion.identity);
                go.transform.SetParent(paiParticulas.transform);
                Destroy(go, 0.9f);
                enconstouChao = false;
            }

            //ou o botao

            

            switch (tipodeInimigoPossuido)
            {
                case TipoInimigo.Vazio:
                    AudioManager.instance.PlaySound(AudioManager.instance.SFXPlayerPulo, this.gameObject);
                    break;
                case TipoInimigo.Gurreiro:
                    AudioManager.instance.PlaySound(AudioManager.instance.SFXGuerreiroPulo, this.gameObject);
                    break;

            }
            saltoCont = SaltoMaxCont;
            rigidbody2d.gravityScale = 0;
           // podePular = false;
            // rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, forcaSalto);
            rigidbody2d.AddForce(new Vector2(rigidbody2d.velocity.x, forcaSalto));
            GameObject go_ = Instantiate(poeiraSalto, new Vector3(this.posPes.position.x, this.posPes.position.y + 0.3f, this.posPes.position.z), Quaternion.identity);
            go_.transform.SetParent(paiParticulas.transform);
            Destroy(go_, 0.9f);

        }
        if (oSaltoJaFoiDado)
        {
            if (saltoCont > 0)
            {
                rigidbody2d.AddForce(new Vector2(rigidbody2d.velocity.x, forcaSalto)); // se a velociadde do jogo for <1 o ganho tem que ser >2
                saltoCont -= Time.fixedDeltaTime ;

                if (saltoCont < 0)
                    saltoCont = 0;
            }
            else
            {
                oSaltoJaFoiDado = false;

                rigidbody2d.gravityScale = gravidade;
            }
        }
    }

    private void SaltoReset()
    {
        oSaltoJaFoiDado = false;
        enconstouChao = true;
        rigidbody2d.gravityScale = gravidade;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Fragmento(Clone)")
        {
            experiencia++;

            if (experiencia == experienciaPorNivel)
            {
                nivel++;
                experiencia = 0;
                //abrir game object da escolha
            }
            AtualizarSliderNivel(experiencia);
        }
        // print(collision.name);
    }
    private void AtualizarSliderVidaPlayer(float value)
    {
        if (sliderVida != null)
            sliderVida.value = value;
    }

    private void AtualizarSliderVidaInimigoPossuido(float value)
    {
        if (sliderVidaInimigoPossuido != null)
        {
            sliderVidaInimigoPossuido.value = value;
            if (vidaDoPossuido <= 0)
            {


                SairPossessao();
            }
        }
    }
    private void AtualizarSliderDash(float value)
    {
        if (sliderPossessao != null)
            sliderPossessao.value = value;
    }
    private void AtualizarSliderNivel(float value)
    {
        if (sliderExperiencia1 != null)
        {
            sliderExperiencia1.value = value;
            sliderExperiencia2.value = value;
        }
    }

    public void Renascer()
    {
        GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        efeitoFogo.SetActive(true);
    }
}
