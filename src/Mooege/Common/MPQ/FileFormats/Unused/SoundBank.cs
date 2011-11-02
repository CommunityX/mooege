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
    //[FileFormat(SNOGroup.SoundBank)]
    public class SoundBank : FileFormat
    {
        public Header Header { get; private set; }
        public List<SoundSample> SoundSamples { get; private set; }
        public int I0 { get; private set; }
        public List<int> SampleTimestamps { get; private set; }
        public int I1 { get; private set; }

        public SoundBank(MpqFile file)
        {
            var stream = file.Open();
            this.Header = new Header(stream);
            this.SoundSamples = stream.ReadSerializedData<SoundSample>();
            this.I0 = stream.ReadValueS32();
            stream.Position += 4;
            this.SampleTimestamps = stream.ReadSerializedInts();
            this.I1 = stream.ReadValueS32();
            stream.Close();
        }
    }

    public class SoundSample : ISerializableData
    {
        public string S0 { get; private set; }
        public string S1 { get; private set; }
        public string S2 { get; private set; }
        public int I0 { get; private set; }
        public int I1 { get; private set; }
        public int I2 { get; private set; }
        public int I3 { get; private set; }
        public int I4 { get; private set; }
        public int Format { get; private set; }
        public SerializableDataPointer Samples { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.S0 = stream.ReadString(129, true);
            this.S1 = stream.ReadString(256, true);
            this.S2 = stream.ReadString(256, true);
            stream.Position += 3;
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
            this.I2 = stream.ReadValueS32();
            this.I3 = stream.ReadValueS32();
            this.I4 = stream.ReadValueS32();
            this.Format = stream.ReadValueS32();
            this.Samples = stream.GetSerializedDataPointer();
            stream.Position += 4;
        }
    }
}
