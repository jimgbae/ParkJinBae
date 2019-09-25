#pragma once
#include"Student.h"
#include"Singleton.h"
#include<vector>
#define MAX_STUDENT 30

class Std_Manager : public Singleton<Std_Manager>
{
	vector<Student*> m_vecStudentList;
public:
	Std_Manager();
	void display();
	void setStudent();
	void showStudent();
	void findNumber();
	void findname();
	void findClass();
	~Std_Manager();
};

