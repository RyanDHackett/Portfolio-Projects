#include <conio.h>
#include <iostream>
#include "Sorter.h"
#include "ExternalSorter.h"
#include <time.h>
using namespace std;
int main()
{
	const int SIZEOFMERGESORTINGARRAY = 100000;
	const int BLOCKSIZE = 1000;
	int* mergeSortArray = new int[SIZEOFMERGESORTINGARRAY];
	ExternalSorter *es = new ExternalSorter();
	Sorter::randomizeSortingArray(mergeSortArray, SIZEOFMERGESORTINGARRAY);
	es->writeInitialDataArrayToFile(mergeSortArray, SIZEOFMERGESORTINGARRAY);
	es->mergeSort(BLOCKSIZE);
	int hi = 0;





	const int SIZEOFSORTINGARRAY = 100000;
	int *arrayToSort = new int[SIZEOFSORTINGARRAY];
	int* tempArray = new int[SIZEOFSORTINGARRAY];
	clock_t t;
	int firstIndexForSorting = 0;

	//BUBBLE SORT
	Sorter::randomizeSortingArray(arrayToSort, SIZEOFSORTINGARRAY);
	Sorter::printStartOfArray(arrayToSort);
	t = clock();
	Sorter::bubbleSort(arrayToSort, SIZEOFSORTINGARRAY);
	t = clock() - t;
	cout << "Bubble sort took "<< t/1000.00 << " seconds." << endl;
	Sorter::printStartOfArray(arrayToSort);

	//SELECTION SORT
	Sorter::randomizeSortingArray(arrayToSort, SIZEOFSORTINGARRAY);
	Sorter::printStartOfArray(arrayToSort);
	t = clock();
	Sorter::selectionSort(arrayToSort, SIZEOFSORTINGARRAY);
	t = clock() - t;
	cout << "Selection sort took " << t / 1000.00 << " seconds." << endl;
	Sorter::printStartOfArray(arrayToSort);

	//INSERTION SORT
	Sorter::randomizeSortingArray(arrayToSort, SIZEOFSORTINGARRAY);
	Sorter::printStartOfArray(arrayToSort);
	t = clock();
	Sorter::insertionSort(arrayToSort, SIZEOFSORTINGARRAY);
	t = clock() - t;
	cout << "Insertion sort took " << t / 1000.00 << " seconds." << endl;
	Sorter::printStartOfArray(arrayToSort);

	//SHELL SORT
	Sorter::randomizeSortingArray(arrayToSort, SIZEOFSORTINGARRAY);
	Sorter::printStartOfArray(arrayToSort);
	t = clock();
	Sorter::shellSort(arrayToSort, SIZEOFSORTINGARRAY);
	t = clock() - t;
	cout << "Shell sort took " << t / 1000.00 << " seconds." << endl;
	Sorter::printStartOfArray(arrayToSort);

	//MERGE SORT *EXHALE*
	Sorter::randomizeSortingArray(arrayToSort, SIZEOFSORTINGARRAY);
	Sorter::printStartOfArray(arrayToSort);
	t = clock();
	Sorter::mergeSort(arrayToSort, firstIndexForSorting, SIZEOFSORTINGARRAY - 1, SIZEOFSORTINGARRAY, tempArray);
	t = clock() - t;
	cout << "Merge sort took " << t / 1000.00 << " seconds." << endl;
	Sorter::printStartOfArray(arrayToSort);

	//QUICK SORT
	Sorter::randomizeSortingArray(arrayToSort, SIZEOFSORTINGARRAY);
	Sorter::printStartOfArray(arrayToSort);
	t = clock();
	Sorter::quickSort(arrayToSort, firstIndexForSorting, SIZEOFSORTINGARRAY-1, SIZEOFSORTINGARRAY);
	t = clock() - t;
	cout << "Quick sort took " << t / 1000.00 << " seconds." << endl;
	Sorter::printStartOfArray(arrayToSort);

	_getch();
	return 0;
}