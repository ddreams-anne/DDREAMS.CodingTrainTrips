using UnityEngine;

namespace DDREAMS.CodingTrainTrips
{
    public class Particle
    {
        public Vector3 Position { get; private set; }
        public Vector3 Velocity { get; private set; }
        public Vector3 Acceleration { get; private set; }
        public Vector3 Rotation { get; private set; }
        public Vector3 Force { get; private set; }
        public float Lifespan { get; private set; }


        public void ApplyForce()
        {
            Acceleration += Force;
        }

        public void ApplyForce(Vector3 force)
        {
            Acceleration += force;
        }

        public void Initialise()
        {
            Position = Vector3.zero;
            Velocity = Vector3.zero;
            Acceleration = Vector3.zero;
            Rotation = Vector3.zero;
            Lifespan = 222.0f;
        }

        public void Initialise(Vector3 position, Vector3 velocity, Vector3 acceleration, Vector3 rotation, float lifespan)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
            Rotation = rotation;
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

        public void SetForce(Vector3 force)
        {
            Force = force;
        }

        public void SetLifespan(float lifespan)
        {
            Lifespan = lifespan;
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
        }

        public void SetRotation(Vector3 rotation)
        {
            Rotation = rotation;
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
