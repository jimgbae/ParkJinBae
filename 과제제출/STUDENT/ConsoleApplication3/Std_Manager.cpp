#include "Std_Manager.h"



Std_Manager::Std_Manager()
{
	while (m_vecStudentList.size() != MAX_STUDENT)
	{
		m_vecStudentList.push_back(new Student);
	}
}

void Std_Manager::display()
{
	int sel;
	while (true)
	{
		cout << "=========================" << endl;
		cout << "   1.�л� ���" << endl;
		cout << "   2.��ü �л����� ���" << endl;
		cout << "   3.�л� ��ȣ �˻�" << endl;
		cout << "   4.�л� �̸� �˻�" << endl;
		cout << "   5.��޺� ���" << endl;
		cout << "   0.����" << endl;
		cout << "=========================" << endl;
		cin >> sel;
		switch (sel) {
		case 1:
			Std_Manager::GetInstance()->setStudent();
			break;
		case 2:
			Std_Manager::GetInstance()->showStudent();
			break;
		case 3:
			Std_Manager::GetInstance()->findNumber();
			break;
		case 4:
			Std_Manager::GetInstance()->findname();
			break;
		case 5:
			Std_Manager::GetInstance()->findClass();
			break;
		case 0:
		{
			Std_Manager::DestroyInstance();
			return;
		}
		default:cout << "�߸� �Է� " << endl;
		}
		system("pause"); system("cls");
	}
}

void Std_Manager::setStudent()
{
	for (int i = 0; i < MAX_STUDENT; i++)
	{
		if (m_vecStudentList[i]->GetNum() == 0)
		{
			m_vecStudentList[i]->SetStudent(i + 1);
			return;
		}
	}
	cout << "�л��� ���� ��� �Ǿ����ϴ�." << endl;
}

void Std_Manager::showStudent()
{
	for (auto iter = m_vecStudentList.begin(); iter != m_vecStudentList.end(); iter++)
	{
		if((*iter)->GetNum() != 0)
			(*iter)->ShowStudent();
	}
}

void Std_Manager::findNumber()
{
	int _find;
	cout << "ã�� �л��� ��ȣ�� �Է� �Ͻÿ� : ";
	cin >> _find;
	for (auto iter = m_vecStudentList.begin(); iter != m_vecStudentList.end(); iter++)
	{
		if (_find == (*iter)->GetNum())
		{
			(*iter)->ShowStudent();
			return;
		}
	}
	cout << "�ش� ��ȣ�� �л��� �����ϴ�." << endl;
}

void Std_Manager::findname()
{
	string _find;
	cout << "ã�� �л��� �̸��� �Է� �Ͻÿ� : ";
	cin >> _find;
	for (auto iter = m_vecStudentList.begin(); iter != m_vecStudentList.end(); iter++)
	{
		if (_find == (*iter)->GetName())
		{
			(*iter)->ShowStudent();
			return;
		}
	}
	cout << "�ش� �̸��� �л��� �����ϴ�." << endl;
}

void Std_Manager::findClass()
{
	int _find = 0;
	char chClass = 'A';
	while (chClass != 'G')
	{
		if (chClass == 'E')
			chClass++;
		cout << "========= [ " << chClass << " ] =========" << endl;
		for (auto iter = m_vecStudentList.begin(); iter != m_vecStudentList.end(); iter++)
		{
			if ((*iter)->GetClass() == chClass)
			{
				(*iter)->ShowStudent();
				_find++;
			}
		}
		cout << "�� " << _find << "��" << endl;
		cout << "=========================" << endl;
		_find = 0;
		chClass++;
	}
}


Std_Manager::~Std_Manager()
{
}
