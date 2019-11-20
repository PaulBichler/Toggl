using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public enum SwapStates
    {
        Black,
        White
    }

    public static class LevelState
    {
        private static SwapStates _swapState;
        public static GameObject Line;
        public static Camera MainCamera;
        public static GameObject Player;
        public static GameObject PauseMenu;
        public static List<GameObject> WhiteObjects;
        public static List<GameObject> BlackObjects;
        public static GameObject Overlay;
        private static readonly int InvertColors = Shader.PropertyToID("_InvertColors");
        public static int LevelId = 0;
        public static bool tutorial1_shown = false;
        public static bool tutorial2_shown = false;

        public static SwapStates SwapState
        {
            get => _swapState;
            set
            {
                _swapState = value;
                OnStateChanged();
            }
        }

        private static void OnStateChanged()
        {
            MainCamera.backgroundColor = SwapState == SwapStates.Black ? Color.black : Color.white;
            var playerMat = LevelState.Player.GetComponent<SpriteRenderer>().material;
            playerMat.SetFloat(InvertColors,playerMat.GetFloat(InvertColors) > 0 ? 0 : 1 );
            
            var lineMat = LevelState.Line.GetComponent<LineRenderer>().material;
            lineMat.SetFloat(InvertColors,lineMat.GetFloat(InvertColors) > 0 ? 0 : 1 );

            foreach (var gameObject in LevelState.BlackObjects)
            {
                gameObject.SetActive(SwapState != SwapStates.Black);
            }

            foreach (var gameObject in LevelState.WhiteObjects)
            {
                gameObject.SetActive(SwapState != SwapStates.White);
            }

        }

        public static void Die()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public static void Completed()
        {
            Time.timeScale = 0;
            GameState.LevelUnlockStatus[$"Level{LevelId + 1}"] = true;
            SaveSystem.Save();
            Overlay.SetActive(true);
        }
        
        public static void ResetState()
        {
            LevelState.WhiteObjects = new List<GameObject>();
            LevelState.BlackObjects = new List<GameObject>();
            LevelState.SwapState = SwapStates.White;
        }
    }
}