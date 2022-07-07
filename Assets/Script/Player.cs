using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : Individuo
{
    [SerializeField] [Header("Especificas do Player")] [Space] float contDashPossessao;
    [SerializeField] float DashPossessaoMaximo;
    [SerializeField] float forcaDashPossessao;
    [SerializeField] bool dashEmAcao, cooldownDashBool;
    [SerializeField] float cooldownDash, cooldownDashMax;
    [SerializeField] SpriteRenderer[] spriteRenderer;
    [SerializeField] Sprite spriteOriginal;
  //  [SerializeField] Rigidbody2D rigidbody2d;

    #region angulo do dash e variaveis utilizadas na movimentaçao do dash
    Gamepad_Input controls;
    [SerializeField] Vector2 moveDash, ultimaPosDash;   // ***************************************** pismel <- estou usando o move do controle esquerdo pra fazer o dash pq 
    //ainda nao tinha visto o do lado direito funcionando pra servir, ai quando tiver é so fazer as substituiçoes, me avisa que eu faço
    [SerializeField] float anguloDash, ultimoAnguloDash;
    [SerializeField] GameObject objetoQueVaiGirar;

    #endregion

    #region variaveis avulsas
    Vector3 move;
    //[SerializeField] Animator animator;
    #endregion


    [SerializeField] [Header("Info do Possuido")] [Space] float vidaDoPossuido;
    [SerializeField] GameObject inimigoPossuido;
    [SerializeField] TipoInimigo tipodeInimigoPossuido;
    [SerializeField] GameObject[] luzesEfeitos;
    [SerializeField] string InimigoPossuido = "possuido";
    [SerializeField] bool inimigoFoiPossuido;
    float conttemp = 0.5f;

    [Header("Utilizado pelo Mouse")]
    [Space] [SerializeField] Camera cam;
    [SerializeField] bool UsandoMouse;
    // Start is called before the first frame update
    private void Awake()
    {
        controls = new Gamepad_Input();
        controls.InputCalls.DashControl.performed += ctx => moveDash = ctx.ReadValue<Vector2>();
        controls.InputCalls.DashControl.canceled += ctx => moveDash = Vector2.zero;

        controls.InputCalls.MoveControl.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.InputCalls.MoveControl.canceled += ctx => move = Vector2.zero;

        controls.InputCalls.DashTrigger.performed += ctx => Dash();


    }
    void Start()
    {
        this.spriteOriginal = GetComponent<SpriteRenderer>().sprite;
        objetoQueVaiGirar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MovimentacaoDoPlayer();
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
            if (contDashPossessao <= 0)
            {
                dashEmAcao = false;
                rigidbody2d.velocity = Vector2.zero;
                contDashPossessao = DashPossessaoMaximo;
            }
        }

        if (this.inimigoPossuido != null)
        {
            this.inimigoPossuido.transform.position = this.transform.position;
        }
    }
    private void OnEnable()
    {
        Inimigos.EventoDePossessao += EventoDePossessao;
        controls.InputCalls.Enable();
    }
    private void OnDisable()
    {
        //  Inimigos.EventoDePossessao -= EventoDePossessao;
        controls.InputCalls.Disable();
    }
    IEnumerator DeleyPossessao()
    {
        yield return new WaitForSeconds(0.1f);
        rigidbody2d.velocity = Vector2.zero;
    }
    private void Mover(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>();
    }

    private void MovimentacaoDoPlayer()
    {
        Basica();
        DashCalculos();

        //if(Input.GetKeyDown(KeyCode.E))
        //{
        //    /*Dash();
        //    ultimaPosDash = new Vector2(1, 0);*/
        //    bulletTime.BulletTime();
        //}

        //if (Input.GetKeyUp(KeyCode.E)) 
        //{
        //    bulletTime.FimBulletTime();
        //    Dash();
        //    ultimaPosDash = new Vector2(1, 0);
        //}
        if (Input.GetKeyDown(KeyCode.E))
        {
            Dash();
            // ultimaPosDash = new Vector2(1, 0);
        }
        MouseMovimentacao();
    }
    private void MouseMovimentacao()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookPos = mousePos - rigidbody2d.position;
        print(lookPos);
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
            cooldownDash -= Time.deltaTime;
            if (cooldownDash <= 0)
            {
                cooldownDashBool = true;
                cooldownDash = cooldownDashMax;
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



    }

    private void Basica()
    {
        //   Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime * this.velocidadeAndar;
        //  transform.Translate(m, Space.World);
        if (!UsandoMouse)
            rigidbody2d.velocity = new Vector2(move.x, move.y) * velocidadeAndar;
        else
        {
            rigidbody2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * velocidadeAndar;
        }

        objetoQueVaiGirar.transform.position = this.transform.position;
        if (rigidbody2d.velocity.x < 0)
        {
            this.transform.rotation = new Quaternion(0, 180, 0, 0);

            //animator.SetBool("walk", true);
        }
        else if (rigidbody2d.velocity.x == 0)
        {
            //animator.SetBool("walk", false);
        }
        else
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);

            //animator.SetBool("walk", true);
        }
    }

    private void EventoDePossessao(float vidaInimigo,float vidaMaximaDoInimigo, SpriteRenderer[] sprite, GameObject possuido, TipoInimigo tipoInimigoPossuido, Collider2D collider, Vector3 posicao, RuntimeAnimatorController controllerNovo)//, Transform transform)
    {

        if (dashEmAcao && possuido.transform.name != InimigoPossuido)
        {
            vidaDoPossuido = vidaInimigo;
            //spriteRenderer.sprite = sprite;
            inimigoFoiPossuido = true;
            possuido.transform.name = InimigoPossuido;
            collider.enabled = false;
            this.spriteRenderer[0].sprite = sprite[0].sprite;
            this.spriteRenderer[1].sprite = sprite[1].sprite;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
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
            possuido.transform.SetParent(this.transform);
            tipodeInimigoPossuido = tipoInimigoPossuido;
            possuido.transform.GetChild(0).gameObject.SetActive(false);
            possuido.transform.GetChild(1).gameObject.SetActive(true);

            possuido.transform.rotation = new Quaternion(0, 0, 0, 0);
            #endregion
            this.GetComponent<Animator>().enabled = false;
        }
    }
    private void SairPossessao()
    {
        //adicionado
        // inimigoPossuido.GetComponent<GlowPlayer>().BrilhaPorra(false);
        inimigoPossuido.transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = 0;
        this.spriteRenderer[0].sprite = null;
        this.spriteRenderer[1].sprite = null;
        inimigoPossuido.transform.name = InimigoPossuido;
        /*    foreach (GameObject item in luzesEfeitos)
            {
                Destroy(item);
            }*/

        //anterior
        inimigoPossuido.transform.GetChild(1).gameObject.SetActive(false);
        inimigoPossuido.transform.GetChild(0).gameObject.SetActive(true);
        inimigoPossuido.transform.SetParent(null);
        inimigoPossuido.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        inimigoPossuido.SetActive(true);
        inimigoPossuido.transform.position = this.transform.position;
        vidaDoPossuido = 0;
        inimigoPossuido = null;
        this.GetComponent<SpriteRenderer>().sprite = spriteOriginal;
        // Inimigos.EventoDePossessao -= EventoDePossessao;
        tipodeInimigoPossuido = TipoInimigo.Vazio;

        this.GetComponent<Animator>().enabled = true;
        foreach (GameObject item in this.luzesEfeitos)
        {
            item.SetActive(true);
        }
        inimigoFoiPossuido = false;
    }
    protected override void Ataque(TipoInimigo tipo)
    {
        //  base.Ataque(tipo);
        //ataque arqueiro
        //ataque guerreiro
        //ataque mago
    }
}
