
// TwoDimensional_OscillatorDlg.cpp : ���� ����������
//

#include <cmath>
#include "stdafx.h"
#include "TwoDimensional_Oscillator.h"
#include "TwoDimensional_OscillatorDlg.h"
#include "afxdialogex.h"
#include "functions.h"
#include "GlobalDrawer.h"
#include <fstream> 
#include <iomanip>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// ��������� ����������� ��������
vector<double> x_data, y_data, arcsin_x_data, time_data, length_data, RK4_data, E_k_data, E_p_data, E_max_data;

// ���������� ���� CAboutDlg ������������ ��� �������� �������� � ����������

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// ������ ����������� ����
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_ABOUTBOX };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // ��������� DDX/DDV

// ����������
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(IDD_ABOUTBOX)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// ���������� ���� CTwoDimensional_OscillatorDlg



CTwoDimensional_OscillatorDlg::CTwoDimensional_OscillatorDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_TWODIMENSIONAL_OSCILLATOR_DIALOG, pParent)
	, x(0.03)
	, y(0.05)
	, Vx(-0.01)
	, Vy(0.02)
	, k1(0.2)
	, k2(0.2)
	, k3(0.2)
	, k4(0.2)
	, mass(2)
	, l(0.15)
	, T(5)
	, delta_t(0.01)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CTwoDimensional_OscillatorDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_X, x);
	DDX_Text(pDX, IDC_Y, y);
	DDX_Text(pDX, IDC_Vx, Vx);
	DDX_Text(pDX, IDC_Vy, Vy);
	DDX_Text(pDX, IDC_K1, k1);
	DDX_Text(pDX, IDC_K2, k2);
	DDX_Text(pDX, IDC_K3, k3);
	DDX_Text(pDX, IDC_K4, k4);
	DDX_Text(pDX, IDC_M, mass);
	DDX_Text(pDX, IDC_L, l);
	DDX_Text(pDX, IDC_T, T);
	DDX_Text(pDX, IDC_Delta_t, delta_t);
}

BEGIN_MESSAGE_MAP(CTwoDimensional_OscillatorDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, &CTwoDimensional_OscillatorDlg::OnBnClickedButton1)
END_MESSAGE_MAP()


// ����������� ��������� CTwoDimensional_OscillatorDlg

BOOL CTwoDimensional_OscillatorDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// ���������� ������ "� ���������..." � ��������� ����.

	// IDM_ABOUTBOX ������ ���� � �������� ��������� �������.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// ������ ������ ��� ����� ����������� ����.  ����� ������ ��� �������������,
	//  ���� ������� ���� ���������� �� �������� ����������
	SetIcon(m_hIcon, TRUE);			// ������� ������
	SetIcon(m_hIcon, FALSE);		// ������ ������

	// ������ ��������� �� ������ ��� ���������
	Main_Drawer.Create(GetDlgItem(IDC_MAIN_STATIC)->GetSafeHwnd());
	XY_Drawer.Create(GetDlgItem(IDC_STATIC_X)->GetSafeHwnd());
	ArcSin_X_Drawer.Create(GetDlgItem(IDC_STATIC_ARCSIN_X)->GetSafeHwnd());
	Energy_Drawer.Create(GetDlgItem(IDC_STATIC_ENERGY)->GetSafeHwnd());

	return TRUE;  // ������� �������� TRUE, ���� ����� �� ������� �������� ����������
}

void CTwoDimensional_OscillatorDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// ��� ���������� ������ ����������� � ���������� ���� ����� ��������������� ����������� ���� �����,
//  ����� ���������� ������.  ��� ���������� MFC, ������������ ������ ���������� ��� �������������,
//  ��� ������������� ����������� ������� ��������.

