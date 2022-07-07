using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Coletor : MonoBehaviour
{
    [SerializeField] GameObject corpoMorto;
    [SerializeField] bool colidindo;
    [SerializeField] GameObject coletor;
    [SerializeField] float novaScale;
    [SerializeField] float tempoScale;

    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        coletor = GameObject.FindGameObjectWithTag("Fragmentos");
        novaScale = 0.2f;
        coletor.GetComponent<SpriteRenderer>().enabled = false;

        playerInput = GetComponent<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {
     
        if (playerInput.actions["Interaction"].triggered && colidindo)
        {
            corpoMorto.GetComponent<AlmaMorto>().podeSpawnar = true;
        }

        if (!colidindo)
        {
            coletor.GetComponent<SpriteRenderer>().enabled = false;
            novaScale = 0.2f;
            coletor.transform.localScale = new Vector3(novaScale, novaScale, 0);
        }
    }

    public void AlmasColetadas()
    {
        coletor.GetComponent<SpriteRenderer>().enabled = true;
        novaScale += 0.1f;
        coletor.transform.localScale = new Vector3 (novaScale, novaScale, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CorpoMorto"))
        {
            corpoMorto = collision.gameObject;
            corpoMorto.GetComponent<AlmaMorto>().colidindo = true;
            colidindo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CorpoMorto"))
        {
            corpoMorto.GetComponent<AlmaMorto>().colidindo = false;
            corpoMorto = null;
            colidindo = false;
        }
    }
}
