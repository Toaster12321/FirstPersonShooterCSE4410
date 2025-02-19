using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used to move the enemy AI around
public class WanderingAI : MonoBehaviour
{
    //Projectiles to shoot (fireball)
    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;

    //speed of the wander 
    //and range to look for obstacles
    public float speed = 3f;
    public float obstacleRange = 5f;

    //state of the game object
    private bool isAlive;

    //by default the enemy starts in an alive state
    private void Start()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //moves the enemy forward if they are alive
        if (isAlive) 
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        
        //creates a ray pointing from the enemy that points in the same direction as the gameobject
        Ray ray = new Ray(transform.position, transform.forward);

        //data containter containing hit information
        RaycastHit hit;

        //perform the raycast with a sphere instead to broaden the range
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            //Get a reference to the game object hit by the spherecast
            GameObject hitObject = hit.transform.gameObject;

            // if the object hit was the player, shoot a fireball at the player
            //otherwise, if the object is within the obstacel range, turn around
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (fireball == null)
                {
                    fireball = Instantiate(fireballPrefab) as GameObject;
                    fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    fireball.transform.rotation = transform.rotation;
                }
            }

            //else, the ray hits something, turn around by a random angle
            // only executes if the object is within the range
            else if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110f, 110f);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    //public method to set the enemy as alive to be accessed by outside scripts
    public void SetAlive(bool alive)
    { 
        isAlive=alive;
    }
}
