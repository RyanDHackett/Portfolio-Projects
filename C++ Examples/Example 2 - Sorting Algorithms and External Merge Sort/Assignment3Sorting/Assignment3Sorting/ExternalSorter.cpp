//ExternalSorter class implementation file
#include "ExternalSorter.h"
using namespace std;

void ExternalSorter::mergeSort(int numberOfRecordsToRead)
{
	if (!initialReadAndSort(numberOfRecordsToRead))
	{
		readAndMerge(numberOfRecordsToRead);
	}

}

bool ExternalSorter::initialReadAndSort(int numberOfRecordsToRead)
{
	int lineNumber = 1;
	DATA *records = new DATA[numberOfRecordsToRead];
	int record = 0;
	int recordCount = 0;
	ifstream dataFileStream;
	ofstream file1stream;
	ofstream file2stream;
	//char ch;
	char ch = '0';
	try {
		dataFileStream.open(DATAFILE);
		file1stream.open(FILE1);
		file2stream.open(FILE2);
		EvenOrOdd fileToPrintto = odd;
		bool printArray = true;
		bool ignore = false;
		string recordString = "";
		bool firstpass = true;
		while (true)
		{
			//Third condition is true on first pass, hence the second condition
			//If end of file is reached early, enters final printing logic, and breaks at the end.
			if ((printArray && lineNumber > 1 && (lineNumber - 1) % numberOfRecordsToRead == 0) || ch == EOF)
			{
				if (recordCount == 0)
				{
					if (firstpass)
					{
						//Second file was never written to
						cout << "Array is sorted and in file 1. Merge sort complete..." << endl;
						return true;
					}
					//Block size was perfectly divisable, exit now
					return false;
				}
				//Flag to ensure an array isn't printed twice..
				printArray = false;
				//sort the array
				ExternalSorter::quickSort(records, 0, recordCount - 1, recordCount);

				//print the array to the right file
				if (fileToPrintto == odd)
				{
					//print to file 1
					writeDataArrayToFile(file1stream,records, recordCount, FILE1);
					fileToPrintto = even;
					if ((lineNumber-1) < numberOfRecordsToRead)
					{
						//Block size was bigger than the array size
						cout << "Array is sorted and in file 1. Merge sort complete..." << endl;
						return true;
					}
					//both files have been written to, it's no longer the first pass
					firstpass = false;
				}
				else if(fileToPrintto == even)
				{
					writeDataArrayToFile(file2stream,records, recordCount, FILE2);
					fileToPrintto = odd;
				}
				recordCount = 0;
				if (ch == EOF)
				{
					break;
				}
			}

			ch = dataFileStream.get();
			switch (ch)
			{
			case ',':
				ignore = true;
				break;
			case '\n':
				ignore = false;
				record = fast_atoi(recordString.c_str());
				records[recordCount].data = record;
				records[recordCount].lineNumber = lineNumber;
				recordString = "";
				++recordCount;
				++lineNumber;
				printArray = true;
				break;
			default:
				if (!ignore)
				{
					recordString += ch;
				}
				break;
			}


		}

	}
	catch (ios_base::failure fail) 
	{
		cout << "File IO error" << endl;
	}


}

