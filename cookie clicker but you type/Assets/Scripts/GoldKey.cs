using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldKey : MonoBehaviour
{

    [SerializeField] float destMinX;
    [SerializeField] float destMaxX;
    [SerializeField] float destY;
    [SerializeField] float speed;
    [SerializeField] float minAng;
    [SerializeField] float maxAng;
    [SerializeField] AudioClip getKeySound;

    Keys keys;
    float destX;
    SpriteRenderer spr;
    char character;
    Rigidbody2D rbd;


    //sets the sprite of the sprite and character of the goldkey
    public void SetUp(Sprite sprite, char a)
    {
        spr.sprite = sprite;
        character = a;
    }


    //called even before START!! woah! just stores the spriterenderer & rbd2d components to be used in SetUp() and Start()
    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        rbd = GetComponent<Rigidbody2D>();

    }


    // Start is called before the first frame update
    void Start()
    {

        keys = GameObject.Find("KEYS").GetComponent<Keys>(); //gets the keys script so that it can trigger golden rain when char is pressed!!
        destX = Random.Range(destMinX, destMaxX);

        //gives goldkey a random angular velocity and direction
        float ang = Random.Range(minAng, maxAng);
        if (Random.Range(0, 2) == 1)
            ang *= -1;
        rbd.angularVelocity = ang;


    }

    // Update is called once per frame
    void Update()
    {
        // goldkey moves towards a random point under the screen, destx is random but desty is serialized
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(destX, destY), step);

        // if the goldkey reaches its coordinate its going to, destroys itself
        if (transform.position.x == destX && transform.position.y == destY) Destroy(gameObject);

        // if you hit it, calls Keys.StartGoldenRain() which makes the timer and then starts the coroutine to turn goldenrain boolean for an amt of time
        if (Input.GetKeyDown(character.ToString()))
        {
            keys.StartGoldenRain();
            AudioSource.PlayClipAtPoint(getKeySound, new Vector3(0, 0, -10));
            Destroy(gameObject);
        }


    }
}
