#include<iostream>
#include<vector>
#include<algorithm>
#include<string>
using namespace std;
#define WIDTH 10
#define HEIGHT 10
#define PLAYER 2

enum BOX
{
	BOX_NOBOX,
	BOX_ONBOX
};

void main()
{
	vector< vector<int> > arr({
		vector<int>({ 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 }),
		vector<int>({ 1, 2, 1, 0, 0, 1, 0, 0, 0, 1 }),
		vector<int>({ 1, 0, 1, 1, 0, 0, 0, 1, 0, 1 }),
		vector<int>({ 1, 0, 1, 1, 1, 1, 1, 1, 0, 1 }),
		vector<int>({ 1, 0, 1, 0, 0, 0, 1, 0, 0, 1 }),
		vector<int>({ 1, 0, 1, 0, 1, 0, 1, 0, 1, 1 }),
		vector<int>({ 1, 0, 1, 0, 1, 0, 1, 0, 0, 1 }),
		vector<int>({ 1, 0, 1, 0, 1, 0, 1, 1, 0, 1 }),
		vector<int>({ 1, 0, 0, 0, 1, 0, 0, 0, 0, 1 }),
		vector<int>({ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }),
	});
	for (int y = 0; y < HEIGHT; y++)
	{
		for (int x = 0; x < WIDTH; x++)
		{
			if (arr[y][x] == BOX_ONBOX)
				cout << "¢Ë";
			else if (arr[y][x] == PLAYER)
				cout << "¢¼";
			else
				cout << "  ";
		}
		cout << endl;
	}
}