bool ExternalSorter::readAndMerge(int blockSize)
{
	DATA firstFileData;
	DATA secondFileData;
	ifstream inputStreamOne;
	ifstream inputStreamTwo;
	ofstream outputStream;
	int inputOneIndex = 1;
	int inputTwoIndex = 1;
	int indexToReadTo = blockSize;
	bool readFromFileOne = true;
	bool inputFilesArentEmpty = true;
	bool unsorted = true;
	bool earlyEndOfFileOne = false;
	bool earlyEndOfFileTwo = false;
	EvenOrOdd inputFiles = odd;
	EvenOrOdd outputFile = odd;
	try
	{
		while (unsorted)
		{
			//While the current input files have records in them
			while (inputFilesArentEmpty)
			{
				//Check for one of the input files to be empty, meaning the sort is finished
				//The sorted data is always in the input file that isn't empty
				if (inputStreamOne.peek() == EOF)//if the first file is empty
				{
					if (inputFiles == odd)
					{
						cout << "Sort complete! Sorted records are in file 2." << endl;
					}
					else
					{
						cout << "Sort complete! Sorted records are in file 4." << endl;
					}
					unsorted = false;
					break;
				}
				else if (inputStreamTwo.peek() == EOF)//if the second file is empty
				{
					if (inputFiles == odd)
					{
						cout << "Sort complete! Sorted records are in file 3." << endl;
					}
					else
					{
						cout << "Sort complete! Sorted records are in file 1." << endl;
					}
					unsorted = false;
					break;
				}
				//open streams to the appropriate input files
				if (inputFiles == odd)
				{
					inputStreamOne.open(FILE1);
					inputStreamTwo.open(FILE2);
				}
				else
				{
					inputStreamOne.open(FILE3);
					inputStreamTwo.open(FILE4);
				}
				//Empty the output files
				if (inputFiles == odd)//empty file 3 and 4 
				{
					outputStream.open(FILE3, ofstream::out | ofstream::trunc);
					outputStream.close();
					outputStream.open(FILE4, ofstream::out | ofstream::trunc);
					outputStream.close();
				}
				else//empty 1 and 2
				{
					outputStream.open(FILE1, std::ofstream::out | std::ofstream::trunc);
					outputStream.close();
					outputStream.open(FILE2, std::ofstream::out | std::ofstream::trunc);
					outputStream.close();
				}

				//for the number of records specified...from inputStreamOne and inputStreamTwo
				while (inputOneIndex != indexToReadTo && inputTwoIndex != indexToReadTo)
				{
					//If readFromFileOne, read a line from inputStreamOne into DATA firstFileData
					if (readFromFileOne)
					{
						//Check to see if end of file is reached early
						if (inputStreamOne.peek() != EOF)
						{
							firstFileData = readLine(inputStreamOne);
							inputOneIndex++;
						}
						else
						{
							earlyEndOfFileOne = true;
							break;
						}
					}
					else//Else read a line from inputStreamTwo into DATA secondFileData
					{
						//Check to see if end of file is reached early
						if (inputStreamTwo.peek() != EOF)
						{
							secondFileData = readLine(inputStreamTwo);
							inputTwoIndex++;
						}
						else
						{
							earlyEndOfFileTwo = true;
							break;
						}
					}

					//compare firstFileData and secondFileData, then output the smallest to output
					if (firstFileData.data <= secondFileData.data)//first file data is smaller
					{
						writeLine(outputStream, firstFileData);
						//tell the program to read from input file 1 on the next pass
						readFromFileOne = true;
					}
					else//second file data is smaller
					{
						writeLine(outputStream, secondFileData);
						//tell the program to read from input file 2 on the next pass
						readFromFileOne = false;
					}

				}
				//reset readFromFileOne
				readFromFileOne = true;
				//Check to see which file has the extra records
				if (inputTwoIndex != indexToReadTo || earlyEndOfFileOne)//The extra is in the second file
				{
					//The second file's data has already been read from before, no need to read again this time
					writeLine(outputStream, secondFileData);
					inputTwoIndex++;
					//Read all extra from second input and write to output file
					while (inputTwoIndex != indexToReadTo)
					{
						secondFileData = readLine(inputStreamTwo);
						writeLine(outputStream, secondFileData);
						inputTwoIndex++;
					}
				}
				else if (inputOneIndex != indexToReadTo || earlyEndOfFileTwo)//The extra is in the first file
				{
					//The second file's data has already been read from before, no need to read again this time
					writeLine(outputStream, firstFileData);
					inputOneIndex++;
					//Read all extra from first input and write to output file
					while (inputOneIndex != indexToReadTo)
					{
						secondFileData = readLine(inputStreamTwo);
						writeLine(outputStream, secondFileData);
						inputTwoIndex++;
					}
				}
				//close current output file before changing it
				outputStream.close();

				if (outputFile == odd)//1 or 3
				{
					outputFile = even;
					//open outputstream to appropriate even, in append mode
					if (inputFiles == odd)//1 and 2
					{
						outputStream.open(FILE4, ofstream::out | ofstream::app);
					}
					else//3 and 4
					{
						outputStream.open(FILE2, ofstream::out | ofstream::app);
					}
				}
				else//2 or 4
				{
					outputFile = odd;
					//open outputstream to appropriate odd, in append mode
					if (inputFiles == odd)//1 and 2
					{
						outputStream.open(FILE3, ofstream::out | ofstream::app);
					}
					else//3 and 4
					{
						outputStream.open(FILE1, ofstream::out | ofstream::app);
					}
				}
				//move the index to read to
				indexToReadTo += blockSize;
			}
			//if sort is complete, clean up and break
			if (!unsorted)
			{
				inputStreamOne.close();
				inputStreamTwo.close();
				outputStream.close();
				break;
			}
			//double the blocksize
			blockSize *= 2;

			//reset index to read to
			indexToReadTo = blockSize;

			//close current input streams
			inputStreamOne.close();
			inputStreamTwo.close();

			//open inputstreams to the appropriate next pair
			if (inputFiles == odd)//1 and 2
			{
				inputFiles = even;
				//open to next pair
				inputStreamOne.open(FILE3);
				inputStreamTwo.open(FILE4);
			}
			else//3 and 4
			{
				inputFiles = odd;
				//open to next pair
				inputStreamOne.open(FILE1);
				inputStreamTwo.open(FILE2);
			}
		}
	}
	catch(ios_base::failure fail)
	{
		cout << "File IO error" << endl;
	}
	return true;
}

