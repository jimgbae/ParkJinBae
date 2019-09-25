#pragma once
#include<iostream>
#include<string>
#define MAX_LEN 100
using namespace std;


class Student
{
private:
	string strName;
	int iKor, iEng, iMath, iSum;
	float fAvg;
	int iNum;
	char chClass;
public:
	Student() { iNum = 0; }
	~Student() {}

	void SetStudent(int _Num);
	void ShowStudent();
	int GetNum()
	{
		return iNum;
	}
	string GetName()
	{
		return strName;
	}
	char GetClass()
	{
		return chClass;
	}
};

