using UnityEngine;

namespace Player
{
    public class Jump : MonoBehaviour
    {
        public float jumpHeight = 2f;
        public float jumpDuration = 0.4f;

        private bool isJumping = false;
        private Vector3 startPosition;

        void Start()
        {
            startPosition = transform.position;
        }

        void Update()
        {
            if ((Input.GetMouseButtonDown(0) || IsTouchBegan()) && !isJumping)
            {
                StartCoroutine(JumpRoutine());
            }
        }
        
        private bool IsTouchBegan()
        {
#if UNITY_ANDROID || UNITY_IOS
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
#else
        return false;
#endif
        }

        private System.Collections.IEnumerator JumpRoutine()
        {
            isJumping = true;

            float elapsed = 0f;
            Vector3 peak = startPosition + Vector3.up * jumpHeight;

            // Вверх
            while (elapsed < jumpDuration / 2f)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / (jumpDuration / 2f);
                transform.position = Vector3.Lerp(startPosition, peak, t);
                yield return null;
            }

            elapsed = 0f;

            // Вниз
            while (elapsed < jumpDuration / 2f)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / (jumpDuration / 2f);
                transform.position = Vector3.Lerp(peak, startPosition, t);
                yield return null;
            }

            transform.position = startPosition;
            isJumping = false;
        }
    }
}