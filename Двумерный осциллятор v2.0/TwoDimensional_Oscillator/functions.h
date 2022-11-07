#include <vector>

using namespace std;

// Функция расчета проекции ускорения на ось х
double f_function(double xn, double m, double l, vector<double> &length_data, vector<double> &k_data)
{
	return k_data[0] * (l - length_data[0])*xn / (length_data[0] * m) + k_data[1] * (length_data[1] - l)*(l - xn) / (length_data[1] * m) + k_data[2] * (l - length_data[2])*xn / (length_data[2] * m) + k_data[3] * (l - length_data[3])*(l + xn) / (length_data[3] * m);
	// return k_data[3] * (length_data[3] - l)*(l + xn) / (length_data[3] * m) + k_data[2] * (length_data[2] - l)*xn / (length_data[2] * m) + k_data[1] * (length_data[1] - l)*(l - xn) / (length_data[1] * m) + k_data[0] * (length_data[0] - l)*xn / (length_data[0] * m);
}

// Функция расчета проекции ускорения на ось y
double g_function(double yn, double m, double l, vector<double> length_data, vector<double> k_data)
{
	return k_data[0] * (length_data[0] - l)*(l - yn) / (length_data[0] * m) + k_data[1] * (l - length_data[1])*yn / (length_data[1] * m) + k_data[2] * (l - length_data[2])*(l + yn) / (length_data[2] * m) + k_data[3] * (l - length_data[3])*yn / (length_data[3] * m);
	// return k_data[3] * (length_data[3] - l)*yn / (length_data[3] * m) + k_data[2] * (length_data[2] - l)*(l + yn) / (length_data[2] * m) + k_data[1] * (length_data[1] - l)*yn / (length_data[1] * m) + k_data[0] * (length_data[0] - l)*(l - yn) / (length_data[0] * m);
}

// Расчет текущей длинны пружин
vector<double> Length_function(double x, double y, double l)
{
	vector<double> length_data(4);

	length_data[0] = sqrt(x*x + (l - y)*(l - y));
	length_data[1] = sqrt((l - x)*(l - x) + y*y);
	length_data[2] = sqrt(x*x + (l + y)*(l + y));
	length_data[3] = sqrt((l + x)*(l + x) + y*y);

	return length_data;
}

// Метод Рунге-Кутты 4-ого порядка
vector<double> RG4_function(double x, double y, double Vx, double Vy, double delta_t, double m, double l, vector<double> &k_data, vector<double> &length_data)
{
	vector<double> result_data(6);

	// Расчет проекций ускорения на Х и Y
	double xn = x;
	double yn = y;

	double Ax = f_function(xn, m, l, length_data, k_data);
	double Ay = g_function(yn, m, l, length_data, k_data);

	// Расчет коэфициентов k_x1, k_y1
	double k_1x = Ax*delta_t;
	double k_1y = Ay*delta_t;
	
	//______________________________________________

	// Расчет проекций ускорения на Х и Y
	xn = x + Vx*delta_t / 2;
	yn = y + Vy*delta_t / 2;

	Ax = f_function(xn, m, l, length_data, k_data);
	Ay = g_function(yn, m, l, length_data, k_data);

	// Расчет коэфициентов k_x2, k_y2
	double k_2x = Ax*delta_t;
	double k_2y = Ay*delta_t;

	//______________________________________________

	// Расчет проекций ускорения на Х и Y
	xn = x + Vx*delta_t / 2 + k_1x*delta_t / 4;
	yn = y + Vy*delta_t / 2 + k_1y*delta_t / 4;

	Ax = f_function(xn, m, l, length_data, k_data);
	Ay = g_function(yn, m, l, length_data, k_data);

	// Расчет коэфициентов k_x3, k_y3
	double k_3x = Ax*delta_t;
	double k_3y = Ay*delta_t;

	//______________________________________________

	// Расчет проекций ускорения на Х и Y
	xn = x + Vx*delta_t + k_2x / 2;
	yn = y + Vy*delta_t + k_2y / 2;

	Ax = f_function(xn, m, l, length_data, k_data);
	Ay = g_function(yn, m, l, length_data, k_data);

	// Расчет коэфициентов k_x4, k_y4
	double k_4x = Ax*delta_t;
	double k_4y = Ay*delta_t;

	//______________________________________________

	// Считаем частоту
	double w = sqrt(-Ax / x);

	result_data[0] = x + (Vx + (k_1x + k_2x + k_3x) / 6)*delta_t;
	result_data[1] = y + (Vy + (k_1y + k_2y + k_3y) / 6)*delta_t;
	result_data[2] = Vx + (k_1x + 2 * k_2x + 2 * k_3x + k_4x) / 6;
	result_data[3] = Vy + (k_1y + 2 * k_2y + 2 * k_3y + k_4y) / 6;
	result_data[4] = asin(x / sqrt(x*x + (Vx / w)*(Vx / w)));
	result_data[5] = 2 * 3.1415926 / w;

	return result_data;
}