using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour{

    // Metoda wywo�ywana po wej�ciu obiektu w obszar Triggera
    void OnTriggerEnter2D(Collider2D col) {
        // Sprawdzamy, czy obiekt, kt�ry wszed� w obszar, to gracz (sprawdzamy po tagu)
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
