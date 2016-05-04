using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.AudioVideoPlayback;

namespace pctesting.TestHardware
{
    public partial class Teapot : Form
    {
        Device device;
        Mesh teapot;
        CheckEnable enable;

        public Teapot(CheckEnable _enable)
        {
            enable = _enable;
        }
        public void InitD3D()
        {
            try
            {
                PresentParameters parameters = new PresentParameters();
                parameters.Windowed = true;
                parameters.SwapEffect = SwapEffect.Discard;
                 device = new Device(0, DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing, parameters);
                teapot = Mesh.Teapot(device);
            }
            catch (Exception e)
            {
                enable.Active = false;
                return;
            }
        }

        public void Render()
        {
            try
            {
                device.Clear(ClearFlags.Target, System.Drawing.Color.Black, 1.0f, 0);
                device.BeginScene();
                try
                {

                    device.RenderState.Lighting = true;
                    device.Lights[0].Type = LightType.Directional;
                    device.Lights[0].Direction = new Vector3(7, -2, 0);
                    device.Lights[0].Diffuse = System.Drawing.Color.Aqua;
                    device.Lights[0].Enabled = true;

                    Material material = new Material();
                    material.Ambient = System.Drawing.Color.White;
                    material.Diffuse = System.Drawing.Color.White;
                    material.Specular = System.Drawing.Color.White;
                    material.Emissive = System.Drawing.Color.Magenta;
                    device.Material = material;

                    Matrix matrix = new Matrix();
                    matrix.Scale(0.5f, 0.5f, 0.5f);
                    matrix *= Matrix.RotationY(System.Environment.TickCount / 300.0f);
                    matrix *= Matrix.RotationX(System.Environment.TickCount / 10f);
                    matrix *= Matrix.RotationZ(System.Environment.TickCount / 5f);
                    float move = 10;
                    matrix.M43 = move;
                    device.RenderState.Clipping = false;
                    device.Transform.World = matrix;
                    device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 10, 1.0f, 0.1f, 20.0f);

                    device.RenderState.CullMode = Cull.Clockwise;
                    teapot.DrawSubset(0);
                }
                finally
                {
                    device.EndScene();
                }
                device.Present();
            }
            catch (Exception e)
            {
                enable.Active = false;
                return;
            }
        }



    }
}
