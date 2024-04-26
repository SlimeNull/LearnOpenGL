#version 330 core
layout (location = 0) in vec3 position0;
layout (location = 1) in vec2 uv0;
out vec2 uvOutput0;

void main()
{
    gl_Position = vec4(position0, 1.0);
    uvOutput0 = uv0;
}