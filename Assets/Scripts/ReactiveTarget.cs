using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//alerts the enemy to react if it was hit by the raycast (bullet)
public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    public Coroutine deathAnim { private set; get; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_particles.enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // method if the target gets hit then react to it
    public void ReactToHit()
    {
        //gets reference to our WanderingAI script and stores it
        WanderingAI behavior = GetComponent<WanderingAI>();
        // if hit sets state to dead aka SetAlive is false
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }

        //doing same for smartmovement and fireball shooter script
        SmartMovement smart = GetComponent<SmartMovement>();
        if (smart != null) smart.ChangeMovementState(SmartMovement.MovementState.PAUSED);

        FireballShooter shooter = GetComponent<FireballShooter>();
        if (shooter != null) shooter.ChangeFiringState(FireballShooter.FiringState.PAUSED);
        
        //AI dies
        if (deathAnim == null) deathAnim = StartCoroutine(Die());
    }

    //coroutine that acts as the death animation
    public IEnumerator Die()
    {
        //has the target fall over to their side
        transform.Rotate(-75, 0, 0);

        //turn on death fire particles
        _particles.enableEmission = true;

        //waits for 1.5s
        yield return new WaitForSeconds(1.5f);

        //then despawns by destroying itself (queue free)
        Destroy(gameObject);
    }
}
