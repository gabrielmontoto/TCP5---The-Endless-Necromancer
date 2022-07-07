using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    [SerializeField] LevelGenerator_mapas lvlgen;
    [SerializeField] LayerMask mask;
    [SerializeField] GameObject mapasPai;


    // Update is called once per frame
    void Update()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 1, mask);
        if(col == null && lvlgen.StopGeneration == true)
        {
            int rand = Random.Range(0, lvlgen.Salas.Count);
            GameObject obj= Instantiate(lvlgen.Salas[rand], transform.position, Quaternion.identity);
            obj.transform.SetParent(mapasPai.transform);
            obj.transform.name = gameObject.name;
            Destroy(gameObject);
        }
        else if(lvlgen.StopGeneration == true)
        {
            Destroy(gameObject);
        }
    }
}
