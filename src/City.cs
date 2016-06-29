using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanguo
{
    //城池
    class City
    {
        uint size;
        uint used;
        public string name;
        UInt16 level;
        UInt32 prepare_max;
        UInt32 prepare_current;
        UInt32 people_num;
        UInt32 defende;
        UInt32 money;
        UInt32 city_index;
        UInt32 next_index_monarch;
        UInt32 next_index_city;

        public City(FileStream fs, uint base_offset = 0)
        {
            if (base_offset != 0)
            {
                fs.Seek(base_offset, SeekOrigin.Begin);
            }
            BinaryReader br = new BinaryReader(fs);

            size = br.ReadUInt32();
            used = br.ReadUInt32();

            byte[] bytes = br.ReadBytes(0x0c);
            name = Encoding.GetEncoding(950).GetString(bytes);

            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);

            br.ReadBytes(0x04 * 9);
            br.ReadBytes(0x04);

            level = br.ReadUInt16();

            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);

            br.ReadBytes(0x04);
            prepare_max = br.ReadUInt32();
            prepare_current = br.ReadUInt32();
            people_num = br.ReadUInt32();
            defende = br.ReadUInt32();
            money = br.ReadUInt32();
            br.ReadUInt32();
            br.ReadUInt32();
            br.ReadUInt32();
            next_index_monarch = br.ReadUInt32();
            next_index_city = br.ReadUInt32();
        }
    }
}
