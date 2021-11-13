using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    static class MatrixCalculation
    {
        static public decimal[] CalculateMatrix(int[,] mainElements, int[] additionalElements)
        {
            
            if (mainElements.GetLength(0) != mainElements.GetLength(1) || mainElements.GetLength(1) != additionalElements.Length)
            {
                return null;
            }
            
            decimal[] answers = new decimal[mainElements.GetLength(0)];
            decimal[,] changedLines = new decimal[mainElements.GetLength(0), mainElements.GetLength(1)];
            decimal[] changedAddElements = new decimal[additionalElements.Length];
            for (int i = 0; i < mainElements.GetLength(1); i++)
            {
                for (int j = 0; j < mainElements.GetLength(0); j++)
                {
                    changedLines[j, i] = mainElements[j, i];
                }
            }
            for (int i = 0; i < additionalElements.Length; i++)
            {
                changedAddElements[i] = additionalElements[i];
            }

            for (int i = 0; i < mainElements.GetLength(0); i++)
            {              
                for (int j = 1 + i; j < mainElements.GetLength(0); j++)
                {
                    for (int P = 0; P < mainElements.GetLength(0); P++)
                    {
                        if (changedLines[i, P] == 0 && changedLines[j, P] != 0)
                        {
                            for (int L = 0; L < mainElements.GetLength(0); L++)
                            {
                                changedLines[i, P] += changedLines[j, P];
                                changedLines[j, P] = changedLines[i, P] - changedLines[j, P];
                                changedLines[i, P] = changedLines[i, P] - changedLines[j, P];
                            }
                        }
                    }
                    decimal helper = changedLines[i, i];
                    int counter = 0;
                    for (int P = 0; P < mainElements.GetLength(0); P++)
                    {                        
                        if (changedLines[j, P] == 0)
                        {
                            counter++; 
                        }
                        if (counter >= mainElements.GetLength(0))
                        {
                            return null;
                        }
                    }
                    decimal coef = -1 * changedLines[j, i] / helper;
                    changedAddElements[j] += coef * changedAddElements[i];
                    for (int P = 0; P < mainElements.GetLength(0); P++)
                    {
                        changedLines[j, P] += coef * changedLines[i, P];
                        changedLines[j, P] = Math.Round(changedLines[j, P], 5);
                    }
                    if (EqualityChecker(changedLines, i, j))
                    {
                        return null;
                    }
                }
            }
            decimal[,] reversedChangedLines = new decimal[mainElements.GetLength(0), mainElements.GetLength(1)];
            for (int i = 0; i < mainElements.GetLength(0); i++)
            {
                for (int j = 0; j < mainElements.GetLength(1); j++)
                {
                    reversedChangedLines[i, j] = changedLines[mainElements.GetLength(1) - i - 1, mainElements.GetLength(0) - j - 1];
                }
            }

            decimal[] reversedAddElements = changedAddElements.Reverse().ToArray();
            for (int i = 0; i < mainElements.GetLength(1); i++)
            {
                decimal answersSum = 0;
                for (int j = 0; j < i + 1; j++)
                {
                    if (j == i)
                    {
                        if (reversedChangedLines[j,j] == 0)
                        {
                            return null;
                        }
                        answers[i] = (reversedAddElements[i] - answersSum) / reversedChangedLines[j, j];
                        break;
                    }
                    answersSum += answers[j] * reversedChangedLines[i, j];
                }
            }
            return answers.Reverse().ToArray();
        }        
        static public ValuePair<int[,], int[]> MatrixSorter(int[,] mainElements, int[] additionalElements)
        {
            ValuePair<int[,], int[]> sortedMatrix = new ValuePair<int[,], int[]>();
            List<int>[] matrixLines = new List<int>[mainElements.GetLength(1)];
            int[] changedAddElements;
            if (mainElements.GetLength(0) == 1 && mainElements.GetLength(1) == 1)
            {
                return new ValuePair<int[,], int[]>(mainElements,additionalElements);
            }
            for (int i = 0; i < mainElements.GetLength(1); i++)
            {
                matrixLines[i] = new List<int>();
            }
            for (int i = 0; i < mainElements.GetLength(1); i++)
            {
                for (int j = 0; j < mainElements.GetLength(0); j++)
                {
                    matrixLines[i].Add(mainElements[j, i]);
                }
            }
            for (int i = 0; i < mainElements.GetLength(1); i++)
            {
                if (matrixLines[i].All(element => element == 0))
                {
                    return null;
                }
            }

            int counter = -1;
            List<int> listOfCoordinates = new List<int>();
            for (int i = 0; i < mainElements.GetLength(1); i++)
            {
                if (i + 1 < mainElements.GetLength(1))
                {
                    for (int j = i + 1; j < mainElements.GetLength(1); j++)
                    {
                        decimal ratio = 0;
                        for (int L = 0; L < matrixLines[i].Count; L++)
                        {
                            if (matrixLines[i].All(element => element == 0) || matrixLines[j].All(element => element == 0))
                            {
                                continue;
                            }
                            else
                            {
                                if (matrixLines[j][L] == 0 && matrixLines[i][L] == 0)
                                {
                                    counter++;
                                }
                                else
                                {
                                    if (matrixLines[i][L] != 0 && matrixLines[j][L] != 0)
                                    {
                                        if (ratio == 0)
                                        {
                                            ratio = Convert.ToDecimal(matrixLines[j][L]) / Convert.ToDecimal(matrixLines[i][L]);
                                        }
                                        if (Convert.ToDecimal(matrixLines[j][L]) / Convert.ToDecimal(matrixLines[i][L]) == ratio)
                                        {
                                            counter++;
                                        }
                                    }
                                }
                            }
                        }
                        if (counter >= matrixLines[j].Count)
                        {
                            if (Convert.ToDecimal(additionalElements[j]) / Convert.ToDecimal(additionalElements[i]) != ratio)
                            {
                                return null;
                            }
                            for (int L = 0; L < matrixLines[j].Count; L++)
                            {
                                matrixLines[j][L] = 0;
                            }
                            listOfCoordinates.Add(j);
                        }
                        counter = 0;
                    }
                    counter = 0;

                }
            }
            List<int>[] ChangedMatrixLines = new List<int>[matrixLines.Length - listOfCoordinates.Count];
            changedAddElements = new int[matrixLines.Length - listOfCoordinates.Count];
            for (int i = 0; i < ChangedMatrixLines.Length; i++)
            {
                ChangedMatrixLines[i] = new List<int>();
            }
            counter = 0;
            for (int i = 0; i < matrixLines.Length; i++)
            {
                if (!listOfCoordinates.Contains(i))
                {
                    ChangedMatrixLines[counter] = matrixLines[i];
                    changedAddElements[counter] = additionalElements[i];
                    counter++;
                }
            }
            for (int i = 0; i < ChangedMatrixLines.Length; i++)
            {
                for (int j = 0; j < ChangedMatrixLines.Length; j++)
                {
                    for (int L = 0; L < ChangedMatrixLines[0].Count; L++)
                    {
                        if (matrixLines[i][L] == 0 && matrixLines[j][L] != 0)
                        {
                            changedAddElements[i] += changedAddElements[j];
                            for (int U = 0; U < mainElements.GetLength(0); U++)
                            {
                                ChangedMatrixLines[i][U] += ChangedMatrixLines[j][U];
                            }
                        }
                    }
                }
            }
            int[,] sortedMains = new int[ChangedMatrixLines[0].Count, ChangedMatrixLines.Length];
            for (int i = 0; i < sortedMains.GetLength(1); i++)
            {
                for (int j = 0; j < sortedMains.GetLength(0); j++)
                {
                    sortedMains[j, i] = ChangedMatrixLines[i][j];
                }
            }
            sortedMatrix.First = sortedMains;
            sortedMatrix.Second = changedAddElements;
            return sortedMatrix;
        }

        static public bool EqualityChecker(decimal[,] mainElements, int firstLineNumber, int secondLineNumber)
        {
            int Tcounter = 0;
            for (int L = 0; L < mainElements.GetLength(0); L++)
            {
                for (int U = 0; U < mainElements.GetLength(0); U++)
                {
                    if (L != U)
                    {
                        if (mainElements[firstLineNumber, L] != 0 &&
                            mainElements[secondLineNumber, L] != 0 &&
                            mainElements[firstLineNumber, U] != 0 &&
                            mainElements[secondLineNumber, U] != 0)
                        {
                            if (mainElements[firstLineNumber, L] / mainElements[secondLineNumber, L] ==
                                mainElements[firstLineNumber, U] / mainElements[secondLineNumber, U])
                            {
                                Tcounter++;
                            }
                        }
                    }
                }
            }
            if (Tcounter >= 2)
            {
                return true;
            }
            return false;
        }
    }
}
