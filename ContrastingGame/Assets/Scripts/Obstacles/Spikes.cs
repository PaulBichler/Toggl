using System;
using Character;
using Game;
using UnityEngine;

namespace Obstacles
{
    public class Spikes : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Animator>().SetBool("Death", true);
                other.gameObject.GetComponent<PlayerMovement>().PlayDeathAnim();
            }
        }
        
    }
}
