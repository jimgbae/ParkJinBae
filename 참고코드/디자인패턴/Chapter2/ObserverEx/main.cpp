#include "Data.h"
#include"DataGraph.h"
#include"DataTable.h"

void main()
{
	Data* data = new Data;
	DataTable* DT = new DataTable;
	DataGraph* DG = new DataGraph;

	cout << "[갱신 1]" << endl;

	//구독개체를 추가한다.	
	data->AddObserver(DT);
	data->setData(6, 2, 3);
	//변경된 상태를 적용해서 알려준다.
	data->NotifyObserver();

	cout << "\n[갱신 2]" << endl;
	//구독개체를 추가한다.
	data->AddObserver(DG);
	data->setData(10, 12, 7);
	//변경된 상태를 적용해서 알려준다.
	data->NotifyObserver();

	cout << "\n[갱신 3]" << endl;
	data->setData(1, 3, 2);
	data->NotifyObserver();

	int iSelect, kor, math, eng;
	cout <<"\n[갱신 4]" << endl;
	cout << "국어 : "; cin >> kor; cout << "수학 : "; cin >> math; cout << "외국어 : "; cin >> eng;
	data->setData(kor, math, eng);
	data->NotifyObserver();

	delete data;
	delete DT;
	delete DG;
}