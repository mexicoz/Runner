using System;
using TMPro;
using UnityEngine;

namespace View
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private TMP_Text score;
        [SerializeField] private Animator gameOver;

        private void Start()
        {
            gameOver.gameObject.SetActive(false);
        }

        public void SetScoreToHud(int score)
        {
            this.score.text = score.ToString();
        }

        public void ShowGameOver()
        {
            gameOver.gameObject.SetActive(true);
            gameOver.CrossFade("play", 0.1f);
        }

        public void DisableGameOverText()
        {
            gameOver.gameObject.SetActive(false);
        }
    }
}