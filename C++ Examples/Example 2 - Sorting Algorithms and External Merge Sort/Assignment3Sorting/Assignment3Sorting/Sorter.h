#include <time.h>
#include <cstdlib>
#include <iostream>
#include <conio.h>
#include <string.h>
#ifndef _SORTER_H
#define _SORTER_H

using namespace std;

static class Sorter
{
public:

	static void bubbleSort(int* &arrayToSort, int sizeOfArray);
	static void selectionSort(int* &arrayToSort, int sizeOfArray);
	static void insertionSort(int* &arrayToSort, int sizeOfArray);
	static void shellSort(int* &arrayToSort, int sizeOfArray);
	static void mergeSort(int arrayToSort[], int first, int last, int sizeOfArray, int tempArray[]);
	static void merge(int arrayToSort[], int first, int last, int mid, int sizeOfArray, int tempArray[]);
	static void quickSort(int *array, int beg, int end, int size);
	static void randomizeSortingArray(int* &arrayToSort, int sizeOfArray);
	static int partition(int *array, int beg, int end);
	static void printStartOfArray(int theArray[]);


};

#endif