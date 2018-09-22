using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour 
{
	#region Properties

	/// <summary>
    /// Gets or sets the resolution of the sphere.
    /// </summary>
    [Range(2, 256)]
	public int resolution = 10;

	[SerializeField, HideInInspector]
	MeshFilter[] MeshFilters;

	SphereFace[] SphereFaces;

	#endregion Properties

	#region Functions

	private void OnValidate()
	{
		Initialize();
		GenerateMesh();
	}

	private void Initialize()
	{
		if (MeshFilters == null || MeshFilters.Length == 0)
		{
			MeshFilters = new MeshFilter[6];
		}

		SphereFaces = new SphereFace[6];

		Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

		for (int i = 0; i < 6; i++)
		{
			if (MeshFilters[i] == null)
			{
				GameObject meshObj = new GameObject("mesh");
				meshObj.transform.parent = transform;

				meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
				MeshFilters[i] = meshObj.AddComponent<MeshFilter>();
				MeshFilters[i].sharedMesh = new Mesh();

				SphereFaces[i] = new SphereFace(MeshFilters[i].sharedMesh, resolution, directions[i]);
			}
		}
	}

	private void GenerateMesh()
	{
		for (int i = 0; i < 6; i++)
		{
			if (MeshFilters[i].gameObject.activeSelf)
			{
				SphereFaces[i].ConstructMesh();
			}
		}
	}
	#endregion Functions
}
