using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderPixel : MonoBehaviour
{

    public GameObject normal;
    public GameObject pixel;
    public GameObject particula;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            if (normal.active == true)
            {
                normal.SetActive(false);
                pixel.SetActive(true);

            }
            else if (normal.active == false) 
            {
                normal.SetActive(true);
                pixel.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            particula.SetActive(false);
        }
    }
}
