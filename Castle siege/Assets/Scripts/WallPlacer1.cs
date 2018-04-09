using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlacer1 : MonoBehaviour
{
    public GameObject wallMesh;
    public GameObject wall;
    public GameObject WariorQueue; 

    private Grid grid;
    private GameObject buildable;
    private GameObject buildableBox;
    private bool isBuildEnabled;
    private bool isBuildableWall;

    // Use this for initialization
    void Start()
    {
        
    }

    void enableBuild()
    {
        grid = FindObjectOfType<Grid>();
        buildable = new GameObject("breakableWall");
        //buildable.AddComponent<FracturedObject>();
        buildable = Instantiate(wallMesh);
        isBuildableWall = true;
        buildable.transform.localScale = wall.transform.localScale;
        buildable.transform.rotation = wall.transform.rotation;
        buildableBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        buildableBox.GetComponent<Collider>().isTrigger = true;
        buildableBox.transform.localScale = buildable.GetComponent<Renderer>().bounds.size;

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
        isBuildableWall = false;
        Destroy(buildable);
        Destroy(buildableBox);
    }

    void enableWariorBuild()
    {
        grid = FindObjectOfType<Grid>();
        buildable = new GameObject("Warior");
        //buildable = Instantiate(WariorQueue.transform.GetChild(0).gameObject as GameObject);
        buildable = Instantiate(WariorQueue);
        buildableBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        buildableBox.GetComponent<Collider>().isTrigger = true;
        buildableBox.transform.localScale = buildable.GetComponent<Collider>().bounds.size;

        Material buildMaterial = Resources.Load("Materials/BuildMaterials/BuildAlowed", typeof(Material)) as Material;
        //buildableBox.AddComponent<MeshRenderer>();
        buildableBox.GetComponent<Renderer>().material = buildMaterial;
        buildableBox.tag = "buildableBox";

        Renderer rend = buildableBox.GetComponent<Renderer>();
        rend.enabled = true;
        isBuildEnabled = true;
        isBuildableWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isBuildEnabled);
        if (isBuildEnabled)
        {
            Debug.Log("war");
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
                buildableBox.transform.localScale = buildable.GetComponent<Renderer>().bounds.size;
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

        if (Input.GetKeyDown("v"))
        {
            if (!isBuildEnabled)
            {
                enableWariorBuild();
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
            GameObject buildObject = new GameObject();
            if (isBuildableWall)
            {
                 buildObject = Instantiate(wall);
            }
            else
            {
                 buildObject = Instantiate(buildable);
            }
            buildObject.transform.position = finalPosition;
            buildObject.transform.rotation = finalRotation;
            //GameObject wallInstance = Instantiate(wallToBuild, finalPosition, new Quaternion(-90, 0, 0, 90));
            //wallToBuild.AddComponent<Rigidbody>();
            //wallToBuild.AddComponent<BoxCollider>();
        }
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
    }
}
