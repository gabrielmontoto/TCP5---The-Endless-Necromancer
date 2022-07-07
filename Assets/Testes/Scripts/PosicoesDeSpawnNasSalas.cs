using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicoesDeSpawnNasSalas : MonoBehaviour
{
    [SerializeField] GameObject[] posicaoObjetos, posicaoInimigos;
    public GameObject[] PosicaoObjetos { get { return posicaoObjetos; } }
    public GameObject[] PosicaoInimigos { get { return posicaoInimigos; } }

}
