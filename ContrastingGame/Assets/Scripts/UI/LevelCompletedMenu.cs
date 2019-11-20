using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class LevelCompletedMenu : MonoBehaviour
    {
        public Button levelSelectionButton;
        public Button nextLevelButton;
        
        void Start()
        {
            levelSelectionButton.onClick.AddListener(() => { SceneManager.LoadScene("MainMenu");});
            nextLevelButton.onClick.AddListener(() => { 
                LevelState.LevelId++;
                SceneManager.LoadScene($"Level{LevelState.LevelId}");
            });
        }
    }
}
