using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;

        public Action OnPlay;
        private void Start()
        {
            playButton.onClick.AddListener(() =>
            {
                OnPlay?.Invoke();
            });
        }
    }
}