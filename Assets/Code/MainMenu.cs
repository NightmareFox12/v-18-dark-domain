using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    const int FPS = 165;
    const string GAME_NAME = "V-18 Dark Domain";

    [Header("Buttons")]
    public Button playButton;
    public Button optionsButton;
    public Button creditsButton;
    public Button exitButton;

// Unity functions
    void Awake(){
        playButton.onClick.AddListener(() => { 
            Debug.Log("🎮 ¡PLAY CLICK DETECTADO!"); 
        });

   
    }

    void Start()
    {
        exitButton.onClick.AddListener(() => { 
            GameManager.Instance.ExitGame();
        }); 
    }

    void Update()
    {
        
    }
}
