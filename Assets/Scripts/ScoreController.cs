using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {


	public static ScoreController _instance;

	public Text scoreTxt;
	public Transform player;

	int _totleScore = -1;
	public int totleScore
	{
		get{
			return _totleScore;
		}
		set{
			_totleScore = value;
		}
	}

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.y - 1 > _totleScore)
		{
			AddScore();
		}
	}

	void AddScore()
	{
		totleScore = (int)player.transform.position.y - 1;
		scoreTxt.text = _totleScore.ToString ();
	}
}
