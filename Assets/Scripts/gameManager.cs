using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    // public float velocidad = 2f;
    //public GameObject grid;
    public Renderer fondo;

  //  public List<GameObject> grids;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //grids = new List<GameObject>();
        // Crear Mapa
       // for (int i = 0; i < 21; i++)
      //  {
        //    Instantiate(grid, new Vector2(-10 + i, -3), Quaternion.identity);
       // }


    }

    // Update is called once per frame 
    void Update()
    {
        // Mover fondo
        fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.015f, 0) * Time.deltaTime;

        // Mover Mapa
       /* for (int i = 0; i < grids.Count; i++)
        {
            grids[i].transform.position = grids[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
        }*/
    }

}
