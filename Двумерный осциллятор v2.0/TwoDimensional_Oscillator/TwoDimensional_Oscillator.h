
// TwoDimensional_Oscillator.h : ������� ���� ��������� ��� ���������� PROJECT_NAME
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�������� stdafx.h �� ��������� ����� ����� � PCH"
#endif

#include "resource.h"		// �������� �������


// CTwoDimensional_OscillatorApp:
// � ���������� ������� ������ ��. TwoDimensional_Oscillator.cpp
//

class CTwoDimensional_OscillatorApp : public CWinApp
{
public:
	CTwoDimensional_OscillatorApp();

// ���������������
public:
	virtual BOOL InitInstance();

// ����������

	DECLARE_MESSAGE_MAP()
};

extern CTwoDimensional_OscillatorApp theApp;
