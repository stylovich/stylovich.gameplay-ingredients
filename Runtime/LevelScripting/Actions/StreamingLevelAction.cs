using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameplayIngredients.LevelStreaming;
using TriInspector;

namespace GameplayIngredients.Actions
{
    [AddComponentMenu(ComponentMenu.actionsPath + "Streaming Level Action")]
    [Callable("Game", "Misc/ic-scene.png")]
    public class StreamingLevelAction : ActionBase
    {
        [ListDrawerSettings(Draggable = true), Scene]
        public string[] Scenes;
        public string SceneToActivate;
        public LevelStreamingManager.StreamingAction Action = LevelStreamingManager.StreamingAction.Load;

        public bool ShowUI = false;
        
        public Callable[] OnLoadComplete;

        public override void Execute(GameObject instigator = null)
        {
            List<string> sceneNames = new List<string>();
            foreach (var scene in Scenes)
                sceneNames.Add(scene);
            Manager.Get<LevelStreamingManager>().LoadScenes(Action, sceneNames.ToArray(), SceneToActivate, ShowUI, OnLoadComplete);
        }

        public override string GetDefaultName()
        {
            return $"Streaming : {Action} {Scenes.Length} scene(s)";
        }
    }
}

