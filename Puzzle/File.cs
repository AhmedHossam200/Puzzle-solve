using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace solve44
{
    class File //o (n*n)
    {
        String pathfile;  //o (1)         
        String atline;//o (1)                         
        long Readbytes = 0; //o (1)                     
        int sizeofpuzzle;    //o (1)              
        int[,] puzzle_arr_2D;//o (1)
        int arrofpointer = 0;//o (1)
        int zeroinplace;//o (1)
        int[] zeroinXY;//o (1)

        public File(String file_path)//o (1)
        {
            this.pathfile = file_path;
            zeroinXY = new int[2];
        }
        
       
        public int get_puzzle_size()//o (1)
        {
            return sizeofpuzzle;
        }
        
        public int[,] get_puzzle2D()//o (1)
        {
            return puzzle_arr_2D;
        }
        
        public long read_from_specific_pos(long position)//o (n*n)
        {
            Readbytes = 0;//o (1)
            int newLineBytes = System.Environment.NewLine.Length;   //o (1)  

            using (var sr = new StreamReader(pathfile))//o(n)*o(n)       
            {
                sr.BaseStream.Seek(position, SeekOrigin.Begin);//o(1)

                atline = sr.ReadLine();//o(1)
                Readbytes += atline.Length + newLineBytes;//o(1)

                sizeofpuzzle = int.Parse(atline);   //o(1)                            

                puzzle_arr_2D = new int[sizeofpuzzle, sizeofpuzzle];//o(1)
                arrofpointer = 0;                                           
                for (int i = 0; i < sizeofpuzzle; i++)     //o (n)*o (n)                    
                {
                    atline = sr.ReadLine();//o(1)
                    Readbytes += atline.Length + newLineBytes;  //o(1)    
                    string[] splite = atline.Split(' ');  //o(1)         
                    for (int l = 0; l < sizeofpuzzle; l++)         
                    {
                        if (int.Parse(splite[l]) == 0)//o(1)
                        {
                            zeroinplace = arrofpointer;//o(1)
                            zeroinXY[0] = i;//o(1)
                            zeroinXY[1] = l;//o(1)
                        }
                        
                        puzzle_arr_2D[i, l] = int.Parse(splite[l]);//o(1)
                        arrofpointer++;//o(1)

                    }
                }

                position += Readbytes;//o(1)
                sr.Close();//o(1)
            }
            return position;//o(1)
        }
        public bool not_end(long pos)//o (1)
        {
            StreamReader sr = new StreamReader(pathfile);//o(1)
            sr.BaseStream.Seek(pos, SeekOrigin.Begin);//o(1)
            if (sr.Peek() == -1)//o(1)
            {
                sr.Close();//o(1)
                return false;//o(1)
            }
            else
            {
                sr.Close();//o(1)
                return true;//o(1)
            }
            if (sr.Peek() == -1)
            {
                sr.Close();//o(1)
                return false;//o(1)
            }
            else
            {
                sr.Close();//o(1)
                return true;//o(1)
            }
        }
       
        public int[] get_zero_place()//o (1)
        {
            return zeroinXY;//o(1)
        }
    }
}
