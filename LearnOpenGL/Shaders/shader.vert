#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aColor;
uniform float time;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
out vec3 Color;

#define M_PI 3.1415926535897932384626433832795

void main()
{
    float lightness = (sin(mod(time, M_PI * 2)) + 1) / 2;
    float magnitude = sqrt(aPosition.x * aPosition.x + aPosition.y * aPosition.y);
    float radian = atan(aPosition.y, aPosition.x);
    radian = mod(radian + time, M_PI * 2);
    magnitude += lightness / 5;
    
    vec3 newPosition = vec3(
        cos(radian) * magnitude,
        sin(radian) * magnitude,
        aPosition.z);

    vec3 outputColor = vec3(
        aColor.x * lightness,
        aColor.y * lightness,
        aColor.z * lightness);

    gl_Position = projection * view * model * vec4(newPosition, 1.0);
    Color = outputColor;
}