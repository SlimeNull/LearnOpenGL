#version 330 core

layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 uv;

uniform mat4 modelMatrix;
uniform mat4 viewMatrix;
uniform mat4 projectionMatrix;

uniform vec3 color;
uniform sampler2D displacementTexture;

out vec3 vertexWorldPosition;
out vec3 vertexNormal;
out vec2 vertexUV;
out vec3 vertexColor;

void main()
{
    vec4 vertexPosition = vec4(position, 1);
    vec4 finalPosition = projectionMatrix * viewMatrix * modelMatrix * vertexPosition;

    gl_Position = finalPosition;

    vertexWorldPosition = vec3(modelMatrix * vec4(position, 1));
    vertexNormal = normal;
    vertexUV = uv;
    vertexColor = color;
}