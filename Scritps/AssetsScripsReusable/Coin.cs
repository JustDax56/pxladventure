using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<CoinSystem>(out var collector))
            {
                collector.CollectCoin();
                Destroy(gameObject); 
            }
        }
    }
}
