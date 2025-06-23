using TMPro;
using UnityEngine;

namespace View
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private TMP_Text score;

        public void SetScoreToHud(int score)
        {
            this.score.text = score.ToString();
        }
    }
}