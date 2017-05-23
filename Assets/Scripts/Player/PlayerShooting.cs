using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;

    Ray shootRay2 = new Ray();
    RaycastHit shootHit2;

    Ray shootRay3 = new Ray();
    RaycastHit shootHit3;

    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    LineRenderer gunLine2;
    LineRenderer gunLine3;
    AudioSource gunAudio;
    Light gunLight;
    Light gunLight2;
    Light gunLight3;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunLine2 = GetComponent<LineRenderer>();
        gunLine3 = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
        gunLight2 = GetComponent<Light>();
        gunLight3 = GetComponent<Light>();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLine2.enabled = false;
        gunLine3.enabled = false;

        gunLight.enabled = false;
        gunLight2.enabled = false;
        gunLight3.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;
        gunLight2.enabled = true;
        gunLight3.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        gunLine2.enabled = true;
        gunLine2.SetPosition(0, transform.position);

        gunLine3.enabled = true;
        gunLine3.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        shootRay2.origin = transform.position;
        shootRay2.direction = transform.right + transform.forward;

        shootRay3.origin = transform.position;
        shootRay3.direction = -transform.right + transform.forward;

        if (Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point);
            gunLine.SetPosition(2, transform.position);
            gunLine.SetPosition(3, shootHit2.point);
            gunLine.SetPosition(4, transform.position);
            gunLine.SetPosition(5, shootHit3.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
            gunLine.SetPosition(2, transform.position);
            gunLine.SetPosition(3, shootRay2.origin + shootRay2.direction * range);
            gunLine.SetPosition(4, transform.position);
            gunLine.SetPosition(5, shootRay3.origin + shootRay3.direction * range);
        }
        if (Physics.Raycast(shootRay2, out shootHit2, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit2.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit2.point);
            }
            gunLine2.SetPosition(1, shootHit.point);
            gunLine2.SetPosition(2, transform.position);
            gunLine2.SetPosition(3, shootHit2.point);
            gunLine2.SetPosition(4, transform.position);
            gunLine2.SetPosition(5, shootHit3.point);
        }
        else
        {
            gunLine2.SetPosition(1, shootRay.origin + shootRay.direction * range);
            gunLine2.SetPosition(2, transform.position);
            gunLine2.SetPosition(3, shootRay2.origin + shootRay2.direction * range);
            gunLine2.SetPosition(4, transform.position);
            gunLine2.SetPosition(5, shootRay3.origin + shootRay3.direction * range);
        }
        if (Physics.Raycast(shootRay3, out shootHit3, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit3.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit3.point);
            }
            gunLine3.SetPosition(1, shootHit.point);
            gunLine3.SetPosition(2, transform.position);
            gunLine3.SetPosition(3, shootHit2.point);
            gunLine3.SetPosition(4, transform.position);
            gunLine3.SetPosition(5, shootHit3.point);
        }
        else
        {
            gunLine3.SetPosition(1, shootRay.origin + shootRay.direction * range);
            gunLine3.SetPosition(2, transform.position);
            gunLine3.SetPosition(3, shootRay2.origin + shootRay2.direction * range);
            gunLine3.SetPosition(4, transform.position);
            gunLine3.SetPosition(5, shootRay3.origin + shootRay3.direction * range);
        }
    }
}
