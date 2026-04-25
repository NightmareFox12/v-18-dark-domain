using System.Collections;
using TMPro;
using UnityEngine;

public class Level_00_Intro : MonoBehaviour
{
    [Header("Configuración de UI")]
    public TMP_Text textComponent; // Tu componente de Texto (TMP)

    [Header("Configuración de Texto")]
    [TextArea(5, 10)] // Esto hace que el cuadro en el Inspector sea grande
    public string[] paragraphs;    // Array para meter muchos párrafos o textos largos

    [Header("Configuración de Tiempos")]
    public float delay = 0.05f;               // Velocidad de la máquina de escribir
    public float delayBetweenParagraphs = 2f; // Tiempo de espera antes de cambiar de párrafo

    private void Awake()
    {
        // Aseguramos que el texto esté vacío al arrancar
        if (textComponent != null)
        {
            textComponent.text = string.Empty;
        }
    }

    void Start()
    {
        if (paragraphs.Length > 0)
        {
            StartCoroutine(PlayIntro());
        }
        else
        {
            Debug.LogWarning("No has puesto ningún párrafo en el script Level_00_Intro.");
        }
    }

    private IEnumerator PlayIntro()
    {
        // Recorremos cada párrafo del Array
        foreach (string p in paragraphs)
        {
            textComponent.text = string.Empty; // Limpiamos para el nuevo párrafo

            // Efecto máquina de escribir letra por letra
            foreach (char c in p)
            {
                textComponent.text += c;
                yield return new WaitForSeconds(delay);
            }

            // Esperamos un tiempo para que el jugador lea el párrafo completo
            yield return new WaitForSeconds(delayBetweenParagraphs);
        }

        // Aquí termina la intro
        OnIntroFinished();
    }

    private void OnIntroFinished()
    {
        Debug.Log("La intro ha terminado. Aquí puedes disparar la llamada de celular.");
        // Ejemplo: SceneManager.LoadScene("Level_01_Tutorial");
    }
}