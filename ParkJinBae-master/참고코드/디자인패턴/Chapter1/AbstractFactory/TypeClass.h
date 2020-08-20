#pragma once
#include <iostream>
using namespace std;

class TypeClass
{
public:
	~TypeClass();

	virtual bool MonsterCheck() = 0;
};

class Goblrin : public TypeClass
{
public:
	virtual bool MonsterCheck();
};

class Reach : public TypeClass
{
public:
	virtual bool MonsterCheck();
};

