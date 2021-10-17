using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class key : MonoBehaviour
{
    [SerializeField] float minFall;
    [SerializeField] float maxFall;
    [SerializeField] float minAng;
    [SerializeField] float maxAng;

    Rigidbody2D rbd;

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        float fallSpeed = UnityEngine.Random.Range(minFall, maxFall);
        rbd.velocity = new Vector2(0, -fallSpeed);
        float ang = UnityEngine.Random.Range(minAng, maxAng);
        if (UnityEngine.Random.Range(0, 2) == 1)
            ang *= -1;
        rbd.angularVelocity = ang;
    }

}
