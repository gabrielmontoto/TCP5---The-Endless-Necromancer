using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragmento : MonoBehaviour
{
    public GameObject alvoPlayer;
    public GameObject alvoColetor;
    [SerializeField] float velocidadeMax;
    [SerializeField] float velocidadeMin;
    [SerializeField] bool follow;
    public int tipoFragmento; // 0 - segue o player; 1 - segue o coletor de fragmentos;
    [SerializeField] Coletor Coletor;
    [SerializeField] GameObject paiParticulas;

    Vector3 velocity = Vector3.zero;

    public GameObject explosao;
    // Start is called before the first frame update
    void Start()
    {
        Coletor = FindObjectOfType<Coletor>();
        paiParticulas = GameObject.FindGameObjectWithTag("GerenciadorParticulas");
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            if (tipoFragmento == 0)
                transform.position = Vector3.SmoothDamp(transform.position, alvoPlayer.transform.position, ref velocity, 
                    Time.deltaTime * Random.Range(velocidadeMin, velocidadeMax));
            else if (tipoFragmento == 1)
                transform.position = Vector3.SmoothDamp(transform.position, alvoColetor.transform.position, ref velocity, 
                    Time.deltaTime * Random.Range(velocidadeMin, velocidadeMax));
        }
        
    }

    public void Seguir() 
    {
        follow = true;
        GetComponentInChildren<SpriteRenderer>().sprite = null;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (tipoFragmento == 0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (follow)
                {
                    GameObject go1 = Instantiate(explosao, new Vector3(alvoPlayer.transform.position.x, alvoPlayer.transform.position.y, 
                        alvoPlayer.transform.position.z), Quaternion.identity);
                    go1.transform.SetParent(paiParticulas.transform);
                    Destroy(this.gameObject);
                }
            }
        }
        else if (tipoFragmento == 1)
        {
            if (collision.gameObject.CompareTag("Fragmentos"))
            {
                Coletor.AlmasColetadas();
                GameObject go2 = Instantiate(explosao, new Vector3(alvoColetor.transform.position.x, alvoColetor.transform.position.y, 
                    alvoColetor.transform.position.z), Quaternion.identity);
                go2.transform.SetParent(paiParticulas.transform);
                Destroy(this.gameObject);
            }
        }
    }
}
