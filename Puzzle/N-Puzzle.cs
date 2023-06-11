using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using solve44;
using System.Media;

namespace Puzzle
{
    public partial class N_Puzzle : Form
    {
        bool solver = false;
        bool loader = false;
        bool player = false;
        int numberofmoves;
        long position = 0;
        File myfile;

        Node start;
        Node path;

        Stack<Node> stepsstack = new Stack<Node>();
        String puzzle_string = "";






        int[,] _2D;
        int puzzle_size;
        int[] zeroplace;
        public int[] arr;

        public int tile_width = 100;
        public int tile_hight = 100;
        public int location_x = 200;
        public int temp_loc;
        public int location_y = 300;
        int size = 0;
        int zero = 0;
        public Label[] tiles;
        public N_Puzzle()
        {
            InitializeComponent();
            IntializeData();
        }

        private void N_Puzzle_Load(object sender, EventArgs e)
        {

        }
        public void IntializeData()
        {
            myfile = new File("test.txt");
            position = myfile.read_from_specific_pos(position);

            puzzle_size = myfile.get_puzzle_size();
            size = puzzle_size;

            _2D = new int[puzzle_size, puzzle_size];
            _2D = myfile.get_puzzle2D();

            zeroplace = new int[2];
            zeroplace = myfile.get_zero_place();

            for (int i = 0; i < myfile.get_puzzle_size(); i++)
            {
                for (int j = 0; j < myfile.get_puzzle_size(); j++)
                {
                    puzzle_string += _2D[i, j].ToString() + " ";
                }
            }
            puzzle_string.Remove(myfile.get_puzzle_size() * myfile.get_puzzle_size());

        }
        public void solve()
        {
            int z_down = 0;
            int z_right = 0;
            int z_left = 0;
            int z_up = 0;
            if (size * size > zero + 1)
            {
                z_right = arr[zero + 1];
            }
            if (zero - 1 >= 0)
            {
                z_left = arr[zero - 1];
            }
            if (zero - size >= 0)
            {
                z_up = arr[zero - size];
            }
            if (arr.Length - 1 >= zero + size)
            {
                z_down = arr[zero + size];
            }
            stepsstack.Pop();
            for (int i = 0; i < stepsstack.Count; i++)
            {

                if (stepsstack.ElementAt(i).node_direction == "right")
                {

                    tiles[z_right].Location = new Point(tiles[z_right].Location.X - (tile_width + 5), tiles[z_right].Location.Y);

                    int tempo = arr[zero];
                    arr[zero] = arr[zero + 1];
                    arr[zero + 1] = tempo;
                    zero++;
                    if (size * size > zero + 1)
                    {
                        z_right = arr[zero + 1];
                    }
                    if (zero - 1 >= 0)
                    {
                        z_left = arr[zero - 1];
                    }
                    if (zero - size >= 0)
                    {
                        z_up = arr[zero - size];
                    }
                    if (arr.Length - 1 >= zero + size)
                    {
                        z_down = arr[zero + size];
                    }

                }
                else if (stepsstack.ElementAt(i).node_direction == "left")
                {

                    tiles[z_left].Location = new Point(tiles[z_left].Location.X + (tile_width + 5), tiles[z_left].Location.Y);

                    int tempo = arr[zero];
                    arr[zero] = arr[zero - 1];
                    arr[zero - 1] = tempo;

                    zero--;

                    if (size * size > zero + 1)
                    {
                        z_right = arr[zero + 1];
                    }
                    if (zero - 1 >= 0)
                    {
                        z_left = arr[zero - 1];
                    }
                    if (zero - size >= 0)
                    {
                        z_up = arr[zero - size];
                    }
                    if (arr.Length - 1 >= zero + size)
                    {
                        z_down = arr[zero + size];
                    }

                }
                else if (stepsstack.ElementAt(i).node_direction == "up")
                {

                    tiles[z_up].Location = new Point(tiles[z_up].Location.X, tiles[z_up].Location.Y + ((tile_hight + 5)));
                    int tempo = arr[zero];
                    arr[zero] = arr[zero - size];
                    arr[zero - size] = tempo;
                    zero -= size;
                    if (size * size > zero + 1)
                    {
                        z_right = arr[zero + 1];
                    }
                    if (zero - 1 >= 0)
                    {
                        z_left = arr[zero - 1];
                    }
                    if (zero - size >= 0)
                    {
                        z_up = arr[zero - size];
                    }
                    if (arr.Length - 1 >= zero + size)
                    {
                        z_down = arr[zero + size];
                    }

                }
                else if (stepsstack.ElementAt(i).node_direction == "down")
                {
                    tiles[z_down].Location = new Point(tiles[z_down].Location.X, tiles[z_down].Location.Y - ((tile_hight + 5)));
                    int tempo = arr[zero];
                    arr[zero] = arr[zero + size];
                    arr[zero + size] = tempo;
                    zero += size;

                    if (size * size > zero + 1)
                    {
                        z_right = arr[zero + 1];
                    }
                    if (zero - 1 >= 0)
                    {
                        z_left = arr[zero - 1];
                    }
                    if (zero - size >= 0)
                    {
                        z_up = arr[zero - size];
                    }
                    if (arr.Length - 1 >= zero + size)
                    {
                        z_down = arr[zero + size];
                    }

                }
                var t = Task.Delay(1000);
                t.Wait();
            }

        }
        public void Show_puzzle(object sender, EventArgs e)
        {
            if (!loader)
            {
                loader = true;
                creat_puzzle();
            }
        }
        public void creat_puzzle()
        {
            tiles = new Label[puzzle_size * puzzle_size];

            location_x = ((this.Width / 2) / (puzzle_size + 3)) * 2;
            location_y = ((this.Height / 1) / (puzzle_size + 7)) * 3;
            tile_width = location_x / 2;
            tile_hight = location_x / 2;

            temp_loc = location_x;
            int c = 0;

            for (int i = 0; i < puzzle_size; i++)
            {
                for (int j = 0; j < puzzle_size; j++)
                {
                    if (_2D[i, j] != 0)
                    {
                        tiles[c] = new Label();
                        tiles[c].Location = new System.Drawing.Point(location_x, location_y);
                        tiles[c].Size = new System.Drawing.Size(tile_width, tile_hight);
                        tiles[c].TabStop = false;
                        tiles[c].BackColor = Color.LightGoldenrodYellow;
                        tiles[c].Text = (_2D[i, j]).ToString();
                        tiles[c].Font = new Font("Arial", 100 / (puzzle_size));//resize to able to show large test case
                        tiles[c].TextAlign = ContentAlignment.MiddleCenter;
                        this.Controls.Add(tiles[c]);
                        c++;
                    }
                    else
                    {
                        zero = c;
                        tiles[c] = new Label();
                        tiles[c].Location = new System.Drawing.Point(0, 0);
                        tiles[c].Size = new System.Drawing.Size(tile_width, tile_hight);
                        tiles[c].TabStop = false;
                        tiles[c].BackColor = this.BackColor;
                        c++;
                    }

                    location_x += tile_width + 5;
                }
                location_x = temp_loc;
                location_y += tile_hight + 5;
            }

            arr = new int[puzzle_size * puzzle_size];
            for (int i = 0; i < puzzle_size * puzzle_size; i++)
            {
                arr[i] = i;
            }

        }
        private void Solve_puzzle(object sender, EventArgs e)
        {

            if (!(solver) && loader)
            {
                solver = true;

                Play_puuzle();


            }
            else
            {
                MessageBox.Show("Please Check Data Before Run", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); //without click on show_puzzle
            }

        }


