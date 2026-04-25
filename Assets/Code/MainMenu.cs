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
            SceneManager.LoadScene("GameScene");
        });

        exitButton.onClick.AddListener(() => { 
            Application.Quit();
        });
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
