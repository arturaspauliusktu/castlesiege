using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlacer1 : MonoBehaviour
{

    public GameObject wall;

    private Grid grid;
    private GameObject buildable;
    private GameObject buildableBox;
    private bool isBuildEnabled;

    // Use this for initialization
    void Start()
    {
        
    }

    void enableBuild()
    {
        Debug.Log("sdadsa");
        grid = FindObjectOfType<Grid>();
        //buildable = Instantiate(wall, new Vector3(0, 0, 0), new Quaternion(-90, 0, 0, 90));
        buildable = new GameObject("breakableWall");
        buildable.AddComponent<FracturedObject>();
        buildable = Instantiate(wall);
        buildableBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        buildableBox.GetComponent<Collider>().isTrigger = true;
        buildableBox.transform.localScale = buildable.transform.GetChild(0).transform.GetChild(200).GetComponent<Renderer>().bounds.size;

        //buildableBox.GetComponent<Material>() = 

        Material buildMaterial = Resources.Load("Materials/BuildMaterials/BuildAlowed", typeof(Material)) as Material;
        //buildableBox.AddComponent<MeshRenderer>();
        buildableBox.GetComponent<Renderer>().material = buildMaterial;
        buildableBox.tag = "buildableBox";

        Renderer rend = buildableBox.GetComponent<Renderer>();
        rend.enabled = true;
        isBuildEnabled = true;
    }

    void disableBuild()
    {
        isBuildEnabled = false;
        Destroy(buildable);
        Destroy(buildableBox);
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuildEnabled)
        {
            Ray pendingBuildSpot = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit pendingInfo;
            Physics.Raycast(pendingBuildSpot, out pendingInfo);

            Vector3 finalPendingBuildPosition = pendingInfo.point;
            finalPendingBuildPosition = grid.GetNearestPointOnGrid(finalPendingBuildPosition);
            finalPendingBuildPosition.y = 0;
            buildable.transform.position = finalPendingBuildPosition;
            buildableBox.transform.position = finalPendingBuildPosition + new Vector3(0, 0.7f, 0);
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
     
                buildableBox.transform.position = buildable.transform.position + new Vector3(0, 0.8f, 0);
                buildableBox.transform.localScale = buildable.transform.GetChild(0).transform.GetChild(200).GetComponent<Renderer>().bounds.size;
            }
        }

        if (Input.GetKeyDown("c"))
        {
            if (!isBuildEnabled)
            {
                enableBuild();
            }
            else
            {
                disableBuild();
            }
        }
    }

    private void placeBuildingNear(Vector3 clickPoint)
    {
        Debug.Log(buildableBox.GetComponent<Renderer>().material.color.r == 0.2512111f);
        if (buildableBox.GetComponent<Renderer>().material.color.r == 0.2512111f)
        {
            var finalPosition = buildable.transform.position;
            var finalRotation = buildable.transform.rotation;
            GameObject wallToBuild = Instantiate(wall);
            wallToBuild.transform.position = finalPosition;
            wallToBuild.transform.rotation = finalRotation;
            //GameObject wallInstance = Instantiate(wallToBuild, finalPosition, new Quaternion(-90, 0, 0, 90));
            //wallToBuild.AddComponent<Rigidbody>();
            //wallToBuild.AddComponent<BoxCollider>();
        }
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
    }
}
