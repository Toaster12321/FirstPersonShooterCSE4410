using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//used to regsiter shooting via raycasting and crosshair GUI
public class RayShooter : MonoBehaviour
{
    //private variable that will store the reference to the camera
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //using GetComponent to obtain the reference to the camera and store it in our var cam
        cam = GetComponent<Camera>();

        //locks the cursor to the game screen and hides the cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    //OnGUI method for drawing crosshair on screen
    private void OnGUI()
    {
        //font size
        int size = 24;

        //coords at which the crosshair is drawn
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;

        //draws the cross hair as text
        GUI.Label(new Rect(posX, posY, size, size), "+");

        //draws button with text on game screen in the upper left
        if (GUI.Button(new Rect(10, 10, 180, 20), "Click here for a free IPad"))
        {
            Debug.Log("Button has been clicked!");
        }
      
    }

    //Coroutine below - a coroutine is a method that can stop and pick up on the frame it left off on
    //places a sphere at a set of coords and removes the sphere after 1s
    private IEnumerator SphereIndicator(Vector3 pos)
    { 
        //creates a new GameObject that is a sphere
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        //places is at the given position and transforms it to be a small paintball shape
        sphere.transform.position = pos;
        sphere.transform.localScale = Vector3.one * 0.05f;

        //waits one second
        yield return new WaitForSeconds(1);

        //after the one second wait the sphere is destroyed
        Destroy(sphere);
    }

    // Update is called once per frame
    void Update()
    {
        //runs the following code if the player presses left click and the cursor is not over a UI element
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //uses a vector3 to store the location of the middle of the screen
            //we divide the width and height by 2 of the camera to get the midpoint of the screen
            //these become the x and y values of the vector with z axis staying at 0
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);

            //creates a ray by calling ScreenPointToRay
            //passing in the point var because this will be the origin for our ray
            Ray ray = cam.ScreenPointToRay(point);

            //creating a RaycastHit object to register where the raycast hits
            RaycastHit hit;
            //if we hit something run code
            if (Physics.Raycast(ray, out hit))
            {
                //Gets a reference of GameObject that was hit
                //then gets a reference to the ReactiveTarget script
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                // if the ray hits an enemy, indicate in the console that it was hit
                //otherwise place a sphere aka bullet
                if (target != null)
                {
                    target.ReactToHit();
                   if (target.deathAnim != null) Messenger.Broadcast(GameEvent.ENEMY_HIT);
                    Debug.Log("target hit");
                    
                }
                else 
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }

                //if the ray hits something we call our coroutine to place the sphere
                StartCoroutine(SphereIndicator(hit.point));
            }
        }
    }


}
