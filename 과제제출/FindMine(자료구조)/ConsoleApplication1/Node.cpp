#include "Node.h"



Node::Node()
{
}

void Node::GetNode(int x, int y, Node* tmpLink)
{
	m_ix = x;
	m_iy = y;
	Link = tmpLink;
}

void Node::SaveLink(Node* tmpLink)
{
	Link = tmpLink;
}


Node::~Node()
{
}
