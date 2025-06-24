using UnityEngine;

namespace GamePlay
{
    [CreateAssetMenu(menuName = "Pause", fileName = "Pause")]
    public class PausedHandler : ScriptableObject
    {
        public bool IsPause { get; set; }
    }
}