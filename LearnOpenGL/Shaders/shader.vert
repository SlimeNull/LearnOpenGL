#version 330 core
layout (location = 0) in vec3 position0;
layout (location = 1) in vec2 uv0;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform mat4 transform;
out vec2 uvOutput0;

void main()
{
    gl_Position = projection * view * model * transform * vec4(position0, 1.0);
    uvOutput0 = uv0;
}