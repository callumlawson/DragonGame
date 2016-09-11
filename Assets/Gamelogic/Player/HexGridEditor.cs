using Assets.Gamelogic.Messaging;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

namespace Assets.Gamelogic.Player
{
    /// <summary>
    /// Player authoritative controller to edit the hexgrid. 
    /// </summary>
    class HexGridEditor : NetworkBehaviour
    {
        #region Persistant State
        private Color CurrentlySelectedColor;
        #endregion

        #region Clientside
        [UsedImplicitly]
        public override void OnStartLocalPlayer()
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
            if (isLocalPlayer)
            {
                if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    var inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(inputRay, out hit))
                    {
                        NewMessenger.Broadcast(new UpdateHex {color = Color.red, hitPoint = hit.point});
                    }
                }
            }
        }
        #endregion
    }
}