using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public static string Key = "BestScoreForCubeJump";
    public Text scoreTxt;

	// Use this for initialization
	void Start ()
	{
	    int score = PlayerPrefs.GetInt(Key, 0);
	    scoreTxt.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
