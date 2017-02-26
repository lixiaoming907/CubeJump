using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIBtn : MonoBehaviour
{

    AudioSource audio;
    // Use this for initialization
    void Start()
    {
        GameObject go = GameObject.Find("AudioStatic");
        if (go)
            audio = go.GetComponent<AudioSource>();
        if (!audio)
            Debug.Log("the aduio is missing");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnReSetBtnClick()
    {
        if (audio)
            audio.Play();
        SceneManager.LoadScene(1);
    }

    public void OnReturnBtnClick()
    {
        if (audio)
            audio.Play();
        SceneManager.LoadScene(0);
    }
}

