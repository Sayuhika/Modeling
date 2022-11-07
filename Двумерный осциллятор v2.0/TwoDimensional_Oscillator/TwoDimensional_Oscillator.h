
// TwoDimensional_Oscillator.h : главный файл заголовка для приложения PROJECT_NAME
//

#pragma once

#ifndef __AFXWIN_H__
	#error "включить stdafx.h до включения этого файла в PCH"
#endif

#include "resource.h"		// основные символы


// CTwoDimensional_OscillatorApp:
// О реализации данного класса см. TwoDimensional_Oscillator.cpp
//

class CTwoDimensional_OscillatorApp : public CWinApp
{
public:
	CTwoDimensional_OscillatorApp();

// Переопределение
public:
	virtual BOOL InitInstance();

// Реализация

	DECLARE_MESSAGE_MAP()
};

extern CTwoDimensional_OscillatorApp theApp;
