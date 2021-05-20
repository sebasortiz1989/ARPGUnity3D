using UnityEngine;

namespace RPG.SavingC
{
    [System.Serializable] //This is making all the fields in the class like serialized fields.
    public class SerializableVector3
    {
        float x, y, z;

        public SerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}

