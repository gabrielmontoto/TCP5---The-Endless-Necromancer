using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySangue : MonoBehaviour
{
    SpriteRenderer sprite;
    [SerializeField] Sprite[] opcoes;
    float cor;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        int rand = Random.Range(1, opcoes.Length);
        sprite.sprite = opcoes[rand];
    }

    // Update is called once per frame
    void Update()
    {
        cor = sprite.color.a;
        cor -= 0.3f * Time.deltaTime;

        if (cor <= 0.1f)
        {
            Destruir();
        }

        sprite.color = new Color(1, 1, 1, cor);
    }

    public void Destruir()
    {
        Destroy(this.gameObject);
    }
}
