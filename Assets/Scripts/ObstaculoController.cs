using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstaculoController : MonoBehaviour
{

    private Rigidbody2D obstaculoRB;
    private GameController _gameController;
    private CameraShaker _cameraShaker;

    // Start is called before the first frame update
    void Start()
    {
        obstaculoRB = GetComponent<Rigidbody2D>();
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

        _cameraShaker = FindObjectOfType(typeof(CameraShaker)) as CameraShaker;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveObjeto();
    }

    void MoveObjeto()
    {
        transform.Translate(Vector2.left * _gameController._obstaculoVelocidade * Time.smoothDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _gameController._fxGame.PlayOneShot(_gameController._fxDie);
            _gameController._vidasPlayer--;
            if (_gameController._vidasPlayer <= 0)
            {
                Debug.Log("Fim do jogo");
                _gameController._txtVidas.text = "0";
                Time.timeScale = 0;
                _gameController._txtRestart.text = "GAME OVER \n APERTE 'R' PARA RESETAR O JOGO.";
            }
            else
            {
                _gameController._txtVidas.text = _gameController._vidasPlayer.ToString();
                Debug.Log(_gameController._fxGame);
                Debug.Log("Tocou no obstáculo");
                _cameraShaker.ShakeIt();
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
        Debug.Log("Cacto foi destruido");
    }
}
