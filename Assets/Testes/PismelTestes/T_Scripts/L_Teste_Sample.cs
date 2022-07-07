using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class L_Teste_Sample : MonoBehaviour
{

    [SerializeField] GameObject esse;
    SpriteRenderer spriterenderer;
    Vector3 move;
    [SerializeField] int velo;

    private PlayerInput playerInput;

    private void Awake()
    {
        spriterenderer = esse.GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();

        //playerInput.actions["MAgic"].performed += ctx => Magic();
        
        playerInput.actions["MoveControl"].performed += ctx => move = ctx.ReadValue<Vector2>();
        playerInput.actions["MoveControl"].canceled += ctx => move = Vector2.zero;
    }

    private void Update()
    {
        Movement();
        //Magic();
    }

    void Movement()
    {
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime * velo;
        transform.Translate(m, Space.World);

        if (move.x < 0)
        {
            spriterenderer.flipX = true;
        }
        else
        {
            spriterenderer.flipX = false;
        }
    }

    void Magic()
    {
        Debug.Log("souChamado");

        if (spriterenderer.color == Color.white)
        {
            Debug.Log(spriterenderer.color);
            spriterenderer.color = Color.red;
            Debug.Log(spriterenderer.color);
        }
        else
        {
            spriterenderer.color = Color.white;
        }
    }

    


    private void OnEnable()
    {
        //controls.InputCalls.Enable();
    }

    private void OnDisable()
    {
        //controls.InputCalls.Disable();
    }

}
