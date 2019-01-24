using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    public bool dead;
    GameObject player;
    PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        dead = false;

    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    void OnTriggerEnter(Collider other)
    {
        //rb.isKinematic = true;
        //transform.parent = other.transform;
        if(other.gameObject.tag == "Terrain")
        {
            dead = true;
        }

        if (other.gameObject.tag == "Player Model" && !dead)
        {
            playerScript.isAlive = false;
            //SceneManager.LoadScene("SampleScene");
        }
    }
}
