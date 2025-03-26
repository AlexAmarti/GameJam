using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Tienda : MonoBehaviour
{
    public int monedas = 10; // Monedas iniciales
    public Text monedaText;

    public Button btnMusicaMolesta;
    public Button btnObstruccionInputs;
    public Button btnPantallaBloqueada;
    public Button btnBaileMolesto;

    public AudioClip earrapeMonstruosSA;
    public AudioClip billieJean;
    public GameObject imagenBloqueoPrefab; // Prefab de las imágenes que bloquean la pantalla
    

    public class Player : MonoBehaviour
    {
        public int monedas = 10;
        public bool bloqueado = false;
        public int obstruccion = 0;
        public int pantalla = 0;
        public int baile = 0;

    }
    public Player oponente; // Referencia al oponente

    public void CargarEscenaPartida()
    {
        SceneManager.LoadScene("General");
    }

    void Start()
    {
        ActualizarUI();

        btnMusicaMolesta.onClick.AddListener(() => ComprarObjeto(5, "MusicaMolesta"));
        btnObstruccionInputs.onClick.AddListener(() => ComprarObjeto(4, "ObstruccionInputs"));
        btnPantallaBloqueada.onClick.AddListener(() => ComprarObjeto(3, "PantallaBloqueada"));
        btnBaileMolesto.onClick.AddListener(() => ComprarObjeto(7, "BaileMolesto"));
    }

    void ComprarObjeto(int costo, string objeto)
    {
        if (monedas >= costo)
        {
            monedas -= costo;
            ActualizarUI();
            UsarObjeto(objeto);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }

    void ActualizarUI()
    {
        monedaText.text = "Monedas: " + monedas;
    }

    void UsarObjeto(string objeto)
    {
        switch (objeto)
        {
            case "MusicaMolesta":
                StartCoroutine(ReproducirMusicaMolesta());
                break;
            case "ObstruccionInputs":
                StartCoroutine(BloquearInputs());
                break;
            case "PantallaBloqueada":
                StartCoroutine(ImagenesBloqueo());
                break;
            case "BaileMolesto":
                StartCoroutine(BaileBillieJean());
                break;
        }
    }

    IEnumerator ReproducirMusicaMolesta()
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = earrapeMonstruosSA;
        audio.volume = 1.0f;
        audio.Play();
        yield return new WaitForSeconds(10);
        audio.Stop();
        Destroy(audio);
    }

    IEnumerator BloquearInputs()
    {
        float bloqueoTiempo = Random.Range(0.1f, 0.5f);
        oponente.bloqueado = true;
        yield return new WaitForSeconds(bloqueoTiempo);
        oponente.bloqueado = false;
    }

    IEnumerator ImagenesBloqueo()
    {
        int cantidad = Random.Range(3, 6);
        for (int i = 0; i < cantidad; i++)
        {
            GameObject imagen = Instantiate(imagenBloqueoPrefab);
            imagen.transform.SetParent(GameObject.Find("Canvas").transform, false);
            imagen.transform.position = new Vector3(Random.Range(100, Screen.width - 100), Random.Range(100, Screen.height - 100), 0);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));
            Destroy(imagen);
        }
    }

    IEnumerator BaileBillieJean()
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = billieJean;
        audio.volume = 1.0f;
        audio.Play();

        Animator anim = oponente.GetComponent<Animator>();
        anim.SetBool("bailando", true);

        yield return new WaitForSeconds(10);
        anim.SetBool("bailando", false);
        audio.Stop();
        Destroy(audio);
    }
}