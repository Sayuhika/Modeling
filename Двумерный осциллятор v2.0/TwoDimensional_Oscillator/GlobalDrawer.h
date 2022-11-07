#pragma once

#include <afxwin.h>
#include <vector>

using namespace std;

class Global_Drawer
{
	// ������������� ������� ���������.
	CRect frame;
	// ��������� �� ������ ����, ������� ���������.
	CWnd * wnd;
	// �������� ���������, ����������� � ������� ���������.
	CDC * dc;
	// �������� ���������, ����������� � ������.
	CDC memDC;
	// ������ ��� ��������� ��������� memDC.
	CBitmap bmp;
	// ���� ��� ������������ ��������� ������������� ������.
	bool init;
public:

	// ������������������� ������ ������ �� ������ HWND.
	void Create(HWND hWnd)
	{
		// �������� ��������� �� ����.
		wnd = CWnd::FromHandle(hWnd);
		// �������� ������������� ����.
		wnd->GetClientRect(frame);
		// �������� �������� ��� ��������� � ���� ����.
		dc = wnd->GetDC();

		// ������� �����-��������.
		memDC.CreateCompatibleDC(dc);
		// ������� ����� ��� ��������� ���������.
		bmp.CreateCompatibleBitmap(dc, frame.Width(), frame.Height());
		// �������� ����� ��� ������������� �����-����������.
		memDC.SelectObject(&bmp);
		init = true;
	}

	//������ �����-��������� ��� ��������
	void DrawBasement()
	{
		if (!init) return;

		CPen subgrid_pen(PS_DOT, 1, RGB(200, 200, 200));
		CPen grid_pen(PS_SOLID, 1, RGB(0, 0, 0));

		CFont font;
		font.CreateFontW(18, 0, 0, 0,
			FW_DONTCARE,
			FALSE,				// ������
			FALSE,				// ������������
			FALSE,				// �������������
			DEFAULT_CHARSET,	// ����� ��������
			OUT_OUTLINE_PRECIS,	// �������� ������������.	
			CLIP_DEFAULT_PRECIS,//  
			CLEARTYPE_QUALITY,	// ��������
			VARIABLE_PITCH,		//
			TEXT("Times New Roman")		//
		);

		int padding = 20;
		int left_keys_padding = 20;
		int bottom_keys_padding = 10;

		int actual_width = frame.Width() - 2 * padding - left_keys_padding;
		int actual_height = frame.Height() - 2 * padding - bottom_keys_padding;

		int actual_top = padding;
		int actual_bottom = actual_top + actual_height;
		int actual_left = padding + left_keys_padding;
		int actual_right = actual_left + actual_width;

		// ����� ���.
		memDC.FillSolidRect(frame, RGB(255, 255, 255));

		// ������ ����� � ��������.
		unsigned int grid_size = 10;

		memDC.SelectObject(&subgrid_pen);

		for (double i = 0.5; i < grid_size; i += 1.0)
		{
			memDC.MoveTo(actual_left + i * actual_width / grid_size, actual_top);
			memDC.LineTo(actual_left + i * actual_width / grid_size, actual_bottom);
			memDC.MoveTo(actual_left, actual_top + i * actual_height / grid_size);
			memDC.LineTo(actual_right, actual_top + i * actual_height / grid_size);
		}

		memDC.SelectObject(&grid_pen);

		for (double i = 0.0; i < grid_size + 1; i += 1.0)
		{
			memDC.MoveTo(actual_left + i * actual_width / grid_size, actual_top);
			memDC.LineTo(actual_left + i * actual_width / grid_size, actual_bottom);
			memDC.MoveTo(actual_left, actual_top + i * actual_height / grid_size);
			memDC.LineTo(actual_right, actual_top + i * actual_height / grid_size);
		}
	}

