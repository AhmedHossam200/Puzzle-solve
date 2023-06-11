using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
namespace solve44
{
    class Star
    {
        priority_queue queue = new priority_queue();//o(1)
        SortedDictionary<String, Node> close_open_set = new SortedDictionary<String, Node>();//o(1)
        SortedDictionary<String, Node> to_del_set = new SortedDictionary<String, Node>();//o(1)
        List<Node> x = new List<Node>();

        public Node solve(Node start, bool huristic)
        {
            queue.add_to_priority_queue(start, huristic);
            Node new_node = new Node(start);
            close_open_set.Add(new_node.puzzle_string, new_node);
            return find_goal(huristic); //o(n)
        }

        public Node find_goal(bool huristic) //o(N)
        {
            Node current_node = new Node();//o(1)
            do
            {
                current_node = queue.remove_from_priority_queue(huristic);

                if (huristic)
                {
                    while (to_del_set.ContainsKey(current_node.puzzle_string) && to_del_set[current_node.puzzle_string].hamming == current_node.hamming)
                    {
                        to_del_set.Remove(current_node.puzzle_string);
                        current_node = queue.remove_from_priority_queue(huristic);
                    }
                }
                else
                {
                    while (to_del_set.ContainsKey(current_node.puzzle_string) && to_del_set[current_node.puzzle_string].manhatten == current_node.manhatten)
                    {
                        to_del_set.Remove(current_node.puzzle_string);
                        current_node = queue.remove_from_priority_queue(huristic);
                    }
                }

                if (huristic && current_node.hamming == 0)
                {
                    return current_node;
                }
                else if (!(huristic) && current_node.manhatten == 0)
                {
                    return current_node;
                }
                else
                {

                }
                current_node.nighbors(ref close_open_set, ref queue, huristic, ref to_del_set);
                close_open_set[current_node.puzzle_string].flag_open_or_closed = false;
            } while (queue.length_queue > 0);//o(n)


            return current_node;
        }
    }

}
