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
using Mooege.Core.GS.Common.Types.Misc;

namespace Mooege.Common.MPQ.FileFormats
{
    //[FileFormat(SNOGroup.Anim2D)]
    public class Anim2D : FileFormat
    {
        public Header header { get; private set; }
        public int i0 { get; private set; }
        public int i1 { get; private set; }
        public FrameAnim Anim { get; private set; }
        public int snoSound { get; private set; }
        public int i2 { get; private set; }
        public List<Anim2DFrame> Frames { get; private set; }

        public Anim2D(MpqFile file)
        {
            var stream = file.Open();
            this.header = new Header(stream);
            this.i0 = stream.ReadValueS32();
            this.i1 = stream.ReadValueS32();
            this.Anim = new FrameAnim(stream);
            this.snoSound = stream.ReadValueS32();
            this.i2 = stream.ReadValueS32();
            this.Frames = stream.ReadSerializedData<Anim2DFrame>();
            stream.Close();
        }
    }

    public class FrameAnim
    {
        public int i0;
        public float Velocity0;
        public float Velocity1;
        public int i1;
        public int i2;

        public FrameAnim(MpqFileStream stream)
        {
            this.i0 = stream.ReadValueS32();
            this.Velocity0 = stream.ReadValueF32();
            this.Velocity1 = stream.ReadValueF32();
            this.i1 = stream.ReadValueS32();
            this.i2 = stream.ReadValueS32();
        }
    }

    public class Anim2DFrame : ISerializableData
    {
        public string s0;
        public RGBAColor Color;

        public void Read(MpqFileStream stream)
        {
            this.s0 = stream.ReadString(64, true);
            this.Color = new RGBAColor(stream);
        }
    }
}