	// ���������� ������ �� ���������� ������.
	void DrawGraph(vector<double> &data, vector<double> &keys, double y_max, double y_min, double x_max, double x_min)
	{
		if (!init) return;

		CPen data_pen(PS_SOLID, 2, RGB(255, 0, 0));
		CFont font;

		font.CreateFontW(18, 0, 0, 0,
			FW_DONTCARE,
			FALSE,				// ������
			FALSE,				// ������������
			FALSE,				// �������������
			DEFAULT_CHARSET,	// ����� ��������
			OUT_OUTLINE_PRECIS,	// �������� ������������.	
			CLIP_DEFAULT_PRECIS,//  
			CLEARTYPE_QUALITY,	// ��������
			VARIABLE_PITCH,		//
			TEXT("Times New Roman")		//
		);

		int padding = 20;
		int left_keys_padding = 20;
		int bottom_keys_padding = 10;

		int actual_width = frame.Width() - 2 * padding - left_keys_padding;
		int actual_height = frame.Height() - 2 * padding - bottom_keys_padding;

		int actual_top = padding;
		int actual_bottom = actual_top + actual_height;
		int actual_left = padding + left_keys_padding;
		int actual_right = actual_left + actual_width;

		unsigned int grid_size = 10;

		double data_y_max(y_max), data_y_min(y_min);
		double data_x_max(x_max), data_x_min(x_min);

		double y = convert_range(data[data.size() - 1], actual_top, actual_bottom, data_y_max, data_y_min);
		double x = convert_range(keys[keys.size() - 1], actual_right, actual_left, data_x_max, data_x_min);
		double y_last = convert_range(data[data.size() - 2], actual_top, actual_bottom, data_y_max, data_y_min);
		double x_last = convert_range(keys[keys.size() - 2], actual_right, actual_left, data_x_max, data_x_min);

		memDC.SelectObject(&data_pen);
		memDC.MoveTo(x_last, y_last);
		memDC.LineTo(x, y);
	
		memDC.SelectObject(&font);
		memDC.SetTextColor(RGB(0, 0, 0));
		for (int i = 0; i < grid_size / 2 + 1; i++)
		{
			CString str;
			str.Format(L"%.1f", data_x_min + i*(data_x_max - data_x_min) / (grid_size / 2));
			memDC.TextOutW(actual_left + (double)i * actual_width / (grid_size / 2) - bottom_keys_padding, actual_bottom + bottom_keys_padding / 2, str);

			str.Format(L"%.2f", data_y_min + i*(data_y_max - data_y_min) / (grid_size / 2));
			memDC.TextOutW(actual_left - 1.5*left_keys_padding, actual_bottom - (double)i * actual_height / (grid_size / 2) - bottom_keys_padding, str);
		}

		dc->BitBlt(0, 0, frame.Width(), frame.Height(), &memDC, 0, 0, SRCCOPY);
	}

	void DrawTwoGraphs(vector<double> &data1, vector<double> &data2, vector<double> &keys, double y_max, double y_min, double x_max, double x_min)
	{
		if (!init) return;

		CPen data2_pen(PS_SOLID, 2, RGB(255, 0, 0));
		CPen data1_pen(PS_SOLID, 2, RGB(0, 0, 255));
		CFont font;

		font.CreateFontW(18, 0, 0, 0,
			FW_DONTCARE,
			FALSE,				// ������
			FALSE,				// ������������
			FALSE,				// �������������
			DEFAULT_CHARSET,	// ����� ��������
			OUT_OUTLINE_PRECIS,	// �������� ������������.	
			CLIP_DEFAULT_PRECIS,//  
			CLEARTYPE_QUALITY,	// ��������
			VARIABLE_PITCH,		//
			TEXT("Times New Roman")		//
		);

		int padding = 20;
		int left_keys_padding = 20;
		int bottom_keys_padding = 10;

		int actual_width = frame.Width() - 2 * padding - left_keys_padding;
		int actual_height = frame.Height() - 2 * padding - bottom_keys_padding;

		int actual_top = padding;
		int actual_bottom = actual_top + actual_height;
		int actual_left = padding + left_keys_padding;
		int actual_right = actual_left + actual_width;

		unsigned int grid_size = 10;

		double data_y_max(y_max), data_y_min(y_min);
		double data_x_max(x_max), data_x_min(x_min);

		double y1 = convert_range(data1[data1.size() - 1], actual_top, actual_bottom, data_y_max, data_y_min);
		double y2 = convert_range(data2[data2.size() - 1], actual_top, actual_bottom, data_y_max, data_y_min);
		double x = convert_range(keys[keys.size() - 1], actual_right, actual_left, data_x_max, data_x_min);
		double y1_last = convert_range(data1[data1.size() - 2], actual_top, actual_bottom, data_y_max, data_y_min);
		double y2_last = convert_range(data2[data2.size() - 2], actual_top, actual_bottom, data_y_max, data_y_min);
		double x_last = convert_range(keys[keys.size() - 2], actual_right, actual_left, data_x_max, data_x_min);

		memDC.SelectObject(&data1_pen);
		memDC.MoveTo(x_last, y1_last);
		memDC.LineTo(x, y1);

		memDC.SelectObject(&data2_pen);
		memDC.MoveTo(x_last, y2_last);
		memDC.LineTo(x, y2);

		memDC.SelectObject(&font);
		memDC.SetTextColor(RGB(0, 0, 0));
		for (int i = 0; i < grid_size / 2 + 1; i++)
		{
			CString str;
			str.Format(L"%.1f", data_x_min + i*(data_x_max - data_x_min) / (grid_size / 2));
			memDC.TextOutW(actual_left + (double)i * actual_width / (grid_size / 2) - bottom_keys_padding, actual_bottom + bottom_keys_padding / 2, str);

			str.Format(L"%.2f", data_y_min + i*(data_y_max - data_y_min) / (grid_size / 2));
			memDC.TextOutW(actual_left - 1.5*left_keys_padding, actual_bottom - (double)i * actual_height / (grid_size / 2) - bottom_keys_padding, str);
		}

		dc->BitBlt(0, 0, frame.Width(), frame.Height(), &memDC, 0, 0, SRCCOPY);
	}

