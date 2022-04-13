using UnityEngine;

public class Bullet : MonoBehaviour
{
    // For all the damage and stuff. Passed through the ActivateNext() method in
    // the pool script.
    public GunData gunData;
    [SerializeField] LineRenderer lineRenderer;
    float length = 6f;

    public float speed = 200f;
    Vector3 lastPosition;

    RaycastHit[] hit = new RaycastHit[1];

    public GameObject hitParticleSystem;

    // These track the age and lifetime of the bullet.
    private float lifetime = 5f;
    private float age;

    private void Start()
    {
        hit = new RaycastHit[1];
    }
    void Update()
    {
        CheckHit();
        CheckAge();

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + transform.forward * length);

        lastPosition = transform.position;
        transform.position += (transform.forward * speed) * Time.deltaTime;
        
    }

    private void OnEnable()
    {
        age = 0f;
    }

    void CheckHit()
    {
        Ray ray = new Ray(lastPosition, transform.forward);
        float distance = Vector3.Distance(lastPosition, transform.position);

        // If the number of things we've hit is greater than zero, we have hit something.
        if (Physics.RaycastNonAlloc(ray, hit, distance) > 0)
        {
            ParticleSystemManager.CreateParticle(hit[0].point, Quaternion.LookRotation(hit[0].normal));
            
            IDamageable damageable = hit[0].transform.GetComponent<IDamageable>();
            damageable?.Damage(50);

            if (hit[0].rigidbody != null)
            {
                hit[0].rigidbody.AddForce(-hit[0].normal * 50);
            }

            gameObject.SetActive(false);
        }
    }

    void CheckAge()
    {
        // If the bullet is too old, deactivate it.
        age += Time.deltaTime;
        if(age > lifetime)
        {
            gameObject.SetActive(false);
        }
    }
}
