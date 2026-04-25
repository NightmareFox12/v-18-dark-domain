using UnityEngine;

public class Level_00_Intro : MonoBehaviour
{
    [Header("Data to Play")]
    public DialogueData introContent; // Aquí arrastrarás el "disco" de Juan

    void Start()
    {
        // 1. Verificamos que el archivo de datos no esté vacío
        if (introContent != null)
        {
            // 2. Le ordenamos al controlador GLOBAL que empiece a hablar
            // Usamos .Instance porque es el "acceso directo" al Prefab
            DialogueController.Instance.PlayDialogue(introContent);
        }
        else
        {
            Debug.LogError("¡No pusiste el archivo de Juan en el script de la Intro!");
        }
    }
}