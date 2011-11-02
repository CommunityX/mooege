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
using Mooege.Core.GS.Common.Types.Math;

namespace Mooege.Common.MPQ.FileFormats
{
    //[FileFormat(SNOGroup.Anim)]
    public class Anim : FileFormat
    {
        public Header header;
        public int i0;
        public int i1;
        public int snoAppearance;
        public List<AnimPermutation> Permutations;
        public int i2;
        public int i3;

        public Anim(MpqFile file)
        {
            var stream = file.Open();
            this.header = new Header(stream);
            this.i0 = stream.ReadValueS32();
            this.i1 = stream.ReadValueS32();
            this.snoAppearance = stream.ReadValueS32();
            this.Permutations = stream.ReadSerializedData<AnimPermutation>();
            this.i2 = stream.ReadValueS32();
            stream.Position += 12;
            this.i3 = stream.ReadValueS32();
            stream.Close();
        }
    }

    public class AnimPermutation : ISerializableData
    {
        public int i0;
        public string AnimName;
        public float velocity;
        public float f0;
        public float f1;
        public float f2;
        public float f3;
        public int Time1;
        public int Time2;
        public int i1;
        public float f4;
        public float f5;
        public float f6;
        public float f7;
        public int i2;
        public List<BoneName> BoneNames;
        public int i3;
        public List<TranslationCurve> TranslationCurves;
        public List<RotationCurve> RotationCurves;
        public List<ScaleCurve> ScaleCurves;
        public float f8;
        public float f9;
        public float f10;
        public float f11;
        public Vector3D v0;
        public Vector3D v1;
        public Vector3D v2;
        public Vector3D v3;
        public float f12;
        public int i4;
        public List<KeyframedAttachment> KeyedAttachments;
        public List<Vector3D> KeyframePosList;
        public List<Vector3D> NonlinearOffset;
        public VelocityVector3D Velocity3D;
        public HardPointLink Link;
        public string s0;
        public string s1;

        public void Read(MpqFileStream stream)
        {
            this.i0 = stream.ReadValueS32();
            this.AnimName = stream.ReadString(65, true);
            stream.Position += 3;
            this.velocity = stream.ReadValueF32();
            this.f0 = stream.ReadValueF32();
            this.f1 = stream.ReadValueF32();
            this.f2 = stream.ReadValueF32();
            this.f3 = stream.ReadValueF32();
            this.Time1 = stream.ReadValueS32();
            this.Time2 = stream.ReadValueS32();
            this.i1 = stream.ReadValueS32();
            this.f4 = stream.ReadValueF32();
            this.f5 = stream.ReadValueF32();
            this.f6 = stream.ReadValueF32();
            this.f7 = stream.ReadValueF32();
            this.i2 = stream.ReadValueS32();
            this.BoneNames = stream.ReadSerializedData<BoneName>();
            stream.Position += 12;
            this.i3 = stream.ReadValueS32();
            this.TranslationCurves = stream.ReadSerializedData<TranslationCurve>();
            stream.Position += 12;
            this.RotationCurves = stream.ReadSerializedData<RotationCurve>();
            stream.Position += 8;
            this.ScaleCurves = stream.ReadSerializedData<ScaleCurve>();
            stream.Position += 8;
            this.f8 = stream.ReadValueF32();
            this.f9 = stream.ReadValueF32();
            this.f10 = stream.ReadValueF32();
            this.f11 = stream.ReadValueF32();
            this.v0 = new Vector3D(stream);
            this.v1 = new Vector3D(stream);
            this.v2 = new Vector3D(stream);
            this.v3 = new Vector3D(stream);
            this.f12 = stream.ReadValueF32();
            this.KeyedAttachments = stream.ReadSerializedData<KeyframedAttachment>();
            this.i4 = stream.ReadValueS32();
            stream.Position += 8;
            this.KeyframePosList = stream.ReadSerializedData<Vector3D>();
            stream.Position += 8;
            this.NonlinearOffset = stream.ReadSerializedData<Vector3D>();
            stream.Position += 8;
            this.Velocity3D = new VelocityVector3D(stream);
            this.Link = new HardPointLink(stream);
            this.s0 = stream.ReadString(256);
            this.s1 = stream.ReadString(256);
            stream.Position += 8;
        }
    }

    public class BoneName : ISerializableData
    {
        public string Name;

        public void Read(MpqFileStream stream)
        {
            this.Name = stream.ReadString(64, true);
        }
    }

    public class TranslationCurve : ISerializableData
    {
        public int i0;
        public List<TranslationKey> Keys;

        public void Read(MpqFileStream stream)
        {
            this.i0 = stream.ReadValueS32();
            this.Keys = stream.ReadSerializedData<TranslationKey>();
        }
    }

    public class RotationCurve : ISerializableData
    {
        public int i0;
        public List<RotationKey> Keys;

        public void Read(MpqFileStream stream)
        {
            this.i0 = stream.ReadValueS32();
            this.Keys = stream.ReadSerializedData<RotationKey>();
        }
    }

    public class ScaleCurve : ISerializableData
    {
        public int i0;
        public List<ScaleKey> Keys;

        public void Read(MpqFileStream stream)
        {
            this.i0 = stream.ReadValueS32();
            this.Keys = stream.ReadSerializedData<ScaleKey>();
        }
    }

    public class TranslationKey : ISerializableData
    {
        public int i0;
        public Vector3D location;

        public void Read(MpqFileStream stream)
        {
            this.i0 = stream.ReadValueS32();
            this.location = new Vector3D(stream);
        }
    }

    public class RotationKey : ISerializableData
    {
        public int i0;
        public Quaternion q0;

        public void Read(MpqFileStream stream)
        {
            this.i0 = stream.ReadValueS32();
            this.q0 = new Quaternion(stream);
        }
    }

    public class ScaleKey : ISerializableData
    {
        public int i0;
        public float scale;

        public void Read(MpqFileStream stream)
        {
            this.i0 = stream.ReadValueS32();
            this.scale = stream.ReadValueF32();
        }
    }

    public class KeyframedAttachment : ISerializableData
    {
        public float f0;
        public TriggerEvent Event;

        public void Read(MpqFileStream stream)
        {
            this.f0 = stream.ReadValueF32();
            this.Event = new TriggerEvent(stream);
        }
    }

    public class VelocityVector3D
    {
        public float velocityX;
        public float velocityY;
        public float velocityZ;

        public VelocityVector3D(MpqFileStream stream)
        {
            this.velocityX = stream.ReadValueF32();
            this.velocityY = stream.ReadValueF32();
            this.velocityZ = stream.ReadValueF32();
        }
    }

}
