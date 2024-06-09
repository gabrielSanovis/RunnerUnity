using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    private GameController _gameController;
    private Rigidbody2D _moedasRB2D;



    // Start is called before the first frame update
    void Start()
    {

        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

        _moedasRB2D = GetComponent<Rigidbody2D>();
        _moedasRB2D.velocity = new Vector2(-5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _gameController.Pontos(1);
            _gameController._fxGame.PlayOneShot(_gameController._fxMoedaColetada);
            Debug.Log("Pegou a moeda");
            Destroy(this.gameObject);
        }
    }

        private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
        Debug.Log("moeda foi destruida");
    }
}
