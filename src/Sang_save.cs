using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanguo
{
    //存档文件
    class Sang_save
    {
        FileStream fs;
        char[] header;
        uint version;
        uint empty;
        string save_title;
        uint unkonwn_32;
        uint monarch_index;
        uint rogue_monarch_index;
        uint current_year;
        UInt16 unkown_16;

        uint monarch_nummax;
        uint monarch_fileoffset;
        uint monarch_head_index;
        uint monarch_next_index;
        uint monarch_tail_index;

        uint city_nummax;
        uint city_fileoffset;
        uint city_head_index;
        uint city_next_index;
        uint city_tail_index;
        uint[] city_index_inlist;

        uint troop_nummax;
        uint troop_fileoffset;
        uint troop_head_index;
        uint troop_next_index;
        uint troop_tail_index;

        uint generals_nummax;
        uint generals_fileoffset;
        uint generals_head_index;
        uint generals_next_index;
        uint generals_tail_index;

        uint path_nummax;
        uint path_fileoffset;
        uint path_point_nummax;
        uint path_point_fileoffset;

        uint equipment_nummax;
        uint equipment_issearched_fileoffset;

        List<Monarch> monarchList;
        public List<City> cityList;
        List<Troop> troopList;
        public List<Generals> generalsList;
        List<Path> pathList;
        List<PathPoint> pathPointList;
        List<Equipment> equipmentList;

        //构造函数，读取文件
        public Sang_save(string filename)
        {
            fs = null;
            monarchList = new List<Monarch>();
            cityList = new List<City>();
            troopList = new List<Troop>();
            generalsList = new List<Generals>();

            load(filename);
        }

        private void load(string filename)
        {
            fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            BinaryReader br = new BinaryReader(fs);

            header = br.ReadChars(4);
            Byte[] bytes = br.ReadBytes(4);
            version = bytes[2];

            br.ReadBytes(4);

            bytes = br.ReadBytes(0x40);
            save_title = Encoding.GetEncoding(950).GetString(bytes);

            br.ReadBytes(4);

            monarch_index = br.ReadUInt32();
            rogue_monarch_index = br.ReadUInt32();

            current_year = br.ReadUInt32();

            br.ReadBytes(0x4 + 0x4 + 0x4);

            monarch_nummax = br.ReadUInt32();
            monarch_fileoffset = br.ReadUInt32();
            monarch_head_index = br.ReadUInt32();
            monarch_next_index = br.ReadUInt32();
            monarch_tail_index = br.ReadUInt32();

            city_nummax = br.ReadUInt32();
            city_fileoffset = br.ReadUInt32();
            city_head_index = br.ReadUInt32();
            city_next_index = br.ReadUInt32();
            city_tail_index = br.ReadUInt32();

            city_index_inlist = new uint[city_nummax];
            for (int i = 0; i < city_nummax; i++)
            {
                city_index_inlist[i] = br.ReadUInt32();
            }

            troop_nummax = br.ReadUInt32();
            troop_fileoffset = br.ReadUInt32();
            troop_head_index = br.ReadUInt32();
            troop_next_index = br.ReadUInt32();
            troop_tail_index = br.ReadUInt32();

            generals_nummax = br.ReadUInt32();
            generals_fileoffset = br.ReadUInt32();
            generals_head_index = br.ReadUInt32();
            generals_next_index = br.ReadUInt32();
            generals_tail_index = br.ReadUInt32();

            path_nummax = br.ReadUInt32();
            path_fileoffset = br.ReadUInt32();

            path_point_nummax = br.ReadUInt32();
            path_point_fileoffset = br.ReadUInt32();

            equipment_nummax = br.ReadUInt32();
            equipment_issearched_fileoffset = br.ReadUInt32();

            uint max = monarch_tail_index;
            if (monarch_tail_index > monarch_nummax)
            {
                max = monarch_nummax;
            }
            for (int i = 0; i < monarch_nummax; i++)
            {
                Monarch m = new Monarch(fs);
                if (i <= max)
                {
                    monarchList.Add(m);
                }
            }

            for (int i = 0; i < city_nummax; i++)
            {
                City c = new City(fs);
                if (i <= city_tail_index)
                {
                    cityList.Add(c);
                }
            }

            for (int i = 0; i < troop_nummax; i++)
            {
                Troop t = new Troop(fs);
            }

            for (int i = 0; i < generals_nummax; i++)
            {
                Generals g = new Generals(fs);
                if (i <= generals_tail_index)
                {
                    generalsList.Add(g);
                }
            }
        }

        public void save()
        {
            fs.Close();
        }

        public Monarch getMonarch(string name)
        {
            foreach (Monarch m in monarchList)
            {
                if (m.name.Contains(name))
                {
                    return m;
                }
            }
            return null;
        }

        public void printMonarchs()
        {
            int i = 0;
            foreach (Monarch m in monarchList)
            {
                Console.WriteLine(Convert.ToString(++i, 16) + ":" + m.name);
            }
        }

        public void printCities()
        {
            int i = 0;
            foreach (City c in cityList)
            {
                Console.WriteLine(Convert.ToString(++i, 16) + ":" + c.name);
            }
        }

        public void printGenerals()
        {
            int i = 0;
            foreach (Generals g in generalsList)
            {
                Console.WriteLine(Convert.ToString(++i, 16) + ":" + g.name);
            }
        }

        public IEnumerable<Generals> reorderGenerals(List<Generals> glist = null)
        {
            return reorderGenerals_byLevel(glist);
        }

        public IEnumerable<Generals> reorderGenerals_byCity(List<Generals> glist=null)
        {
            IEnumerable<Generals> query = null;
            if (glist == null)
            {
                query = from g in generalsList orderby g.city_index select g;
            }
            else
            {
                query = from g in glist orderby g.city_index select g;
            }
            return query;
        }

        public IEnumerable<Generals> reorderGenerals_byLevel(List<Generals> glist = null)
        {
            IEnumerable<Generals> query = null;
            if (glist == null)
            {
                query = from g in generalsList orderby g.level select g;
            }
            else
            {
                query = from g in glist orderby g.level select g;
            }
            return query;
        }
    }
}
