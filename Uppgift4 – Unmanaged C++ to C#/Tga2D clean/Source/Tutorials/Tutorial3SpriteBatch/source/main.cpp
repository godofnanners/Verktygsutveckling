#include <string>
#include <tga2d/engine.h>
#include <tga2d/error/error_manager.h>

#include <tga2d/sprite/sprite.h>
#include <tga2d/sprite/sprite_batch.h>
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



// This is where the application starts of for real. By keeping this in it's own file
// we will have the same behaviour for both console and windows startup of the
// application.
//
void Go( void )
{
	TutorialCommon::Init(L"TGA2D: Tutorial 3");
   

	// MAIN LOOP
	Tga2D::CSpriteBatch spriteBatch(true); // When inited to true the batch takes ownership of newed sprites and will delete them
	spriteBatch.Init("sprites/tga_logo.dds");

	for (unsigned int i=0; i< 100000; i++)
	{
		Tga2D::CSprite* sprite = new Tga2D::CSprite(nullptr);

		float randomX = static_cast<float>(rand() % 1000) / 1000.0f;
		float randomY = static_cast<float>(rand() % 1000) / 1000.0f;

		sprite->SetPosition(VECTOR2F(randomX, randomY));
		sprite->SetColor(Tga2D::CColor(randomX, randomY, randomX, 1));
		sprite->SetPivot(VECTOR2F(0.5f, 0.5f));
		sprite->SetSizeRelativeToScreen(VECTOR2F(0.1f, 0.1f));

		spriteBatch.AddObject(sprite); // Add the sprites to the batch
	}
	
	while (true)
	{
		if (!Tga2D::CEngine::GetInstance()->BeginFrame())
		{
			break;
		}

		// Render the image on the screen
		spriteBatch.Render();

		Tga2D::CEngine::GetInstance()->EndFrame();
	}
	Tga2D::CEngine::GetInstance()->Shutdown();
}
