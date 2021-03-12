using UnityEngine;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultQuickSave = "QuickSave";
        const string defaultSave = "Save";

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
                GetComponent<SavingSystem>().Save(defaultSave);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                GetComponent<SavingSystem>().Load(defaultSave);
            }

        }
    }
}

