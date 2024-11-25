using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    public class Camera
    {

        public Matrix Translation;

        public float Zoom = 4f;

        public Vector2 Position;

        public Camera() 
        {
            


        }


        public void UpdateMatrices()
        {

            Zoom = 1f;

            /// Creation de la camera.
            Translation = Matrix.CreateLookAt(new Vector3(Position.X / (2 * Zoom), Position.Y / (2 * Zoom), 5), new Vector3(Position.X / (2 * Zoom), Position.Y / (2 * Zoom), 0), Vector3.Up);

            /// Application du zoom à la camera.
            Translation *= Matrix.CreateScale(Zoom * 5, Zoom * 5, 1f);

            /// Application de la rotation à la camera.
            Translation *= Matrix.CreateRotationX(0f) * Matrix.CreateRotationY(0f) * Matrix.CreateRotationZ(0f);

            /// Creation de la perspective de la camera à l'écran.
            Translation *= Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 1, 1f, 2048f);



        }

    }
}
