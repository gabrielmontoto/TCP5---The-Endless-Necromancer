using ClassesNaoMono;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField][Tooltip("ainda nao esta funcionando, 1 é velocidade normal \nmais que isso é mais rapido \nmenos que isso mais lento")] float velocidadeDoJogo;
   // [SerializeField] GameObject[] objetos, inimigos;

    private MapeamentoPosicaoMapas mapeamentoSalas;
   // public float VelocidadeDoJogo { get { return velocidadeDoJogo; } set { velocidadeDoJogo = value; } }
    public static GameManager instance;

    [SerializeField] ArmazenadorSpawnInimigosObjetos andares;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        mapeamentoSalas = FindObjectOfType<MapeamentoPosicaoMapas>();



        OrganizadorDosAndares();
    }

    // Update is called once per frame
    void Update()
    {
      //  if(mapeamentoSalas.Salas[0] != null) //trocar por evento
      //  print("sim é sim");
    }

    public void AleatorizarObjetosNasSalas()
    {
        for (int i = 0; i < 25; i++)
        {
            // if(float.Parse(mapeamentoSalas.Salas[i].name) <5)
            //   {
            int quantidadeObjetos = mapeamentoSalas.Salas[i].GetComponent<PosicoesDeSpawnNasSalas>().PosicaoObjetos.Length;
            int quantidadeInimigos = mapeamentoSalas.Salas[i].GetComponent<PosicoesDeSpawnNasSalas>().PosicaoInimigos.Length;
            //um for pra varrer todas as posicoes
            for (int j = 0; j < quantidadeObjetos; j++)
            {
                Transform posSpawn = mapeamentoSalas.Salas[i].GetComponent<PosicoesDeSpawnNasSalas>().PosicaoObjetos[j].transform;
                GameObject obj = null;
                if (float.Parse(mapeamentoSalas.Salas[i].name) < 1)
                {
                    
                    obj = Instantiate(andares.objetosAndar0[Random.Range(0, andares.objetosAndar0.Length)],
                       posSpawn.position,
                       Quaternion.identity);
                   
                }
                else if (float.Parse(mapeamentoSalas.Salas[i].name) >= 1 && float.Parse(mapeamentoSalas.Salas[i].name) < 2)
                {
                     obj = Instantiate(andares.objetosAndar1[Random.Range(0, andares.objetosAndar1.Length)],
                       mapeamentoSalas.Salas[i].GetComponent<PosicoesDeSpawnNasSalas>().PosicaoObjetos[j].transform.position,
                       Quaternion.identity);
                }
                else if (float.Parse(mapeamentoSalas.Salas[i].name) >= 2 && float.Parse(mapeamentoSalas.Salas[i].name) < 3)
                {
                     obj = Instantiate(andares.objetosAndar2[Random.Range(0, andares.objetosAndar2.Length)],
                       mapeamentoSalas.Salas[i].GetComponent<PosicoesDeSpawnNasSalas>().PosicaoObjetos[j].transform.position,
                       Quaternion.identity);
                }
                else if (float.Parse(mapeamentoSalas.Salas[i].name) >= 3 && float.Parse(mapeamentoSalas.Salas[i].name) < 4)
                {
                     obj = Instantiate(andares.objetosAndar3[Random.Range(0, andares.objetosAndar3.Length)],
                       mapeamentoSalas.Salas[i].GetComponent<PosicoesDeSpawnNasSalas>().PosicaoObjetos[j].transform.position,
                       Quaternion.identity);
                }
                else if (float.Parse(mapeamentoSalas.Salas[i].name) >= 4 && float.Parse(mapeamentoSalas.Salas[i].name) < 5)
                {
                     obj = Instantiate(andares.objetosAndar4[Random.Range(0, andares.objetosAndar4.Length)],
                       mapeamentoSalas.Salas[i].GetComponent<PosicoesDeSpawnNasSalas>().PosicaoObjetos[j].transform.position,
                       Quaternion.identity);
                }
                if(obj!=null)
                obj.transform.SetParent(posSpawn);
            }
            for (int j = 0; j < quantidadeInimigos; j++)
            {
                Transform posSpawn = mapeamentoSalas.Salas[i].GetComponent<PosicoesDeSpawnNasSalas>().PosicaoInimigos[j].transform;
                GameObject obj = null;
                if (float.Parse(mapeamentoSalas.Salas[i].name) < 1)
                {
                    obj = Instantiate(andares.inimigosAndar0[Random.Range(0, andares.inimigosAndar0.Length)],
                       mapeamentoSalas.Salas[i].GetComponent<PosicoesDeSpawnNasSalas>().PosicaoObjetos[j].transform.position,
                       Quaternion.identity);
                }
                else if (float.Parse(mapeamentoSalas.Salas[i].name) >= 1 && float.Parse(mapeamentoSalas.Salas[i].name) < 2)
                {
                    obj = Instantiate(andares.inimigosAndar1[Random.Range(0, andares.inimigosAndar1.Length)],
                       mapeamentoSalas.Salas[i].GetComponent<PosicoesDeSpawnNasSalas>().PosicaoObjetos[j].transform.position,
                       Quaternion.identity);
                }
                else if (float.Parse(mapeamentoSalas.Salas[i].name) >= 2 && float.Parse(mapeamentoSalas.Salas[i].name) < 3)
                {
                    obj = Instantiate(andares.inimigosAndar2[Random.Range(0, andares.inimigosAndar2.Length)],
                       mapeamentoSalas.Salas[i].GetComponent<PosicoesDeSpawnNasSalas>().PosicaoObjetos[j].transform.position,
                       Quaternion.identity);
                }
                else if (float.Parse(mapeamentoSalas.Salas[i].name) >= 3 && float.Parse(mapeamentoSalas.Salas[i].name) < 4)
                {
                    obj = Instantiate(andares.inimigosAndar3[Random.Range(0, andares.inimigosAndar3.Length)],
                       mapeamentoSalas.Salas[i].GetComponent<PosicoesDeSpawnNasSalas>().PosicaoObjetos[j].transform.position,
                       Quaternion.identity);
                }
                else if (float.Parse(mapeamentoSalas.Salas[i].name) >= 4 && float.Parse(mapeamentoSalas.Salas[i].name) < 5)
                {
                    obj = Instantiate(andares.inimigosAndar4[Random.Range(0, andares.inimigosAndar4.Length)],
                       mapeamentoSalas.Salas[i].GetComponent<PosicoesDeSpawnNasSalas>().PosicaoObjetos[j].transform.position,
                       Quaternion.identity);
                }
                if (obj != null)
                    obj.transform.SetParent(posSpawn);
            }

            // }
        }
    }

    private void OrganizadorDosAndares()
    {

    }
    public void reiniciar()
    {
        SceneManager.LoadScene(1);
    }
}
