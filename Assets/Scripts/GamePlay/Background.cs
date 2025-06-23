using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private Transform background1;
        [SerializeField] private Transform background2;
        [SerializeField] private float speed = 2f;
        [SerializeField] private List<GameObject> enemies1;
        [SerializeField] private List<GameObject> enemies2;

        private float _backgroundWidth;

        void Start()
        {
            _backgroundWidth = background1.GetComponent<SpriteRenderer>().bounds.size.x;
            ShowAllEnemy(enemies1);
            HideEnemy(enemies1);
            ShowAllEnemy(enemies2);
            HideEnemy(enemies2);
        }

        void Update()
        {
            background1.position += Vector3.left * speed * Time.deltaTime;
            background2.position += Vector3.left * speed * Time.deltaTime;

            if (background1.position.x < Camera.main.transform.position.x - _backgroundWidth)
            {
                background1.position = new Vector3(background2.position.x + _backgroundWidth, 
                    background1.position.y, background1.position.z);
                ShowAllEnemy(enemies1);
                HideEnemy(enemies1);
            }

            if (background2.position.x < Camera.main.transform.position.x - _backgroundWidth)
            {
                background2.position = new Vector3(background1.position.x + _backgroundWidth, 
                    background2.position.y, background2.position.z);
                ShowAllEnemy(enemies2);
                HideEnemy(enemies2);
            }
        }

        private void HideEnemy(List<GameObject> list)
        {
            var index = Random.Range(0, list.Count - 1);
            list[index].SetActive(false);
        }

        private void ShowAllEnemy(List<GameObject> list)
        {
            foreach (var item in list)
            {
                item.SetActive(true);
            }
        }
    }
}