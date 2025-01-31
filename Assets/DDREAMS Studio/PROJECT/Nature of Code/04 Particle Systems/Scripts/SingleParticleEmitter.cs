using UnityEngine;

namespace DDREAMS.CodingTrainTrips
{
    public class SingleParticleEmitter : MonoBehaviour
    {
        [Header("Particle Settings")]
        [SerializeField]
        [Tooltip("The Prefab that will be used as a particle.")]
        private GameObject _ParticlePrefab;

        [Header("Transform Settings")]
        [SerializeField]
        [Tooltip("A force that will alter the path of the particle.")]
        private Vector3 _Force = Vector3.down;

        [SerializeField]
        [Tooltip("The rotation speed of the particle per axis in eulerAngles per second.")]
        private Vector3 _Rotation = new(60.0f, 60.0f, 60.0f);


        private const string ERROR__NO_PARTICLE_PREFAB = "No Prefab found for the particle. Please add a particle Prefab.";


        private Particle _particle;
        private GameObject _particlePrebaf;


        private void Start()
        {
            if (!ReferenceFound()) return;

            _particle = new Particle();

            InitialiseParticle();
            InstantiateParticlePrefab();
        }

        private void FixedUpdate()
        {
            _particle.ApplyForce(Time.fixedDeltaTime * _particle.Force);
            _particle.Update();

            _particlePrebaf.transform.position = _particle.Position;
            _particlePrebaf.transform.Rotate(Time.fixedDeltaTime * _particle.Rotation);

            if (_particle.IsDead()) InitialiseParticle();
        }


        private void InitialiseParticle()
        {
            _particle.Initialise();
            _particle.SetPosition(transform.position);
            _particle.SetRotation(_Rotation);
            _particle.SetVelocity(new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(0.0025f, 0.025f), 0.0f));
        }

        private void InstantiateParticlePrefab()
        {
            _particlePrebaf = Instantiate(_ParticlePrefab);

            _particlePrebaf.name = "Single Particle";
            _particlePrebaf.transform.position = _particle.Position;
            _particlePrebaf.transform.parent = transform;
        }

        private bool ReferenceFound()
        {
            bool referenceFound = true;

            if (_ParticlePrefab == null)
            {
                Debug.LogWarning(ERROR__NO_PARTICLE_PREFAB);
                referenceFound = false;
            }

            if (!referenceFound) enabled = false;

            return referenceFound;
        }
    }
}
