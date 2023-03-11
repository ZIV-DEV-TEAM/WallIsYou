using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMesh
{
    private MeshCollider _meshCollider;
    private MeshFilter _meshFilter;
    
    public ChangeMesh(MeshCollider meshCollider, MeshFilter meshFilter )
    {
        _meshFilter = meshFilter;
        _meshCollider = meshCollider;
    }

    public void SetMesh(Mesh newMesh)
    {
        _meshFilter.mesh = newMesh;
        _meshCollider.sharedMesh = newMesh;
    }
}
