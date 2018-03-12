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
        buildableBox.transform.localScale = new Vector3(1.129f, 1.629f, 1.729f);
        
        //buildableBox.GetComponent<Material>() = 

        Material buildMaterial = Resources.Load("Materials/BuildMaterials/BuildAlowed", typeof(Material)) as Material;
        //buildableBox.AddComponent<MeshRenderer>();
        buildableBox.GetComponent<Renderer>().material = buildMaterial;
        buildableBox.tag = "buildableBox";

        Renderer rend = buildableBox.GetComponent<Renderer>();
        rend.enabled = true;

        buildable.transform.localScale = new Vector3(0.129f, 0.129f, 0.129f);
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
        buildableBox.transform.position = finalPendingBuildPosition + new Vector3(0, 0.55f, 0f);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                placeBuildingNear(hitInfo.point);
            }
        }

        if (Input.GetKeyDown("r"))
        {
            buildable.transform.eulerAngles = new Vector3(buildable.transform.eulerAngles.x, buildable.transform.eulerAngles.y, buildable.transform.eulerAngles.z + 90);
            buildableBox.transform.eulerAngles = buildable.transform.eulerAngles;
            buildableBox.transform.position = buildable.transform.position;
        }
    }

    private void placeBuildingNear(Vector3 clickPoint)
    {
        Debug.Log(buildableBox.GetComponent<Renderer>().material.color.r == 0.2512111f);
        if (buildableBox.GetComponent<Renderer>().material.color.r == 0.2512111f)
        {
            var finalPosition = buildable.transform.GetChild(0).transform.position;
            var finalRotation = buildable.transform.GetChild(0).transform.rotation;
            GameObject wallToBuild = Instantiate(wall.transform.GetChild(0).gameObject);
            wallToBuild.transform.position = finalPosition;
            wallToBuild.transform.rotation = finalRotation;
            wallToBuild.transform.localScale = new Vector3(0.129f, 0.129f, 0.129f);
            //GameObject wallInstance = Instantiate(wallToBuild, finalPosition, new Quaternion(-90, 0, 0, 90));
            wallToBuild.AddComponent<Rigidbody>();
            wallToBuild.AddComponent<BoxCollider>();
        }
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
    }
}
