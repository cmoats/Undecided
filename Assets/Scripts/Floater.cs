using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.ProBuilder;

public class Floater : MonoBehaviour
{

    public MeshFilter waterFilter;
    Rigidbody rb;
    BoxCollider box;


    public float buyoantForce = 20;
    public float waterDensity = 20;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if(transform.position.x >= 1000)
        {
            buyoantForce = 0;
            waterDensity = 0;
        }
        else
        {
            buyoantForce = 1000;
            waterDensity = 20;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vertexHeight = ClosestWaterVertex();
        Vector3[] boxCorners = GetColliderVertexPositions(box);


        //checks if the height is below the water vertex and then add forces
        foreach (Vector3 corner in boxCorners)
        {
            if (corner.y < vertexHeight.y)
            {
                rb.AddForceAtPosition(Vector3.up * buyoantForce, corner);
                rb.AddForce(rb.velocity * -1 * waterDensity);
            }
        }

    }

    private Vector3 ClosestWaterVertex()
    {
        Vector3[] vertices = waterFilter.mesh.vertices;

        //Translates vertices to world position
        List<Vector3> verts = VertsToWorld(vertices);

        //Calculates the distance of the vertices from the boat
        float closestDistance = 100000;
        Vector3 closestVertex = Vector3.positiveInfinity;

        //TODO make this more efficient
        foreach (Vector3 v in verts)
        {
            float distanceFromBoat = Vector3.Distance(v, transform.position); //compare distance between empty and current vertex
            if (Mathf.Abs(distanceFromBoat) < closestDistance)
            {
                closestDistance = distanceFromBoat;
                closestVertex = v;
            }

        }

        if (closestDistance != Mathf.Infinity) //This makes sure that v is not empty
        {
            return closestVertex;
        }

        else
        {
            Debug.Log("No Vertex Found");
            return Vector3.zero;
        }
    }

    // converts mesh vertices to world coordinates
    private List<Vector3> VertsToWorld(Vector3[] vertices)
    {
        Matrix4x4 localToWorld = waterFilter.transform.localToWorldMatrix;
        List<Vector3> verts = new List<Vector3>();
        foreach (Vector3 v in vertices)
        {
            verts.Add(localToWorld.MultiplyPoint3x4(v));
        }

        return verts;
    }

    private Vector3[] GetColliderVertexPositions(BoxCollider box)
    {
        Vector3[] vertices = new Vector3[8];
        vertices[0] = transform.TransformPoint(box.center + new Vector3(-box.size.x, -box.size.y, -box.size.z) * 0.5f);
        vertices[1] = transform.TransformPoint(box.center + new Vector3(box.size.x, -box.size.y, -box.size.z) * 0.5f);
        vertices[2] = transform.TransformPoint(box.center + new Vector3(box.size.x, -box.size.y, box.size.z) * 0.5f);
        vertices[3] = transform.TransformPoint(box.center + new Vector3(-box.size.x, -box.size.y, box.size.z) * 0.5f);
        vertices[4] = transform.TransformPoint(box.center + new Vector3(-box.size.x, box.size.y, -box.size.z) * 0.5f);
        vertices[5] = transform.TransformPoint(box.center + new Vector3(box.size.x, box.size.y, -box.size.z) * 0.5f);
        vertices[6] = transform.TransformPoint(box.center + new Vector3(box.size.x, box.size.y, box.size.z) * 0.5f);
        vertices[7] = transform.TransformPoint(box.center + new Vector3(-box.size.x, box.size.y, box.size.z) * 0.5f);

        return vertices;
    }
}
