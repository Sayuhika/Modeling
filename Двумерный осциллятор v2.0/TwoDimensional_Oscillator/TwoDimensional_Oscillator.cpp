
// TwoDimensional_Oscillator.cpp : ?????????? ????????? ??????? ??? ??????????.
//

#include "stdafx.h"
#include "TwoDimensional_Oscillator.h"
#include "TwoDimensional_OscillatorDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CTwoDimensional_OscillatorApp

BEGIN_MESSAGE_MAP(CTwoDimensional_OscillatorApp, CWinApp)
	ON_COMMAND(ID_HELP, &CWinApp::OnHelp)
END_MESSAGE_MAP()


// ???????? CTwoDimensional_OscillatorApp

CTwoDimensional_OscillatorApp::CTwoDimensional_OscillatorApp()
{
	// ????????? ?????????? ????????????
	m_dwRestartManagerSupportFlags = AFX_RESTART_MANAGER_SUPPORT_RESTART;

	// TODO: ???????? ??? ????????,
	// ????????? ???? ?????? ??? ????????????? ? InitInstance
}


// ???????????? ?????? CTwoDimensional_OscillatorApp

CTwoDimensional_OscillatorApp theApp;


// ????????????? CTwoDimensional_OscillatorApp

BOOL CTwoDimensional_OscillatorApp::InitInstance()
{
	// InitCommonControlsEx() ????????? ??? Windows XP, ???? ????????
	// ?????????? ?????????? ComCtl32.dll ?????? 6 ??? ????? ??????? ?????? ??? ?????????
	// ?????? ???????????.  ? ????????? ?????? ????? ????????? ???? ??? ???????? ?????? ????.
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	// ???????? ???? ???????? ??? ????????? ???? ????? ??????? ??????????, ??????? ?????????? ????????????
	// ? ????? ??????????.
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);

	CWinApp::InitInstance();


	AfxEnableControlContainer();

	// ??????? ????????? ????????, ? ??????, ???? ?????????? ???? ????????
	// ????????????? ?????? ???????? ??? ?????-???? ??? ???????? ??????????.
	CShellManager *pShellManager = new CShellManager;

	// ????????? ??????????? ?????????? "???????????? Windows" ??? ????????? ????????? ?????????? MFC
	CMFCVisualManager::SetDefaultManager(RUNTIME_CLASS(CMFCVisualManagerWindows));

	// ??????????? ?????????????
	// ???? ??? ??????????? ?? ???????????? ? ?????????? ????????? ??????
	// ????????? ???????????? ?????, ?????????? ??????? ?? ?????????
	// ?????????? ???????? ?????????????, ??????? ?? ?????????
	// ???????? ?????? ???????, ? ??????? ???????? ?????????
	// TODO: ??????? ???????? ??? ?????? ?? ???-?????? ??????????,
	// ???????? ?? ???????? ???????????
	SetRegistryKey(_T("????????? ??????????, ????????? ? ??????? ??????? ??????????"));

	CTwoDimensional_OscillatorDlg dlg;
	m_pMainWnd = &dlg;
	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: ??????? ??? ??? ????????? ???????? ??????????? ????
		//  ? ??????? ?????? "??"
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: ??????? ??? ??? ????????? ???????? ??????????? ????
		//  ? ??????? ?????? "??????"
	}
	else if (nResponse == -1)
	{
		TRACE(traceAppMsg, 0, "??????????????. ?? ??????? ??????? ?????????? ????, ??????? ?????? ?????????? ?????????? ?????????.\n");
		TRACE(traceAppMsg, 0, "??????????????. ??? ????????????? ????????? ?????????? MFC ??? ??????????? ???? ?????????? #define _AFX_NO_MFC_CONTROLS_IN_DIALOGS.\n");
	}

	// ??????? ????????? ????????, ????????? ????.
	if (pShellManager != NULL)
	{
		delete pShellManager;
	}

#if !defined(_AFXDLL) && !defined(_AFX_NO_MFC_CONTROLS_IN_DIALOGS)
	ControlBarCleanUp();
#endif

	// ????????? ?????????? ???? ???????, ?????????? ???????? FALSE, ????? ????? ???? ????? ??
	//  ?????????? ?????? ??????? ?????????? ????????? ??????????.
	return FALSE;
}

