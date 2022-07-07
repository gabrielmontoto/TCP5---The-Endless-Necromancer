using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator_mapas : MonoBehaviour
{
    [SerializeField] GameObject mapasPai;
    [SerializeField] Transform[] posInicialHorizontal,posInicialVertical;
    [SerializeField] List<GameObject> salas; // 0 -> LR  1 ->LRB 2-> LRT 3-> all
    [SerializeField] GameObject[] salasLr, salasLRB, salasLRT, salasLRBT;
    [SerializeField]GameObject[] objetotempCima, objetotempBaixo;
    public List<GameObject> Salas { get { return salas; }  }
    private int direction;
    [SerializeField]private float moveAmountX,moveAmountY;


   [SerializeField] private float timePorSala;
    [SerializeField] float startTime = 0.1f;


    [SerializeField] float minX, MaxX, minY,MaxY;
   [SerializeField] private bool stopGeneration;
    GameObject objFinal, objInicial;

    public bool StopGeneration { get { return stopGeneration; }  }
    private int downCounter;

    [SerializeField] LayerMask room;

    [Space] [SerializeField] PosicaoSpawnMapa posicaoSpawnMapa;

    [SerializeField] GameObject player, SalaFim,salaInicial;
    [SerializeField] bool fimspawn;
    // Start is called before the first frame update
    void Start()
    {
        AdicionarSalas();

        //GameObject obj;
        if (posicaoSpawnMapa == PosicaoSpawnMapa.Vertical)
        {
            int rand = Random.Range(0, posInicialHorizontal.Length);
            transform.position = posInicialHorizontal[rand].position;
            objInicial = Instantiate(salaInicial, transform.position, Quaternion.identity);


            // Instantiate(Player, salas[0].transform.GetChild(0).position, Quaternion.identity);
            player.transform.position = objInicial.transform.GetChild(0).position;
            direction = Random.Range(1, 6);
        }
        else
        {
            int rand = Random.Range(0, posInicialVertical.Length);
            transform.position = posInicialVertical[rand].position;
            objInicial = Instantiate(salas[3], transform.position, Quaternion.identity);
           
            direction = Random.Range(1, 6);
        }

        objInicial.transform.SetParent(mapasPai.transform);
        objInicial.transform.name = "5,0";

    }
    private void Update()
    {
        if(timePorSala <=0 && stopGeneration == false)
        { 
            Move();
            timePorSala = startTime;
        }
        else
        {
            if(stopGeneration == false)
            timePorSala -= Time.deltaTime;
        }

        if (fimspawn == false && stopGeneration)
        {
            objInicial = mapasPai.transform.GetChild(0).gameObject;
           // Instantiate(Player, objInicial.transform.position, Quaternion.identity);
            //Instantiate(FimFase, objFinal.transform.position, Quaternion.identity);
            fimspawn = true;
        }
    }

    private void Move()
    {
        
        if (posicaoSpawnMapa == PosicaoSpawnMapa.Vertical)
        {
            if (direction == 1 || direction == 2) // se nao passou da borda da direita
            {

                if (transform.position.x < MaxX)
                {
                    downCounter = 0;
                    Vector2 newPos = new Vector2(transform.position.x + moveAmountX, transform.position.y);
                    transform.position = newPos;

                    int rand = Random.Range(0, salas.Count);
                    objFinal = Instantiate(salas[rand], transform.position, Quaternion.identity);
                    objFinal.transform.SetParent(mapasPai.transform);
                 //   print("1");
                    objFinal.name = ColocarNome(objFinal.transform.position);
                    direction = Random.Range(1, 6);
                    if (direction == 3)
                        direction = 2;
                    else if (direction == 4)
                        direction = 5;
                }
                else
                {
                    direction = 5;
                }
            }
            else if (direction == 3 || direction == 4) //se nao foi menor que a borda da esquerda
            {

                if (transform.position.x > minX)
                {
                    downCounter = 0;
                    Vector2 newPos = new Vector2(transform.position.x - moveAmountX, transform.position.y);
                    transform.position = newPos;

                    int rand = Random.Range(0, salas.Count);
                    objFinal = Instantiate(salas[rand], transform.position, Quaternion.identity);
                    objFinal.transform.SetParent(mapasPai.transform);
                    objFinal.name = ColocarNome(objFinal.transform.position);
                 //   print("2");
                    direction = Random.Range(3, 6);
                }
                else
                {
                    direction = 5;
                }
            }
            else if (direction == 5)
            {
                downCounter++;
                if (transform.position.y > minY) //se esta sendo maior que o valor mais baixo
                {
                    Collider2D detection = Physics2D.OverlapCircle(transform.position, 0.5f, room);
                    
                    if (detection.GetComponent<RoomType>().Tipo != 1 && detection.GetComponent<RoomType>().Tipo != 3)
                    {
                        string nome;
                        if (downCounter >= 2)
                        {
                            if (objInicial != null)
                            {
                                if (detection.transform.name == objInicial.transform.name)
                                {
                                    nome = objInicial.transform.name;
                                }
                                else
                                {
                                    nome = "vazio";
                                }
                            }
                            else
                            {
                                nome = "vazio";
                            }
                            detection.GetComponent<RoomType>().DestruirSala();  //*******************************

                            objFinal = Instantiate(objetotempCima[Random.Range(0, objetotempCima.Length)], transform.position, Quaternion.identity);
                            objFinal.transform.SetParent(mapasPai.transform);
                            objFinal.name = ColocarNome(objFinal.transform.position);
                            if (nome!="vazio")
                            {
                                objFinal.transform.name = nome;
                            }
                        }
                        else
                        {
                            if (objInicial != null)
                            {
                                if (detection.transform.name == objInicial.transform.name)
                                {
                                    nome = objInicial.transform.name;
                                }
                                else
                                {
                                    nome = "vazio";
                                }
                            }
                            else
                            {
                                nome = "vazio";
                            }
                            detection.GetComponent<RoomType>().DestruirSala(); // ********************************
                          //  int randbottom = Random.Range(1, 4);
                         //   if (randbottom == 2)
                          //      randbottom = 1;
                            objFinal = Instantiate(objetotempBaixo[Random.Range(0, objetotempBaixo.Length)], transform.position, Quaternion.identity);
                            objFinal.transform.SetParent(mapasPai.transform);
                            objFinal.name = ColocarNome(objFinal.transform.position);
                            if (nome != "vazio")
                            {
                                objFinal.transform.name = nome;
                            }
                        }
                    }

                    Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmountY);
                    transform.position = newPos;


                    int rand = Random.Range(0, objetotempCima.Length);
                    objFinal = Instantiate(objetotempCima[rand], transform.position, Quaternion.identity);
                    objFinal.transform.SetParent(mapasPai.transform);
                    objFinal.name = ColocarNome(objFinal.transform.position);
                    direction = Random.Range(1, 6);
                }
                else
                {
                    
                   // objFinal.transform.name = "5,1";
                    Transform posFinal = objFinal.transform;
                    Destroy(objFinal);
                    Collider2D detection = Physics2D.OverlapCircle(transform.position, 0.5f, room);
                    detection.GetComponent<RoomType>().DestruirSala();
                    GameObject obj= Instantiate(SalaFim, posFinal.position, Quaternion.identity);
                    obj.transform.name = "5,1";
                    obj.transform.SetParent(mapasPai.transform);
                    stopGeneration = true;
                }
            }
        }
        #region nao usado
        /*    else
            {
                if (direction == 1 || direction == 2)
                {
                    if (transform.position.y < MaxY)
                    {
                        downCounter = 0;
                        Vector2 newPos = new Vector2(transform.position.x, transform.position.y + moveAmountY);
                        transform.position = newPos;

                        int rand = 3;//Random.Range(0, salas.Length);
                        objFinal= Instantiate(salas[rand], transform.position, Quaternion.identity);

                        direction = Random.Range(1, 6);
                        if (direction == 3)
                            direction = 2;
                        else if (direction == 4)
                            direction = 5;
                    }
                    else
                    {
                        direction = 5;
                    }

                }
                else if (direction == 3 || direction == 4)
                {
                    if (transform.position.y > minY)
                    {
                        downCounter = 0;
                        Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmountY);
                        transform.position = newPos;

                        int rand = 3;//Random.Range(0, salas.Length);
                        objFinal= Instantiate(salas[rand], transform.position, Quaternion.identity);

                        direction = Random.Range(3, 6);
                    }
                    else
                    {
                        direction = 5;
                    }
                }
                else if (direction == 5)
                {
                    downCounter++;
                    if (transform.position.x < MaxX)
                    {
                        Collider2D detection = Physics2D.OverlapCircle(transform.position, 0.5f, room);
                        if ( detection.GetComponent<RoomType>().Tipo != 3)
                        {
                            string nome;
                            if (downCounter >= 2)
                            {

                                if (objInicial != null)
                                {
                                    if (detection.transform.name == objInicial.transform.name)
                                    {
                                        nome = objInicial.transform.name;
                                    }
                                    else
                                    {
                                        nome = "vazio";
                                    }
                                }
                                else
                                {
                                    nome = "vazio";
                                }
                                detection.GetComponent<RoomType>().DestruirSala();
                                objFinal= Instantiate(salas[3], transform.position, Quaternion.identity);
                                if (nome != "vazio")
                                {
                                    objFinal.transform.name = nome;
                                }
                            }
                            else
                            {
                                if (objInicial != null)
                                {
                                    if (detection.transform.name == objInicial.transform.name)
                                    {
                                        nome = objInicial.transform.name;
                                    }
                                    else
                                    {
                                        nome = "vazio";
                                    }
                                }
                                else
                                {
                                    nome = "vazio";
                                }
                                detection.GetComponent<RoomType>().DestruirSala();
                                int randbottom = 3;//Random.Range(0, 3);
                                if (randbottom == 2)
                                    randbottom = 1;
                                objFinal= Instantiate(salas[randbottom], transform.position, Quaternion.identity);
                                if (nome != "vazio")
                                {
                                    objFinal.transform.name = nome;
                                }
                            }
                        }

                        Vector2 newPos = new Vector2(transform.position.x + moveAmountX, transform.position.y);
                        transform.position = newPos;

                        int rand = Random.Range(0, salas.Length);
                        objFinal= Instantiate(salas[rand], transform.position, Quaternion.identity);
                        int d;
                        if(rand == 0)
                        {
                            d = 5;
                        }
                        else if(rand == 1)
                        {
                            d = Random.Range(3, 5);

                        }
                        else if(rand == 2)
                        {
                            d = Random.Range(1, 4);
                            if(d == 3)
                            {
                                d = 5;
                            }
                        }
                        else
                        {
                            d = 3;
                        }

                        direction = d;// 0 -> LR | 1 ->LRB | 2-> LRT | 3-> all
                    }
                    else
                    {
                      //  print(direction);
                     //   print(transform.position.x + " / " + minX);
                        stopGeneration = true;
                        objFinal.transform.name = "SalaFinal";
                    }
                }
            }
            print(direction);
          //  Instantiate(salas[0], transform.position, Quaternion.identity);
           // direction = Random.Range(1, 6);

            */
        #endregion
    }

    private void AdicionarSalas()
    {
        for (int i = 0; i < salasLr.Length; i++)
        {
            salas.Add(salasLr[i]);
        }
        for (int i = 0; i < salasLRB.Length; i++)
        {
            salas.Add(salasLRB[i]);
        }
        for (int i = 0; i < salasLRT.Length; i++)
        {
            salas.Add(salasLRT[i]);
        }
        for (int i = 0; i < salasLRBT.Length; i++)
        {
            salas.Add(salasLRBT[i]);
        }

        objetotempCima = new GameObject[salasLRT.Length + salasLRBT.Length];
        objetotempBaixo = new GameObject[salasLRB.Length + salasLRBT.Length];
     //   print(objetotempCima.Length);
        for (int i = 0; i < salasLRT.Length; i++)
        {

            objetotempCima[i] = salasLRT[i];
        }
        for (int i = 0; i < salasLRBT.Length; i++)
        {
          
            objetotempCima[salasLRT.Length+i] = salasLRBT[i];
        }


        for (int i = 0; i < salasLRB.Length; i++)
        {

            objetotempBaixo[i] = salasLRB[i];
        }
        for (int i = 0; i < salasLRBT.Length; i++)
        {
           // print(i + "---");
            objetotempBaixo[salasLRT.Length + i] = salasLRBT[i];
        }
    }

    private string ColocarNome(Vector2 posicao)
    {
        string nome = "";
     //   print("---------------");
       // print(posicao);

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
              //  print(moveAmountX * i + "/" + moveAmountY * -j);
                if (posicao.x == moveAmountX * j && posicao.y == moveAmountY*-i)
                {
                    nome = i+","+j;
                    //print("entrou");
                    break;
                    
                }
            }

        }



        return nome;
    }
}

public enum PosicaoSpawnMapa
{
    Vertical,
    Horizontal
}