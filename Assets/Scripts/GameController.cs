using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{

    // Propriedades global
    [Header("Configuração Global")]
    
    // Propriedades do Chão
    [Header("Configuração do Chão")]
    public float _chaoDestruido;
    public float _chaoTamanho;
    public float _chaoVelocidade;
    public GameObject _chaoPrefab;


    [Header("Configuração do Obstáculo")]
    public float _obstaculoTempo;
    public GameObject _obstaculoPrefab;
    public float _obstaculoVelocidade;

    [Header("Configuração de gameOver")]

    [Header("Configuração da Moeda")]
    public float _coinTempo;
    public GameObject _coinPrefab;

    [Header("Configuração UI")]
    public int _pontosPlayer;
    public Text _txtRestart;
    public Text _txtPontos;
    public int _vidasPlayer;
    public Text _txtVidas;
    public Text _txtMetros;

    [Header("Configuração de Distancia")]
    public int _metrosPercorridos = 0;

    [Header("Sons e Efeitos")]
    public AudioSource _fxGame;
    public AudioClip _fxMoedaColetada;
    public AudioClip _fxJump;
    public AudioClip _fxDie;




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnObstaculo");
        StartCoroutine("SpawnCoin");
        InvokeRepeating("DistanciaPercorrida", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_vidasPlayer <= 0 && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
            _txtRestart.text = "";
        }
    }
    IEnumerator SpawnObstaculo()
    {
        yield return new WaitForSeconds(_obstaculoTempo);
        GameObject objetoObstaculoTemp = Instantiate(_obstaculoPrefab);
        StartCoroutine("SpawnObstaculo");
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("SpawnCoin");
    }

    IEnumerator SpawnCoin()
    {
        int moedasaleatorias = Random.Range(1,5);
        for (int contagem = 1; contagem <= moedasaleatorias; contagem ++)
        {
            yield return new WaitForSeconds(_coinTempo);
            GameObject _objetoSpawn = Instantiate(_coinPrefab);
            _objetoSpawn.transform.position = new Vector3(_objetoSpawn.transform.position.x, _objetoSpawn.transform.position.y, 0);
        }
    }

    public void Pontos(int _qtdPontos)
    {
        _pontosPlayer = _pontosPlayer + _qtdPontos;
        _txtPontos.text = _pontosPlayer.ToString();
    }

    void DistanciaPercorrida()
    {
        _metrosPercorridos++;
        _txtMetros.text = _metrosPercorridos.ToString() + "M";
        if( (_metrosPercorridos % 30) == 0)
        {
            _chaoVelocidade += 0.5f;
            _obstaculoTempo -= 0.15f;
            _obstaculoVelocidade += 0.15f;
        }
    }
}
