#include "stdafx.h"
#include "GameWorld.h"
#include <tga2d/sprite/sprite.h>
#include<tga2d/sprite/sprite_batch.h>

CGameWorld::CGameWorld()
{
	myTga2dLogoSprite = nullptr;
}

CGameWorld::~CGameWorld() 
{
	delete myTga2dLogoSprite;
	myTga2dLogoSprite = nullptr;
	myLineSpriteBatch->ClearAll();
	delete myLineSpriteBatch;
	myLineSpriteBatch = nullptr;
}

void CGameWorld::Init()  
{
	myLineSpriteBatch= new Tga2D::CSpriteBatch(true); // When inited to true the batch takes ownership of newed sprites and will delete them
	myLineSpriteBatch->Init("sprites/Circle.dds");
}



void CGameWorld::Update(float /*aTimeDelta*/)
{ 	
}

void CGameWorld::Render()
{
	myLineSpriteBatch->Render();
}

void CGameWorld::SetSpritePosition(float aXPosition,float aYPosition)
{
		
		Tga2D::CSprite* sprite = new Tga2D::CSprite(nullptr);

		sprite->SetPosition(VECTOR2F(aXPosition, aYPosition));
		sprite->SetColor(Tga2D::CColor(1, 1, 1, 1));
		sprite->SetPivot(VECTOR2F(0.5f, 0.5f));
		sprite->SetSizeRelativeToImage(VECTOR2F(0.1f, 0.1f));

		myLineSpriteBatch->AddObject(sprite); // Add the sprites to the batch
}

void CGameWorld::ClearSprites()
{
	myLineSpriteBatch->ClearAll();
}

void CGameWorld::SetSprite(std::string aSpritePath)
{
	myLineSpriteBatch->ClearAll();
	myLineSpriteBatch = new Tga2D::CSpriteBatch(true); // When inited to true the batch takes ownership of newed sprites and will delete them
	myLineSpriteBatch->Init(aSpritePath.c_str());

	Tga2D::CSprite* sprite = new Tga2D::CSprite(nullptr);

	
}
