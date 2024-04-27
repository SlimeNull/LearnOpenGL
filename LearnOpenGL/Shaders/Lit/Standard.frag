#version 330 core

in vec3 vertexWorldPosition;
in vec3 vertexNormal;
in vec2 vertexUV;
in vec3 vertexColor;

uniform float ambient;
uniform float diffuse;
uniform float specular;

uniform sampler2D colorTexture;
uniform sampler2D ambientTexture;
uniform sampler2D diffuseTexture;
uniform sampler2D specularTexture;

out vec4 fragmentColor;

void main()
{
    vec4 color = vec4(vertexColor, 1);
    vec4 ambientColor = color * ambient;
}