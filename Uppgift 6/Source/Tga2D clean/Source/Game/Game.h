#pragma once
#include <fstream>
#include "GameWorld.h"
class CGameWorld;

class CGame
{
	
public:
	CGame();
	~CGame();
	bool Init(const std::wstring& aVersion = L"" );
	bool Init(const std::wstring& aVersion, HWND aHWND, int aWidth = 1280, int aHeight = 720);
	void InitCallBack();
	void UpdateCallBack();
	void ScaleTextures(float aScale);
	void ShutDown();
private:
	LRESULT WinProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);

	CGameWorld * myGameWorld;

};