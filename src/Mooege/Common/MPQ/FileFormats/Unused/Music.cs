/*
 * Copyright (C) 2011 mooege project
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using System.Collections.Generic;
using CrystalMpq;
using Gibbed.IO;
using Mooege.Common.MPQ.FileFormats.Types;
using Mooege.Core.GS.Common.Types.SNO;

namespace Mooege.Common.MPQ.FileFormats
{
    //[FileFormat(SNOGroup.Music)]
    public class Music : FileFormat
    {
        public Header Header { get; private set; }
        public int I0 { get; private set; }
        public float F0 { get; private set; }
        public int Ticks0 { get; private set; }
        public int Ticks1 { get; private set; }
        public List<PlaylistEntry> PlaylistEntries { get; private set; }
        public int I1 { get; private set; }
        public RandomMusicSoundParams[] Params { get; private set; }

        public Music(MpqFile file)
        {
            var stream = file.Open();
            this.Header = new Header(stream);
            this.I0 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
            this.Ticks0 = stream.ReadValueS32();
            this.Ticks1 = stream.ReadValueS32();
            this.PlaylistEntries = stream.ReadSerializedData<PlaylistEntry>();
            this.I1 = stream.ReadValueS32();
            stream.Position += 4;
            this.Params = new RandomMusicSoundParams[2];
            Params[0] = new RandomMusicSoundParams(stream);
            Params[1] = new RandomMusicSoundParams(stream);
            stream.Close();
        }
    }

    public class PlaylistEntry : ISerializableData
    {
        public int SNOSoundBank { get; private set; }
        public string Name { get; private set; }
        public float F0 { get; private set; }
        public int SamplingRate { get; private set; }
        public int I1 { get; private set; }
        public int I2 { get; private set; }
        public int I3 { get; private set; }
        public int I4 { get; private set; }
        public int I5 { get; private set; }
        public int I6 { get; private set; }
        public int Ticks0 { get; private set; }
        public int I7 { get; private set; }
        public int Ticks1 { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.SNOSoundBank = stream.ReadValueS32();
            this.Name = stream.ReadString(129, true);
            stream.Position += 3;
            this.F0 = stream.ReadValueF32();
            this.SamplingRate = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
            this.I2 = stream.ReadValueS32();
            this.I3 = stream.ReadValueS32();
            this.I4 = stream.ReadValueS32();
            this.I5 = stream.ReadValueS32();
            this.I6 = stream.ReadValueS32();
            this.Ticks0 = stream.ReadValueS32();
            this.I7 = stream.ReadValueS32();
            this.Ticks1 = stream.ReadValueS32();
            stream.Position += 8;
        }
    }

    public class RandomMusicSoundParams
    {
        public int SNOSound { get; private set; }
        public int Ticks0 { get; private set; }
        public int Ticks1 { get; private set; }
        public int Ticks2 { get; private set; }

        public RandomMusicSoundParams(MpqFileStream stream)
        {
            this.SNOSound = stream.ReadValueS32();
            this.Ticks0 = stream.ReadValueS32();
            this.Ticks1 = stream.ReadValueS32();
            this.Ticks2 = stream.ReadValueS32();
        }
    }


}
