using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Button continueButton;

    public bool tutorial1 = true;
    
    // Start is called before the first frame update
    void Start()
    {
        if(tutorial1 && LevelState.tutorial1_shown) gameObject.SetActive(false);
        if(!tutorial1 && LevelState.tutorial2_shown) gameObject.SetActive(false);
        
        continueButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1; 
            this.gameObject.SetActive(false);

            if (tutorial1)
                LevelState.tutorial1_shown = true;
            else
                LevelState.tutorial2_shown = true;
        });
    }
}
