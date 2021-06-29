using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerarNiveis : MonoBehaviour
{
    public Transform pontoInicial;
    public GameObject salas;
    

    public float deslocamento;

    private float intervaloEntreSalas;
    public float intervaloInicialSalas = 0.25f;

    public float bordaDireita;
   
    private bool terminarNivel;
    void Start()
    {
        transform.position = pontoInicial.position;
        Instantiate(salas, transform.position, Quaternion.identity);

    }

    private void Update()
    {
        if (intervaloEntreSalas <= 0 && terminarNivel == false)
        {
            Mover();
            intervaloEntreSalas = intervaloInicialSalas;        
        }
        else 
        {
            intervaloEntreSalas -= Time.deltaTime;
        }

    }

    private void Mover()
    {
     
            if (transform.position.x < bordaDireita)
            {
                transform.position += Vector3.right * deslocamento;
                
            }
            else
            {
                terminarNivel = true; 
            }
        

        
    

        Instantiate(salas, transform.position, Quaternion.identity);
    }
}
