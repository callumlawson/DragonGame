using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Gamelogic.Map
{
    [NetworkSettings(channel = 2)]
    public class HexGrid : NetworkBehaviour
    {
        #region Persistant State
        //TODO: Remove redundant coordinates.
        [System.Serializable]
        public struct HexCellState
        {
            public Color Color;
            public Vector3 Position;
            public HexCoordinates Coordinates;
        }

        [System.Serializable]
        public class HexCellList : SyncListStruct<HexCellState> { }

        [System.Serializable]
        public struct HexGridDimensions
        {
            public int Width;
            public int Height;
        }

        [SyncVar] private HexCellList Cells = new HexCellList();
        [SyncVar] private HexGridDimensions hexGridDimensions;
        #endregion

        #region Clientside
        public Text CellLabelPrefab;
        private readonly Color defaultColor = Color.white;
        private HexMesh hexMesh;
        public Canvas GridSpaceCanvas;

        [UsedImplicitly]
        public override void OnStartClient()
        {
            Debug.Log("Client started!");
            hexMesh = GetComponentInChildren<HexMesh>();
            Cells.Callback = (op, i) => RenderCells();
            RenderCells();
            foreach (var cell in Cells.ToList())
            {
                AddCellLabel(cell);
            }
        }

        private void RenderCells()
        {
            hexMesh.Triangulate(Cells.ToList());
        }

        private void AddCellLabel(HexCellState cell)
        {
            var label = Instantiate(CellLabelPrefab);
            label.rectTransform.SetParent(GridSpaceCanvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(cell.Position.x, cell.Position.z);
            label.text = cell.Coordinates.ToStringOnSeparateLines();
        }
        #endregion

        #region Serverside
        public static HexGrid Instance;

        [UsedImplicitly]
        public override void OnStartServer()
        {
            Debug.Log("Server started!");
            Instance = this;

            //Persistant data would be loaded here.
            hexGridDimensions = new HexGridDimensions
            {
                Width = 12,
                Height = 12
            };
        
            for (var z = 0; z < hexGridDimensions.Height; z++)
            {
                for (var x = 0; x < hexGridDimensions.Width; x++)
                {
                    CreateCell(x, z);
                }
            }
        }

        public void ReColorCell(Vector3 localPosition, Color color)
        {
            localPosition = transform.InverseTransformPoint(localPosition);
            var coordinates = HexCoordinates.FromPosition(localPosition);

            var cell = Cells[IndexFromHexCoordinates(hexGridDimensions, coordinates)];
            var oldColor = cell.Color;
            if (oldColor != color)
            {
                cell.Color = color;
                Cells[IndexFromHexCoordinates(hexGridDimensions, coordinates)] = cell;
                Cells.Dirty(IndexFromHexCoordinates(hexGridDimensions, coordinates));
            }
        }

        private void CreateCell(int x, int z)
        {
            Vector3 position;
            // ReSharper disable once PossibleLossOfFraction
            position.x = (x + z*0.5f - z/2)*(HexMetrics.InnerRadius*2f);
            position.y = 0f;
            position.z = z*(HexMetrics.OuterRadius*1.5f);

            //TODO check position.
            var cell = new HexCellState
            {
                Coordinates = HexCoordinates.FromOffsetCoordinates(x, z),
                Color = defaultColor,
                Position = position
            };
            Cells.Add(cell);
        }

        private static int IndexFromHexCoordinates(HexGridDimensions dimensions, HexCoordinates coordinates)
        {
            var index = coordinates.X + coordinates.Z * dimensions.Width + coordinates.Z / 2;
            return index;
        }
        #endregion
    }
}