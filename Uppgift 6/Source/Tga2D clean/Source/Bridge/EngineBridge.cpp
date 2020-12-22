#include "pch.h"
#include "EngineBridge.h"
#include <Windows.h>
#include"..\Game\Game.h"
EngineBridge::EngineBridge()
{
    myGame = nullptr;
}

void EngineBridge::Init(System::IntPtr aHWND, int aWidth, int aHeight)
{
    myGame = new CGame();
    myGame->Init(L"Editor", static_cast<HWND>(aHWND.ToPointer()), aWidth, aHeight);
}

void EngineBridge::TextureScale(float aScale)
{
    myGame->ScaleTextures(aScale);
}

void EngineBridge::ShutDown()
{
    myGame->ShutDown();
}
