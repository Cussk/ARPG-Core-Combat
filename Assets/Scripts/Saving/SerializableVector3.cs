using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    [System.Serializable]

    public class SerializableVector3
    {
        float x, y, z;

        //player current position
        public SerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        //designate a new vector3
        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}
