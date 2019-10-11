#include "View.h"

MyView::MyView()
{
	cout << "Create MyView" << endl;
}

MyView::~MyView()
{
	cout << "Destory MyView" << endl;
}

void MyView::Render()
{
	for (int y = 0; y < 10; y++)
	{
		for (int x = 0; x < 10; x++)
		{
			if (y == 0 || y == 10-1)
				cout << "бс";
			else if (x == 0 || x == 10-1)
				cout << "бс";
			else
				cout << "  ";
		}
		cout << endl;
	}
}

OptionView::OptionView()
{
	cout << "Create Option" << endl;
}

OptionView::~OptionView()
{
	cout << "Delete Option" << endl;
}

void OptionView::Render()
{
	cout << "======Option=======" << endl;
}