void CTwoDimensional_OscillatorDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // �������� ���������� ��� ���������

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// ������������ ������ �� ������ ����������� ��������������
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// ��������� ������
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// ������� �������� ��� ������� ��� ��������� ����������� ������� ��� �����������
//  ���������� ����.
HCURSOR CTwoDimensional_OscillatorDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CTwoDimensional_OscillatorDlg::OnBnClickedButton1()
{
	UpdateData(TRUE);

	// ��������� ���� ��� ������
	ofstream output_file;
	output_file.open("data.txt");
	output_file << "����������� ������������� �������� ������� �� �������: " <<endl;

	// ������� �������
	x_data.clear(), y_data.clear(), arcsin_x_data.clear(), time_data.clear(), length_data.clear(), RK4_data.clear(), E_k_data.clear(), E_p_data.clear(), E_max_data.clear();

	// ��������� ������ �������� ���������� ������
	vector<double> k_data(4);
	k_data[0] = k1;
	k_data[1] = k2;
	k_data[2] = k3;
	k_data[3] = k4;
	
	// ��������� ����������� ��������
	double time(0), energy_max(0), E_p_energy(0), E_k_energy(0), period_th(0), n(0), power_of_time(0), test_time(delta_t);
	bool pow_time_flag(true);

	// ���� ��������� �������� �������
	length_data = Length_function(x, y, l);

	for (int i = 0; i < 4; i++)
	{
		E_p_energy += k_data[i] * (length_data[i] - l) * (length_data[i] - l) / 2;
	}
	E_k_energy = mass*(Vx*Vx + Vy*Vy) / 2;
	energy_max = E_p_energy + E_k_energy;

	// ��������� ������� ���������� ����������;
	x_data.push_back(x);
	y_data.push_back(y);
	time_data.push_back(0);
	E_k_data.push_back(E_k_energy);
	E_p_data.push_back(E_p_energy);
	E_max_data.push_back(energy_max);
	arcsin_x_data.push_back(0);

	// ������ ����� ��� ��������
	Main_Drawer.DrawBasement();
	XY_Drawer.DrawBasement();
	Energy_Drawer.DrawBasement();
	ArcSin_X_Drawer.DrawBasement();

	while (pow_time_flag)
	{
		if (test_time < 1)
		{
			power_of_time++;
			test_time *= 10;
		}
		else
		{
			pow_time_flag = false;
		}
	}

	//������� ������ � ������, ������ � ���� ������������ �������
	n = T / delta_t;
	for (double i = 1; i <= n; i++)
	{
		// ������� ����� � �������� ����� �����-�����
		RK4_data = RG4_function(x, y, Vx, Vy, delta_t, mass, l, k_data, length_data);

		// ������ ��������� �������� ��� ���������� ��������
		x = RK4_data[0];
		y = RK4_data[1];
		Vx = RK4_data[2];
		Vy = RK4_data[3];
		time += delta_t;
		E_p_energy = E_k_energy = 0;

		// ������ ������������� �������
		period_th += RK4_data[5];

		// ���������� ����
		length_data = Length_function(x, y, l);

		// ���� �������� �������
		for (int i = 0; i < 4; i++)
		{
			E_p_energy += k_data[i] * (length_data[i] - l) * (length_data[i] - l) / 2;
		}

		E_k_energy = mass*(Vx*Vx + Vy*Vy) / 2;
		energy_max = E_p_energy + E_k_energy;

		// ��������� � ������� ����� ���������
		x_data.push_back(x);
		y_data.push_back(y);
		time_data.push_back(time);
		E_k_data.push_back(E_k_energy);
		E_p_data.push_back(E_p_energy);
		E_max_data.push_back(energy_max);
		arcsin_x_data.push_back(RK4_data[4]);

		Main_Drawer.DrawGraph(y_data, x_data, l, -l, l, -l);

		ArcSin_X_Drawer.DrawGraph(arcsin_x_data, time_data, 3.1415926 / 2, -3.1415926 / 2, T, 0);

		XY_Drawer.DrawTwoGraphs(y_data, x_data, time_data, l, -l, T, 0);

		Energy_Drawer.DrawThreeGraphs(E_p_data, E_k_data, E_max_data, time_data, E_max_data[0], 0, T, 0);

		output_file << "�����: " << setprecision(power_of_time) << fixed << time; 
		output_file << "\t�������: " << setprecision(8) << scientific << energy_max << endl;
	}
	period_th = period_th / n;

	output_file << endl;
	output_file << setprecision(4) << fixed <<"������������� ������� ������ ���������� �� �: " << period_th << endl;
	output_file.close();
}
