using System;
using System.Collections.Generic;
using UnityEngine;

namespace Trell.ArmyFuckingMerge.Grahpics
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(MeshFilter))]
	public class OutlineNormalCalculator : MonoBehaviour
	{
		//in which texcoord will be save normals
		[SerializeField] private int textCoordNumber = 1;

		[SerializeField] private float maximumDistanceToMergeVertices = 0.01f;

		private Mesh mesh;

        private void Start()
        {
            mesh = GetComponent<MeshFilter>().sharedMesh;

			var vertices = mesh.vertices;
			var triangles = mesh.triangles;

			var outlineNormals =  new Vector3[vertices.Length];

			var mergeVertices =  new List<MergeVertex>();
			var mergeVertexIndexes = new int[vertices.Length];

			FindMergeVertices(vertices, mergeVertices, mergeVertexIndexes);

			// unity store triangles as three vertices indexes, so every three entries there is one triangle
			int numTriangels = triangles.Length / 3;

			for(int  i = 0; i < numTriangels; i++)
            {
				int vertexStart = i * 3;
				int v1Index = triangles[vertexStart];
				int v2Index = triangles[vertexStart + 1];
				int v3Index = triangles[vertexStart + 2];
				// Get this triangle's normal vector and the weigth for each vertex
				ComputeNormalAndWeigths(vertices[v1Index], vertices[v2Index], vertices[v3Index], out Vector3 normal, out Vector3 weigth);
				//add the weigthed normal to each merge vertex
				AddWeigthedNormal(normal * weigth.x, v1Index, mergeVertexIndexes, mergeVertices);
				AddWeigthedNormal(normal * weigth.y, v2Index, mergeVertexIndexes, mergeVertices);
				AddWeigthedNormal(normal * weigth.z, v3Index, mergeVertexIndexes, mergeVertices);
			}

			for (int i = 0; i < outlineNormals.Length; i++)
            {
				int mvIndex = mergeVertexIndexes[i];

				var vertex = mergeVertices[mvIndex];
				outlineNormals[i] = vertex.Normal.normalized;
            }
			mesh.SetUVs(textCoordNumber, outlineNormals);
        }

        private void AddWeigthedNormal(Vector3 weigthedNormal, int vIndex, int[] mergeVertexIndexes, List<MergeVertex> mergeVertices)
        {
			int mvIndex = mergeVertexIndexes[vIndex];
			mergeVertices[mvIndex].Normal += weigthedNormal;
        }

        private void ComputeNormalAndWeigths(Vector3 a, Vector3 b, Vector3 c, out Vector3 normal, out Vector3 weigth)
        {
			normal = Vector3.Cross(b - a, c - a).normalized;
			weigth = new Vector3(Vector3.Angle(b - a, c - a), Vector3.Angle(c - b, a - b), Vector3.Angle(a - c, b - c));
        }

        private void FindMergeVertices(Vector3[] vertices, List<MergeVertex> mergeVertices, int[] mergeVertexIndexes)
        {
			for(int  i = 0; i< vertices.Length; i++)
            {
				if (SearchPreviouslyRegistredMV(vertices[i], mergeVertices, out int index))
				{
					mergeVertexIndexes[i] = index;
				}
				else
				{
					var mergeVertex = new MergeVertex()
					{
						Position = vertices[i],
						Normal = Vector3.zero
					};

					mergeVertexIndexes[i] = mergeVertices.Count;
					mergeVertices.Add(mergeVertex);
				}		    
				
                
            }
        }

        private bool SearchPreviouslyRegistredMV(Vector3 position, List<MergeVertex> mergeVertices, out int index)
        {
			for(int  i = 0; i<mergeVertices.Count; i++)
            {
				if(Vector3.Distance(mergeVertices[i].Position, position) <= maximumDistanceToMergeVertices)
                {
					index = i;
					return true;
                }
            }
			index = -1;
			return false;
        }
    }

	public class MergeVertex
    {
		public Vector3 Position;
		public Vector3 Normal;
    }
}
