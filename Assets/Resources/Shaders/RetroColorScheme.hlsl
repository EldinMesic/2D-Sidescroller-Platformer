//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED

#define H 255

void RetroColor_float(in float Scale, in float3 Color, out float Out)
{
    Out = (0.2126 * Color.r) + (0.7152 * Color.g) + (0.0722 * Color.b);
    Out *= Scale;
}
#endif //MYHLSLINCLUDE_INCLUDED