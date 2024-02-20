using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshManager : MonoBehaviour
{
    private MeshFilter meshFilter;

    [HideInInspector]
    public Mesh originalMesh, clonedMesh;

    public Vector3[] vertices { get; private set; }
    public int[] triangles { get; private set; }

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        originalMesh = meshFilter.sharedMesh;
        clonedMesh = new Mesh();
        clonedMesh.name = "clone";
        clonedMesh.vertices = originalMesh.vertices;
        clonedMesh.triangles = originalMesh.triangles;
        clonedMesh.normals = originalMesh.normals;
        clonedMesh.uv = originalMesh.uv;
        meshFilter.mesh = clonedMesh;

        vertices = clonedMesh.vertices;
        triangles = clonedMesh.triangles;
    }
}
