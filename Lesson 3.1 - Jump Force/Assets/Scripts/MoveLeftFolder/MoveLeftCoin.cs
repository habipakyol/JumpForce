using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftCoin : MonoBehaviour
{
    public float speed = 10f;
    private PlayerController PlayerControllerScript;
    private float leftbound = -10;

    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControllerScript.gameover == false)
        {
            if (PlayerControllerScript.doubleSpeed)
            {
                transform.Translate(Vector3.left * Time.deltaTime * (speed * 2));
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }

        if (transform.position.x < leftbound && CompareTag("Coin"))
        {
            Destroy(gameObject);
        }


    }
}
