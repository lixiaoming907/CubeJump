using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

	public static GroundController _instance;

	public float floorMax_Y = 3.14f;
	public float floorMin_Y = 0.5f;

	public GameObject floorPre;
    public GameObject backCube;
	public Transform player;

	public Transform lastFloor;
    public Transform lastBackCubeGroup;

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		Cube._instance.follow += CreateFloor;
		Cube._instance.follow += CreateBackCubeGroup;
	}
	
	// Update is called once per frame
	void Update () {

	}

    //生成背景cube
    public void CreateBackCubeGroup()
    {
        if (ScoreController._instance.totleScore >= (int)lastBackCubeGroup.position.y)
        {
            GameObject go = Instantiate(backCube, new Vector3(0, lastBackCubeGroup.position.y + 30.0f, 0), Quaternion.identity) as GameObject;
            lastBackCubeGroup = go.transform;
        }
    }

    //生成地板
	void CreateFloor()
	{
		float pos_y = Random.Range (lastFloor.position.y + floorMin_Y,lastFloor.position.y + floorMax_Y);
        float t = Random.Range(GameController._instance.screen2worldMinPosX, lastFloor.position.x - 1.5f);
	    float tt = Random.Range(lastFloor.position.x + 1.5f, GameController._instance.screen2worldMaxPosX);
	    float pos_x = Random.Range(0, 2) == 0 ? t : tt;
        //while (Mathf.Abs(pos_x - lastFloor.position.x) < 1.5f) {
        //	pos_x = Random.Range (0.5f,Camera.main.ScreenToWorldPoint (tempV).x);
        //}
        GameObject go = Instantiate (floorPre,new Vector3(pos_x,pos_y,0),Quaternion.identity) as GameObject;
		lastFloor = go.transform;

		Ground ground = lastFloor.GetComponent<Ground>();
	    ground.Init(GameController._instance.maxSpeed);
	}
}
