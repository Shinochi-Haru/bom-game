using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] ParticleSystem m_particle;
    [SerializeField] float _force = 20;
    [SerializeField] float _radius = 5;
    [SerializeField] float _upwards = 0;
    Vector3 _position;
    Rigidbody rb;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// ”j‰ó‚µ‚½Œã‚É”š”­‚·‚é
    /// </summary>
    private void OnDestroy()
    {
        Explosion();
    }

    /// <summary>
    /// ”š”­‚·‚éŒø‰Ê
    /// </summary>
    public void Explosion()
    {
        //m_particle.Play();
        // m_position = m_particle.transform.position;

        // ”ÍˆÍ“à‚ÌRigidbody‚ÉAddExplosionForce
        Vector3 _explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(_explosionPos, _radius);
        //Collider[] hitColliders = Physics.OverlapSphere(m_position, m_radius);
        //for (int i = 0; i < colliders.Length; i++)
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            //var rb = colliders[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(_force, _explosionPos, _radius, _upwards, ForceMode.Impulse);
            }
        }
    }
}