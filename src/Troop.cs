using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanguo
{
    class Troop
    {
        uint size;

        public Troop(FileStream fs, uint base_offset = 0)
        {
            if (base_offset != 0)
            {
                fs.Seek(base_offset, SeekOrigin.Begin);
            }
            BinaryReader br = new BinaryReader(fs);

            size = br.ReadUInt32();
            br.ReadBytes(0x04);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
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
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
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
        }
    }
}
