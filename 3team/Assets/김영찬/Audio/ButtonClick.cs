using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public AudioSource audioSource;
    
    // Start is called before the first frame updat
    public void buttonclick()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

}
