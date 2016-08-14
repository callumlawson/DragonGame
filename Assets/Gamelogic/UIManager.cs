using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Gamelogic
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public ColorSelectUI ColorSelectUI;

        [UsedImplicitly]
        void Awake()
        {
            Instance = this;
        }
    }
}
