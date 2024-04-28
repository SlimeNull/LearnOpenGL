#version 330 core

#define MAX_LIGHT_COUNT 10

struct spot_light
{
    vec3 position;
    vec3 direction;
    vec3 range;
    vec3 color;
};

struct directional_light
{
    vec3 position;
    vec3 direction;
    vec3 color;
};

struct point_light
{
    vec3 position;
    vec3 color;
};

in vec3 vertexWorldPosition;
in vec3 vertexWorldNormal;
in vec2 vertexUV;
in vec3 vertexColor;

uniform float ambient = 0.1;
uniform float diffuse = 0.1;
uniform float specular = 0.5;
uniform float ambientColor;

uniform sampler2D colorTexture;
uniform sampler2D ambientTexture;
uniform sampler2D diffuseTexture;
uniform sampler2D specularTexture;

uniform vec3 viewPosition;

uniform int spotLightCount = 0;
uniform int directionalLightCount = 0;
uniform int pointLightCount = 0;
uniform spot_light spotLights[MAX_LIGHT_COUNT];
uniform directional_light directionalLights[MAX_LIGHT_COUNT];
uniform point_light pointLights[MAX_LIGHT_COUNT];

out vec4 fragmentColor;

void main()
{
    vec3 color = vertexColor;
    vec3 ambientColor = color * ambient;
    vec3 diffuseColor = vec3(0, 0, 0);
    vec3 specularColor = vec3(0, 0, 0);

    for (int i = 0; i < pointLightCount; i++)
    {
        vec3 lightDir = normalize(pointLights[i].position - vertexWorldPosition);
        vec3 viewDir = normalize(viewPosition - vertexWorldPosition);
        vec3 reflectDir = reflect(-lightDir, vertexWorldNormal);

        float diff = max(dot(vertexWorldNormal, lightDir), 0.0);
        float spec = pow(max(dot(viewDir, reflectDir), 0), 32);
        
        diffuseColor += pointLights[i].color * diff * diffuse;
        specularColor += pointLights[i].color * spec * specular;
    }

    vec4 finalColor = vec4(ambientColor + diffuseColor + specularColor, 1);

    fragmentColor = finalColor;
}