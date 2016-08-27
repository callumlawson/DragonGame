using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic.UI
{
    public class ColorSelectUI : MonoBehaviour
    {
        public Action<int> OnColorIndexUpdated;

        // Use this for initialization
        [UsedImplicitly]
        void Awake()
        {
            var toggles = gameObject.GetComponentsInChildren<Toggle>();
            for (int toggle = 0; toggle < toggles.Length; toggle++)
            {
                var index = toggle;
                toggles[toggle].onValueChanged.AddListener(triggered => FireColorIndexUpdateEvent(triggered, index));
            }
        }

        private void FireColorIndexUpdateEvent(bool triggered, int index)
        {
            if (triggered)
            {
                Debug.Log("Button index: " + index);
                if (OnColorIndexUpdated != null)
                {
                    OnColorIndexUpdated(index);
                }
            }
        }
    }
}
