#pragma once
class Node
{
private:
	int m_ix;
	int m_iy;
	Node* Link;
public:
	Node();
	void GetNode(int x, int y, Node* tmpLink);
	void SaveLink(Node* tmpLink);
	inline int GetX()
	{
		return m_ix;
	}
	inline int GetY()
	{
		return m_iy;
	}
	inline Node* GetLink()
	{
		return Link;
	}
	~Node();
};

