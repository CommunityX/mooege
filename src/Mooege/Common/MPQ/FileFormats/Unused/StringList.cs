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
    //[FileFormat(SNOGroup.StringList)]
    public class StringList : FileFormat
    {
        public Header header { get; private set; }
        public List<StringTableEntry> Strings { get; private set; }

        public StringList(MpqFile file)
        {
            var stream = file.Open();
            this.header = new Header(stream);
            stream.Position += 12;
            this.Strings = stream.ReadSerializedData<StringTableEntry>();
            stream.Close();
        }
    }

    public class StringTableEntry : ISerializableData
    {
        public string Label { get; private set; }
        public string Text { get; private set; }
        public string Comment { get; private set; }
        public string Speaker { get; private set; }
        public int i0 { get; private set; }
        public int i1 { get; private set; }
        public int i2 { get; private set; }

        public void Read(MpqFileStream stream)
        {
            stream.Position += 8;
            this.Label = stream.ReadSerializedString();
            stream.Position += 8;
            this.Text = stream.ReadSerializedString();
            stream.Position += 8;
            this.Comment = stream.ReadSerializedString();
            stream.Position += 8;
            this.Speaker = stream.ReadSerializedString();
            this.i0 = stream.ReadValueS32();
            this.i1 = stream.ReadValueS32();
            this.i2 = stream.ReadValueS32();
            stream.Position += 4;
        }
    }
}