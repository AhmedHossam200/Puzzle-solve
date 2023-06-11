using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
namespace solve44
{
    class Detect
    {

        public static int swaps_nummber = 0;// o(1)
        public static int sizeL = 0;// o(1)
        public static void calculateswaps(int[] arr, int startnode, int endnode) 
        {
            if (startnode < endnode)
            {
                int mid = (endnode + startnode) / 2;// o(1)
                calculateswaps(arr, startnode, mid);//o(1)
                calculateswaps(arr, mid + 1, endnode);//o(1)
                mergesswap(arr, startnode, mid, endnode);//o(1)
            }
        }
        static void mergesswap(int[] arr, int start, int midpoint, int end) // o(n)!!!!!!!!!!!!!or nlogn
        {
            sizeL = (midpoint - start) + 1;       //o(1)
            int[] tmp = new int[end - start + 1];//o(1)
            int i = start, j = midpoint + 1, k = 0;//o(1)
            do//o(n)
            {
                if (arr[i] < arr[j])
                {
                    tmp[k] = arr[i];//o(1)
                    sizeL--;//o(1)
                    k++;//o(1)
                    i++;//o(1)
                }
                else
                {
                    tmp[k] = arr[j];
                    swaps_nummber += sizeL;
                    k++;
                    j++;
                }
            } while (i <= midpoint && j <= end); //o(n)

            while (i <= midpoint)//o(n)
            {
                tmp[k] = arr[i];
                k++;
                i++;
            }
            while (j <= end)//o(n)
            {
                tmp[k] = arr[j];
                k++;
                j++;
                sizeL--;
            }
            k = 0;
            i = start;
            do
            {
                arr[i] = tmp[k];
                i++;
                k++;
            } while (k < tmp.Length && i <= end); //o(n)

        }
        public static bool detect_puzzle(int[,] puzzle2D, int puzzlesize, int zero_row)
        {
            //o(n*n)
            int[] n_puzzle = new int[puzzlesize * puzzlesize];//o(1)
            int pointer = 0;//o(1)
            int zero_place = 0;//o(1)
            for (int i = 0; i < puzzlesize; i++)//o(n)
            {
                for (int k = 0; k < puzzlesize; k++)//o(n)
                {
                    if (puzzle2D[i, k] == 0)//o(1)
                    {
                        zero_place = pointer;//o(1)
                    }
                    n_puzzle[pointer] = puzzle2D[i, k];//o(1)
                    pointer++;//o(1)
                }
            }

            swaps_nummber = 0;  //o(1)
            sizeL = 0;//o(1)

            if (puzzlesize % 2 == 0)//o(1)
            {

                calculateswaps(n_puzzle, 0, puzzlesize * puzzlesize - 1);
                swaps_nummber = (swaps_nummber - zero_place);//o(1)
                swaps_nummber += zero_row;         //o(1)            
                if (swaps_nummber % 2 != 0)//o(1)
                {
                    return true;//o(1)
                }
                else//o(1)
                {
                    return false;//o(1)
                }
            }
            
            else 
            {
                calculateswaps(n_puzzle, 0, puzzlesize * puzzlesize - 1);////o(1)
                swaps_nummber = (swaps_nummber - zero_place) ;

                if (swaps_nummber % 2 == 0)//o(1)
                {
                    return true;//o(1)
                }
                else
                {
                    return false;//o(1)
                }
            }

        }

    }
}
