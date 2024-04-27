﻿using LearnOpenGL;
using OpenGaming;
using OpenGaming.Meshes;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

var nativeWindowSettings = new NativeWindowSettings()
{
    ClientSize = new Vector2i(800, 600),
    Title = "Learn OpenGL"
};

using (var window = new AppWindow(GameWindowSettings.Default, nativeWindowSettings))
{

    var cubeMesh = CubeMesh.Create();
    window.Run();
}
