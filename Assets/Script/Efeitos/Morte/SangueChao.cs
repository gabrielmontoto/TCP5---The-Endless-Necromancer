using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SangueChao : MonoBehaviour
{

    public ParticleSystem particle;
    public List<ParticleCollisionEvent> collisionEvents;
    Vector3 posIndividuo;
    public float contador;
    [SerializeField] GameObject sangue;
    [SerializeField] GameObject paiGore;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        contador = 0;
        paiGore = GameObject.FindGameObjectWithTag("GerenciadorGore");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        int nCollisionEvents = particle.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < nCollisionEvents; i++)
        {
            posIndividuo = collisionEvents[i].intersection;

            float rand = Random.Range(0f, 0.4f);
            if (i == contador)
            {
                if (this.transform.position.x < posIndividuo.x)
                {
                    var go = Instantiate(sangue, new Vector3(posIndividuo.x - (contador / 5), posIndividuo.y - rand, 
                        posIndividuo.z), Quaternion.identity);
                    go.transform.SetParent(paiGore.transform);
                }
                if (this.transform.position.x > posIndividuo.x)
                {
                    var go = Instantiate(sangue, new Vector3(posIndividuo.x + (contador / 5), posIndividuo.y - rand, 
                        posIndividuo.z), Quaternion.identity);
                    go.transform.SetParent(paiGore.transform);
                }
                contador += 2;
            }
        }
    }
}
