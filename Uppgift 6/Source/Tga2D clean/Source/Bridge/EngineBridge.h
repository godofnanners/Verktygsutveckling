#pragma once
class CGame;
public ref class EngineBridge
{
public:
	EngineBridge();
	void Init(System::IntPtr aHWND,int aWidth, int aHeight);
	void TextureScale(float aScale);
	void ShutDown();
private:
	CGame* myGame;
};

