#include <fstream>
#include <iostream>
#include <conio.h>
#include <iomanip>
#include <string.h>
#include <stdlib.h> 
#include <sstream>
#include "Sorter.h"
#ifndef _EXTERNALSORTER_H
#define _EXTERNALSORTER_H

using namespace std;

static class ExternalSorter
{
public:
	struct DATA{ int data; int lineNumber; };
	void mergeSort(int numberOfRecordsToRead);
	void writeInitialDataArrayToFile(int theArray[], int arraySize);
	void writeDataArrayToFile(ofstream &output, DATA theArray[], int arraySize, const char *fileName);
	enum EvenOrOdd {even,odd};
	const char* DATAFILE = "DataToSort.txt";
	const char* FILE1 = "Sort1.txt";
	const char* FILE2 = "Sort2.txt";
	const char* FILE3 = "Sort3.txt";
	const char* FILE4 = "Sort4.txt";
	bool initialReadAndSort(int numberOfRecordsToRead);
	bool readAndMerge(int numberOfRecordsToRead);
	DATA readLine(ifstream &input);
	void writeLine(ofstream &output, DATA data);
	int fast_atoi(const char * str);
	static void quickSort(DATA *array, int beg, int end, int size);
	static int partition(DATA *array, int beg, int end);



};

#endif