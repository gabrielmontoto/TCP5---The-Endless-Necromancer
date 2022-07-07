using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gore : MonoBehaviour
{
    public float tempo;
    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        tempo = 4.2f;
        GetComponent<Rigidbody2D>().AddForce(Vector2.one * 10, ForceMode2D.Impulse);
        scale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        tempo -= Time.deltaTime;

        scale -= 0.15f * Time.deltaTime;
        transform.localScale = new Vector2(scale, scale);

        if (tempo <= 0)
        {
            Destruir();
        }
    }

    public void Destruir()
    {
        Destroy(this.gameObject);
    }
}
