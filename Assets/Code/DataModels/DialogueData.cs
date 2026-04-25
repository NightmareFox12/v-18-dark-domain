using UnityEngine;

// Este es el molde genérico para CUALQUIER diálogo
[System.Serializable] 
public class Dialogue 
{
    public string characterName; // Quién habla
    [TextArea(3, 10)]
    public string sentence;      // Qué dice
}

// Este es el creador de archivos
[CreateAssetMenu(fileName = "DialogueData", menuName = "Narrative/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public Dialogue[] dialogues; 
    public float paragraphDelay = 2.0f;
}