using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.SceneManagement;

namespace RPG.UI.Basics
{
    public class StartPanel : MonoBehaviour, ISaveable
    {
        [SerializeField] GameObject startMenu;
        [SerializeField] bool screenUp = true;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                CloseScreen();
            }
        }

        public object CaptureState()
        {
            return screenUp;
        }

        public void RestoreState(object state)
        {
            screenUp = (bool)state;
            CheckStateStatus();
        }

        private void CheckStateStatus()
        {
            if (screenUp == true)
            {
                startMenu.SetActive(true);
            }
            else if (screenUp == false)
            {
                startMenu.SetActive(false);
            }
        }

        public void CloseScreen()
        {
            if (screenUp == true)
            {
                startMenu.SetActive(false);
                screenUp = false;
                FindObjectOfType<SavingWrapper>().Save();
            }
        }
    }
}
