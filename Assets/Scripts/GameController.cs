using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public Vector3 spawnValues;

    public GameObject enemyBlue;
    public GameObject enemyRed;

    public int hazardCount;

    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText textScore;
    public GUIText healthText;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText startText;


    public GameObject boltBlue;
    public GameObject boltRed;
    public Canvas gameCanvas;
    public GameObject img;

    private List<GameObject> boltPoints;

    public List<GameObject> enemies;

    private bool gameOver, restart, start;
    private int score;
    private Quaternion spawnRotation = Quaternion.identity;
    private List<string> levels;

    private int enemyCount;
    private int oneEnemyCount = 0;
    private int maxEnemyCount = 8;
    private PlayerController pc;
    

    private int health = 100;
    public int Health
    {
        get { return health; }
    }

    private int boltBluePoint = 5;
    private int boltRedPoint = 5;


    public int BoltBluePoint
    {
        get { return boltBluePoint; }
        set { if (value <= 5) boltBluePoint = value; }
    }

    public int BoltRedPoint
    {
        get { return boltRedPoint; }
        set { if (value <= 5) boltRedPoint = value; }
    }

    #region private methods - addition
    //private methods 
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private float GetEnemyRatio()
    {
        if (enemies.Count == 0)
            return -1;

        return (float)oneEnemyCount / (float)enemies.Count;
    }

    private void AddRed(Vector3 pos)
    {
        enemies.Add((GameObject)Instantiate(enemyRed, pos, spawnRotation));
    }

    private void AddBlue(Vector3 pos)
    {
        enemies.Add((GameObject)Instantiate(enemyBlue, pos, spawnRotation));
        oneEnemyCount++;
    }

  
    #endregion

    //void Start
    void Start()
    {
        start = gameOver = restart = false;
        restartText.text = gameOverText.text = "";
        score  = 0;
        levels = new List<string>() { "s.2.2", "s.3.3", "s.6.1", "boss" };
        // levels = new List<string>() { "s.1.1" };
        UpdateStats(0,0);
        DrawStart();
        
        enemies = new List<GameObject>();
        boltPoints = new List<GameObject>();

        pc = MyController.GetPlayerController();

        
    }

    void Update()
    {
        GenerateBackground();
        DrawBoltPoint();

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

        if (!start && Input.GetKeyDown(KeyCode.P))
        {
            start = true;
            startText.text = "";
        }

        if (gameOver)
            GameOver();

        if (start)
        {
            DrawUI();
            StartCoroutine(SpawnLevel());
            start = false;
        }
    }

    void GenerateBackground()
    {

    }

    /// <summary>
    /// spawn square of enemies
    /// </summary>
    /// <param name="sizex"></param>
    /// <param name="sizey"></param>
    void SpawnSquare(int sizex, int sizey)
    {
        if (sizex > maxEnemyCount)
            Debug.LogError("max enemy count reached");

        Vector3 startingPoint = new Vector3(-4 + ((maxEnemyCount - sizex) * 0.5f), 0, 20); 
        for (int i = 0; i < sizex; i++)
        {
            for (int j = 0; j < sizey; j++)
            {
                Vector3 newposition = startingPoint + new Vector3(i * 1.5f, 0, j * 1.5f);
                enemyCount++;             
                System.Random rnd = new System.Random();
                int next = 0;
                next = Random.Range(0, 100);
                Debug.Log(next + " ratio:" + GetEnemyRatio());
                
                if (next > 50 && (GetEnemyRatio() < 0.4 || GetEnemyRatio() > 0.6))
                    AddRed(newposition);
                else
                    AddBlue(newposition);
            } 

        }
    }

    /// <summary>
    /// not implemented yet
    /// </summary>
    void SpawnTriangle()
    {     
    }

    /// <summary>
    /// not implemented yet
    /// </summary>
    void SpawnBoss()
    {
    }


    IEnumerator SpawnLevel()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            Debug.Log("enemyCount :" + enemies.Count);
            if (enemies.Count == 0 )
            {
                string[] level_desc = levels[0].Split('.');
                if (level_desc.Length == 0)
                    Debug.LogError("level_desc bad format");
                switch (level_desc[0])
                {
                    case "s":       SpawnSquare(int.Parse(level_desc[1]), int.Parse(level_desc[2])); break;
                    case "boss":    GameWin();break;
                }
                levels.RemoveAt(0);
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
    /// <summary>
    /// remove enemy from enemies list
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveEnemy(GameObject obj)
    {
        if ( obj == null)
        {
            Debug.LogError("RemoveEnemy error obj null");
            return;
        }
        enemies.RemoveAt(enemies.FindIndex(a => a = obj));
    }



    #region  Update, score, game events
    /// <summary>
    /// 
    /// </summary>
    /// 

    /// <summary>
    /// add score to player
    /// </summary>
    /// <param name="newScoreValue"></param>
    void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        DrawScore();
    }

    void AddHealth(int newLifeValue)
    {
        health += newLifeValue;
        CheckHealth();
        DrawHealth();
    }

    private void CheckHealth()
    {
        if (health == 0)
            gameOver = true;
    }

    /// <summary>
    /// draw to UI stats and game info
    /// </summary>
    private void DrawUI()
    {
        DrawHealth();
        DrawScore();
        DrawBoltPoint();
    }

    private void DrawStart()
    {
        startText.text = "Welcome and enjoy!.\n\nTo control press arrows.\nTo fire press 'u',i'. \nTo shield press 'j','k' \n \n \n To play press 'p'. \n";
    }

    private void DrawHealth()
    {
        img.transform.localScale = new Vector3((float)health / 100, 1, 1);
    }

    private void DrawScore()
    {
        textScore.text = " s.:" + score;
    }

    private void DrawBoltPoint()
    {
        if (boltPoints == null)
            return;

        foreach (GameObject o in boltPoints)
            Destroy(o);

        boltPoints = new List<GameObject>();

        Vector3 startPosBlue = new Vector3(520, 104, 0);
        Vector3 startPosRed = new Vector3(24, 104, 0);

        for (int i = 0; i < boltBluePoint; i++)
        {
            GameObject img = Instantiate(boltBlue) as GameObject;
            img.transform.SetParent(gameCanvas.transform);
            img.transform.position = startPosBlue + new Vector3(0, 128, 0) * i;
            img.transform.rotation = Quaternion.identity;
            boltPoints.Add(img);
        }

        for (int i = 0; i < boltRedPoint; i++)
        {
            GameObject img = Instantiate(boltRed) as GameObject;
            img.transform.SetParent(gameCanvas.transform);
            img.transform.position = startPosRed + new Vector3(0, 128, 0) * i;
            img.transform.rotation = Quaternion.identity;
            boltPoints.Add(img);
        }

    }


    public void UpdateStats(int addHealth, int addScore)
    {
        AddHealth(addHealth);
        AddScore(addScore);
    }

    public void GameOver()
    {
        gameOverText.text = "you LOSE :(";
        gameOver = true;
        if (pc!=null)
          pc.DestroyPlayer();

    }

    public void GameWin()
    {
        gameOverText.text = "you WIN :)";
        gameOver = true;

    }
    #endregion

}
