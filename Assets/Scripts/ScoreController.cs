using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{


    public static ScoreController _instance;

    public Text scoreTxt;
    public Transform player;

    private int maxScore;

    int _totleScore = -1;
    public int totleScore
    {
        get
        {
            return _totleScore;
        }
        set
        {
            if (value > maxScore)
            {
                maxScore = value;
                PlayerPrefs.SetInt(StartUI.Key, value);
            }

            _totleScore = value;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {
        maxScore = PlayerPrefs.GetInt(StartUI.Key);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y - 1 > _totleScore)
        {
            AddScore();
        }
    }

    void AddScore()
    {
        totleScore = (int)player.transform.position.y - 1;
        scoreTxt.text = _totleScore.ToString();
    }
}
