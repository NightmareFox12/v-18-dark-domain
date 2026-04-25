using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    // LA MAGIA: Esto permite llamar al script desde CUALQUIER lugar
    public static DialogueController Instance;

    [Header("UI References")]
    public TMP_Text nameText;
    public TMP_Text sentenceText;
    public GameObject dialogPanel; // Para encender/apagar toda la caja

    [Header("Settings")]
    public float typingSpeed = 0.04f;

    private bool isTyping = false;

    private void Awake()
    {
        // Configuración del Singleton
        if (Instance == null)
        {
            Instance = this;
            // No destruye este objeto al cambiar de escena (opcional)
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        dialogPanel.SetActive(false); // Empezamos ocultos
    }

    public void PlayDialogue(DialogueData data, UnityEvent callback = null)
    {
        if (isTyping) return; // Evitar que se solapen diálogos

        dialogPanel.SetActive(true);
        StartCoroutine(TypeSequence(data, callback));
    }

    private IEnumerator TypeSequence(DialogueData data, UnityEvent callback)
    {
        isTyping = true;

        foreach (var dialogue in data.dialogues)
        {
            nameText.text = dialogue.characterName;
            sentenceText.text = "";

            foreach (char letter in dialogue.sentence)
            {
                sentenceText.text += letter;
                // Aquí podrías meter un sonido: AudioSource.PlayOneShot(beep);
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(data.paragraphDelay);
        }

        isTyping = false;
        dialogPanel.SetActive(false);

        // Si pasamos una función para cuando termine, la ejecutamos
        callback?.Invoke();
    }
}