#include "pch.h"
#include "EngineBridge.h"
#include <Windows.h>
#include <string>
#include"..\Game\Game.h"
#include <msclr/marshal.h>

EngineBridge::EngineBridge()
{
    myGame = nullptr;
}

void EngineBridge::Init(System::IntPtr aHWND, int aWidth, int aHeight)
{
    myGame = new CGame();
    myGame->Init(L"Editor", static_cast<HWND>(aHWND.ToPointer()), aWidth, aHeight);
}

void EngineBridge::SetSpritePath(System::String^ aPath)
{
    msclr::interop::marshal_context oMarshalContext;

    const char* pParameter = oMarshalContext.marshal_as<const char*>(aPath);
    myGame->SetSpritePath(pParameter);
}

void EngineBridge::SetSpritePosition(float aXPosition, float aYPosition)
{
    myGame->SetSpritePosition(aXPosition, aYPosition);
}

void EngineBridge::ClearSprites()
{
    myGame->ClearSprites();
}

void EngineBridge::ShutDown()
{
    myGame->ShutDown();
}
