using UnityEngine;

namespace View
{
    public class ViewHandler : MonoBehaviour
    {
        [SerializeField] private MainMenu mainMenuPrefab;
        [SerializeField] private Hud hudPrefab;
        
        private MainMenu _mainMenu;
        private Hud _hud;

        public MainMenu GetMainMenu()
        {
            return _mainMenu;
        }

        public Hud GetHud()
        {
            return _hud;
        }
        
        public void InstantiateHud()
        {
            _hud = Instantiate(hudPrefab, gameObject.transform);
            _hud.gameObject.SetActive(false);
        }

        public void InstantiateMainMenu()
        {
            _mainMenu = Instantiate(mainMenuPrefab, gameObject.transform);
        }
    }
}