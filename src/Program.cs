using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanguo
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = @"d:\Download\Sango2\test\Sango01.sav";
            Sang_save ssave = new Sang_save(filename);
            List<Generals> mglist = new List<Generals>();
            
            Monarch m = ssave.getMonarch("曹操");
            int count = 0;
            Generals g= ssave.generalsList[(int)m.generals_index];
            mglist.Add(g);
            Console.WriteLine(count++ + ":" + g.name.Trim('\0'));
            while(true)
            {
                if (g.next_index_in_monarch != 0xffffffff)
                {
                    g = ssave.generalsList[(int)g.next_index_in_monarch];
                    Console.WriteLine(count++ + ":" +g.name.Trim('\0'));
                    mglist.Add(g);
                    g.save();
                }
                else
                {
                    break;
                }
            }
            ssave.save();

            List<Generals> otherglist = new List<Generals>();
            foreach (Generals g1 in ssave.generalsList)
            {
                if (!mglist.Contains(g1))
                {
                    otherglist.Add(g1);
                }
            }
            IEnumerable<Generals> query = ssave.reorderGenerals(mglist);
            //IEnumerable<Generals> query = ssave.reorderGenerals();
            //IEnumerable<Generals> query = ssave.reorderGenerals(otherglist);

            foreach (Generals gq in query)
            {
                Console.WriteLine(gq.name.Trim('\0') + "\tlevel:"
                    + gq.experience + "\t"
                    + gq.level + "\t"
                    //+ Convert.ToString(gq.status, 16) + "\t"
                    + ((gq.city_index != 0xffffffff) ? ssave.cityList[(int)gq.city_index].name.Trim('\0') : "no city"));
            }
        }
    }
}
