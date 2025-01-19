using UnityEngine;

namespace DDREAMS.CodingTrainTrips
{
    public class Particle
    {
        public Vector3 Position { get; private set; }
        public Vector3 Velocity { get; private set; }
        public Vector3 Acceleration { get; private set; }
        public float Lifespan { get; private set; }


        public void ApplyForce(Vector3 force)
        {
            Acceleration += force;
        }

        public Vector3 GetAcceleration()
        {
            return Acceleration;
        }

        public float GetLifespan()
        {
            return Lifespan;
        }

        public Vector3 GetPosition()
        {
            return Position;
        }

        public Vector3 GetVelocity()
        {
            return Velocity;
        }

        public void Initialise()
        {
            Position = Vector3.zero;
            Velocity = Vector3.zero;
            Acceleration = Vector3.zero;
            Lifespan = 255.0f;
        }

        public void Initialise(Vector3 position, Vector3 velocity, Vector3 acceleration, float lifespan)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
            Lifespan = lifespan;
        }

        public bool IsDead()
        {
            return Lifespan <= 0.0f;
        }

        public void SetAcceleration(Vector3 acceleration)
        {
            Acceleration = acceleration;
        }

        public void SetLifespan(float lifespan)
        {
            Lifespan = lifespan;
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
        }

        public void SetVelocity(Vector3 velocity)
        {
            Velocity = velocity;
        }

        public void Update()
        {
            Velocity += Acceleration;
            Position += Velocity;
            Lifespan -= 1.0f;
        }
    }
}