ExternalSorter::DATA ExternalSorter::readLine(ifstream &input)
{
	char ch;
	string recordString = "";
	DATA data;
	while (true)
	{
		ch = input.get();
		switch (ch)
		{
		case ',':
			data.data = fast_atoi(recordString.c_str());
			recordString = "";
			break;
		case '\n':
			data.lineNumber = fast_atoi(recordString.c_str());
			return data;
			break;
		default:
			recordString += ch;
			break;
		}
	}
	return data;
	
}

void ExternalSorter::writeLine(ofstream &output, DATA data)
{
	output << data.data << ',' << data.lineNumber << endl;
}


void ExternalSorter::writeInitialDataArrayToFile(int theArray[], int arraySize)
{
	ofstream output;
	output.open(DATAFILE);
	for (int i = 0; i < arraySize; i++)
	{
		output << setw(5) << setfill('0') << theArray[i] << ","<< i+1 <<endl;
	}
	output.close();
}

void ExternalSorter::writeDataArrayToFile(ofstream &output, DATA theArray[], int arraySize, const char *fileName)
{
	for (int i = 0; i < arraySize; i++)
	{
		output << setw(5) << setfill('0') << theArray[i].data << "," << theArray[i].lineNumber << endl;
	}
}

//http://stackoverflow.com/questions/16826422/c-most-efficient-way-to-convert-string-to-int-faster-than-atoi
int ExternalSorter::fast_atoi(const char * str)
{
	int val = 0;
	while (*str) {
		val = val * 10 + (*str++ - '0');
	}
	return val;
}

void ExternalSorter::quickSort(DATA *array, int beg, int end, int size)
{
	if (beg<end)
	{
		int pivot = partition(array, beg, end);   //Calling Procedure to Find Pivot

		quickSort(array, beg, pivot - 1, size);         // Subsort left (Recursion)

		quickSort(array, pivot + 1, end, size);	      // Subsort right (Recursion)
	}
}

int ExternalSorter::partition(DATA *array, int beg, int end)
{
	int p = beg;
	DATA pivot = array[beg];
	int location;

	for (location = beg + 1; location <= end; location++)
	{
		if (pivot.data>array[location].data)
		{
			array[p] = array[location];
			array[location] = array[p + 1];
			array[p + 1] = pivot;
			p++;
		}
	}
	return p;
}


