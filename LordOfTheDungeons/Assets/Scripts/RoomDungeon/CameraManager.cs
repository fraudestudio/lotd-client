using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    // Current bounds that the camera is using
    private Bounds currentBounds;

    // Normal bounds off the room
    private Bounds roomBounds;
    public Bounds RoomBounds { get => roomBounds; set => roomBounds = value; }

    // Tells if the camera can change position or not
    private bool canChange = false;

    // Timer used for movement
    private float timer = 0;

    // x position added to the bounds
    private float sideXPlus;
    // y position added to the bounds
    private float sideYPlus;
    // zoom of the camera
    private float zoomSize;
    // camera speed
    public float cameraSpeed = 0.2f;



    /// <summary>
    /// Values set for the camera before movement
    /// </summary>
    /// <param name="sideXPlus">x position added to the bounds</param>
    /// <param name="sideYPlus">y position added to the bounds</param>
    /// <param name="zoomSize">zoom of the camera</param>
    /// <param name="cameraSpeed">speed of the camera</param>
    private void SetValueForCamera(float sideXPlus, float sideYPlus, float zoomSize, float cameraSpeed)
    {
        this.sideXPlus = sideXPlus;
        this.sideYPlus = sideYPlus;
        this.zoomSize = zoomSize;
        this.cameraSpeed = cameraSpeed;
    }

    /// <summary>
    /// Center the camera on the room
    /// </summary>
    /// <param name="cameraSpeed">speed of the camera</param>
    public void CenterOnRoom(float cameraSpeed)
    {
        timer = 0;

        // we set the camera a bit on the side
        SetValueForCamera(5, 0, 13.75f, cameraSpeed);
        
        // Tells that the current bounds are the room one
        currentBounds = NormalizeBounds(RoomBounds);

        // Start the animation
        canChange = true;
    }

    /// <summary>
    /// Center the camera on a list of objects that we want
    /// </summary>
    /// <param name="objects">list of the objects that we want to center</param>
    /// <param name="zoom">Zoom of the camera</param>
    /// <param name="cameraSpeed">speed of the camera</param>
    public void CenterOnObjects(List<GameObject> objects, float zoom, float cameraSpeed)
    {
        timer = 0;
        SetValueForCamera(0, 0, zoom, cameraSpeed);

        // Add the objects to the bounds 
        currentBounds = CreateBounds(objects[objects.Count / 2].transform.position, Vector3.zero);
        foreach (GameObject obj in objects)
        {
            currentBounds.Encapsulate(obj.transform.position);
        }

        // Star the animation
        canChange = true;

    }

    /// <summary>
    /// Change the camera of position and zoom 
    /// in function of the attributes set with 
    /// SetValueForCamera() if canChange allows it
    /// </summary>
    private void Update()
    {
        if (canChange)
        {
            if (mainCamera.transform.position != currentBounds.center && mainCamera.orthographicSize != currentBounds.size.x * zoomSize)
            {
                timer += cameraSpeed * Time.deltaTime;
                mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, zoomSize, timer);
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, currentBounds.center, timer);
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -10);
            }
            else
            {
                timer = 0;
                canChange = false;
                mainCamera.orthographicSize = currentBounds.size.x * zoomSize;
                mainCamera.transform.position = new Vector3(currentBounds.center.x,currentBounds.center.y, -10);
            }

        }

    }

    /// <summary>
    /// Normalize the bounds with the added x and y position
    /// </summary>
    /// <param name="bounds">bounds that we want to normalize</param>
    /// <returns>the new normalized bounds</returns>
    private Bounds NormalizeBounds(Bounds bounds)
    {
        Bounds newBounds = bounds;

        newBounds.center = new Vector3(newBounds.center.x + sideXPlus, newBounds.center.y + sideYPlus);

        return newBounds;
    }

    /// <summary>
    /// Create a normalized bounds
    /// </summary>
    /// <param name="center">the center of the bounds</param>
    /// <param name="size">the size of the bounds</param>
    /// <returns>a normalized bounds</returns>
    private Bounds CreateBounds(Vector3 center, Vector3 size)
    {
        return NormalizeBounds(new Bounds(center, size));
    }
}
