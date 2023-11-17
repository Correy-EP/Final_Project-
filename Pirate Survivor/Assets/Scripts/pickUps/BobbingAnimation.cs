using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingAnimation : MonoBehaviour
{
    public float freq;
    public float magnitude;
    public Vector3 direction;

    Vector3 initalPostions;



    public void Start()
    {
        initalPostions = transform.position;
    }

    public void Update()
    {
        transform.position = initalPostions+ direction* Mathf.Sin (Time.time * freq)*magnitude;
    }
}
