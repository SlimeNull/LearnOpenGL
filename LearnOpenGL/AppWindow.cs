using System.ComponentModel;
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
        // front
        -0.5f, -0.5f, 0.5f, 0, 0,
        -0.5f, 0.5f, 0.5f, 0, 1,
        0.5f, 0.5f, 0.5f, 1, 1,

        -0.5f, -0.5f, 0.5f, 0, 0,
        0.5f, 0.5f, 0.5f, 1, 1,
        0.5f, -0.5f, 0.5f, 1, 0,

        // top
        -0.5f, 0.5f, 0.5f, 0, 0,
        -0.5f, 0.5f, -0.5f, 0, 1,
        0.5f, 0.5f, -0.5f, 1, 1,

        -0.5f, 0.5f, 0.5f, 0, 0,
        0.5f, 0.5f, -0.5f, 1, 1,
        0.5f, 0.5f, 0.5f, 1, 0,
        
        // bottom
        -0.5f, -0.5f, -0.5f, 0, 0,
        -0.5f, -0.5f, 0.5f, 0, 1,
        0.5f, -0.5f, 0.5f, 1, 1,

        -0.5f, -0.5f, -0.5f, 0, 0,
        0.5f, -0.5f, 0.5f, 1, 1,
        0.5f, -0.5f, -0.5f, 1, 0,

        // right
        0.5f, -0.5f, 0.5f, 0, 0,
        0.5f, 0.5f, 0.5f, 0, 1,
        0.5f, 0.5f, -0.5f, 1, 1,

        0.5f, -0.5f, 0.5f, 0, 0,
        0.5f, 0.5f, -0.5f, 1, 1,
        0.5f, -0.5f, -0.5f, 1, 0,

        // left
        -0.5f, -0.5f, -0.5f, 0, 0,
        -0.5f, 0.5f, -0.5f, 0, 1,
        -0.5f, 0.5f, 0.5f, 1, 1,

        -0.5f, -0.5f, -0.5f, 0, 0,
        -0.5f, 0.5f, 0.5f, 1, 1,
        -0.5f, -0.5f, 0.5f, 1, 0,

        // back
        0.5f, -0.5f, -0.5f, 0, 0,
        0.5f, 0.5f, -0.5f, 0, 1,
        -0.5f, 0.5f, -0.5f, 1, 1,

        0.5f, -0.5f, -0.5f, 0, 0,
        -0.5f, 0.5f, -0.5f, 1, 1,
        -0.5f, -0.5f, -0.5f, 1, 0,
    };

    private float _moveSpeed = 1;
    private float _radianPerPixel = MathF.PI / 100;

    // These are the handles to OpenGL objects. A handle is an integer representing where the object lives on the
    // graphics card. Consider them sort of like a pointer; we can't do anything with them directly, but we can
    // send them to OpenGL functions that need them.

    // What these objects are will be explained in OnLoad.
    private int _vertexBufferObject;
    private int _vertexArrayObject;

    private Matrix4 _viewMatrix = Matrix4.Identity;
    private Matrix4 _projectMatrix = Matrix4.Identity;

    // This class is a wrapper around a shader, which helps us manage it.
    // The shader class's code is in the Common project.
    // What shaders are and what they're used for will be explained later in this tutorial.
    private Shader? _shader;
    private Matrix4 _translateMatrix4 = Matrix4.Identity;
    private Matrix4 _rotationMatrix4 = Matrix4.Identity;
    private Matrix4 _scaleMatrix4 = Matrix4.Identity;



    public AppWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    private Matrix4 GetRotationMatrix(float xRadian, float yRadian, float zRadian)
    {
        Matrix4 result = new Matrix4(
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

        if (xRadian != 0)
        {
            result = new Matrix4(
                1, 0, 0, 0,
                0, MathF.Cos(xRadian), -MathF.Sin(xRadian), 0,
                0, MathF.Sin(xRadian), MathF.Cos(xRadian), 0,
                0, 0, 0, 1) * result;
        }

        if (yRadian != 0)
        {
            result = new Matrix4(
                MathF.Cos(yRadian), 0, MathF.Sin(yRadian), 0,
                0, 1, 0, 0,
                -MathF.Sin(yRadian), 0, MathF.Cos(yRadian), 0,
                0, 0, 0, 1) * result;
        }

        if (zRadian != 0)
        {
            result = new Matrix4(
                MathF.Cos(zRadian), -MathF.Sin(zRadian), 0, 0,
                MathF.Sin(zRadian), MathF.Cos(zRadian), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1) * result;
        }

        return result;
    }

    // Now, we start initializing OpenGL.
    protected override unsafe void OnLoad()
    {
        base.OnLoad();

        // This will be the color of the background after we clear it, in normalized colors.
        // Normalized colors are mapped on a range of 0.0 to 1.0, with 0.0 representing black, and 1.0 representing
        // the largest possible value for that channel.
        // This is a deep green.
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        _vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);


        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);


        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject);

        // 定义数据结构, 由 3 个 float 构成模型顶点坐标以及 2 个 float 构成 UV 坐标
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

        // 启用属性 0 和 1
        GL.EnableVertexAttribArray(0);
        GL.EnableVertexAttribArray(1);


        // shader.vert and shader.frag contain the actual shader code.
        using var vertShaderSource = File.OpenText("Shaders/shader.vert");
        using var fragShaderSource = File.OpenText("Shaders/shader.frag");
        _shader = new Shader(vertShaderSource, fragShaderSource);

        using var avatarFileStream = File.OpenRead("Assets/avatar.jpg");
        using var avatarMemoryStream = new MemoryStream((int)avatarFileStream.Length);
        avatarFileStream.CopyTo(avatarMemoryStream);

        var image = Stbi.LoadFromMemory(avatarMemoryStream, 3);

        var texture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, texture);
        GL.ActiveTexture(TextureUnit.Texture0);

        fixed (byte* imageData = image.Data)
        {
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, image.Width, image.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, (nint)imageData);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        //var texture = GL.GenTexture();
        //GL.BindTexture(TextureTarget.Texture2D, texture);
        //GL.GenerateTextureMipmap(texture);


        _shader.Use();

        _viewMatrix = Matrix4.CreateTranslation(0, 0, -3);
        _projectMatrix = Matrix4.CreatePerspectiveFieldOfView(MathF.PI / 2, (float)ClientSize.X / ClientSize.Y, 0.1f, 100f);

        // Setup is now complete! Now we move to the OnRenderFrame function to finally draw the triangle.
        GL.Enable(EnableCap.CullFace);
        GL.FrontFace(FrontFaceDirection.Cw);
    }

    // Now that initialization is done, let's create our render loop.

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        // This clears the image, using what you set as GL.ClearColor earlier.
        // OpenGL provides several different types of data that can be rendered.
        // You can clear multiple buffers by using multiple bit flags.
        // However, we only modify the color, so ColorBufferBit is all we need to clear.
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // To draw an object in OpenGL, it's typically as simple as binding your shader,
        // setting shader uniforms (not done here, will be shown in a future tutorial)
        // binding the VAO,
        // and then calling an OpenGL function to render.

        // Bind the shader
        _shader!.Use();

        var transformLocation = GL.GetUniformLocation(_shader.Handle, "transform");
        var finalMatrix = _translateMatrix4 * _rotationMatrix4 * _scaleMatrix4;
        GL.UniformMatrix4(transformLocation, false, ref finalMatrix);


        var modelMatrix = Matrix4.CreateTranslation(0, 0, -3f);
        GL.UniformMatrix4(GL.GetUniformLocation(_shader.Handle, "model"), false, ref modelMatrix);
        GL.UniformMatrix4(GL.GetUniformLocation(_shader.Handle, "view"), false, ref _viewMatrix);
        GL.UniformMatrix4(GL.GetUniformLocation(_shader.Handle, "projection"), false, ref _projectMatrix);

        var timeLocation = GL.GetUniformLocation(_shader.Handle, "time");
        GL.Uniform1(timeLocation, (float)Environment.TickCount / 1000);

        // Bind the VAO
        GL.BindVertexArray(_vertexArrayObject);

        // And then call our drawing function.
        // For this tutorial, we'll use GL.DrawArrays, which is a very simple rendering function.
        // Arguments:
        //   Primitive type; What sort of geometric primitive the vertices represent.
        //     OpenGL used to support many different primitive types, but almost all of the ones still supported
        //     is some variant of a triangle. Since we just want a single triangle, we use Triangles.
        //   Starting index; this is just the start of the data you want to draw. 0 here.
        //   How many vertices you want to draw. 3 for a triangle.
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3 * 12);


        // OpenTK windows are what's known as "double-buffered". In essence, the window manages two buffers.
        // One is rendered to while the other is currently displayed by the window.
        // This avoids screen tearing, a visual artifact that can happen if the buffer is modified while being displayed.
        // After drawing, call this function to swap the buffers. If you don't, it won't display what you've rendered.
        SwapBuffers();

        // And that's all you have to do for rendering! You should now see a yellow triangle on a black screen.
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        var keyboardInput = KeyboardState;
        var mouseInput = MouseState;

        if (keyboardInput.IsKeyDown(Keys.Escape))
        {
            Close();
        }

        if (keyboardInput.IsKeyDown(Keys.Space))
        {
            _translateMatrix4.Column3 = _translateMatrix4.Column3 with
            {
                Y = (float)(_translateMatrix4.Column3.Y + _moveSpeed * e.Time)
            };
        }

        if (keyboardInput.IsKeyDown(Keys.LeftShift))
        {
            _translateMatrix4.Column3 = _translateMatrix4.Column3 with
            {
                Y = (float)(_translateMatrix4.Column3.Y - _moveSpeed * e.Time)
            };
        }

        if (keyboardInput.IsKeyDown(Keys.W))
        {
            _translateMatrix4.Column3 = _translateMatrix4.Column3 with
            {
                Z = (float)(_translateMatrix4.Column3.Z - _moveSpeed * e.Time)
            };
        }

        if (keyboardInput.IsKeyDown(Keys.W))
        {
            _translateMatrix4.Column3 = _translateMatrix4.Column3 with
            {
                Z = (float)(_translateMatrix4.Column3.Z + _moveSpeed * e.Time)
            };
        }

        if (keyboardInput.IsKeyDown(Keys.A))
        {
            _translateMatrix4.Column3 = _translateMatrix4.Column3 with
            {
                X = (float)(_translateMatrix4.Column3.X - _moveSpeed * e.Time)
            };
        }

        if (keyboardInput.IsKeyDown(Keys.D))
        {
            _translateMatrix4.Column3 = _translateMatrix4.Column3 with
            {
                X = (float)(_translateMatrix4.Column3.X + _moveSpeed * e.Time)
            };
        }

        if (mouseInput.IsButtonDown(MouseButton.Left))
        {
            var xOffset = mouseInput.X - mouseInput.PreviousX;
            var yOffset = mouseInput.Y - mouseInput.PreviousY;

            _rotationMatrix4 = _rotationMatrix4 * GetRotationMatrix(-_radianPerPixel * yOffset, -_radianPerPixel * xOffset, 0);
        }

        if (mouseInput.ScrollDelta != default)
        {
            if (mouseInput.ScrollDelta.Y > 0)
            {
                _scaleMatrix4 *= new Matrix4(
                    1.1f, 0, 0, 0,
                    0, 1.1f, 0, 0,
                    0, 0, 1.1f, 0,
                    0, 0, 0, 1);
            }
            else if (mouseInput.ScrollDelta.Y < 0)
            {
                _scaleMatrix4 *= new Matrix4(
                    0.9f, 0, 0, 0,
                    0, 0.9f, 0, 0,
                    0, 0, 0.9f, 0,
                    0, 0, 0, 1);
            }
        }
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        // When the window gets resized, we have to call GL.Viewport to resize OpenGL's viewport to match the new size.
        // If we don't, the NDC will no longer be correct.
        GL.Viewport(0, 0, Size.X, Size.Y);
        _projectMatrix = Matrix4.CreatePerspectiveFieldOfView(MathF.PI / 2, (float)ClientSize.X / ClientSize.Y, 0.1f, 100f);
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
        // Unbind all the resources by binding the targets to 0/null.
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
        GL.UseProgram(0);

        // Delete all the resources.
        GL.DeleteBuffer(_vertexBufferObject);
        GL.DeleteVertexArray(_vertexArrayObject);

        if (_shader is not null)
        {
            GL.DeleteProgram(_shader.Handle);
        }

        base.OnUnload();
    }
}