using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour
{

    public static Cube _instance;
    public AudioSource audio;
    public AudioClip clipCharge;
    public AudioClip clip1;
    public AudioClip clip2;

    public float maxScale_X = 1.5f;
    public float minScale_X = 0.5f;
    private float curScale_X = 1.0f;

    public float maxScale_Y = 1.5f;
    public float minScale_Y = 0.5f;
    private float curScale_Y = 1.0f;

    private float scale_X = 1;
    private float scale_Y = 1;

    private Rigidbody rigid;
    private bool isScale = false;
    private bool needRestore = false;

    public Transform lastFloor;

    private float jumpPos_y = 0;

    public delegate void CanFollow();
    public CanFollow follow;

    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController._instance.isShowTime)
            return;
        
        if ((jumpPos_y - transform.position.y > 10) && rigid.velocity.y <= 0 && !GameController._instance.isGameOver)
        {
            Debug.Log("gameover!!");
            GameController._instance.GameOver();
        }

        if (needRestore)
        {
            if (curScale_X >= 1)
            {
                curScale_X = 1.0f;
                curScale_Y = 1.0f;
                scale_X = 1;
                scale_Y = 1;
                needRestore = false;
                transform.localScale = Vector3.one;
            }
            else if (curScale_X != 1)
            {
                curScale_X += Time.deltaTime * 5;
                curScale_Y -= Time.deltaTime * 5;
                transform.localScale = new Vector3(curScale_X, curScale_Y, 1);
            }
            return;
        }

        if (isScale == true)
        {
            if (curScale_X <= scale_Y)
            {
                curScale_X = scale_Y;
                curScale_Y = scale_X;
            }
            else
            {
                curScale_X -= Time.deltaTime * 5;
                curScale_Y += Time.deltaTime * 5;
            }

            transform.localScale = new Vector3(curScale_X, curScale_Y, 1);
            return;
        }
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audio.isPlaying && audio.clip != clipCharge)
            {
                audio.Stop();
                audio.clip = clipCharge;
                audio.Play();
            }
            if (curScale_X >= maxScale_X)
            {
                curScale_X = maxScale_X;
            }
            else
            {
                curScale_X += Time.deltaTime;
            }

            if (curScale_Y <= minScale_Y)
            {
                curScale_Y = minScale_Y;
            }
            else
            {
                curScale_Y -= Time.deltaTime;
            }

            transform.localScale = new Vector3(curScale_X, curScale_Y, 1);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            int send = Random.Range(0, 2);
            audio.Stop();
            audio.clip = send == 0 ? clip1 : clip2;
            audio.Play();
            jumpPos_y = transform.position.y;
            isScale = true;
            scale_X = curScale_X;
            scale_Y = curScale_Y;
            rigid.AddForce(Vector3.up * curScale_X * 6, ForceMode.Impulse);
            transform.parent = null;
        }
#elif UNITY_ANDROID
		if(Input.touchCount > 0)
		{
			if (!audio.isPlaying && audio.clip != clipCharge)
            {
                audio.Stop();
                audio.clip = clipCharge;
                audio.Play();
            }
            if (curScale_X >= maxScale_X)
            {
                curScale_X = maxScale_X;
            }
            else
            {
                curScale_X += Time.deltaTime;
            }

            if (curScale_Y <= minScale_Y)
            {
                curScale_Y = minScale_Y;
            }
            else
            {
                curScale_Y -= Time.deltaTime;
            }

            transform.localScale = new Vector3(curScale_X, curScale_Y, 1);
		}
		if(Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			int send = Random.Range(0, 2);
            audio.Stop();
            audio.clip = send == 0 ? clip1 : clip2;
            audio.Play();
            jumpPos_y = transform.position.y;
            isScale = true;
            scale_X = curScale_X;
            scale_Y = curScale_Y;
            rigid.AddForce(Vector3.up * curScale_X * 6, ForceMode.Impulse);
            transform.parent = null;
		}
#endif


    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Ground" && isScale == true && transform.position.y > col.transform.position.y && rigid.velocity.y <= 0)
        {
            isScale = false;
            needRestore = true;
            transform.parent = col.transform;
            rigid.velocity = Vector3.zero;
            if (col.transform.position.y > jumpPos_y)
            {
                Transform temp = lastFloor;
                lastFloor = col.transform;
                Destroy(temp.gameObject);
            }
            follow();
        }
    }

    //void OnGUI()
    //{
    //    GUILayout.Label("curScale_X: " + curScale_X);
    //    GUILayout.Label("curScale_Y: " + curScale_Y);
    //}
}
