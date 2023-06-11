using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace solve44
{
    class priority_queue
    {
        List<Node> queue;//o(1)
        public int length_queue;//o(1)
        public priority_queue()
        {
            queue = new List<Node>();
            length_queue = 0;//o(1)
        }
        
       
        public void clear()
        {
            queue.Clear();//o(1)
            length_queue = 0;//o(1)
        }
        
        public void swap(ref List<Node> qeueu, int index1, int index2) //o(1)
        {
            Node temp = qeueu[index1]; //o(1)
            qeueu[index1] = qeueu[index2];//o(1)
            qeueu[index2] = temp;//o(1)
        }
        
        public bool isempty()//o(1)
        {
            if (length_queue == 0)//o(1)
                return true;//o(1)
            else
                return false;//o(1)
        }

        public void add_to_priority_queue(Node puzzle, bool huristic)  //o(logn)
        {
            if (isempty())  
            {
                queue.Add(puzzle); //o(1)
                length_queue++;//o(1)
            }
            else          
            {
                queue.Add(puzzle);//o(1)        
                length_queue++;//o(1)
                heap_sort_adding(huristic); //o(logn)
            }
        }
        private void heap_sort_adding(bool huristic) //o(logn)
        {
            bool leaf;           //o(1)             
            int i = length_queue - 1;   //o(1)
            while (i > 1) 
            {
                if ((i) % 2 == 0) 
                    leaf = false;//o(1)
                else
                    leaf = true;//o(1)
                if (leaf)
                {
                    if (queue[i - 1].fn < queue[((i - 1) / 2) - 1].fn)
                    {
                        swap(ref queue, i - 1, ((i - 1) / 2) - 1); //o(1)   
                        i = ((i - 1) / 2); //o(1)
                    }
                    else if (queue[i - 1].fn == queue[((i - 1) / 2) - 1].fn)
                    {
                        if (huristic) //if  huristic true
                        {
                            if (queue[i - 1].hamming < queue[((i - 1) / 2) - 1].hamming)
                            {
                                swap(ref queue, i - 1, ((i - 1) / 2) - 1);//o(1)
                                i = ((i - 1) / 2);//o(1)
                            }
                            else
                            {
                                break;//o(1)
                            }
                        }
                        else
                        {
                            if (queue[i - 1].manhatten < queue[((i - 1) / 2) - 1].manhatten)
                            {
                                swap(ref queue, i - 1, ((i - 1) / 2) - 1);//o(1)
                                i = ((i - 1) / 2);//o(1)
                            }
                            else
                            {
                                break;//o(1)
                            }
                        }
                    }
                    else
                        break;//o(1)
                }
                else
                {
                    if (queue[i - 1].fn < queue[(i / 2) - 1].fn)
                    {
                        swap(ref queue, i - 1, (i / 2) - 1);//o(1)
                        i = (i / 2);//o(1)
                    }
                    else if (queue[i - 1].fn == queue[(i / 2) - 1].fn)
                    {
                        if (huristic)
                        {
                            if (queue[i - 1].hamming < queue[(i / 2) - 1].hamming)
                            {
                                swap(ref queue, i - 1, (i / 2) - 1);//o(1)
                                i = (i / 2);//o(1)
                            }
                            else
                            {
                                break;//o(1)
                            }
                        }
                        else
                        {
                            if (queue[i - 1].manhatten < queue[(i / 2) - 1].manhatten)
                            {
                                swap(ref queue, i - 1, (i / 2) - 1);//o(1)
                                i = (i / 2);//o(1)
                            }
                            else
                            {
                                break;//o(1)
                            }
                        }
                    }
                    else
                        break;//o(1)
                }
            }

            if (length_queue == 2)
            {
                if (queue[1].fn < queue[0].fn)
                {
                    swap(ref queue, 0, 1);//o(1)
                }
                else if (queue[1].fn == queue[0].fn)
                {
                    if (huristic)
                    {
                        if (queue[1].hamming < queue[0].hamming)
                        {
                            swap(ref queue, 0, 1);//o(1)
                        }
                    }
                    else
                    {
                        if (queue[1].manhatten < queue[0].manhatten)
                        {
                            swap(ref queue, 0, 1);
                        }
                    }
                }
            }
        }
        
        
        public Node remove_from_priority_queue(bool huristic)
        {
            int i = length_queue - 1;//o(1)
            Node temp = queue[0];//o(1)
            swap(ref queue, 0, i);//o(1)
            queue.RemoveAt(i);//o(1)
            length_queue--;//o(1)
            heap_sort_removing(huristic); //o(log n)
            return temp; //o(1)
        }
        private void heap_sort_removing(bool huristic)//o (logn)
        {
            int i = 1;//o(1)
            Node left;//o(1)
            Node right;//o(1)
            Node root;//o(1)
            int left_hn;//o(1)
            int right_hn;//o(1)
            int root_hn;//o(1)
            int left_fn;//o(1)
            int right_fn;//o(1)
            int root_fn;//o(1)

            while ((i * 2) <= length_queue && (i * 2) + 1 <= length_queue)
            {
                if (i * 2 <= length_queue && (i * 2) + 1 <= length_queue)
                {
                    left = queue[(i * 2) - 1];
                    right = queue[(i * 2)];
                    root = queue[i - 1];
                    if (huristic)
                    {
                        left_hn = left.hamming;
                        right_hn = right.hamming;
                        root_hn = root.hamming;
                    }
                    else
                    {
                        left_hn = left.manhatten;//o(1)
                        right_hn = right.manhatten;//o(1)
                        root_hn = root.manhatten;//o(1)
                    }

                    left_fn = left.fn;//o(1)

                    right_fn = right.fn;//o(1)
                    root_fn = root.fn;//o(1)

                    if (left_fn < right_fn && (left_fn < root_fn || (left_fn == root_fn && left_hn < root_hn)))
                    {
                        swap(ref queue, i - 1, (i * 2) - 1);
                        i = i * 2;//o(1)
                        continue;//o(1)
                    }
                    else if (right_fn < left_fn && (right_fn < root_fn || (right_fn == root_fn && right_hn < root_hn)))
                    {
                        swap(ref queue, i - 1, (i * 2));
                        i = (i * 2) + 1;//o(1)
                        continue;//o(1)
                    }
                    else if (left_fn == right_fn && (left_hn < right_hn && (left_fn < root_fn || (left_fn == root_fn && left_hn < root_hn))))
                    {
                        swap(ref queue, i - 1, (i * 2) - 1);
                        i = i * 2;//o(1)
                        continue;//o(1)
                    }
                    else if (right_fn == left_fn && (right_hn < left_hn && (right_fn < root_fn || (right_fn == root_fn && right_hn < root_hn))))
                    {
                        swap(ref queue, i - 1, (i * 2));
                        i = (i * 2) + 1;//o(1)
                        continue;//o(1)
                    }
                    else if (right_fn == left_fn && (right_hn == left_hn && (right_fn < root_fn || (right_fn == root_fn))))
                    {
                        swap(ref queue, i - 1, (i * 2));
                        i = (i * 2) + 1;//o(1)
                        continue;//o(1)
                    }
                    else
                    {
                        break;//o(1)
                    }
                }


            }
            if ((i * 2) + 1 > length_queue && i * 2 <= length_queue)
            {
                left = queue[(i * 2) - 1];
                root = queue[i - 1];
                
                if (huristic)
                {
                    left_hn = left.hamming;//o(1)
                    root_hn = root.hamming;//o(1)
                }
                else
                {
                    left_hn = left.manhatten;//o(1)
                    root_hn = root.manhatten;//o(1)
                }
                
                left_fn = left.fn;//o(1)

                root_fn = root.fn;//o(1)

                if (left_fn < root_fn || (left_fn == root_fn && left_hn < root_hn))
                {
                    swap(ref queue, i - 1, (i * 2) - 1);
                    i = i * 2;//o(1)
                }
                else
                {
                }

            }
        }
    }
}
