#pragma once

namespace Tga2D
{
	class CSprite;
	class FBXModel;
	class CAudio;
	class CSpriteBatch;
}

class CGameWorld
{
public:
	CGameWorld(); 
	~CGameWorld();

	void Init();
	void Update(float aTimeDelta); 
	void Render();
	void SetSprite(std::string aSpritePath);
	void SetSpritePosition(float aXPositions, float aYPositions);
	void ClearSprites();
private:
	Tga2D::CSprite* myTga2dLogoSprite;
	Tga2D::CSpriteBatch* myLineSpriteBatch;
};