using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Move : MonoBehaviour
{
    float currentDestination;
    [SerializeField] float speed;
    RectTransform trnsfrm;




    public void SetDestination(float newDestination)
    {
        currentDestination = newDestination;
    }

    // Start is called before the first frame update
    void Start()
    {
        trnsfrm = GetComponent<RectTransform>();
        currentDestination = GetComponent<RectTransform>().localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        trnsfrm.localPosition = Vector2.MoveTowards(trnsfrm.localPosition, new Vector2(currentDestination, trnsfrm.localPosition.y), step);
        //UnityEngine.Debug.Log("x " + trnsfrm.localPosition.x + ", y " + trnsfrm.localPosition.y);

    }
}
