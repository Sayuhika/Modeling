
// TwoDimensional_OscillatorDlg.h : файл заголовка
//

#pragma once

#include "GlobalDrawer.h"

// диалоговое окно CTwoDimensional_OscillatorDlg
class CTwoDimensional_OscillatorDlg : public CDialogEx
{
	// Создание
public:
	CTwoDimensional_OscillatorDlg(CWnd* pParent = NULL);	// стандартный конструктор

															// Данные диалогового окна
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_TWODIMENSIONAL_OSCILLATOR_DIALOG };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// поддержка DDX/DDV


														// Реализация
protected:
	HICON m_hIcon;

	// Созданные функции схемы сообщений
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	double x;
	double y;
	double Vx;
	double Vy;
	double k1;
	double k2;
	double k3;
	double k4;
	double mass;
	double l;
	double T;
	double delta_t;
	afx_msg void OnBnClickedButton1();

	Global_Drawer Main_Drawer;
	Global_Drawer XY_Drawer;
	Global_Drawer Energy_Drawer;
	Global_Drawer ArcSin_X_Drawer;
};
