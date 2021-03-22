using System.Collections;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultQuickSave = "QuickSave";
        const string defaultSave = "Save";

        [SerializeField] float fadeInTime;

        private IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediatly();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSave);
            yield return fader.FadeIn(fadeInTime);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                GetComponent<SavingSystem>().Save(defaultQuickSave);
            }
            if (Input.GetKeyDown(KeyCode.F9))
            {
                GetComponent<SavingSystem>().Load(defaultQuickSave);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSave);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSave);
        }
    }
}

