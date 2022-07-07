using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tocha : MonoBehaviour
{

    [SerializeField] bool flick;
    [SerializeField] int r;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        flick = true;

        /*r = Random.Range(0.0f, 1f);

        if (r > 0.5f)
            flick = false;
        else if (r < 0.5f)
            flick = true;*/

        r = Random.Range(1, 9);

        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flick)
        {
            if (r == 1)
                anim.Play("Tocha_1");
            else if (r == 2)
                anim.Play("Tocha_2");
            else if (r == 3)
                anim.Play("Tocha_3");
            else if (r == 4)
                anim.Play("Tocha_4");
            else if (r == 5)
                anim.Play("Tocha_5");
            else if (r == 6)
                anim.Play("Tocha_6");
            else if (r == 7)
                anim.Play("Tocha_7");
            else if (r == 8)
                anim.Play("Tocha_8");
            else if (r == 9)
                anim.Play("Tocha_9");
        }
    }
}
