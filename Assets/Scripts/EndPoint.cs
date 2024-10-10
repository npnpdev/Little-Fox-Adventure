using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour{

    // Metoda wywo³ywana po wejœciu obiektu w obszar Triggera
    void OnTriggerEnter2D(Collider2D col) {
        // Sprawdzamy, czy obiekt, który wszed³ w obszar, to gracz (sprawdzamy po tagu)
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Game over");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
