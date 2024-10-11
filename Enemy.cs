using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 normals;
    public Vector3 interactionPoint;
    //public GameObject normalsCheckPrefab;
    
    public void SetNormals(Vector3 normal_vector)
    {
        this.normals = normal_vector;//.Normalize();
        //Debug.LogError((int)(this.normals.x * 100));
        //this.normals = new Vector3((int)(this.normals.x * 100), (int)(this.normals.y * 100), (int)(this.normals.z * 100));
        this.normals.Normalize();
        //var check = Instantiate(normalsCheckPrefab, normal_vector, Quaternion.identity);
        //check.transform.SetParent(transform);
        //check.transform.localPosition = normal_vector;  
    }

    float FindPlaneEquation()
    {
        Vector3 pos = transform.position;
        float d = -(normals.x * pos.x + normals.y * pos.y + normals.z * pos.z);
        return d;
    }

    void FindInteractionPlaneLine(Vector3 coefficients, Vector3 point, float d)
    {
        //float t = -(point.x * normals.x + point.y * normals.y + point.z * normals.z) / (coefficients.x * normals.x + coefficients.y * normals.y + coefficients.z + normals.z);
        float t = -1 * ((d + normals.x * point.x + normals.y * point.y + normals.z * point.z) / 
            (normals.x * coefficients.x + normals.y * coefficients.y + normals.z * coefficients.z));
        interactionPoint = new Vector3(coefficients.x * t + point.x, coefficients.y * t + point.y, coefficients.z * t + point.z);
        //Debug.LogError(t);
        //Debug.LogError(coefficients);
        //Debug.LogError(point);
    }



    public void ComputeDeadlyPoint(Vector3 coefficients_list, Vector3 point)
    {
        
        float d = FindPlaneEquation();
        

        FindInteractionPlaneLine(coefficients_list, point, d);

    }

}
