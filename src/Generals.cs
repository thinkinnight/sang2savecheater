using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanguo
{
    //将军，武将
    class Generals
    {
        FileStream fs;
        uint base_offset;
        uint size;
        public string name;
        UInt16 hp_max;
        UInt16 hp_current;
        UInt16 mp_max;
        UInt16 mp_current;
        UInt16 hp_base;
        UInt16 mp_base;

        public uint experience;
        public UInt16 level;
        UInt16 arm_base;
        UInt16 intelligence_base;
        UInt16 arm_current;
        UInt16 intelligence_current;
        UInt16 morale;
        UInt16 fatigue;
        UInt16 loyalty;
        UInt16 infantry_max;
        UInt16 infantry_current;
        UInt16 cavalry_max;
        UInt16 cavalry_current;

        public uint status;
        public uint city_index;
        public uint next_index_in_monarch;

        public Generals(FileStream fs, UInt32 base_offset = 0)
        {
            this.fs = fs;
            if (base_offset != 0)
            {
                fs.Seek(base_offset, SeekOrigin.Begin);
                this.base_offset = base_offset;
            }
            else
            {
                this.base_offset = (uint)fs.Position;
            }
            BinaryReader br = new BinaryReader(fs);

            size = br.ReadUInt32();
            byte[] bytes = br.ReadBytes(0x0c);
            name = Encoding.GetEncoding(950).GetString(bytes);

            br.ReadBytes(0x20);
            hp_max = br.ReadUInt16();
            hp_current = br.ReadUInt16();
            mp_max = br.ReadUInt16();
            mp_current = br.ReadUInt16();
            hp_base = br.ReadUInt16();
            mp_base = br.ReadUInt16();

            br.ReadBytes(0x02 * 0x07);

            experience = br.ReadUInt32();
            level = br.ReadUInt16();
            arm_base = br.ReadUInt16();
            intelligence_base = br.ReadUInt16();
            arm_current = br.ReadUInt16();
            intelligence_current = br.ReadUInt16();
            morale = br.ReadUInt16();
            fatigue = br.ReadUInt16();
            loyalty = br.ReadUInt16();

            br.ReadBytes(0x02);
            br.ReadBytes(0x02);

            infantry_max = br.ReadUInt16();
            infantry_current = br.ReadUInt16();
            cavalry_max = br.ReadUInt16();
            cavalry_current = br.ReadUInt16();

            br.ReadBytes(0x02 * 5);
            br.ReadBytes(0x02 * 8);
            br.ReadBytes(0x01 * 8);
            br.ReadByte();

            br.ReadBytes(0x02 * 8);
            br.ReadBytes(0x02 * 8);

            br.ReadBytes(0x02);

            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x02);
            status = br.ReadUInt32();
            br.ReadBytes(0x04);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x04);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02 * 5);
            br.ReadBytes(0x01 * 8);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x02);
            br.ReadBytes(0x02);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            city_index = br.ReadUInt32();

            next_index_in_monarch = br.ReadUInt32();
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
            br.ReadBytes(0x04);
        }

        public void save()
        {
            BinaryWriter bw = new BinaryWriter(fs);

            //满HP
            if (hp_current < hp_max)
            {
                Console.WriteLine("HP" + hp_current + "/" + hp_max);
                hp_current = hp_max;
                fs.Seek(base_offset, SeekOrigin.Begin);
                bw.Seek(0x32, SeekOrigin.Current);
                bw.Write(hp_current);
            }

            //满MP
            if (mp_current < mp_max)
            {
                Console.WriteLine("MP" + mp_current + "/" + mp_max);
                mp_current = mp_max;
                fs.Seek(base_offset, SeekOrigin.Begin);
                bw.Seek(0x36, SeekOrigin.Current);
                bw.Write(mp_current);
            }

            if (experience < 52000)
            {
                Console.WriteLine("经验值"+experience);
                experience = 52000;
                fs.Seek(base_offset, SeekOrigin.Begin);
                bw.Seek(0x4A, SeekOrigin.Current);
                bw.Write(experience);
            }

            //满士气
            if (morale < 100)
            {
                Console.WriteLine("士气" + morale);
                morale = 100;
                fs.Seek(base_offset, SeekOrigin.Begin);
                bw.Seek(0x58, SeekOrigin.Current);
                bw.Write(morale);
            }

            //无疲劳
            if (fatigue >0)
            {
                Console.WriteLine("疲劳度" + fatigue);
                fatigue = 0;
                fs.Seek(base_offset, SeekOrigin.Begin);
                bw.Seek(0x5A, SeekOrigin.Current);
                bw.Write(fatigue);
            }

            //忠诚度100
            if (loyalty < 100)
            {
                Console.WriteLine("忠诚度" + loyalty);
                loyalty = 100;
                fs.Seek(base_offset, SeekOrigin.Begin);
                bw.Seek(0x5C, SeekOrigin.Current);
                bw.Write(loyalty);
            }

            //满兵力
            if (infantry_current < infantry_max)
            {
                Console.WriteLine("步兵:" + infantry_current + "/" + infantry_max);
                infantry_current = infantry_max;
                fs.Seek(base_offset, SeekOrigin.Begin);
                bw.Seek(0x64, SeekOrigin.Current);
                bw.Write(infantry_current);
            }

            if (cavalry_current < cavalry_max)
            {
                Console.WriteLine("骑兵:" + cavalry_current+ "/" + cavalry_max);
                cavalry_current = cavalry_max;
                fs.Seek(base_offset, SeekOrigin.Begin);
                bw.Seek(0x68, SeekOrigin.Current);
                bw.Write(cavalry_current);
            }

        }
    }
}
