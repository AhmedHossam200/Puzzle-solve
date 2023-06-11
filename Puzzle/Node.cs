using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace solve44
{
    class Node
    {
        public String puzzle_string;  //o(1)
        public String node_direction = "";//o(1)
        public int manhatten;//o(1)
        public int hamming;//o(1)
        public int moves;//o(1)
        public int fn;//o(1)
        public int[,] puzzle2D;//o(1)
        public int puzzle_size; //o(1)            
        public int[] zero_placeXY;//o(1)
        public bool flag_open_or_closed;//o(1)
        public Node parent;//o(1)

        public Node()//o(1)
        {
        }
        public Node(Node temp)//o(n)*o(n)
        {
            parent = new Node();
            parent = temp;
            
            this.manhatten = temp.manhatten;
            hamming = temp.hamming;
            moves = temp.moves;
            
            this.fn = temp.fn;
            this.puzzle_string = temp.puzzle_string;
            
            puzzle_size = temp.puzzle_size;
            puzzle2D = new int[puzzle_size, puzzle_size];
            
            for (int i = 0; i < puzzle_size; i++)//o(n)*o(n)
            {
                for (int j = 0; j < puzzle_size; j++)
                {
                    puzzle2D[i, j] = temp.puzzle2D[i, j];
                }
            }
            
            zero_placeXY = new int[2];//o(1)
            zero_placeXY[0] = temp.zero_placeXY[0];//o(1)
            zero_placeXY[1] = temp.zero_placeXY[1];//o(1)

            this.flag_open_or_closed = true;//o(1)
        }
        public Node(int[,] puzzle2D, int size, int moves, int[] zero, String puzzle_string, bool huristic)
        {
            zero_placeXY = new int[2];//o(1)
            this.zero_placeXY[0] = zero[0];//o(1)
            this.zero_placeXY[1] = zero[1];//o(1)
            puzzle_size = size;//o(1)
            this.puzzle2D = new int[puzzle_size, puzzle_size];//o(1)
            for (int i = 0; i < puzzle_size; i++)//o(n)
            {
                for (int j = 0; j < puzzle_size; j++) //o(n)
                {
                    this.puzzle2D[i, j] = puzzle2D[i, j];
                }
            }
            this.moves = moves;//o(1)
            calculate_hamming();//o(n*n)/////
            calculate_manhatten_distance();//o(n*n)//////
            if (huristic)//o(1)
            {
                fn = this.moves + hamming;
            }
            else
            {
                fn = this.moves + manhatten;
            }
            this.puzzle_string = puzzle_string;
            this.flag_open_or_closed = true;
        }

        public bool isequal(ref Node dest)//o(1)
        {
            if (this.puzzle_string.Equals(dest.puzzle_string))
            {
                return true;
            }
            return false;
        }// cmp 2 nodes by thier puzzle string 
        public void calculate_fn(bool huristic)//o(n*n)
        {
            if (huristic)
            {
                calculate_hamming(); //o(N*N)
                fn = moves + hamming;
            }
            else
            {
                calculate_manhatten_distance(); //o(N*N)
                fn = moves + manhatten;
            }
        }
        
        public void calculate_hamming()//o(n*n)
        {
            int counter = 0;
            for (int k = 0; k < puzzle_size; k++)  //o(n)
            {
                for (int m = 1; m <= puzzle_size; m++) //o(n)
                {
                    if (puzzle_size * k + m != puzzle2D[k, m - 1] && puzzle2D[k, m - 1] != 0)
                    {
                        counter++;
                    }
                }
            }
            hamming = counter;
          
        }
        
        public void calculate_manhatten_distance()//o(n*n)
        {
            int manhattens = 0;
            for (int k = 0; k < puzzle_size; k++) //o(n)
            {
                for (int m = 0; m < puzzle_size; m++) //o(n)
                {
                    int value = this.puzzle2D[k, m];//o(1)
                    if (value != 0)
                    {
                        int x = (value - 1) / this.puzzle_size; //o(1)
                        int y = (value - 1) % this.puzzle_size; //o(1)
                        int dx = (k - x); //o(1)
                        int dy = (m - y); //o(1)
                        manhattens += Math.Abs(dx) + Math.Abs(dy);//o(1)
                    }
                }
            }
            manhatten = manhattens;
        }

        public void display_node()//o(n*n)
        {
            for (int i = 0; i < puzzle_size; i++) //o(n)
            {
                for (int j = 0; j < puzzle_size; j++) //o(n)
                {
                    Console.Write(this.puzzle2D[i, j] + " ");
                }
                Console.WriteLine();
            }
            
        }

        public void move_up(bool huristic) //o(N*N)
        {            
            int temp_swap = puzzle2D[zero_placeXY[0] - 1, zero_placeXY[1]];//o(1)
            puzzle2D[zero_placeXY[0] - 1, zero_placeXY[1]] = puzzle2D[zero_placeXY[0], zero_placeXY[1]];
            puzzle2D[zero_placeXY[0], zero_placeXY[1]] = temp_swap;//o(1)

            int index_zero = (this.puzzle_size * zero_placeXY[0] + zero_placeXY[1]);//o(1)
            int index = ((this.puzzle_size * (zero_placeXY[0] - 1)) + zero_placeXY[1]);//o(1)

            string[] temp = puzzle_string.Split(' ');//o(1)
            string xx = temp[index_zero];//o(1)
            temp[index_zero] = temp[index];//o(1)
            temp[index] = xx;//o(1)

            puzzle_string = "";
            for (int i = 0; i < (this.puzzle_size) * (this.puzzle_size); i++)//o(n*n)
            {
                puzzle_string += temp[i] + " "; //o(1)
            }
            puzzle_string.Remove(((this.puzzle_size) * (this.puzzle_size)));
            
            this.moves++;
            
            calculate_fn(huristic); //o(N*N)
            zero_placeXY[0]--;
        }
        
        public void move_down(bool huristic)
        {
            int temp_swap = puzzle2D[zero_placeXY[0] + 1, zero_placeXY[1]]; //o(1)
            puzzle2D[zero_placeXY[0] + 1, zero_placeXY[1]] = puzzle2D[zero_placeXY[0], zero_placeXY[1]]; //o(1)
            puzzle2D[zero_placeXY[0], zero_placeXY[1]] = temp_swap; //o(1)

            int index_zero = (this.puzzle_size * zero_placeXY[0] + zero_placeXY[1]); //o(1)
            int index = ((this.puzzle_size * (zero_placeXY[0] + 1)) + zero_placeXY[1]); //o(1)

            string[] temp = puzzle_string.Split(' ');
            string xx = temp[index_zero];
            temp[index_zero] = temp[index];
            temp[index] = xx;
            
            puzzle_string = "";
            for (int i = 0; i < (this.puzzle_size) * (this.puzzle_size); i++)//o(n*n)
            {
                puzzle_string += temp[i] + " "; //o(1)
            }
            puzzle_string.Remove(((this.puzzle_size) * (this.puzzle_size)));
            
            this.moves++;
            calculate_fn(huristic); //o(n*n)
            zero_placeXY[0]++;
        }
        
        public void move_right(bool huristic)
        {
            int temp_swap = puzzle2D[zero_placeXY[0], zero_placeXY[1] + 1]; //o(1)
            puzzle2D[zero_placeXY[0], zero_placeXY[1] + 1] = puzzle2D[zero_placeXY[0], zero_placeXY[1]]; //o(1)
            puzzle2D[zero_placeXY[0], zero_placeXY[1]] = temp_swap;//o(1)

            int index_zero = (this.puzzle_size * zero_placeXY[0] + zero_placeXY[1]); //o(1)
            int index = ((this.puzzle_size * (zero_placeXY[0])) + zero_placeXY[1] + 1); //o(1)

            string[] temp = puzzle_string.Split(' '); 
            string xx = temp[index_zero]; //o(1)
            temp[index_zero] = temp[index]; //o(1)
            temp[index] = xx; //o(1)
            puzzle_string = "";
            for (int i = 0; i < (this.puzzle_size) * (this.puzzle_size); i++)//o(n*n)
            {
                puzzle_string += temp[i] + " ";
            }
            puzzle_string.Remove(((this.puzzle_size) * (this.puzzle_size)));
            
            this.moves++; //o(1)
            calculate_fn(huristic); //o(n*n)
            zero_placeXY[1]++; //o(1)
        }
        
        public void move_left(bool huristic)
        {
            int temp_swap = puzzle2D[zero_placeXY[0], zero_placeXY[1] - 1]; //o(1)
            puzzle2D[zero_placeXY[0], zero_placeXY[1] - 1] = puzzle2D[zero_placeXY[0], zero_placeXY[1]]; //o(1)
            puzzle2D[zero_placeXY[0], zero_placeXY[1]] = temp_swap; //o(1)

            int index_zero = (this.puzzle_size * zero_placeXY[0] + zero_placeXY[1]); //o(1)
            int index = ((this.puzzle_size * (zero_placeXY[0])) + zero_placeXY[1] - 1); //o(1)

            string[] temp = puzzle_string.Split(' '); //o(1)
            string xx = temp[index_zero]; //o(1)
            temp[index_zero] = temp[index]; //o(1)
            temp[index] = xx; //o(1)

            puzzle_string = ""; //o(1)
            for (int i = 0; i < (this.puzzle_size) * (this.puzzle_size); i++)//o(n*n)
            {
                puzzle_string += temp[i] + " "; //o(1)
            }
            puzzle_string.Remove(((this.puzzle_size) * (this.puzzle_size)));
            
            this.moves++;
            calculate_fn(huristic); //o(n*n)
            zero_placeXY[1]--; //o(1)

        }
        
        public void search_exist_befor(ref SortedDictionary<String, Node> closed, ref priority_queue queue, bool huristic, String direction, ref SortedDictionary<String, Node> to_del)
        {
            //o(log n)
            if (closed.ContainsKey(this.puzzle_string))
            {
                Node temp = new Node();  //o(1)
                temp = closed[this.puzzle_string];  //o(1)

                if (temp.flag_open_or_closed == true) 
                {
                    if (temp.fn > this.fn)
                    {
                        to_del.Add(this.puzzle_string, closed[this.puzzle_string]);  //o(1)
                        closed.Remove(this.puzzle_string);  //o(1)

                        Node nody = new Node(this);  //o(1)
                        nody.node_direction = direction;  //o(1)

                        queue.add_to_priority_queue(nody, huristic);  //o(log n) 
                        closed.Add(this.puzzle_string, nody);  //o(1)
                    }
                }
                else          
                {
                    if (temp.fn > this.fn)
                    {
                        closed.Remove(this.puzzle_string);  //o(1)

                        Node nody = new Node(this);  //o(1)
                        nody.node_direction = direction;  //o(1)

                        queue.add_to_priority_queue(nody, huristic); //o(log n) 
                        closed.Add(this.puzzle_string, nody);  //o(1)
                    }
                }
            }
            else
            {
                Node success_node = new Node(this); //o(1)
                success_node.node_direction = direction; //o(1)

                queue.add_to_priority_queue(success_node, huristic); //o(log n) 
                closed.Add(success_node.puzzle_string, success_node); //o(1)
            }

            if (direction == "down")
            {
                this.move_up(huristic); //o(N*N)
            }
            else if (direction == "up")
            {
                this.move_down(huristic); //o(N*N)
            }
            else if (direction == "left")
            {
                this.move_right(huristic); //o(N*N)
            }
            else
            {
                this.move_left(huristic); //o(N*N)
            }
            this.moves--; //o(1)
            this.moves--; //o(1)
            this.calculate_fn(huristic); //o(N*N)
        }
        
        public void nighbors(ref SortedDictionary<String, Node> closed, ref priority_queue queue, bool huristic, ref SortedDictionary<String, Node> to_del)
        { //o(log n)
            int row = zero_placeXY[0]; //o(1)
            int coloum = zero_placeXY[1]; //o(1)

            if (row == 0 && coloum == 0)
            {
                //  corner top  right
                this.move_down(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "down", ref to_del);  //o(log n)

                // corner top left
                this.move_right(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "right", ref to_del);  //o(log n)
            }
            
            // corner top right 
            else if (row == 0 && coloum == puzzle_size - 1)
            {
                this.move_down(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "down", ref to_del);  //o(log n)
                this.move_left(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "left", ref to_del);  //o(log n)
            }
            
            //corner down left 
            else if (row == puzzle_size - 1 && coloum == 0)
            {
                this.move_up(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "up", ref to_del);  //o(log n)

                this.move_right(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "right", ref to_del);  //o(log n)
            }
            

            //corner down right
            else if (row == puzzle_size - 1 && coloum == puzzle_size - 1)
            {
                this.move_up(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "up", ref to_del);  //o(log n)

                this.move_left(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "left", ref to_del);  //o(log n)
            }
            // edge up
            else if (row == 0 && (coloum != 0 || coloum != puzzle_size - 1))
            {
                this.move_left(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "left", ref to_del);  //o(log n)

                this.move_right(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "right", ref to_del);  //o(log n)

                this.move_down(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "down", ref to_del);  //o(log n)
            }
            else if (row == puzzle_size - 1)
            {
                this.move_up(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "up", ref to_del);  //o(log n)

                this.move_left(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "left", ref to_del);  //o(log n)

                this.move_right(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "right", ref to_del); //o(log n)
            }
            
          
            else if (coloum == 0)
            {
                this.move_up(huristic);//o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "up", ref to_del); //o(log n)

                this.move_down(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "down", ref to_del); //o(log n)

                this.move_right(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "right", ref to_del); //o(log n)
            }
            else if (coloum == puzzle_size - 1)
            {
                
                
                 
                this.move_left(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "left", ref to_del); //o(log n)

                this.move_up(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "up", ref to_del); //o(log n)

                this.move_down(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "down", ref to_del); //o(log n)
            }
            else
            {
          
                this.move_left(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "left", ref to_del);  //o(log n)

                this.move_right(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "right", ref to_del); //o(log n)

                this.move_up(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "up", ref to_del); //o(log n)

                this.move_down(huristic); //o(N*N)
                search_exist_befor(ref closed, ref queue, huristic, "down", ref to_del); //o(log n)
            }
        }
    }
}
