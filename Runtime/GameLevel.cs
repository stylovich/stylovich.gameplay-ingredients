using UnityEngine;
using TriInspector;

namespace GameplayIngredients
{
    public class GameLevel : ScriptableObject
    {
        [ListDrawerSettings(Draggable = true), Scene]
        public string[] StartupScenes;
    }
}
