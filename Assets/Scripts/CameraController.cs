using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform player;

	bool canFollow = false;

	float curVel = 0;
	float soomthTime = 0.5f;
	// Use this for initialization
	void Start () {
		Cube._instance.follow += Follow;
	}
	
	// Update is called once per frame
	void Update () {
		if (canFollow) 
		{
			float t = 0;
			t = Mathf.SmoothDamp(transform.position.y, player.position.y, ref curVel, soomthTime);
			Vector3 newV = new Vector3(transform.position.x,t,transform.position.z);
			transform.position = newV;

			if(Mathf.Abs(player.position.y - transform.position.y) <= 0.1f)
			{
				canFollow = false;
			}
		}
	}

	void Follow()
	{
		canFollow = true;
	}
}
