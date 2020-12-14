#pragma once
#include <tga2d/engine.h>
#include <tga2d/error/error_manager.h>

namespace TutorialCommon
{
	void Init(std::wstring aAppName, bool aFullDebug = false, bool aVsyncEnable = true)
	{
		unsigned short windowWidth = 1920;
		unsigned short windowHeight = 1080;


		Tga2D::SEngineCreateParameters createParameters;
		if (aFullDebug)
		{
			createParameters.myActivateDebugSystems = Tga2D::eDebugFeature_Fps | Tga2D::eDebugFeature_Mem | Tga2D::eDebugFeature_Filewatcher | Tga2D::eDebugFeature_Cpu | Tga2D::eDebugFeature_Drawcalls | Tga2D::eDebugFeature_OptimiceWarnings;

		}
		else
		{
			createParameters.myActivateDebugSystems = Tga2D::eDebugFeature_Filewatcher;

		}

		createParameters.myWindowHeight = windowHeight;
		createParameters.myWindowWidth = windowWidth;
		createParameters.myRenderHeight = windowHeight;
		createParameters.myRenderWidth = windowWidth;
		createParameters.myTargetWidth = windowWidth;
		createParameters.myTargetHeight = windowHeight;
		createParameters.myWindowSetting = Tga2D::EWindowSetting_Overlapped;
		//createParameters.myAutoUpdateViewportWithWindow = true;
		createParameters.myStartInFullScreen = false;
		createParameters.myPreferedMultiSamplingQuality = Tga2D::EMultiSamplingQuality_High;

		createParameters.myApplicationName = aAppName;
		createParameters.myEnableVSync = aVsyncEnable;

		if (!Tga2D::CEngine::GetInstance()->Start(createParameters))
		{
			ERROR_PRINT("Fatal error! Engine could not start!");
			system("pause");
	
		}
	}
}