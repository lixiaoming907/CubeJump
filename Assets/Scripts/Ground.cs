using UnityEngine;
using System.Collections;

public enum GroundType
{
    floor,
    backCube
}

public class Ground : MonoBehaviour
{

    public float speed = 0.2f;
    public float maxSpeed = 3f;

    public int dir = 1;
    public int scaleNum = 2;

    public GroundType ty;

    Vector3 screenPos;
    // Use this for initialization
    void Start()
    {
        if (ty == GroundType.floor)
        {
            float t =Random.Range(0, 3);
            if(t == 0)
                GetComponent<MeshRenderer>().material.color = new Color(1.0f, Random.Range(0, 1.0f), Random.Range(0, 1.0f), 1.0f);
            else if(t == 1)
                GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0, 1.0f), 1.0f, Random.Range(0, 1.0f), 1.0f);
            else
                GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f),1.0f);
        }
        transform.localScale = new Vector3(scaleNum, 1, 1);
        dir = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    public void Init(float sd)
    {
        maxSpeed = sd;
        speed = Random.Range(speed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController._instance.isShowTime)
            return;

        screenPos = dir == 1 ? Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.5f * scaleNum, 0, 0))
            : Camera.main.WorldToScreenPoint(transform.position - new Vector3(0.5f * scaleNum, 0, 0));

        if (dir == 1 && screenPos.x >= Screen.width)
        {
            InversDirection();
        }
        else if (dir == -1 && screenPos.x <= 0)
        {
            InversDirection();
        }

        transform.Translate(transform.right * dir * speed * Time.deltaTime);
    }

    void InversDirection()
    {
        dir *= -1;
    }
}
