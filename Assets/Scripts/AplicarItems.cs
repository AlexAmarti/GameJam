using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public bool bloqueado = false;
    public AudioClip audioClipMusicaMolesta;


}

public class AplicarItems : MonoBehaviour
{
    public Jugador jugador1;
    public Jugador jugador2;

    private void Start()
    {
        AplicarItem(jugador1, GestorItems.instancia.itemJugador1);
        AplicarItem(jugador2, GestorItems.instancia.itemJugador2);
    }

    void AplicarItem(Jugador jugador, string item)
    {
        if (string.IsNullOrEmpty(item)) return;

        switch (item)
        {
            case "MusicaMolesta":
                StartCoroutine(ReproducirMusicaMolesta(jugador));
                break;
            case "ObstruccionInputs":
                StartCoroutine(BloquearInputs(jugador));
                break;
            case "PantallaBloqueada":
                StartCoroutine(ImagenesBloqueo(jugador));
                break;
            case "BaileMolesto":
                StartCoroutine(BaileBillieJean(jugador));
                break;
        }
    }

    IEnumerator ReproducirMusicaMolesta(Jugador jugador)
    {
        AudioSource audio = jugador.gameObject.AddComponent<AudioSource>();
        audio.clip = jugador.audioClipMusicaMolesta;
        audio.volume = 1.0f;
        audio.Play();
        yield return new WaitForSeconds(10);
        audio.Stop();
        Destroy(audio);
    }

    IEnumerator BloquearInputs(Jugador jugador)
    {
        jugador.bloqueado = true;
        yield return new WaitForSeconds(0.5f);
        jugador.bloqueado = false;
    }

    IEnumerator ImagenesBloqueo(Jugador jugador)
    {
        Debug.Log("Pantalla bloqueada");
        yield return new WaitForSeconds(3);
    }

    IEnumerator BaileBillieJean(Jugador jugador)
    {
        Animator anim = jugador.GetComponent<Animator>();
        anim.SetBool("bailando", true);
        yield return new WaitForSeconds(10);
        anim.SetBool("bailando", false);
    }
}
