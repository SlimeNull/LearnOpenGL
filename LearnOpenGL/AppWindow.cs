using System.ComponentModel;
using OpenGaming;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbiSharp;

namespace LearnOpenGL;

partial class AppWindow : GameWindow
{
    // Create the vertices for our triangle. These are listed in normalized device coordinates (NDC)
    // In NDC, (0, 0) is the center of the screen.
    // Negative X coordinates move to the left, positive X move to the right.
    // Negative Y coordinates move to the bottom, positive Y move to the top.
    // OpenGL only supports rendering in 3D, so to create a flat triangle, the Z coordinate will be kept as 0.
    private readonly float[] _vertices =
    {
    };

    public AppWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    // Now, we start initializing OpenGL.
    protected override unsafe void OnLoad()
    {
        base.OnLoad();

    }

    // Now that initialization is done, let's create our render loop.

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
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