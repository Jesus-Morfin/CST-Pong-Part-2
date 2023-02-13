using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public AudioSource pongCollison;
    public AudioClip hit;

    private void OnCollisionEnter(Collision other)
    {
        pongCollison.PlayOneShot(hit);
    }
}
