#pragma once
#include<iostream>
#include<string>
#include<Windows.h>
#include<iomanip>
#include<conio.h>
#include<time.h>
#include<crtdbg.h>
using namespace std;

#define EASY_WIDTH 10
#define EASY_HEIGHT 10
#define EASY_MINE 10
#define NORMAL_WIDTH 15
#define NORMAL_HEIGHT 15
#define NORMAL_MINE 25
#define HARD_WIDTH 30
#define HARD_HEIGHT 15
#define HARD_MINE 50

enum KEY
{
	KEY_LEFT = 'a',
	KEY_RIGHT = 'd',
	KEY_UP = 'w',
	KEY_DOWN = 's',
	KEY_ENTER = 13,
	KEY_FLAG = 'z',
	KEY_OPTION = 'n',
	KEY_EXIT = 27
};
