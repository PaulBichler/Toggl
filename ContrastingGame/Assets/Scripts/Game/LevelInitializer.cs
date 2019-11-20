using System.Linq;
using UnityEngine;

namespace Game
{
    public class LevelInitializer : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1;
            LevelState.MainCamera = Camera.main;
            LevelState.Player = GameObject.FindGameObjectWithTag("Player");
            LevelState.Line = GameObject.FindGameObjectWithTag("Line");
            LevelState.Overlay = GameObject.FindGameObjectWithTag("Overlay");
            LevelState.PauseMenu = GameObject.FindWithTag("PauseMenu");
            LevelState.PauseMenu.SetActive(false);
            LevelState.Overlay.SetActive(false);
            LevelState.ResetState();
            LevelState.BlackObjects = GameObject.FindGameObjectsWithTag("Black").ToList();
            LevelState.WhiteObjects = GameObject.FindGameObjectsWithTag("White").ToList();
            LevelState.SwapState = SwapStates.White;
        }
        
    }
}
