using UnityEngine;
using System.IO;

public struct Pose
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public Pose(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
    }
}

public class TriangleObjects : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;

    public Pose pose1;
    public Pose pose2;
    public Pose pose3;
    public Pose pose4;

    public Light directionalLight;
    public Light pointLight;
    public Light spotLight;

    public Camera pinholeCamera;

    void Start()
    {
        // Instantiate the objects
        object1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        object2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        object3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        object4 = GameObject.CreatePrimitive(PrimitiveType.Capsule);

        // Increase the vertex count and add a MeshFilter component to object1
        //MeshFilter meshFilter1 = object1.AddComponent<MeshFilter>();
        Vector3[] vertices1 = new Vector3[10002];
        int[] triangles1 = new int[10002];
        for (int i = 0; i < 10002; i++)
        {
            vertices1[i] = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            triangles1[i] = i;
        }
        Mesh mesh1 = object1.GetComponent<MeshFilter>().sharedMesh;
        mesh1.vertices = vertices1;
        mesh1.triangles = triangles1;
        MeshFilter meshFilter1 = object3.GetComponent<MeshFilter>();
        meshFilter1.mesh = mesh1;

        // Increase the vertex count and add a MeshFilter component to object2
        //MeshFilter meshFilter2 = object2.AddComponent<MeshFilter>();
        Mesh mesh2 = object2.GetComponent<MeshFilter>().sharedMesh;
        Vector3[] vertices2 = new Vector3[10002];
        int[] triangles2 = new int[10002];
        for (int i = 0; i < 10002; i++)
        {
            vertices2[i] = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            triangles2[i] = i;
        }
        mesh2.vertices = vertices2;
        mesh2.triangles = triangles2;
        MeshFilter meshFilter2 = object3.GetComponent<MeshFilter>();
        meshFilter2.mesh = mesh2;

        // Increase the vertex count and add a MeshFilter component to object3
        //MeshFilter meshFilter3 = object3.AddComponent<MeshFilter>();
        Mesh mesh3 = object3.GetComponent<MeshFilter>().sharedMesh;
        Vector3[] vertices3 = new Vector3[10002];
        int[] triangles3 = new int[10002];
        for (int i = 0; i < 10002; i++)
        {
            vertices3[i] = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            triangles3[i] = i;
        }
        mesh3.vertices = vertices3;
        mesh3.triangles = triangles3;
        MeshFilter meshFilter3 = object3.GetComponent<MeshFilter>();
        meshFilter3.mesh = mesh3;

        // Increase the vertex count and add a MeshFilter component to object4
        //MeshFilter meshFilter4 = object4.AddComponent<MeshFilter>();
        Mesh mesh4 = object4.GetComponent<MeshFilter>().sharedMesh;
        Vector3[] vertices4 = new Vector3[10002];
        int[] triangles4 = new int[10002];
        for (int i = 0; i < 10002; i++)
        {
            vertices4[i] = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            triangles4[i] = i;
        }
        mesh4.vertices = vertices4;
        mesh4.triangles = triangles4;
        MeshFilter meshFilter4 = object3.GetComponent<MeshFilter>();
        meshFilter4.mesh = mesh4;

        // Set the default pose for each object
        pose1 = new Pose(new Vector3(-2, 0, 0), Quaternion.Euler(0, 45, 0), new Vector3(2, 2, 2));
        pose2 = new Pose(new Vector3(2, 0, 0), Quaternion.Euler(0, -45, 0), new Vector3(3, 3, 3));
        pose3 = new Pose(new Vector3(0, 0, -2), Quaternion.Euler(45, 0, 0), new Vector3(1, 4, 1));
        pose4 = new Pose(new Vector3(0, 0, 2), Quaternion.Euler(-45, 0, 0), new Vector3(1, 3, 1));

        // Set the pose for each object
        object1.transform.position = pose1.position;
        object1.transform.rotation = pose1.rotation;
        object1.transform.localScale = pose1.scale;
        object2.transform.position = pose2.position;
        object2.transform.rotation = pose2.rotation;
        object2.transform.localScale = pose2.scale;
        object3.transform.position = pose3.position;
        object3.transform.rotation = pose3.rotation;
        object3.transform.localScale = pose3.scale;
        object4.transform.position = pose4.position;
        object4.transform.rotation = pose4.rotation;
        object4.transform.localScale = pose4.scale;

        // Add a Lambertian material to each object
        Material material = new Material(Shader.Find("Standard"));
        object1.GetComponent<MeshRenderer>().material = material;
        object2.GetComponent<MeshRenderer>().material = material;
        object3.GetComponent<MeshRenderer>().material = material;
        object4.GetComponent<MeshRenderer>().material = material;

        // Add a DirectionalLight to the scene
        directionalLight = new GameObject("Directional Light").AddComponent<Light>();
        directionalLight.type = LightType.Directional;
        directionalLight.intensity = 1f;
        directionalLight.transform.rotation = Quaternion.Euler(50, -30, 0);

        // Add a PointLight to the scene
        pointLight = new GameObject("Point Light").AddComponent<Light>();
        pointLight.type = LightType.Point;
        pointLight.intensity = 0.5f;
        pointLight.transform.position = new Vector3(1, 1, 1);

        // Add a SpotLight to the scene
        spotLight = new GameObject("Spot Light").AddComponent<Light>();
        spotLight.type = LightType.Spot;
        spotLight.intensity = 0.75f;
        spotLight.transform.position = new Vector3(-1, 1, -1);
        spotLight.transform.rotation = Quaternion.Euler(45, 0, 0);

        // Add a pinhole camera to the scene
        pinholeCamera = new GameObject("Pinhole Camera").AddComponent<Camera>();
        pinholeCamera.fieldOfView = 60f;
        pinholeCamera.transform.position = new Vector3(0, 0, -14);
        pinholeCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    void Update()
    {

        if (Input.GetMouseButton(0)) {



            // Create a 640x480 image using the ray caster
            int width = 640;
            int height = 480;
            Texture2D image = new Texture2D(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Cast a ray from the pinhole camera through pixel (x, y)
                    Ray ray = pinholeCamera.ScreenPointToRay(new Vector3(x, y, 0));

                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100.0f, Color.red);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit);
                    if (hit.collider.gameObject.name == "blackhole")
                    {
                        Debug.Log("Ray intersected with blackhole: " + hit.collider.gameObject.name);
                        Debug.DrawLine(ray.origin, hit.point, Color.magenta);
                        image.SetPixel(x, y, hit.transform.GetComponent<MeshRenderer>().material.color);
                    }
                    else
                    {
                        Debug.Log("Ray did not intersect blackhole");
                    }
                }
            }
            // Save the image to a file
            byte[] bytes = image.EncodeToPNG();
            File.WriteAllBytes("image.png", bytes);
        }





            
        
    }
}