	void DrawThreeGraphs(vector<double> &data1, vector<double> &data2, vector<double> &data3, vector<double> &keys, double y_max, double y_min, double x_max, double x_min)
	{
		if (!init) return;

		CPen data1_pen(PS_SOLID, 2, RGB(255, 0, 0));
		CPen data2_pen(PS_SOLID, 2, RGB(0, 0, 255));
		CPen data3_pen(PS_SOLID, 2, RGB(0, 255, 0));
		CFont font;

		font.CreateFontW(18, 0, 0, 0,
			FW_DONTCARE,
			FALSE,				// ������
			FALSE,				// ������������
			FALSE,				// �������������
			DEFAULT_CHARSET,	// ����� ��������
			OUT_OUTLINE_PRECIS,	// �������� ������������.	
			CLIP_DEFAULT_PRECIS,//  
			CLEARTYPE_QUALITY,	// ��������
			VARIABLE_PITCH,		//
			TEXT("Times New Roman")		//
		);

		int padding = 20;
		int left_keys_padding = 20;
		int bottom_keys_padding = 10;

		int actual_width = frame.Width() - 2 * padding - left_keys_padding;
		int actual_height = frame.Height() - 2 * padding - bottom_keys_padding;

		int actual_top = padding;
		int actual_bottom = actual_top + actual_height;
		int actual_left = padding + left_keys_padding;
		int actual_right = actual_left + actual_width;

		unsigned int grid_size = 10;

		double data_y_max(y_max), data_y_min(y_min);
		double data_x_max(x_max), data_x_min(x_min);

		double y1 = convert_range(data1[data1.size() - 1], actual_top, actual_bottom, data_y_max, data_y_min);
		double y2 = convert_range(data2[data2.size() - 1], actual_top, actual_bottom, data_y_max, data_y_min);
		double y3 = convert_range(data3[data3.size() - 1], actual_top, actual_bottom, data_y_max, data_y_min);
		double x = convert_range(keys[keys.size() - 1], actual_right, actual_left, data_x_max, data_x_min);
		double y1_last = convert_range(data1[data1.size() - 2], actual_top, actual_bottom, data_y_max, data_y_min);
		double y2_last = convert_range(data2[data2.size() - 2], actual_top, actual_bottom, data_y_max, data_y_min);
		double y3_last = convert_range(data3[data3.size() - 2], actual_top, actual_bottom, data_y_max, data_y_min);
		double x_last = convert_range(keys[keys.size() - 2], actual_right, actual_left, data_x_max, data_x_min);

		memDC.SelectObject(&data1_pen);
		memDC.MoveTo(x_last, y1_last);
		memDC.LineTo(x, y1);

		memDC.SelectObject(&data2_pen);
		memDC.MoveTo(x_last, y2_last);
		memDC.LineTo(x, y2);

		memDC.SelectObject(&data3_pen);
		memDC.MoveTo(x_last, y3_last);
		memDC.LineTo(x, y3);

		memDC.SelectObject(&font);
		memDC.SetTextColor(RGB(0, 0, 0));
		for (int i = 0; i < grid_size / 2 + 1; i++)
		{
			CString str;
			str.Format(L"%.1f", data_x_min + i*(data_x_max - data_x_min) / (grid_size / 2));
			memDC.TextOutW(actual_left + (double)i * actual_width / (grid_size / 2) - bottom_keys_padding, actual_bottom + bottom_keys_padding / 2, str);

			str.Format(L"%.3f", (data_y_min + i*(data_y_max - data_y_min) / (grid_size / 2))*1000);
			memDC.TextOutW(actual_left - 1.5*left_keys_padding, actual_bottom - (double)i * actual_height / (grid_size / 2) - bottom_keys_padding, str);
		}

		dc->BitBlt(0, 0, frame.Width(), frame.Height(), &memDC, 0, 0, SRCCOPY);
	}

	double convert_range(double value, double outmax, double outmin, double inmax, double inmin)
	{
		double k = (outmax - outmin) / (inmax - inmin);
		return ((value - inmin) * k + outmin);
	}
};