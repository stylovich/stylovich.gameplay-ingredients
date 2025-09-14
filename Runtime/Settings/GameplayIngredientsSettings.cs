using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriInspector;

namespace GameplayIngredients
{
    [HelpURL(Help.URL + "settings")]
    [DeclareBoxGroup("Editor", Title = "Editor")]
    [DeclareBoxGroup("Managers", Title = "Managers")]
    [DeclareBoxGroup("Callables", Title = "Callables")]
    public class GameplayIngredientsSettings : ScriptableObject
    {
        public string[] excludedeManagers { get { return m_ExcludedManagers; } }
        public bool verboseCalls { get { return m_VerboseCalls; } }
        public bool allowUpdateCalls { get { return m_AllowUpdateCalls; } }

        public bool disableWelcomeScreenAutoStart { get { return m_DisableWelcomeScreenAutoStart; } }

        [Group("Editor")]
        [SerializeField]
        protected bool m_DisableWelcomeScreenAutoStart;

        [Group("Managers")]
        [SerializeField, ListDrawerSettings(Draggable = true), ExcludedManager]
        protected string[] m_ExcludedManagers;

        [Group("Callables")]
        [SerializeField, InfoBox("Verbose Calls enable logging at runtime, this can lead to performance drop, use only when debugging.", TriMessageType.Warning)]
        private bool m_VerboseCalls = false;

        [Group("Callables")]
        [SerializeField, InfoBox("Per-update calls should be avoided due to high performance impact. Enable and use with care, only if strictly necessary.", TriMessageType.Warning)]
        private bool m_AllowUpdateCalls = false;

        const string kAssetName = "GameplayIngredientsSettings";

        public static GameplayIngredientsSettings currentSettings
        {
            get
            {
                if (hasSettingAsset)
                    return Resources.Load<GameplayIngredientsSettings>(kAssetName);
                else
                    return defaultSettings;
            }
        }

        public static bool hasSettingAsset
        {
            get
            {
                return Resources.Load<GameplayIngredientsSettings>(kAssetName) != null;
            }
        }


        public static GameplayIngredientsSettings defaultSettings
        {
            get
            {
                if (s_DefaultSettings == null)
                    s_DefaultSettings = CreateDefaultSettings();
                return s_DefaultSettings;
            }
        }

        static GameplayIngredientsSettings s_DefaultSettings;

        static GameplayIngredientsSettings CreateDefaultSettings()
        {
            var defaultAsset = CreateInstance<GameplayIngredientsSettings>();
            defaultAsset.m_VerboseCalls = false;
            defaultAsset.m_ExcludedManagers = new string[0];
            defaultAsset.m_DisableWelcomeScreenAutoStart = false;
            return defaultAsset;
        }
    }
}
