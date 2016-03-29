//Sorter class implementation file

#include "Sorter.h"
using namespace std;

void Sorter::bubbleSort(int* &arrayToSort, int sizeOfArray)
{
	int outer;
	int inner;
	for (outer = sizeOfArray - 1; outer > 0; outer--)
	{      
		for (inner = 0; inner < outer; inner++) 
		{       
			if (arrayToSort[inner] > arrayToSort[inner + 1])
			{                  
				int temp = arrayToSort[inner];
				arrayToSort[inner] = arrayToSort[inner + 1];
				arrayToSort[inner + 1] = temp;
			}        
		}    
	}
}

void Sorter::selectionSort(int* &arrayToSort, int sizeOfArray)
{
	int outer;
	int inner;
	int min;    
	for (outer = 0; outer < sizeOfArray - 1; outer++)
	{        
		min = outer;        
		for (inner = outer + 1; inner < sizeOfArray; inner++)
		{            
			if (arrayToSort[inner] < arrayToSort[min])
			{ 
				min = inner; 
			}	
		}
		int temp = arrayToSort[outer];
		arrayToSort[outer] = arrayToSort[min];
		arrayToSort[min] = temp;
	}

}

void Sorter::insertionSort(int* &arrayToSort, int sizeOfArray)
{
	int i;
	int j;
	int tmp;
	for (i = 1; i < sizeOfArray; i++)
	{
		j = i;
		while (j > 0 && arrayToSort[j - 1] > arrayToSort[j])
		{
			tmp = arrayToSort[j];
			arrayToSort[j] = arrayToSort[j - 1];
			arrayToSort[j - 1] = tmp;
			j--;
		} //end of while loop
	} //end of for loop


}

void Sorter::shellSort(int* &arrayToSort, int sizeOfArray)
{
	int i;
	int j; 
	int increment; 
	int temp;
	for (increment = sizeOfArray / 2; increment > 0; increment /= 2)
	{
		for (i = increment; i<sizeOfArray; i++)
		{
			temp = arrayToSort[i];
			for (j = i; j >= increment; j -= increment)
			{
				if (temp < arrayToSort[j - increment])
				{
					arrayToSort[j] = arrayToSort[j - increment];
				}
				else
				{
					break;
				}
			}
			arrayToSort[j] = temp;
		}
	}
}

void Sorter::mergeSort(int arrayToSort[], int first, int last, int sizeOfArray, int tempArray[])
{
	int mid;
	if (first < last)
	{
		mid = (first + last) / 2;
		mergeSort(arrayToSort, first, mid,sizeOfArray,tempArray);
		mergeSort(arrayToSort, mid + 1, last,sizeOfArray,tempArray);
		merge(arrayToSort, first, last, mid,sizeOfArray,tempArray);
	}
	return;
}

void Sorter::printStartOfArray(int theArray[])
{
	for (int i = 0; i < 10; i++)
	{
		cout << theArray[i] << " ";
	}
	cout << endl;
}

void Sorter::merge(int arrayToSort[], int first, int last, int mid, int sizeOfArray,int tempArray[])
{
	int i, j, k;
	i = first;
	k = first;
	j = mid + 1;
	while (i <= mid && j <= last)
	{
		if (arrayToSort[i] < arrayToSort[j])
		{
			tempArray[k] = arrayToSort[i];
			k++;
			i++;
		}
		else
		{
			tempArray[k] = arrayToSort[j];
			k++;
			j++;
		}
	}
	while (i <= mid)
	{
		tempArray[k] = arrayToSort[i];
		k++;
		i++;
	}
	while (j <= last)
	{
		tempArray[k] = arrayToSort[j];
		k++;
		j++;
	}
	for (i = first; i < k; i++)
	{
		arrayToSort[i] = tempArray[i];
	}
}

int Sorter::partition(int *array, int beg, int end)          //Function to Find Pivot Point
{
	int p = beg, pivot = array[beg], location;

	for (location = beg + 1; location <= end; location++)
	{
		if (pivot>array[location])
		{
			array[p] = array[location];
			array[location] = array[p + 1];
			array[p + 1] = pivot;

			p++;
		}
	}
	return p;
}


void Sorter::quickSort(int *array, int beg, int end, int size)
{
	if (beg<end)
	{
		int pivot = partition(array, beg, end);   //Calling Procedure to Find Pivot

		quickSort(array, beg, pivot - 1, size);         // Subsort left (Recursion)

		quickSort(array, pivot + 1, end, size);	      // Subsort right (Recursion)
	}
}

void Sorter::randomizeSortingArray(int* &arrayToSort, int sizeOfArray)
{
	srand(time(NULL));
	for (int i = 0; i < sizeOfArray; i++)
	{
		arrayToSort[i] = rand();
	}
}
