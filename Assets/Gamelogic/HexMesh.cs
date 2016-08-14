using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Gamelogic
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class HexMesh : MonoBehaviour
    {
        private List<Color> colors;
        private Mesh hexMesh;
        private List<Vector3> vertices;
        private List<int> triangles;
        private MeshCollider meshCollider;

        [UsedImplicitly]
        void Awake()
        {
            hexMesh = new Mesh();
            GetComponent<MeshFilter>().mesh = hexMesh;
            meshCollider = GetComponent<MeshCollider>();
            hexMesh.name = "Hex Mesh";
            vertices = new List<Vector3>();
            triangles = new List<int>();
            colors = new List<Color>();
        }

        public void Triangulate(List<HexGrid.HexCellState> cells)
        {
            hexMesh.Clear();
            vertices.Clear();
            triangles.Clear();
            colors.Clear();

            for (int i = 0; i < cells.Count; i++)
            {
                Triangulate(cells[i]);
            }

            hexMesh.vertices = vertices.ToArray();
            hexMesh.colors = colors.ToArray();
            hexMesh.triangles = triangles.ToArray();
            hexMesh.RecalculateNormals();
            meshCollider.sharedMesh = hexMesh;
        }

        private void Triangulate(HexGrid.HexCellState cell)
        {
            var center = cell.Position;
            for (var i = 0; i < 6; i++)
            {
                AddTriangle(
                    center,
                    center + HexMetrics.corners[i],
                    center + HexMetrics.corners[(i + 1) % 6]
                    );
                AddTriangleColor(cell.Color);
            }
        }

        private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            var vertexIndex = vertices.Count;
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);
        }

        private void AddTriangleColor(Color color)
        {
            colors.Add(color);
            colors.Add(color);
            colors.Add(color);
        }
    }
}