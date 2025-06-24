using System;
using UnityEngine;

namespace Player
{
    public class PlayerCollide : MonoBehaviour
    {
        public Action OnGameOver;
        private void OnCollisionEnter2D(Collision2D other)
        {
            OnGameOver?.Invoke();
        }
    }
}