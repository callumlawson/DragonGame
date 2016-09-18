using System;
using Assets.Gamelogic.Messaging;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

namespace Assets.Gamelogic.Player
{
    [Serializable]
    public class HexUpdateMessage : CustomMsg
    {
        public Color color;
        public Vector3 hitPoint;
    }

    /// <summary>
    ///     Player authoritative controller to edit the hexgrid.
    /// </summary>
    internal class HexGridEditor : NetworkBehaviour
    {
        #region Persistant State

        private Color CurrentlySelectedColor;

        #endregion

        #region Clientside

        [UsedImplicitly]
        public override void OnStartClient()
        {
            if (isLocalPlayer)
            {
                //Cannot use method groups for commands!
                //UIState.Instance.ColorSelectUI.OnColorIndexUpdated += index => CmdSelectColor(index);
            }
        }

        [UsedImplicitly]
        public void Update()
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(inputRay, out hit))
                {
                    Debug.Log("Sending hex update broadcast.");
                    NetworkMessenger.Broadcast(new HexUpdateMessage {color = Color.red, hitPoint = hit.point});
                }
            }
        }

        #endregion
    }
}