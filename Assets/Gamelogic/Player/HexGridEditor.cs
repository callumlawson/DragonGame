using Assets.Gamelogic.Messaging;
using Assets.Gamelogic.UI;
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
                        CmdChangeColor(hit.point);
                    }
                }
            }
        }
        #endregion

        #region Serverside
        [SerializeField] private Color[] colors;
        [SerializeField] private Color activeColor;

        [UsedImplicitly]
        public override void OnStartServer()
        {
            activeColor = colors[0];
        }

        [Command]
        private void CmdChangeColor(Vector3 hitPoint)
        {
            //Messenger.Broadcast(MessageTypes.UpdateHex, hitPoint, activeColor);
        }

        [Command]
        private void CmdSelectColor(int index)
        {
            activeColor = colors[index];
        }
        #endregion
    }
}