using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xbuttonSFX : MonoBehaviour
{
    public bool isclicked;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isclicked)
        {
            audioSource.Play();
            isclicked = false;
        }
    }

    public void xbuttonclick()
    {
        isclicked = true;
    }
}
