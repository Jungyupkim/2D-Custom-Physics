using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMesh : MonoBehaviour
{
    [HideInInspector]
    public Vector3[] vertices { get; private set; }

    private HMatrix2D transformMatrix = new HMatrix2D();
    HMatrix2D toOriginMatrix = new HMatrix2D();
    HMatrix2D fromOriginMatrix = new HMatrix2D();
    HMatrix2D rotateMatrix = new HMatrix2D();

    private MeshManager meshManager;
    HVector2D pos;

    void Start()
    {
        meshManager = GetComponent<MeshManager>();
        pos = new HVector2D(gameObject.transform.position.x, 
                            gameObject.transform.position.y);

        Rotate(45f);
    }


    void Translate(float x, float y)
    {
        transformMatrix.setIdentity();
        transformMatrix.setTranslationMat(x, y);
        Transform();

        pos = transformMatrix * pos;
    }

    void Rotate(float angle)
    {
        transformMatrix.setIdentity();

        toOriginMatrix.setTranslationMat(-pos.x, -pos.y);
        fromOriginMatrix.setTranslationMat(pos.x, pos.y);

        rotateMatrix.setRotationMat(angle);

        transformMatrix = fromOriginMatrix * rotateMatrix * toOriginMatrix;

        Transform();
        pos = transformMatrix * pos;
        
    }

    private void Transform()
    {
        vertices = meshManager.clonedMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            // IMPORTANT! Unity's mesh vertices are in local space.
            //
            // So we need to convert the vertex position from local to
            // world space *before* applying our transformation matrix.
            //
            // We must then convert back from world space to local space
            // *after* the transformation.
            
            // Convert from the vertex's local to world space coordinates.
            //
            vertices[i] = transform.TransformPoint(vertices[i]);

            // Now we can perform the transformations using the vertex's 
            // world space coordinates.
            //
            HVector2D vert = new HVector2D(vertices[i].x, vertices[i].y);
            vert = transformMatrix * vert;
            
            // The new vertex coordinate is still in world spce.
            //
            vertices[i].x = vert.x;
            vertices[i].y = vert.y;

            // Convert from the vertices' world to local space coordinates.
            //
            vertices[i] = transform.InverseTransformPoint(vertices[i]);
        }
        meshManager.clonedMesh.vertices = vertices;
    }

    private void Update()
    {
        //float speed = 1f;


    }
}
