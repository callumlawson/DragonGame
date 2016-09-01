using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic.UI
{
    class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        void Awake()
        {
            Instance = this;
        }

        public ColorSelectUI ColorSelectUI;
        public Text MoneyUI;
        public InputField ChatInput;
        public Text ChatHistory;
    }
}
