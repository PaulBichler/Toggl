using System;
using Game;
using UnityEngine;

namespace Character
{
    public class ColorSwap : MonoBehaviour
    {
        private PlayerMovement _mvmt;
        private AudioSource _source;
        public void Start()
        {
            _source = GetComponent<AudioSource>();
            _mvmt = GetComponent<PlayerMovement>();
        }
        private SwapStates SwapLevelColor () => LevelState.SwapState != SwapStates.White ? SwapStates.White : SwapStates.Black;
        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                this.ChangeLevelColor();
                _source.PlayOneShot(_mvmt.swap);
            }
        }

        private void ChangeLevelColor()
        {
            LevelState.SwapState = SwapLevelColor();
        }
    }
}
