#include "Inventory.h"
#include<Windows.h>

void main()
{
	Inventory* Equip;
	Inventory* Noitem = new Item("없음");
	Inventory* inventory = new Bag("Main Bag");

	Inventory* item1 = new Item("검");
	Inventory* item2 = new Item("활");
	Inventory* item3 = new Item("지팡이");

	Inventory* bag1 = new Bag("주문서 가방");
	Inventory* item4 = new Item("파이어볼");
	Inventory* item5 = new Item("썬더볼트");

	Inventory* bag2 = new Bag("화살 가방");
	Inventory* item6 = new Item("불 화살");
	Inventory* item7 = new Item("독 화살");

	Inventory* item8 = new Item("돈");

	Inventory* bag3 = new Bag("선물 상자");
	Inventory* item9 = new Item("빨간코");
	Inventory* item10 = new Item("물약");
	Inventory* item11 = new Item("보따리");

	Equip = Noitem;
	inventory->AddInventory(Noitem);
	inventory->AddInventory(item1);
	inventory->AddInventory(item2);
	inventory->AddInventory(item3);

	bag1->AddInventory(item4);
	bag1->AddInventory(item5);
	inventory->AddInventory(bag1);

	bag2->AddInventory(item6);
	bag2->AddInventory(item7);
	inventory->AddInventory(bag2);

	inventory->AddInventory(item8);

	inventory->AddInventory(bag3);
	bag3->AddInventory(item9);
	bag3->AddInventory(item10);
	bag3->AddInventory(item11);

	int iSelect, ichose, itemcount = 12;
	string str, stritem;
	stritem = "item" + to_string(itemcount++);
	char buf[256];
	bool bbool = false;
	while (!bbool)
	{
		system("cls");
		inventory->View();
		cout << "착용중 아이템 : ";
		cout << setfill(' ') << Equip->GetName().c_str() << " - Item" << endl;
		cout << "1. 아이템 추가 2.나가기" << endl << endl;
		cin >> iSelect;
		switch (iSelect)
		{
		case 1:
			cin >> str;
			Inventory* stritem = new Item(str);
			break;
		case 2:
			bbool = true;
			break;
		}
	}

	delete inventory;
}