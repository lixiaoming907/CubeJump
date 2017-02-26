using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public static GameController _instance;

    public Animator anim;
    public GameObject gameOverUI;
    public AudioClip failClip;
    public AudioSource backGroundAudio;

    [HideInInspector]
    public float screen2worldMinPosX;
    [HideInInspector]
    public float screen2worldMaxPosX;
    [HideInInspector]
    public bool isShowTime = true;
    [HideInInspector]
    public bool isGameOver = false;

    private float _maxSpeed = 3f;
    public float maxSpeed
    {
        get
        {
            return _maxSpeed;
        }
        set
        {
            _maxSpeed = value;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start()
    {
        Vector3 v = new Vector3(0,0,10);
        Vector3 vv = new Vector3(Screen.width,Screen.height,10);
        screen2worldMinPosX = Camera.main.ScreenToWorldPoint(v).x + 0.5f;
        screen2worldMaxPosX = Camera.main.ScreenToWorldPoint(vv).x - 0.5f;
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        StartCoroutine(RealdyGameUI());
    }

    private IEnumerator RealdyGameUI()
    {
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        anim.gameObject.SetActive(false);
        isShowTime = false;
    }

    public void GameOver()
    {
        isGameOver = true;
        backGroundAudio.Stop();
        gameOverUI.SetActive(true);
        backGroundAudio.clip = failClip;
        backGroundAudio.loop = false;
        backGroundAudio.Play();
    }
}
