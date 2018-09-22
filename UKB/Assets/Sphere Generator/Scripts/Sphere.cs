using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour 
{
	#region Properties

	/// <summary>
    /// Gets or sets the resolution of the sphere
    /// </summary>
    [Range(2, 256)]
	public int resolution = 10;

	/// <summary>
    /// Gets or sets the mesh filter of the sphere
    /// </summary>
	[SerializeField, HideInInspector]
	MeshFilter[] MeshFilters;

	/// <summary>
    /// Gets or sets the sphere faces of the sphere
    /// </summary>
	SphereFace[] SphereFaces;

	#endregion Properties

	#region Functions

	/// <summary>
    /// Standard method when object is on validation
    /// </summary>
	private void OnValidate()
	{
		Initialize();
		GenerateMesh();
	}

	/// <summary>
    /// Initialize the sphere faces on the sphere
    /// </summary>
	private void Initialize()
	{
		// Initialize the meshes if needed
		if (MeshFilters == null || MeshFilters.Length == 0)
		{
			MeshFilters = new MeshFilter[6];
		}

		// Initialize the sphere faces
		SphereFaces = new SphereFace[6];

		// Defines all directions to turn un cube into
		Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

		for (int i = 0; i < 6; i++)
		{
			if (MeshFilters[i] == null)
			{
				// Creation of a mesh
				GameObject meshObj = new GameObject("mesh");
				meshObj.transform.parent = transform;

				// Default material
				meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
				MeshFilters[i] = meshObj.AddComponent<MeshFilter>();
				MeshFilters[i].sharedMesh = new Mesh();
			}

			// Creation of a sphere faces
			SphereFaces[i] = new SphereFace(MeshFilters[i].sharedMesh, resolution, directions[i]);
		}
	}

	/// <summary>
    /// Generate the meshes
    /// </summary>
	private void GenerateMesh()
	{
		foreach (SphereFace face in SphereFaces)
		{
			face.ConstructMesh();
		}
	}
	#endregion Functions
}
