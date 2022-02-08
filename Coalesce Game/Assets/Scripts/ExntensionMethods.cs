using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public static class ExntensionMethods
    {
        public static Vector3 RemapToVector3(this Vector2 vector)
            => new Vector3(vector.x, 0f, vector.y);

        public static Vector3 Mask(this Vector3 vector, Vector3 mask)
            => new Vector3(vector.x * mask.x, vector.y * mask.y, vector.z * mask.z);
    }
}
