using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    int Life = 3;
    int highScore;


    public static event Action<int> scoreAction;
    public static event Action<int,int> gameOverAction;


    private void OnEnable()
    {
        hoop.score += Score;
        UiManager.restart += Restart;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadHighScore();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Score()
    {
        score += 1;
        scoreAction(score);
        Debug.Log(score);
        
    }

    void ResetScore()
    {
        score = 0;
    }

    void Damage()
    {
        Life -= 1;
        if (Life == 0)
        {
            Debug.Log("GameOver");
            GameOver();
        }
    }

    void GameOver()
    {
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }
        gameOverAction(score, highScore);

    }

    void Restart()
    {
        ResetScore();
    }

    void SaveHighScore()
    {
        if (Directory.Exists(Application.dataPath + "/Save data/") == false)
            Directory.CreateDirectory(Application.dataPath + "/Save data/");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Save data/Score.secure");
        ScoreData data = new ScoreData();

        data.highscore = this.highScore;

        bf.Serialize(file, data);
        file.Close();
    }

    void LoadHighScore()
    {
        if (File.Exists(Application.dataPath + "/Save data/Score.secure"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/Save data/Score.secure", FileMode.Open);
            ScoreData data = (ScoreData)bf.Deserialize(file);
            file.Close();

            this.highScore = data.highscore;
            //scoreText.text = highscore.ToString();
        }


    }
}

[Serializable]
class ScoreData
{
    public int highscore;
}
