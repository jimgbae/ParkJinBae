#include "Student.h"

void Student::SetStudent(int _Num)
{
	cout << "�̸��� �Է� �Ͻÿ� : ";
	cin >> strName;
	cout << "3���� ������ �Է� �Ͻÿ�" << endl;
	cout << "���� : ";
	cin >> iKor;
	cout << "���� : ";
	cin >> iEng;
	cout << "���� : ";
	cin >> iMath;
	iSum = iKor + iEng + iMath;
	fAvg = (float)iSum / 3;
	iNum = _Num;
	if (fAvg >= 90)
		chClass = 'A';
	else if (fAvg >= 80)
		chClass = 'B';
	else if (fAvg >= 70)
		chClass = 'C';
	else if (fAvg >= 60)
		chClass = 'D';
	else
		chClass = 'F';
	return;
}

void Student::ShowStudent()
{
	cout << "----------------------" << endl;
	cout << iNum << "�� �л�" << endl;
	cout << "�̸� : " << strName << endl;
	cout << "���� ���� : " << iKor << endl << "���� ���� : " << iMath << endl;
	cout << "���� ���� : " << iEng << endl << "�հ� ���� : " << iSum << "\t ��� ���� : " << fAvg << endl;
	cout << "��� : [ " << chClass << " ] " << endl << endl;
	return;
}
