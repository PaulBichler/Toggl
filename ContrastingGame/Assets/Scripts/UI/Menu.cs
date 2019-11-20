using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        public List<Button> levelSelectButtons;

        public Button credits;
        public Button selectLevels;
        public Button quitGame;
        private GameObject _currentMenu;
        public GameObject mainMenu;
        public Sprite lockedImage;
        public GameObject levelSelectMenu;
        public Button levelSelectMenuBack;
        public GameObject creditsMenu;
        public Button muteButton;
        public Sprite unmuteImage;
        public Sprite muteImage;
        private bool isMuted = false;


        // Start is called before the first frame update
        void Start()
        {
            GameState.LevelUnlockStatus = SaveSystem.Load().playerProgress;
            if (Camera.main != null) Camera.main.backgroundColor = Color.white;
            _currentMenu = mainMenu;
            for (int i = 0; i < levelSelectButtons.Count; i++)
            {
                var levelId = i;
                if (GameState.LevelUnlockStatus[$"Level{levelId}"])
                {
                    levelSelectButtons[i].onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene($"Level{levelId}");
                        LevelState.LevelId = levelId;
                    });
                }
                else
                {
                    levelSelectButtons[i].GetComponent<SVGImage>().sprite = lockedImage;
                }
            }
            levelSelectMenuBack.onClick.AddListener(Back);
            quitGame.onClick.AddListener(() => Application.Quit(0));
            credits.onClick.AddListener(ToCreditsScreen);
            selectLevels.onClick.AddListener(ToLevelScreen);
        }

        private void ToLevelScreen()
        {
            if (Camera.main != null) Camera.main.backgroundColor = Color.black;
            _currentMenu.SetActive(false);
            levelSelectMenu.SetActive(true);
            _currentMenu = levelSelectMenu;
        }

        private void Back()
        {
            if (Camera.main != null) Camera.main.backgroundColor = Color.white;
            _currentMenu.SetActive(false);
            mainMenu.SetActive(true);
            _currentMenu = mainMenu;
        }

        private void ToCreditsScreen()
        {
            _currentMenu.SetActive(false);
            creditsMenu.SetActive(true);
            _currentMenu = creditsMenu;
        }
    }
}