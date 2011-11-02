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

using System;
using System.Collections.Generic;
using CrystalMpq;
using Gibbed.IO;
using Mooege.Common.MPQ.FileFormats.Types;
using Mooege.Core.GS.Common.Types.SNO;
using Mooege.Core.GS.Common.Types.Math;

namespace Mooege.Common.MPQ.FileFormats
{
    //[FileFormat(SNOGroup.Textures)]
    public class Textures : FileFormat
    {
        public Header Header { get; private set; }
        public int PixelFormat { get; private set; } //16 0x10
        public int Width { get; private set; } //20 0x14
        public int Height { get; private set; } //24 0x18
        public int FaceCount { get; private set; } //28 0x1C
        public int I0 { get; private set; } //32 0x20
        public SerializableDataPointer[] MipMapLevelMax { get; private set; } //36 0x24    + (60 * 8=480) +0x1E0

        public int TexFrameCount { get; private set; } //520 0x208
        public List<TexFrame> Frame { get; private set; } //524 0x20C
        public Vector2D V0 { get; private set; } //536 0x218
        public int I6 { get; private set; } //544 0x220
        public int I7 { get; private set; } //548 0x224
        public int I8 { get; private set; } //552 0x228
        public long I9 { get; private set; } //560 0x22C
        public int I10 { get; private set; } //568 0x230
        public sbyte C0 { get; private set; } //572 0x234
        public sbyte C1 { get; private set; } //573 0x235
        public sbyte C2 { get; private set; } //574 0x236

        public string S0 { get; private set; } //592 0x250     + 256
        public List<ImageFileID> ImageFileIDs; //848 0x350

        public int I3 { get; private set; } //860 0x35C
        public int I4 { get; private set; } //864 0x360
        public int I5 { get; private set; } //868 0x364

        public Textures(MpqFile file)
        {
            var stream = file.Open();
            this.Header = new Header(stream);
            stream.Position += 4;
            this.PixelFormat = stream.ReadValueS32();
            this.Width = stream.ReadValueS32();
            this.Height = stream.ReadValueS32();
            this.FaceCount = stream.ReadValueS32();
            this.I0 = stream.ReadValueS32();
            this.MipMapLevelMax = new SerializableDataPointer[60];
            for (int i = 0; i < 60; i++)
                this.MipMapLevelMax[i] = stream.GetSerializedDataPointer();
            stream.Position += 4;
            this.TexFrameCount = stream.ReadValueS32();
            this.Frame = stream.ReadSerializedData<TexFrame>();
            stream.Position += 4;
            this.V0 = new Vector2D(stream);
            this.I6 = stream.ReadValueS32();
            this.I7 = stream.ReadValueS32();
            this.I8 = stream.ReadValueS32();
            stream.Position += 4;
            this.I9 = stream.ReadValueS64();
            this.I10 = stream.ReadValueS32();
            this.C0 = stream.ReadValueS8();
            this.C1 = stream.ReadValueS8();
            this.C2 = stream.ReadValueS8();
            stream.Position += 17;
            this.S0 = stream.ReadString(256, true);
            stream.Position += 4;
            this.ImageFileIDs = stream.ReadSerializedData<ImageFileID>();
            this.I3 = stream.ReadValueS32();
            this.I4 = stream.ReadValueS32();
            this.I5 = stream.ReadValueS32();
            stream.Close();
        }
    }

    public class ImageFileID : ISerializableData
    {
        public string InternalName { get; private set; }
        public string FileName { get; private set; }
        public int EntryNumber { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.InternalName = stream.ReadString(256, true);
            this.FileName = stream.ReadString(256, true);
            this.EntryNumber = stream.ReadValueS32();
        }
    }

    public class TexFrame : ISerializableData
    {
        public string Name { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Xend { get; private set; }
        public float Yend { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.Name = stream.ReadString(64);
            this.X = stream.ReadValueF32();
            this.Y = stream.ReadValueF32();
            this.Xend = stream.ReadValueF32();
            this.Yend = stream.ReadValueF32();
        }
    }
}
