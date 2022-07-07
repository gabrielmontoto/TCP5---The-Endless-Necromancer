using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorParedes : MonoBehaviour
{
    [SerializeField] Sprite[] imagens;
    public static Sprite Imagens {   get ;  private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        int rand = Random.Range(0, imagens.Length);
        Imagens = imagens[rand];
    }

}
