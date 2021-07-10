using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    void Update()
    {
        //to make a transform animation
        transform.Rotate(150 * Time.deltaTime, 0, 0);
    }

    //to check if player collects the coins
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController.numberOfCoins++;
            Destroy(gameObject);
        }
    }
}
