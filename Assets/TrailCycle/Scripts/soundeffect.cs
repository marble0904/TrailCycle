using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundeffect : MonoBehaviour
{

    public AudioSource au;
    void Start()
    {
        au = GetComponent<AudioSource>();
        au.Play();

        StartCoroutine(Checking(() => {
            Destroy(gameObject);
        }));
    }

    public delegate void functionType();
    private IEnumerator Checking(functionType callback)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!au.isPlaying)
            {
                callback();
                break;
            }
        }
    }
}