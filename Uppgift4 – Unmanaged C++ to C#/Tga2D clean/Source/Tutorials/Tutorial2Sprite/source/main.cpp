#include <string>
#include <tga2d/engine.h>
#include <tga2d/error/error_manager.h>

#include <tga2d/sprite/sprite.h>
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
	TutorialCommon::Init(L"TGA2D: Tutorial 2");
   
	// Create the sprite with the path to the image
	Tga2D::CSprite sprite("sprites/tga_logo.dds");

	// Setting the pivot so all operations will be in the middle of the image (rotation, position, etc.)
	sprite.SetPivot(VECTOR2F(0.5f, 0.5f));

	float timer = 0;
	while (true)
	{
		timer += 1.0f / 60.0f;
		if (!Tga2D::CEngine::GetInstance()->BeginFrame())
		{
			break;
		}

		// Set a new position
		sprite.SetPosition(VECTOR2F( (cosf(timer) + 1) / 2, (sinf(timer) + 1) / 2));
		// Set the rotation
		sprite.SetRotation(cosf(timer));

		// Render the image on the screen
		sprite.Render();

		// using the same instance we reuse the image and set a new position
		sprite.SetPosition(VECTOR2F((sinf(timer) + 1) / 2, (cosf(timer) + 1) / 2));

		// Render it a second time at a new position
		sprite.Render();

		Tga2D::CEngine::GetInstance()->EndFrame();
	}
	Tga2D::CEngine::GetInstance()->Shutdown();

}
