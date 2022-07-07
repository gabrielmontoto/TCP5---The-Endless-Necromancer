using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlma : MonoBehaviour
{

    [SerializeField] GameObject portal;
    [SerializeField] float velocidadeMax;
    [SerializeField] float velocidadeMin;
    [SerializeField] bool follow;

    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        portal = GameObject.FindGameObjectWithTag("Portal");
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            transform.position = Vector3.SmoothDamp(transform.position, portal.transform.position, ref velocity,
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
        if (collision.gameObject.CompareTag("Portal"))
        {
            portal.GetComponent<DeathPortal>().sumir = true;
            Destroy(this.gameObject, 2f);
        }
    }
}
