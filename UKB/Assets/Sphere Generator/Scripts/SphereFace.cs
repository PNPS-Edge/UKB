using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereFace 
{
	#region Properties

	///<summary>
	/// Gets or sets the mesh of the sphere face
	///</summary>
	public Mesh Mesh;

	///<summary>
	/// Gets or sets the resolution of the sphere face
	///</summary>
	public int Resolution;

	///<summary>
	/// Gets or sets the Vector3 of the up of the mesh face
	///</summary>
	public Vector3 LocalUp;

	///<summary>
	/// Gets or sets the Vector3 of the A axis relative to the up of the sphere face
	///</summary>
	public Vector3 AxisA;

	///<summary>
	/// Gets or sets the Vector3 of the B axis relative to the up of the sphere face
	///</summary>
	public Vector3 AxisB;

	#endregion Properties

	#region Methods

	///<summary>
	/// Initializes a new instance of the <see cref="SphereFace"/> class.
	///</summary>
	public SphereFace(Mesh mesh, int resolution, Vector3 localUp)
	{
		this.Mesh = mesh;
		this.Resolution = resolution;
		this.LocalUp = localUp;

		AxisA = new Vector3(localUp.y, localUp.z, localUp.x);
		AxisB = Vector3.Cross(localUp, AxisA);
	}

	///<summary>
	/// Construct the Mesh
	///</summary>
	public void ConstructMesh()
	{
		Vector3[] vertices = new Vector3[Resolution * Resolution];
		
		int[] triangles = new int[(Resolution-1) * (Resolution-1) * 6];

		int triIndex = 0;

		for (int y = 0; y < Resolution; y++)
		{
			for(int x = 0; x < Resolution; x++)
			{
				int i = x + y * Resolution;
				Vector2 percent = new Vector2(x,y) / (Resolution-1);
				Vector3 pointOnUnitCube = LocalUp + (percent.x-.5f) * 2 * AxisA + (percent.y - 0.5f) * 2 * AxisB;
				Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;

				vertices[i] = pointOnUnitSphere;

				if(x != Resolution - 1 && y != Resolution - 1)
				{
					triangles[triIndex] = i;
					triangles[triIndex + 1] = i + Resolution + 1;
					triangles[triIndex + 2] = i + Resolution;

					triangles[triIndex + 3] = i;
					triangles[triIndex + 4] = i + 1;
					triangles[triIndex + 5] = i + Resolution + 1;

					triIndex += 6;
				}
			}
		}

		// clear and recalc with new values
		Mesh.Clear();
		Mesh.vertices = vertices;
		Mesh.triangles = triangles;
		Mesh.RecalculateNormals();
	}

	#endregion Methods
}
