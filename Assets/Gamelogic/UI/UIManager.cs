using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public ColorSelectUI ColorSelectUI;
        public Text MoneyUI;

        [UsedImplicitly]
        void Awake()
        {
            Instance = this;
        }
    }
}
