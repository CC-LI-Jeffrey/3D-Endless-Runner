using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource coinSound;
    [SerializeField] AudioSource dieSound;
    public static GameObject instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        } 
        else {
            Destroy(gameObject);
        }
    }

    public void playCoinSound()
    {
        coinSound.Play();
    }

    public void playDieSound()
    {
        dieSound.Play();
    }
}
