using System;
using UnityEngine;

namespace Player
{
    public class PlayerCollide : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Time.timeScale = 0;
        }
    }
}