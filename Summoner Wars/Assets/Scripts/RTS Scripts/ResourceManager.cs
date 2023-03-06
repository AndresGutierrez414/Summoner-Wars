using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RTS{
    public static class ResourceManager
    {

        public static float ScrollSpeed { get { return 100; } }
        public static float RotateSpeed { get { return 200; } }
        public static int ScrollWidth { get { return 50; } }
        public static float MinCameraHeight { get { return 10; } }
        public static float MaxCameraHeight { get { return 40; } }
        public static float RotateAmount { get { return 360; } }
        private static Vector3 invalidPosition = new Vector3(-99999, -99999, -99999);
        public static Vector3 InvalidPosition { get { return invalidPosition; } }
    }
}

