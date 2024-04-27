using System.ComponentModel;
using LearnOpenGL.Components;
using OpenGaming;
using OpenGaming.Components;
using OpenGaming.Materials;
using OpenGaming.Meshes;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbiSharp;

namespace LearnOpenGL;

partial class AppWindow : GameWindow
{
    public AppWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {

        cubeObject = new()
        {
            Components =
            {
                new MeshRenderer()
                {
                    Material = StandardMaterial.Create(),
                    Mesh = CubeMesh.Instance,
                },

                new MyCube(),
            }
        };

        cameraObject = new()
        {
            Components =
            {
                new Camera()
                {
                    ClearType = CameraClearType.Color,
                    ClearColor = new Color4(30, 30, 30, 255)
                }
            }
        };

        game = new Game()
        {
            Objects =
            {
                cubeObject,
                cameraObject,
            },
            Output = this,
        };
    }

    Game game;
    GameObject cubeObject;
    GameObject cameraObject;

    bool _notFirstFrame;

    // Now, we start initializing OpenGL.
    protected override unsafe void OnLoad()
    {
        base.OnLoad();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        if (!_notFirstFrame)
        {
            game.GameStart();
            _notFirstFrame = true;
        }

        game.GameUpdate();
        game.GameLateUpdate();

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
    }

    // Now, for cleanup.
    // You should generally not do cleanup of opengl resources when exiting an application,
    // as that is handled by the driver and operating system when the application exits.
    // 
    // There are reasons to delete opengl resources, but exiting the application is not one of them.
    // This is provided here as a reference on how resource cleanup is done in opengl, but
    // should not be done when exiting the application.
    //
    // Places where cleanup is appropriate would be: to delete textures that are no
    // longer used for whatever reason (e.g. a new scene is loaded that doesn't use a texture).
    // This would free up video ram (VRAM) that can be used for new textures.
    //
    // The coming chapters will not have this code.
    protected override void OnUnload()
    {


        base.OnUnload();
    }
}