using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{

    [SerializeField] Material dissolve;
    [SerializeField] public bool podeDissolver;
    [SerializeField] float dissolveAmout;

    public GameObject particulasMorte;
    [SerializeField] GameObject pai;
    ParticleSystem.EmissionModule emission;

    public GameObject fragmento;
    public GameObject alvo;
    public int nFragmentos;
    public float randX;
    public float randY;

    bool spawnado = false;

    public GameObject goreGuerreiro;
    [SerializeField] bool podeGore;
    // Start is called before the first frame update
    void Start()
    {
       // dissolve = GetComponent<SpriteRenderer>().material;

        dissolveAmout = 1.2f;

        emission = particulasMorte.GetComponent<ParticleSystem>().emission;
        alvo = FindObjectOfType<Teste_Player>().gameObject;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            podeDissolver = true;
        }

        if (podeDissolver)
        {
            dissolveAmout -= Time.deltaTime;

            if (dissolveAmout <= 0.8f && goreGuerreiro != null)
            {
                goreGuerreiro.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                goreGuerreiro.GetComponent<BoxCollider2D>().isTrigger = true;
                podeGore = true;
            }
            if (dissolveAmout <= 0f)
            {
                dissolveAmout = 0;
                //Destroy(particulasMorte);
                Destroy(pai, 10f);
                podeDissolver = false;
            }

            dissolve.SetFloat("_DissolveAmount", dissolveAmout);
        }

        if (podeGore)
        {
            goreGuerreiro.GetComponent<GoreGuerreiro>().IniciarGore();
        }
    }

    public void Drop()
    {
        if (!spawnado)
        {
            this.GetComponent<SpriteRenderer>().material = dissolve;
            GameObject go =  Instantiate(particulasMorte, new Vector3(this.transform.position.x, this.transform.position.y + 2f,
                this.transform.position.z), Quaternion.identity);
            go.transform.SetParent(this.transform);

            Destroy(go, 10f);

            for (int i = 0; i < nFragmentos; i++)
            {
                var drop = Instantiate(fragmento, new Vector3(this.transform.position.x + Random.Range(-1, randX),
                    this.transform.position.y + Random.Range(-1, randY), this.transform.position.z), Quaternion.identity);
                drop.transform.SetParent(this.transform);
                drop.GetComponent<Fragmento>().tipoFragmento = 0;
                drop.GetComponent<Fragmento>().alvoPlayer = alvo;
            }
            spawnado = true;
        }
    }
}
