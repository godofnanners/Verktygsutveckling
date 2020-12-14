#include <string>

#include <tga2d/drawers/debug_drawer.h>
#include <tga2d/math/vector2.h>
#include <tga2d/engine.h>
#include "../../TutorialCommon/TutorialCommon.h"
#include <tga2d/sprite/sprite.h>


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
	TutorialCommon::Init(L"TGA2D: Tutorial 9");
   
	Tga2D::CSprite mySprite("../source/tutorials/Tutorial9Spritesheets/animation.dds");
	mySprite.SetSizeRelativeToImage({ 0.5f, 0.5f }); // We have 4 images wide, then set the image to half the size
	mySprite.SetPosition({ 0.5f, 0.5f });
	mySprite.SetPivot({ 0.5f, 0.5f });

	struct UV
	{
		UV(VECTOR2F aStart, VECTOR2F aEnd) { myStart = aStart; myEnd = aEnd; }
		VECTOR2F myStart;
		VECTOR2F myEnd;
	};

	const float addingUVX = 1.0f / 8.0f; // 8 sprites per row
	const float addingUVY = 1.0f / 8.0f; // 8 sprites per col
	std::vector<UV> myUvs;
	for (int j=0; j< 8; j++)
	{
		for (int i = 0; i < 8; i++)
		{
			myUvs.push_back(UV({ addingUVX * i, addingUVY * j }, { (addingUVX * i) + addingUVX, (addingUVY * j) + addingUVY }));
		}
	}

	const float deltaTime = 1.0f / 60.0f;
	float timer = 0.0f;
	unsigned short aIndex = 0;
	while (true)
	{
		timer += deltaTime;
		if (!Tga2D::CEngine::GetInstance()->BeginFrame())
		{
			break;
		}

		// Cycle the sheet
		if (timer >= 0.05f)
		{
			aIndex++;
			if (aIndex > 28)
			{
				aIndex = 0;
			}
			timer = 0.0f;
		}


		mySprite.SetTextureRect(myUvs[aIndex].myStart.x, myUvs[aIndex].myStart.y, myUvs[aIndex].myEnd.x, myUvs[aIndex].myEnd.y);
		mySprite.Render();

		Tga2D::CEngine::GetInstance()->EndFrame();
	}
	Tga2D::CEngine::GetInstance()->Shutdown();
}
