using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // define UI object
    public static UI Singleton;

    // score display
    public TextMeshProUGUI scoreText;

    //public TextMeshProUGUI gameOverText;
    public GameObject gameOverUI;
    public TextMeshProUGUI gameOverText;
    public static bool isGameOver;

    // health heart 1,2,3
    public Image heart1;
    public Image heart2;
    public Image heart3;

    // number of candies on the screen
    private int numCandies;

    // track score
    private int score;

    // audio
    public AudioSource sfx;
    public AudioClip loseSfx;
    public AudioClip winSfx;

    // call start
    private void Start()
    {
        // initialize object
        Singleton = this;
        // initialize score 
        scoreText.text = "Score0";
        score = 0;

        // store the number of candies in scene
        numCandies = FindObjectsOfType<Candy>().Length;

        // disable the gameover screen to start
        gameOverUI.SetActive(false);

        // game begins
        isGameOver = false;

        //initialize audio
        //sfx = GetComponent<AudioSource>();
    }

    // increase score, static method
    public static void IncreaseScore()
    {
        Singleton.IncreaseScoreInternal();
    }

    // modify UI components to increase score
    private void IncreaseScoreInternal()
    {
        // add to score, update text display
        score++;
        scoreText.text = "Score" + score;

        if (score == numCandies)
        {
            GameOver(true);
        }
    }

    // change health images, static method
    public static void ChangeHealth(float health)
    {
        Singleton.ChangeHealthInternal(health);
    }

    // modify health images, internal
    private void ChangeHealthInternal(float health)
    {
        // adjust the image color
        if (health == 2)
        {
            //heart3.GetComponent<Image>().sprite = emptyHeart;
            heart3.color = new Color(0.145f,0.745f,0.925f);
        }
        else if (health == 1)
        {
            heart2.color = new Color(0.145f, 0.745f, 0.925f);
        }
        else if (health == 0)
        {
            heart1.color = new Color(0.145f, 0.745f, 0.925f);

            // game over if health = 0
            GameOver(false);
        }
    }

    // game over ui
    public void GameOver(bool win)
    {
        // activate the game over screen
        isGameOver = true;
        gameOverUI.SetActive(true);

        // display the message and score, play audio
        if (win)
        {
            gameOverText.text = "Congrats! You Won!\n";
            sfx.PlayOneShot(winSfx, 1);
        }
        else
        {
            gameOverText.text = "Oh no... You lost... :(\n";
            sfx.PlayOneShot(loseSfx, 1);
        }
        gameOverText.text += "\nScore: " + score;

    }

    // restart button
    public void Restart()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // menu button
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    // quit button
    public void Quit()
    {
        Application.Quit();
    }
}
