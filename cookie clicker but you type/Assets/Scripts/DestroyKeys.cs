using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyKeys : MonoBehaviour
{

    //destroys thing that touches it - the falling keys are destroyed when they go under the screen so u don't lag like crazy
    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }



}
