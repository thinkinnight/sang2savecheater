using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanguo
{
    //君主
    class Monarch
    {
        uint size;
        public string name;
        uint self_index_in_generals;

        public uint generals_index;
        uint experience;
        uint type;

        UInt16[] equipments;

        uint city_index;
        uint troop_index;

        uint self_index_in_monarch;

        public Monarch(FileStream fs, uint base_offset=0)
        {
            if (base_offset != 0)
            {
                fs.Seek(base_offset, SeekOrigin.Begin);
            }
            BinaryReader br = new BinaryReader(fs);

            size = br.ReadUInt32();
            byte[] bytes = br.ReadBytes(0x0c);
            name = Encoding.GetEncoding(950).GetString(bytes);

            self_index_in_generals = br.ReadUInt32();

            br.ReadBytes(0x08);
            generals_index = br.ReadUInt32();

            br.ReadBytes(0x08);
            br.ReadBytes(0x08);

            experience = br.ReadUInt32();
            type = br.ReadUInt32();

            br.ReadBytes(0x100);
            equipments = new UInt16[0x100];

            for (int i = 0; i < 0x100; i++)
            {
                equipments[i] = br.ReadUInt16();
            }

            br.ReadBytes(0x04);

            city_index = br.ReadUInt32();
            troop_index = br.ReadUInt32();

            br.ReadBytes(0x02);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);

            self_index_in_monarch = br.ReadUInt32();

        }
    }
}
