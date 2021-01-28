#include "stdafx.h"
#include <tga2d/Engine.h>
#include "Game.h"

#include <tga2d/error/error_manager.h>

using namespace std::placeholders;

#ifdef _DEBUG
#pragma comment(lib,"TGA2D_Debug.lib")
std::wstring BUILD_NAME = L"Debug";
#endif // DEBUG
#ifdef _RELEASE
#pragma comment(lib,"TGA2D_Release.lib")
std::wstring BUILD_NAME = L"Release";
#endif // DEBUG
#ifdef _RETAIL
#pragma comment(lib,"TGA2D_Retail.lib")
std::wstring BUILD_NAME = L"Retail";
#endif // DEBUG

CGame::CGame()
{
	myGameWorld = new CGameWorld();
}


CGame::~CGame()
{
}

LRESULT CGame::WinProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	lParam;
	wParam;
	hWnd;
	switch (message)
	{
		// this message is read when the window is closed
	case WM_DESTROY:
	{
		// close the application entirely
		PostQuitMessage(0);
		return 0;
	}
	}

	return 0;
}


bool CGame::Init(const std::wstring& aVersion)
{
	Tga2D::SEngineCreateParameters createParameters;

	createParameters.myInitFunctionToCall = [this] {InitCallBack(); };
	createParameters.myWinProcCallback = [this](HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {return WinProc(hWnd, message, wParam, lParam); };
	createParameters.myUpdateFunctionToCall = [this] {UpdateCallBack(); };
	createParameters.myApplicationName = L"TGA 2D " + BUILD_NAME + L"[" + aVersion + L"] ";
	//createParameters.myPreferedMultiSamplingQuality = Tga2D::EMultiSamplingQuality_High;
	createParameters.myActivateDebugSystems = Tga2D::eDebugFeature_Fps |
		Tga2D::eDebugFeature_Mem |
		Tga2D::eDebugFeature_Drawcalls |
		Tga2D::eDebugFeature_Cpu |
		Tga2D::eDebugFeature_Filewatcher |
		Tga2D::eDebugFeature_OptimiceWarnings;

	if (!Tga2D::CEngine::Start(createParameters))
	{
		ERROR_PRINT("Fatal error! Engine could not start!");
		system("pause");
		return false;
	}

	// End of program
	return true;
}

bool CGame::Init(const std::wstring& aVersion, HWND aHWND, int aWidth, int aHeight)
{
	Tga2D::SEngineCreateParameters createParameters;

	createParameters.myHwnd = &aHWND;
	createParameters.myInitFunctionToCall = [this] { InitCallBack(); };
	createParameters.myWinProcCallback = [this](HWND aHWND, UINT message, WPARAM wParam, LPARAM lParam) { return WinProc(aHWND, message, wParam, lParam); };
	createParameters.myUpdateFunctionToCall = [this] { UpdateCallBack(); };
	createParameters.myApplicationName = L"TGA 2D " + BUILD_NAME + L"[" + aVersion + L"] ";
	createParameters.myRenderWidth = aWidth;
	createParameters.myRenderHeight = aHeight;
	createParameters.myTargetWidth = aWidth;
	createParameters.myTargetHeight = aHeight;
	createParameters.myWindowWidth = aWidth;
	createParameters.myWindowHeight = aHeight;
	createParameters.myActivateDebugSystems = Tga2D::eDebugFeature_Fps |
		Tga2D::eDebugFeature_Mem |
		Tga2D::eDebugFeature_Drawcalls |
		Tga2D::eDebugFeature_Cpu |
		Tga2D::eDebugFeature_Filewatcher |
		Tga2D::eDebugFeature_OptimiceWarnings;

	if (!Tga2D::CEngine::Start(createParameters))
	{
		ERROR_PRINT("Fatal error! Engine could not start!");
		system("pause");
		return false;
	}

	// End of program
	return true;
}

void CGame::ShutDown()
{
	delete myGameWorld;
	Tga2D::CEngine::GetInstance()->Shutdown();
}

void CGame::InitCallBack()
{
	myGameWorld->Init();
}

void CGame::UpdateCallBack()
{
	myGameWorld->Update(Tga2D::CEngine::GetInstance()->GetDeltaTime());
	myGameWorld->Render();
}

void CGame::SetSpritePath(std::string aPath)
{
	myGameWorld->SetSprite(aPath);
}

void CGame::SetSpritePosition(float aXPosition, float aYPosition)
{
	myGameWorld->SetSpritePosition(aXPosition, aYPosition);
}

void CGame::ClearSprites()
{
	myGameWorld->ClearSprites();
}
