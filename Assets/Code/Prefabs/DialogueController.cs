using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance;

    [Header("UI References")]
    public TMP_Text nameText;
    public TMP_Text sentenceText;
    public GameObject dialogPanel;

    [Header("Settings")]
    public float typingSpeed = 0.04f;

    private bool isTyping = false;
    private bool cancelTyping = false;
    private bool inputBuffer = false; // El "Vigilante" del input

    private void Awake()
    {
        // Configuración Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialogPanel.SetActive(false);
    }

    private void Update()
    {
        // El Update siempre está escuchando, no importa si la Corrutina duerme
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame ||
            Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame ||
            (Pointer.current != null && Pointer.current.press.wasPressedThisFrame))
        {
            inputBuffer = true;
        }
    }

    public void PlayDialogue(DialogueData data, UnityEvent callback = null)
    {
        if (isTyping) return;
        
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
            cancelTyping = false;
            inputBuffer = false; // Reset al empezar cada frase

            // --- FASE 1: ESCRIBIR LETRA POR LETRA ---
            foreach (char letter in dialogue.sentence)
            {
                if (inputBuffer) 
                {
                    cancelTyping = true;
                    break; 
                }

                sentenceText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            // Si se presionó saltar, mostramos todo el texto de una
            if (cancelTyping) sentenceText.text = dialogue.sentence;

            // Limpiamos el buffer y esperamos un frame para evitar saltos accidentales
            inputBuffer = false; 
            yield return new WaitForEndOfFrame();

            // --- FASE 2: ESPERAR AL JUGADOR PARA CONTINUAR ---
            // Se queda aquí hasta que el Update vuelva a poner inputBuffer en true
            yield return new WaitUntil(() => inputBuffer);
            
            inputBuffer = false; // Limpiamos para el siguiente párrafo
            yield return new WaitForSeconds(0.1f); // Pequeño delay de cortesía
        }

        isTyping = false;
        dialogPanel.SetActive(false);
        
        // Ejecutamos cualquier evento que hayamos mandado (como cerrar la intro)
        callback?.Invoke();
    }
}