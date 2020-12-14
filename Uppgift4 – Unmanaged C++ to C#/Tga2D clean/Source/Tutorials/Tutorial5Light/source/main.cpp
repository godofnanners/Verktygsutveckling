#include <string>


#include "../../TutorialCommon/TutorialCommon.h"
#include <tga2d/sprite/sprite.h>
#include <tga2d/light/light.h>
#include <tga2d/engine.h>
#include <tga2d/light/light_manager.h>

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
	TutorialCommon::Init(L"TGA2D: Tutorial 5");
   

	// Create the sprite with the path to the image
	Tga2D::CSprite sprite("sprites/tga_logo.dds");
	sprite.SetMap(NORMAL_MAP, "../source/tutorials/Tutorial5Light/data/tga_logo_n.dds");

	// Setting the pivot so all operations will be in the middle of the image (rotation, position, etc.)
	sprite.SetPivot(VECTOR2F(0.5f, 0.5f));
	sprite.SetPosition(VECTOR2F(0.5f, 0.5f));


	const int numberOfLoggos = 50;
	std::vector<VECTOR2F> positions;
	for (int i = 0; i < numberOfLoggos; i++)
	{
		float randX = (float)(rand() % 100) * 0.01f;
		float randY = (float)(rand() % 100) * 0.01f;
		positions.push_back({ randX , randY });
	}

	Tga2D::CEngine::GetInstance()->SetAmbientLightValue(0.1f);
	// LIGHT (Up to 8)

	srand((unsigned int)time(0));

#define RANOM_0_TO_1 (rand() % 100 / 100.0f)
#define RANOM_1_OR_0 (float)(rand() % 2)
	std::vector<Tga2D::CLight*> myLights;
	for (int i=0; i< NUMBER_OF_LIGHTS_ALLOWED; i++)
	{
		Tga2D::CLight* light = new Tga2D::CLight();
		light->myFallOff = 0.2f;
		light->myIntensity = 0.1f + (RANOM_0_TO_1+RANOM_0_TO_1);
		light->myPosition.Set(RANOM_0_TO_1, RANOM_0_TO_1);
		light->myColor.Set(RANOM_1_OR_0, RANOM_1_OR_0, RANOM_1_OR_0, 1);
		myLights.push_back(light);
	}

	


	float timer = 0;
	while (true)
	{
		timer += 1.0f / 60.0f;
		if (!Tga2D::CEngine::GetInstance()->BeginFrame())
		{
			break;
		}

		for (Tga2D::CLight* light : myLights)
		{
			light->Render();
		}
		sprite.Render();


		// Render all the loggos onto the sprite
		for (int i = 0; i < numberOfLoggos; i++)
		{
			sprite.SetPosition({ positions[i].x, positions[i].y });
			sprite.SetRotation(static_cast<float>(cos(timer * 0.5f * sin(positions[i].x))));
			sprite.Render();
		}

	

		Tga2D::CEngine::GetInstance()->EndFrame();
	}
	Tga2D::CEngine::GetInstance()->Shutdown();
}
