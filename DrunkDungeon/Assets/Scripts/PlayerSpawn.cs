using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    public GameObject player;
   

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player);
        player.GetComponent<Transform>().position = transform.position;
       
    }

    // Update is called once per frame
    
}
