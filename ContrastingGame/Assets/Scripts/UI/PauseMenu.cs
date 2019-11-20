using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public Button continueButton;
        public Button mainMenuButton;
        public Button muteButton;
        public Sprite unmuteImage;
        public Sprite muteImage;
        private bool isMuted = false;
        void Start()
        {
            continueButton.onClick.AddListener(() => { Time.timeScale = 1; this.gameObject.SetActive(false); });
            mainMenuButton.onClick.AddListener(() => { SceneManager.LoadScene("MainMenu"); });
            
            muteButton.onClick.AddListener(() =>
            {
                if (isMuted)
                {
                    AudioListener.pause = false;
                    muteButton.gameObject.GetComponent<SVGImage>().sprite = unmuteImage;
                    isMuted = false;
                }
                else
                {
                    AudioListener.pause = true;
                    muteButton.gameObject.GetComponent<SVGImage>().sprite = muteImage;
                    isMuted = true;
                }
            });
        } 
    }
}
