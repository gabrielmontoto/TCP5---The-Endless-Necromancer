using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmaMorto : MonoBehaviour
{
    [SerializeField] GameObject alvo;
    public GameObject fragmento;
    [SerializeField] int qntFragmentos;
    public int contador;
    public float tempo;
    public bool podeSpawnar;

    [SerializeField] GameObject botao;
    public bool colidindo;
    bool podeBotao;

    [SerializeField] GameObject[] cranios;

    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        alvo = GameObject.FindGameObjectWithTag("Fragmentos");
        int rand = Random.Range(1, 4);

        for (int i = 0; i < rand; i++) 
        {
            cranios[i].SetActive(true);
        }
        qntFragmentos = rand;
        podeBotao = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (podeBotao)
        {
            if (colidindo && !podeSpawnar)
                botao.SetActive(true);
            else
                botao.SetActive(false);
        }

        if (podeSpawnar)
        {
            podeBotao = false;
            tempo += 1 * Time.deltaTime;

            if (tempo >= 1)
            {
                if (contador < qntFragmentos)
                    Spawnar(contador);

                contador += 1;
                tempo = 0;
            }

            if (contador >= qntFragmentos)
                podeSpawnar = false;
        }
    }

    void Spawnar(int j)
    {

        var drop = Instantiate(fragmento, new Vector3(cranios[j].transform.position.x, cranios[j].transform.position.y,
                    cranios[j].transform.position.z), Quaternion.identity);
        drop.transform.parent = gameObject.transform;
        drop.GetComponent<Fragmento>().tipoFragmento = 1;
        drop.GetComponent<Fragmento>().alvoColetor = alvo;

        cranios[j].GetComponent<Animator>().Play("Cranio_" + j);
    }
}
