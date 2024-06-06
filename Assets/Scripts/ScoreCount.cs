using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public Text HScore;
    public Text Score;
    public static int score;
    private int hscore;

    private static ScoreCount instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Score.text = score.ToString();

        hscore = PlayerPrefs.GetInt("HighScore", 0);
        HScore.text = hscore.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Elmas"))
        {
            SoundManager.Instance.PlayEffectSound(SoundManager.Instance.CoinSound);
            score++;
            Score.text = score.ToString();
            Destroy(other.gameObject);

            if (score > hscore)
            {
                hscore = score;
                HScore.text = hscore.ToString();

                PlayerPrefs.SetInt("HighScore", hscore);
                PlayerPrefs.Save();
            }
        }
    }

    private void OnEnable()
    {
        Score.text = score.ToString();
    }
}