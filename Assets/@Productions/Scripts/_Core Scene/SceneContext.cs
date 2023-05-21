using Sirenix.OdinInspector;
using UISystem;
using UnityEngine;

namespace CustomTools.Core
{
    [System.Serializable]
    public class SceneContext
    {
        [Title("Scene UI")]
        public SceneUI UI;
        public HUDUI HUDUI;

        [Title("Gameplay")]
        public LevelManager LevelManager;
        public Player Player;

        [Title("Scene UI")]
        public AudioManager AudioManager;
    }
}