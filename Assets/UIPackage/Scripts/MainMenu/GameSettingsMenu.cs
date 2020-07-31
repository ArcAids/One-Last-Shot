using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcAid.UI
{
    public class GameSettingsMenu : MonoBehaviour
    {
        [SerializeField]
        AudioSettingsMenu audioSettings;

        public void LoadSettings()
        {
            audioSettings.UpdateSettings();
        }
    }
}