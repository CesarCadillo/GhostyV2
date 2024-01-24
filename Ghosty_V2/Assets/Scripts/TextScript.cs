using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
public class TextScript : MonoBehaviour
{
    [SerializeField] float velocidadEscritura = 0.1f;
    [SerializeField] AudioClip audioBuddy;
    private TextMeshProUGUI texto;
    private string textoCompleto;
    private AudioSource audioSource;

    void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
        textoCompleto = texto.text;
        texto.text = ""; 
        audioSource = GetComponent<AudioSource>();

        if(audioSource != null)
        {
            audioSource.clip = audioBuddy;
        }

        StartCoroutine(EscribirTexto());
    }

    IEnumerator EscribirTexto()
    {

        if (audioSource.clip != null)
        {
            audioSource.Play();
        }

        for (int i = 0; i <= textoCompleto.Length; i++)
        {
            texto.text = textoCompleto.Substring(0, i);
            yield return new WaitForSeconds(velocidadEscritura);
        }

        yield return new WaitForSeconds(4.5f);

        SceneManager.LoadScene("MENUof");
    }
}
