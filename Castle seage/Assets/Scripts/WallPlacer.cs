using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlacer : MonoBehaviour
{

    public GameObject wall;

    private Grid grid;
    private GameObject buildable;
    private GameObject buildableBox;

    // Use this for initialization
    void Start()
    {
        grid = FindObjectOfType<Grid>();
        buildable = Instantiate(wall, new Vector3(0, 0, 0), new Quaternion(-90, 0, 0, 90));
        buildableBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        buildableBox.GetComponent<Collider>().isTrigger = true;
        buildableBox.transform.localScale = new Vector3(1, 1.5f, 1.6f);
        
        //buildableBox.GetComponent<Material>() = 

        Material buildMaterial = Resources.Load("Materials/BuildMaterials/BuildAlowed", typeof(Material)) as Material;
        //buildableBox.AddComponent<MeshRenderer>();
        buildableBox.GetComponent<Renderer>().material = buildMaterial;
        buildableBox.tag = "buildableBox";

        Renderer rend = buildableBox.GetComponent<Renderer>();
        rend.enabled = true;

        buildable.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

        Ray pendingBuildSpot = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit pendingInfo;
        Physics.Raycast(pendingBuildSpot, out pendingInfo);

        Vector3 finalPendingBuildPosition = pendingInfo.point;
        finalPendingBuildPosition = grid.GetNearestPointOnGrid(finalPendingBuildPosition);
        finalPendingBuildPosition.y = 0;
        buildable.transform.position = finalPendingBuildPosition;
        buildableBox.transform.position = finalPendingBuildPosition + new Vector3(0, 0.5f, 0.75f);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                placeBuildingNear(hitInfo.point);
            }
        }
    }

    private void placeBuildingNear(Vector3 clickPoint)
    {
        Debug.Log(buildableBox.GetComponent<Renderer>().material.color.r == 0.2512111f);
        if (buildableBox.GetComponent<Renderer>().material.color.r == 0.2512111f)
        {
            var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
            GameObject wallInstance = Instantiate(wall, finalPosition, new Quaternion(-90, 0, 0, 90));
            wallInstance.AddComponent<BoxCollider>();
        }
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
    }
}
