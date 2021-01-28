#pragma once
class CGame;

public ref class EngineBridge
{
public:
	EngineBridge();
	void Init(System::IntPtr aHWND,int aWidth, int aHeight);
	void SetSpritePath(System::String^ aPath);
	void SetSpritePosition(float aXPositions, float aYPositions);
	void ClearSprites();
	void ShutDown();
private:
	CGame* myGame;
};

