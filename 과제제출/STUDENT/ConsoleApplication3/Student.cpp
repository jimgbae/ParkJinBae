#include "Student.h"

void Student::SetStudent(int _Num)
{
	cout << "이름을 입력 하시오 : ";
	cin >> strName;
	cout << "3과목 점수를 입력 하시오" << endl;
	cout << "국어 : ";
	cin >> iKor;
	cout << "영어 : ";
	cin >> iEng;
	cout << "수학 : ";
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
	cout << iNum << "번 학생" << endl;
	cout << "이름 : " << strName << endl;
	cout << "국어 점수 : " << iKor << endl << "수학 점수 : " << iMath << endl;
	cout << "영어 점수 : " << iEng << endl << "합계 점수 : " << iSum << "\t 평균 점수 : " << fAvg << endl;
	cout << "등급 : [ " << chClass << " ] " << endl << endl;
	return;
}
