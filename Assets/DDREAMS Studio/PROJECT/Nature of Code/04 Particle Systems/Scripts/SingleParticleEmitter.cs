using UnityEngine;

namespace DDREAMS.CodingTrainTrips
{
    public class SingleParticleEmitter : MonoBehaviour
    {
        [SerializeField]
        private GameObject _ParticlePrebaf;

        [SerializeField]
        private Vector3 _Force = Vector3.down;

        [SerializeField]
        private Vector3 _Rotation = Vector3.down;


        private Particle _particle;
        private GameObject _particlePrebaf;


        private void Start()
        {
            _particle = new Particle();

            InitialiseParticle();
            InstantiateParticlePrefab();
        }

        private void FixedUpdate()
        {
            _particle.ApplyForce(Time.fixedDeltaTime * _Force);
            _particle.Update();

            _particlePrebaf.transform.position = _particle.GetPosition();
            _particlePrebaf.transform.Rotate(Time.fixedDeltaTime * _particle.GetRotation());

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
            _particlePrebaf = Instantiate(_ParticlePrebaf);

            _particlePrebaf.name = "Single Particle";
            _particlePrebaf.transform.position = _particle.GetPosition();
            _particlePrebaf.transform.parent = transform;
        }
    }
}
