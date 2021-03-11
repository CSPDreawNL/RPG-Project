using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultQuickSave = "QuickSave";

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
        }
    }
}

