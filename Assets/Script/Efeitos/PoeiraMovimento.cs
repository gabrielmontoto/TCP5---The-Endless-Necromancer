using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoeiraMovimento : MonoBehaviour
{

    [SerializeField] GameObject paiParticulas;
    [SerializeField] GameObject poeiraMovimento;

    [Space] [SerializeField] Transform posPes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PoeiraMovimento_D()
    {
        GameObject go = Instantiate(poeiraMovimento, new Vector3(this.posPes.position.x + 1.3f, this.posPes.position.y + 0.3f,
                    this.posPes.position.z), Quaternion.identity);
        go.transform.SetParent(paiParticulas.transform);
        //go.transform.Rotate(0, -90, 0);
    }
    public void PoeiraMovimento_E()
    {
        GameObject go_2 = Instantiate(poeiraMovimento, new Vector3(this.posPes.position.x - 2.1f, this.posPes.position.y + 0.3f,
                    this.posPes.position.z), Quaternion.identity);
        go_2.transform.SetParent(paiParticulas.transform);
        //go_2.transform.Rotate(0, 90, 0);
    }
}
