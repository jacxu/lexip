using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public Vector3 spawnValues;
    public GameObject hazard;
    public int hazardCount;

    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText textScore;
    public GUIText restartText;
    public GUIText gameOverText;

    private bool gameOver, restart;

    private int score;


    public int GetScore()
    {
        return score;
    }

    void Start()
    {
        gameOver = restart = false;
        restartText.text = gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);

            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' to restart";
                restart = true;
                break; 
            }
        }

    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        textScore.text = " Score:" + score;
    }

    public void GameOver()
    {
        gameOverText.text = "GameOver";
        gameOver = true;

    }


}
