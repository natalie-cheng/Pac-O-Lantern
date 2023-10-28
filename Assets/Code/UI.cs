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

    // health heart 1,2,3
    // https://nicolemariet.itch.io/pixel-heart-animation-32x32-16x16-freebie
    public Image heart1;
    public Image heart2;
    public Image heart3;

    //private Sprite emptyHeart;

    // number of candies on the screen
    private int numCandies = 0;

    // track score
    private int score = 0;


    // call start
    private void Start()
    {
        // initialize object
        Singleton = this;
        // initialize score display
        scoreText.text = "Score 0";

        // initialize empty heart
        //emptyHeart = Resources.Load<Sprite>("Hearts/Hearts_2");

        // store the number of candies in scene
        numCandies = FindObjectsOfType<Candy>().Length;

        // disable the gameover screen to start
        gameOverUI.SetActive(false);

        //gameOverText.text = "";
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
        scoreText.text = "Score " + score;

        //if (score == numCandies)
        //{
        //    GameOver(true);
        //}
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

    public void GameOver(bool win)
    {
        gameOverUI.SetActive(true);
        if (win)
        {
            gameOverText.text = "Congrats! You Won!\n";
        }
        else
        {
            gameOverText.text = "Oh no.. You lost.. :(\n";
        }
        gameOverText.text += "Score: " + score;

    }

    public void Restart()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {

    }
}
