using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bioinfarmatics.Library.Utils
{
    public class SequenceAlignment
    {
        public SequenceAlignment() { }

        public NeedlemanWunschModel NeedlemanWunsch(char[] seqOne, char[] seqTwo, int match, int misMatch, int gap)
        {
            Array.Resize<char>(ref seqOne, seqOne.Length + 1);
            Array.Resize<char>(ref seqTwo, seqTwo.Length + 1);

            for (int i = seqOne.Length - 2; i >= 0; i--)
                seqOne[i + 1] = seqOne[i];
            for (int i = seqTwo.Length - 2; i >= 0; i--)
                seqTwo[i + 1] = seqTwo[i];
            seqOne[0] = seqTwo[0] = '*';

            var columns = seqOne.Length;
            var rows = seqTwo.Length;

            int[,] matrix = new int[rows, columns];

            #region FillMatrix        
            matrix[0, 0] = 0;

            for (int i = 1; i < columns; i++)
                matrix[0, i] = matrix[0, i - 1] + gap;
            for (int i = 1; i < rows; i++)
                matrix[i, 0] = matrix[i - 1, 0] + gap;

            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < columns; j++)
                {
                    if (seqOne[j] == seqTwo[i])
                    {
                        matrix[i, j] = matrix[i - 1, j - 1] + match;
                    }
                    else
                    {
                        var mMpoint = matrix[i - 1, j - 1] + misMatch;
                        var gapPointOne = matrix[i - 1, j] + gap;
                        var gapPointTwo = matrix[i, j - 1] + gap;

                        if (gapPointOne >= gapPointTwo)
                        {
                            if (gapPointOne > mMpoint)
                                matrix[i, j] = gapPointOne;
                            else if (gapPointOne < mMpoint)
                                matrix[i, j] = mMpoint;
                        }
                        else if (gapPointOne < gapPointTwo)
                        {
                            if (gapPointTwo > mMpoint)
                                matrix[i, j] = gapPointTwo;
                            else if (gapPointTwo < mMpoint)
                                matrix[i, j] = mMpoint;
                        }
                        else
                        {
                            if (gapPointOne >= mMpoint)
                                matrix[i, j] = gapPointOne;
                            else
                                matrix[i, j] = mMpoint;
                        }
                    }
                }
            }
            #endregion

            #region FindPath

            var currentItem = matrix[rows - 1, columns - 1];
            int[,] pathMatrix = new int[rows, columns];
            var tempRow = rows - 1;
            var tempColumn = columns - 1;
            pathMatrix[tempRow, tempColumn] = 1;

            #region DrawPath
            while (currentItem != 0)
            {
                if (seqOne[tempColumn] == seqTwo[tempRow])
                {
                    tempRow--; tempColumn--;
                    currentItem = matrix[tempRow, tempColumn];
                    pathMatrix[tempRow, tempColumn] = 1;
                }
                else
                {
                    var mMpoint = matrix[tempRow - 1, tempColumn - 1];
                    var gapPointOne = matrix[tempRow - 1, tempColumn];
                    var gapPointTwo = matrix[tempRow, tempColumn - 1];

                    if (gapPointOne >= gapPointTwo)
                    {
                        if (gapPointOne > mMpoint)
                        {
                            tempRow--;
                            currentItem = matrix[tempRow, tempColumn];
                            pathMatrix[tempRow, tempColumn] = 1;
                        }

                        else if (gapPointOne < mMpoint)
                        {
                            tempRow--; tempColumn--;
                            currentItem = matrix[tempRow, tempColumn];
                            pathMatrix[tempRow, tempColumn] = 1;
                        }

                    }
                    else if (gapPointOne < gapPointTwo)
                    {
                        if (gapPointTwo > mMpoint)
                        {
                            tempColumn--;
                            currentItem = matrix[tempRow, tempColumn];
                            pathMatrix[tempRow, tempColumn] = 1;
                        }
                        else if (gapPointTwo < mMpoint)
                        {
                            tempRow--; tempColumn--;
                            currentItem = matrix[tempRow, tempColumn];
                            pathMatrix[tempRow, tempColumn] = 1;
                        }
                    }
                    else
                    {
                        if (gapPointOne >= misMatch)
                        {
                            tempRow--;
                            currentItem = matrix[tempRow, tempColumn];
                            pathMatrix[tempRow, tempColumn] = 1;
                        }
                        else
                        {
                            tempRow--; tempColumn--;
                            currentItem = matrix[tempRow, tempColumn];
                            pathMatrix[tempRow, tempColumn] = 1;
                        }
                    }
                }
            }
            #endregion
            #endregion

            #region NewSequences
            var newSeqOne = new List<char>();
            var newSeqTwo = new List<char>();

            for (int i = 1; i < seqOne.Length; i++)
            {
                var gapCount = 0;
                for (int j = 1; j < seqTwo.Length; j++)
                {
                    if (pathMatrix[j, i] == 1)
                        gapCount++;
                }
                newSeqOne.Add(seqOne[i]);
                while (--gapCount != 0)
                    newSeqOne.Add('-');
            }

            for (int i = 1; i < seqTwo.Length; i++)
            {
                var gapCount = 0;
                for (int j = 1; j < seqOne.Length; j++)
                {
                    if (pathMatrix[i, j] == 1)
                        gapCount++;
                }
                newSeqTwo.Add(seqTwo[i]);
                while (--gapCount != 0)
                    newSeqTwo.Add('-');
            }
            #endregion

            return new NeedlemanWunschModel{
                Matrix = matrix,
                PathMatrix = pathMatrix,
                SequenceOne = newSeqOne.ToArray<char>(),
                SequenceTwo = newSeqTwo.ToArray<char>()
            };
        }

    }

    public class NeedlemanWunschModel
    {
        public int[,] Matrix { get; set; }
        public int[,] PathMatrix { get; set; }
        public char[] SequenceOne { get; set; }
        public char[] SequenceTwo { get; set; }
    }
}
