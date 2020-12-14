#include <string>


#include "../../TutorialCommon/TutorialCommon.h"
#include <tga2d/sprite/sprite.h>
#include <tga2d/light/light.h>
#include <tga2d/engine.h>
#include <tga2d/light/light_manager.h>
#include <tga2d/primitives/custom_shape.h>
#include "tga2d/shaders/customshader.h"
#include <tga2d/texture/texture_manager.h>
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
	TutorialCommon::Init(L"TGA2D: Tutorial 7");

	// Background, not needed, but beatiful
	Tga2D::CCustomShape myShape;
	myShape.Reset();
	myShape.AddPoint({ 0.0f, 0.0f }, Tga2D::CColor(1, 0, 0, 1));
	myShape.AddPoint({ 1.0f, 0.0f }, Tga2D::CColor(0, 1, 0, 1));
	myShape.AddPoint({ 0.0f, 1.0f }, Tga2D::CColor(0, 0, 1, 1));

	myShape.AddPoint({ 1.0f, 0.0f }, Tga2D::CColor(0, 1, 0, 1));
	myShape.AddPoint({ 0.0f, 1.0f }, Tga2D::CColor(0, 0, 1, 1));
	myShape.AddPoint({ 1.0f, 1.0f }, Tga2D::CColor(0, 0, 1, 1));
	myShape.BuildShape();


	//The target texture we will render to instead of the screen, this is a sprite, which means it have all the nice features of one (rotation, position etc.)
	Tga2D::CSprite myRenderTargetTexture;
	myRenderTargetTexture.SetSizeRelativeToScreen({ 1, 1 }); // Make it fullscreen

	// Ordinary sprite that we will render onto the target
	Tga2D::CSprite myTga2dLogoSprite("sprites/tga_logo.dds");
	myTga2dLogoSprite.SetPivot({ 0.5f, 0.5f });
	myTga2dLogoSprite.SetPosition({ 0.5f, 0.5f });
	myTga2dLogoSprite.SetSizeRelativeToImage({ 0.5f,0.5f });


	// Create a new shader to showcase the fullscreen shader with.
	Tga2D::CCustomShader customShader; // Create	
	customShader.Init("../source/tutorials/Tutorial7RenderTarget/custom_sprite_vertex_shader.fx", "../source/tutorials/Tutorial7RenderTarget//custom_sprite_pixel_shader.fx");

	// Tell the target sprite to use this shader
	myRenderTargetTexture.SetCustomShader(&customShader);


	// We need a couple of tga loggos!
	const int numberOfLoggos = 20;
	std::vector<VECTOR2F> positions;
	for (int i = 0; i < numberOfLoggos; i++)
	{
		float randX = (float)(rand() % 100) * 0.01f;
		float randY = (float)(rand() % 100) * 0.01f;
		positions.push_back({ randX , randY });
	}

	float timer = 0;
	while (true)
	{
		timer += 1.0f / 60.0f;
		if (!Tga2D::CEngine::GetInstance()->BeginFrame(Tga2D::CColor(0, 0, 0.7f, 1)))
		{
			break;
		}

		// Render background
		myShape.Render();

		// Set the new sprite as a target instead of the screen
		Tga2D::CEngine::GetInstance()->SetRenderTarget(&myRenderTargetTexture);
		// Render all the loggos onto the sprite
		for (int i=0; i< numberOfLoggos; i++)
		{
			myTga2dLogoSprite.SetPosition({ positions[i].x, positions[i].y });
			myTga2dLogoSprite.Render();
		}


		// Set the target back to the screen
		Tga2D::CEngine::GetInstance()->SetRenderTarget(nullptr);


		// Render the target sprite which hols a lot of loggos!
		myRenderTargetTexture.SetPosition(VECTOR2F(0.5f, 0.5f));
		myRenderTargetTexture.SetPivot(VECTOR2F(0.5f, 0.5f));
		myRenderTargetTexture.SetSizeRelativeToScreen(VECTOR2F(0.7f, 0.7f));
		myRenderTargetTexture.SetRotation(cosf(timer) * 0.1f);
		myRenderTargetTexture.Render();

		
		Tga2D::CEngine::GetInstance()->EndFrame();
	}
	Tga2D::CEngine::GetInstance()->Shutdown();
}