        public void Play_puuzle()
        {
            if (Detect.detect_puzzle(_2D, puzzle_size, zeroplace[0]))
            {
                Star A = new Star();
                start = new Node(_2D, puzzle_size, 0, zeroplace, puzzle_string, false);
                path = new Node();
                if (radioButton1.Enabled)
                {
                    path = A.solve(start, false);
                }
                else
                {
                    path = A.solve(start, true);
                }
                numberofmoves = path.moves;
                Steps.Items.Clear();
                Steps.Items.Add("Number Of Moves = " + numberofmoves.ToString());
                while (path.parent != null)
                {
                    stepsstack.Push(path);
                    path = path.parent;
                }
                stepsstack.Push(start);

                int s = stepsstack.Count;
                String steps = "";
                int x, y;
                for (int i = 1; i < s; i++)
                {
                    if (stepsstack.ElementAt(i).node_direction == "right")
                    {
                        x = stepsstack.ElementAt(i).zero_placeXY[0];
                        y = stepsstack.ElementAt(i).zero_placeXY[1];
                        steps = (i).ToString() + "- Move " + stepsstack.ElementAt(i).puzzle2D[x, y - 1].ToString() + " left";
                        Steps.Items.Add(steps);
                    }
                    else if (stepsstack.ElementAt(i).node_direction == "left")
                    {
                        x = stepsstack.ElementAt(i).zero_placeXY[0];
                        y = stepsstack.ElementAt(i).zero_placeXY[1];
                        steps = (i).ToString() + "- Move " + stepsstack.ElementAt(i).puzzle2D[x, y + 1].ToString() + " right";
                        Steps.Items.Add(steps);

                    }
                    else if (stepsstack.ElementAt(i).node_direction == "up")
                    {
                        x = stepsstack.ElementAt(i).zero_placeXY[0];
                        y = stepsstack.ElementAt(i).zero_placeXY[1];
                        steps = (i).ToString() + "- Move " + stepsstack.ElementAt(i).puzzle2D[x + 1, y].ToString() + " Down";
                        Steps.Items.Add(steps);

                    }
                    else if (stepsstack.ElementAt(i).node_direction == "down")
                    {
                        x = stepsstack.ElementAt(i).zero_placeXY[0];
                        y = stepsstack.ElementAt(i).zero_placeXY[1];
                        steps = (i).ToString() + "- Move " + stepsstack.ElementAt(i).puzzle2D[x - 1, y].ToString() + " Up";
                        Steps.Items.Add(steps);

                    }
                }
            }
            else
            {
                MessageBox.Show("Unsolvable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.Enabled = true;
        }
        private void play_Click(object sender, EventArgs e)
        {
            if (!(player) && solver)
            {
                player = true;

                solve();
            }

            else
                MessageBox.Show("Please Check Data Before Run", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }





        private void Steps_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        
      
      

        private void button1_Click(object sender, EventArgs e)
        {
            Show_puzzle(sender, e);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Solve_puzzle(sender, e);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            play_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
