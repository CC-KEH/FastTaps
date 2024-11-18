using TMPro;
using System.Collections;
using UnityEngine;

public class TileGameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOverCanvasGroup;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI scoreText;
    private int score;
    public void Start()
    {
        NewGame();
    }
    public void NewGame()
    {
        UpdateScore(0);
        bestScoreText.text = PlayerPrefs.GetInt("2048BestScore", 0).ToString();
        gameOverCanvasGroup.alpha = 0;
        gameOverCanvasGroup.interactable = false;
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }
    public void GameOver()
    {
        Debug.Log("2048: Game Over");
        board.enabled = false;
        gameOverCanvasGroup.interactable = true;
        StartCoroutine(Fade(gameOverCanvasGroup, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float end, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        float elapsedTime = 0;
        float duration = 0.5f;
        float start = canvasGroup.alpha;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsedTime/duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = end;
    }
    public void IncreaseScore(int value)
    {
        Debug.Log("Increasing Score by " + value);
        UpdateScore(score + value);
    }

    public void UpdateScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
        SaveBestScore();
    }

    public void SaveBestScore()
    {
        int currentBestScore = PlayerPrefs.GetInt("2048BestScore", 0);
        if (score > currentBestScore)
        {
            PlayerPrefs.SetInt("2048BestScore", score);
            bestScoreText.text = score.ToString();
        }
    }
    public void ResetBestScore()
    {
        PlayerPrefs.SetInt("2048BestScore", 0);
        bestScoreText.text = "0";
    }
}
