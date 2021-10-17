using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheMenu : MonoBehaviour
{
    [SerializeField] AudioClip inni;
    [SerializeField] AudioClip outi;
    [SerializeField] GameObject Menu;
    [SerializeField] float right;
    [SerializeField] float left;


    void OnMouseEnter()
    {
        AudioSource.PlayClipAtPoint(inni, new Vector3(0, 0, -10));
        Menu.GetComponent<Move>().SetDestination(left);
    }

    void OnMouseExit()
    {
        AudioSource.PlayClipAtPoint(outi, new Vector3(0, 0, -10));
        Menu.GetComponent<Move>().SetDestination(right);

    }
}
