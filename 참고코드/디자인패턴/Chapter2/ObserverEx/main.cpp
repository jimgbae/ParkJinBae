#include "Data.h"
#include"DataGraph.h"
#include"DataTable.h"

void main()
{
	Data* data = new Data;
	DataTable* DT = new DataTable;
	DataGraph* DG = new DataGraph;

	cout << "[���� 1]" << endl;

	//������ü�� �߰��Ѵ�.	
	data->AddObserver(DT);
	data->setData(6, 2, 3);
	//����� ���¸� �����ؼ� �˷��ش�.
	data->NotifyObserver();

	cout << "\n[���� 2]" << endl;
	//������ü�� �߰��Ѵ�.
	data->AddObserver(DG);
	data->setData(10, 12, 7);
	//����� ���¸� �����ؼ� �˷��ش�.
	data->NotifyObserver();

	cout << "\n[���� 3]" << endl;
	data->setData(1, 3, 2);
	data->NotifyObserver();

	int iSelect, kor, math, eng;
	cout <<"\n[���� 4]" << endl;
	cout << "���� : "; cin >> kor; cout << "���� : "; cin >> math; cout << "�ܱ��� : "; cin >> eng;
	data->setData(kor, math, eng);
	data->NotifyObserver();

	delete data;
	delete DT;
	delete DG;
}