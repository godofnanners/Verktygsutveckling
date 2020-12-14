#include <string>
#include <tga2d/engine.h>
#include <tga2d/error/error_manager.h>
#include <tga2d/texture/texture_manager.h>
#include <tga2d/sprite/sprite.h>
#include <tga2d/shaders/customshader.h>
#include "../../TutorialCommon/TutorialCommon.h"

// Making sure that DX2DEngine lib is linked
//
#ifdef _DEBUG
#pragma comment(lib,"TGA2D_Debug.lib")
#elif NDEBUG
#pragma comment(lib,"TGA2D_Release.lib")
#endif // _DEBUG



void Go( void );
int main( const int /*argc*/, const char * /*argc*/[] )
{
    Go();
    return 0;
}



void Go( void )
{
	TutorialCommon::Init(L"TGA2D: Tutorial 10 - Shaders from shadertoy.com", true);
   

	// Create the sprite WITHOU a image!
	Tga2D::CSprite sprite;
	Tga2D::CSprite sprite2;
	Tga2D::CSprite sprite3;

	// Setting the pivot so all operations will be in the middle of the image (rotation, position, etc.)
	sprite.SetPivot(VECTOR2F(0.5f, 0.5f));
	sprite2.SetPivot(VECTOR2F(0.5f, 0.5f));
	sprite3.SetPivot(VECTOR2F(0.5f, 0.5f));

	/////////// Custom shader

	// Create a new shader
	Tga2D::CCustomShader customShader; // Create
	customShader.SetShaderdataFloat4(Tga2D::Vector4f(1, 0, 0, 1), Tga2D::EShaderDataID_1); // Add some data to it
	customShader.SetTextureAtRegister(Tga2D::CEngine::GetInstance()->GetTextureManager().GetTexture("sprites/tga_logo.dds"), Tga2D::EShaderTextureSlot_1); // Add a texture

	// Run PostInit to set all the data
	customShader.Init("../source/tutorials/Tutorial10Customshader/custom_sprite_vertex_shader.fx", "../source/tutorials/Tutorial10Customshader//custom_sprite_pixel_shader.fx");

	// Tell the sprite to use this shader
	sprite.SetCustomShader(&customShader);
	sprite.SetPosition(VECTOR2F(0.3f , 0.5f));
	sprite.SetSizeRelativeToScreen({ 0.3f, 0.3f });


	// Second sprite
	Tga2D::CCustomShader customShader2; // Create
	customShader2.SetShaderdataFloat4(Tga2D::Vector4f(1, 0, 1, 1), Tga2D::EShaderDataID_1); // Add some data to it
	customShader2.SetTextureAtRegister(Tga2D::CEngine::GetInstance()->GetTextureManager().GetTexture("sprites/tga_logo.dds"), Tga2D::EShaderTextureSlot_1); // Add a texture																																				   // Run PostInit to set all the data
	customShader2.Init("../source/tutorials/Tutorial10Customshader/custom_sprite_vertex_shader_2.fx", "../source/tutorials/Tutorial10Customshader//custom_sprite_pixel_shader_2.fx");

	// Tell the sprite to use this shader
	sprite2.SetCustomShader(&customShader2);
	sprite2.SetPosition(VECTOR2F(0.5f, 0.5f));
	sprite2.SetSizeRelativeToScreen({ 0.3f, 0.3f });

	// Third
	Tga2D::CCustomShader customShader3; // Create
	customShader3.SetShaderdataFloat4(Tga2D::Vector4f(1, 0, 1, 1), Tga2D::EShaderDataID_1); // Add some data to it
	customShader3.SetTextureAtRegister(Tga2D::CEngine::GetInstance()->GetTextureManager().GetTexture("sprites/tga_logo.dds"), Tga2D::EShaderTextureSlot_1); // Add a texture																																				   // Run PostInit to set all the data
	customShader3.Init("../source/tutorials/Tutorial10Customshader/custom_sprite_vertex_shader_3.fx", "../source/tutorials/Tutorial10Customshader//custom_sprite_pixel_shader_3.fx");

	// Tell the sprite to use this shader
	sprite3.SetCustomShader(&customShader3);
	sprite3.SetPosition(VECTOR2F(0.7f, 0.5f));
	sprite3.SetSizeRelativeToScreen({ 0.3f, 0.3f });

	float timer = 0;
	while (true)
	{
		timer += 1.0f / 60.0f;
		if (!Tga2D::CEngine::GetInstance()->BeginFrame())
		{
			break;
		}


		sprite.Render();
		sprite2.Render();
		sprite3.Render();

		Tga2D::CEngine::GetInstance()->EndFrame();
	}
	Tga2D::CEngine::GetInstance()->Shutdown();
}
