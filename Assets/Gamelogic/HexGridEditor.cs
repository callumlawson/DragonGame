using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

namespace Assets.Gamelogic
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
        public void Awake ()
        {
            UIManager.Instance.ColorSelectUI.OnColorIndexUpdated += CmdSelectColor;
        }

        [UsedImplicitly]
        public void Update()
        {
            if (!isLocalPlayer) return;

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
        #endregion

        #region Serverside
        [SerializeField]
        private Color[] colors;
        private HexGrid hexGrid;
        private Color activeColor;

        [UsedImplicitly]
        public override void OnStartServer()
        {
            activeColor = colors[0];
            hexGrid = HexGrid.Instance;
        }

        [Command]
        private void CmdChangeColor(Vector3 hitPoint)
        {
            //TODO: Sanatise Input
            hexGrid.ReColorCell(hitPoint, activeColor);
        }

        [UsedImplicitly][Command]
        private void CmdSelectColor(int index)
        {
            //TODO: Sanatise Input
            Debug.Log("Color updated!");
            activeColor = colors[index];
        }
        #endregion
    }
}