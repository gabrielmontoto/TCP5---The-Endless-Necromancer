using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    [SerializeField] GameObject pai;
    [SerializeField] int tipo;
    public int Tipo { get { return tipo; } }

    public void DestruirSala()
    {
        Destroy(pai.gameObject);
    }
}
