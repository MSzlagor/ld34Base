using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float instructLength = 2.0f;
    public static GameManager instance = null;
    public BoardManager boardScript;

    private Text instructionText;
    private GameObject instructImage;
    private int level = 3;
    private bool startingGame;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    private void HideInstructText()
    {
        instructImage.SetActive(false);
        startingGame = false;
    }


    private void onLevelWasLoaded(int index)
    {
        InitGame();
    }


    void InitGame()
    {
        startingGame = true;

        instructImage = GameObject.Find("LevelImage");
        instructionText = GameObject.Find("LevelText").GetComponent<Text>();
        instructionText.text = "Use arrow keys to move. Eat brown food to grow. Escape.";
        instructImage.SetActive(true);
        Invoke("HideInstructText", instructLength);

        boardScript.SetupScene(level);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (startingGame)
            return;
	}
}
