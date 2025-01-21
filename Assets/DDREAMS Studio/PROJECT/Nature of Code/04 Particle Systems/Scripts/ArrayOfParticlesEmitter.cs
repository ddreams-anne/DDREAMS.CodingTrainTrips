using UnityEngine;

namespace DDREAMS.CodingTrainTrips
{
    public class ArrayOfParticlesEmitter : MonoBehaviour
    {
        [Header("Particle Settings")]
        [SerializeField]
        [Tooltip("The Prefab that will be used as a particle.")]
        private GameObject _ParticlePrefab;

        [SerializeField]
        [Tooltip("The number of particles to emit.")]
        private int _NumberOfParticles = 99;

        [SerializeField]
        [Tooltip("The lifespan of the particles.")]
        private float _Lifespan = 666.0f;

        [Header("Transform Settings")]
        [SerializeField]
        [Tooltip("The minimum force per axis that will alter the path of a particle.")]
        private Vector3 _MinimumForce = new(-0.0001f, -0.0001f, -0.0001f);

        [SerializeField]
        [Tooltip("The maximum force per axis that will alter the path of a particle.")]
        private Vector3 _MaximumForce = new(-0.0005f, -0.0005f, -0.0005f);

        [SerializeField]
        [Tooltip("The minimum velocity per axis that will alter the path of a particle.")]
        private Vector3 _MinimumVelocity = new(-0.01f, -0.01f, -0.01f);

        [SerializeField]
        [Tooltip("The maximum velocity per axis that will alter the path of a particle.")]
        private Vector3 _MaximumVelocity = new(0.01f, 0.01f, 0.01f);

        [SerializeField]
        [Tooltip("The minimum rotation speed of a particle per axis in eulerAngles per second.")]
        private Vector3 _MinimumRotation = new(-90.0f, -90.0f, -90.0f);

        [SerializeField]
        [Tooltip("The maximum rotation speed of a particle per axis in eulerAngles per second.")]
        private Vector3 _MaximumRotation = new(90.0f, 90.0f, 90.0f);

        [SerializeField]
        [Tooltip("Use force on the particles?")]
        private bool _UseForce = true;

        [SerializeField]
        [Tooltip("Use velocity on the particles?")]
        private bool _UseVelocity = true;

        [SerializeField]
        [Tooltip("Use rotation of the particles?")]
        private bool _UseRotation = true;

        private const string ERROR__NO_PARTICLE_PREFAB = "No Prefab found for the particles. Please add a particle Prefab.";


        public Particle[] _particles;
        private GameObject[] _particlePrebafs;

        private void Awake()
        {
            if (!ReferenceFound()) return;

            _particles = new Particle[_NumberOfParticles];
            _particlePrebafs = new GameObject[_NumberOfParticles];
        }

        private void Start()
        {
            if (!ReferenceFound()) return;

            InitialiseParticles();
            InstantiateParticlePrefabs();
        }

        private void FixedUpdate()
        {
            if (!ReferenceFound()) return;

            for (int particleIndex = 0; particleIndex < _NumberOfParticles; particleIndex++)
            {
                if (_UseForce) _particles[particleIndex].ApplyForce(Time.fixedDeltaTime * _particles[particleIndex].Force);

                _particles[particleIndex].Update();

                _particlePrebafs[particleIndex].transform.position = _particles[particleIndex].Position;
                _particlePrebafs[particleIndex].transform.Rotate(Time.fixedDeltaTime * _particles[particleIndex].Rotation);

                if (_particles[particleIndex].IsDead()) _particles[particleIndex] = CreateNewParticle();
            }
        }


        private Particle CreateNewParticle()
        {
            Particle newParticle = new();

            newParticle.Initialise();
            newParticle.SetLifespan(_Lifespan);
            newParticle.SetPosition(transform.position);

            if (_UseForce)
            {
                Vector3 force = new(Random.Range(_MinimumForce.x, _MaximumForce.x), Random.Range(_MinimumForce.y, _MaximumForce.y), Random.Range(_MinimumForce.z, _MaximumForce.z));

                newParticle.SetForce(force);
            }
            else newParticle.SetForce(Vector3.zero);

            if (_UseRotation)
            {
                Vector3 rotation = new(Random.Range(_MinimumRotation.x, _MaximumRotation.x), Random.Range(_MinimumRotation.y, _MaximumRotation.y), Random.Range(_MinimumRotation.z, _MaximumRotation.z));

                newParticle.SetRotation(rotation);
            }
            else newParticle.SetRotation(Vector3.zero);

            if (_UseVelocity)
            {
                Vector3 velocity = new(Random.Range(_MinimumVelocity.x, _MaximumVelocity.x), Random.Range(_MinimumVelocity.y, _MaximumVelocity.y), Random.Range(_MinimumVelocity.z, _MaximumVelocity.z));

                newParticle.SetVelocity(velocity);
            }
            else newParticle.SetVelocity(Vector3.zero);

            return newParticle;
        }

        private void InitialiseParticles()
        {
            for (int particleIndex = 0; particleIndex < _NumberOfParticles; particleIndex++) _particles[particleIndex] = CreateNewParticle();
        }

        private void InstantiateParticlePrefabs()
        {
            for (int particleIndex = 0; particleIndex < _NumberOfParticles; particleIndex++)
            {
                _particlePrebafs[particleIndex] = Instantiate(_ParticlePrefab);

                _particlePrebafs[particleIndex].name = $"Particle {particleIndex.ToString("D7")}";
                _particlePrebafs[particleIndex].transform.position = _particles[particleIndex].Position;
                _particlePrebafs[particleIndex].transform.parent = transform;
            }
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
