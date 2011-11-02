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
using Mooege.Common.Extensions;
using Mooege.Common.MPQ.FileFormats.Types;
using Mooege.Core.GS.Common.Types.SNO;
using Mooege.Core.GS.Common.Types.Math;

namespace Mooege.Common.MPQ.FileFormats
{
    //[FileFormat(SNOGroup.UI)]
    public class UI : FileFormat
    {
        public Header Header { get; private set; }
        public string H2O { get; private set; }
        public string XML { get; private set; }
        public int I0 { get; private set; }
        public int NumControls { get; private set; }
        public List<UIControlData> ControlList { get; private set; }
        public int NumStyles { get; private set; }
        public List<UIStyle> StyleList { get; private set; }
        public DataElements DataElements { get; private set; }

        public UI(MpqFile file)
        {
            //632
            var stream = file.Open();
            //System.Console.WriteLine(file.Name);
            this.Header = new Header(stream);
            this.H2O = stream.ReadString(256);
            this.XML = stream.ReadString(256);
            this.I0 = stream.ReadValueS32();
            this.NumControls = stream.ReadValueS32();
            this.ControlList = stream.ReadSerializedData<UIControlData>();
            stream.Position += 4;
            this.NumStyles = stream.ReadValueS32();
            this.StyleList = stream.ReadSerializedData<UIStyle>();
            stream.Position += 4;
            this.DataElements = new DataElements(stream);
            stream.Close();
        }
    }

    public class UIControlData : ISerializableData
    {
        public int I0 { get; private set; }
        public int I1 { get; private set; }
        public string S0 { get; private set; }
        public int I2 { get; private set; }
        public string Name { get; private set; }
        public int I3 { get; private set; }
        public UIStyle Style { get; private set; }
        public UIAnimationElements AnimationElements { get; private set; }

        public void Read(MpqFileStream stream)
        {
            //System.Console.WriteLine("UIControlData: " + stream.Position);
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
            this.S0 = stream.ReadString(512, true);
            this.I2 = stream.ReadValueS32();
            this.Name = stream.ReadString(512, true);
            this.I3 = stream.ReadValueS32();
            this.Style = new UIStyle(stream);
            this.AnimationElements = new UIAnimationElements(stream);
            stream.Position += 12;
        }

    }

    public class UIStyle : ISerializableData
    {
        public int I0 { get; private set; }
        public string S0 { get; private set; }
        public DataElements DataElements { get; private set; }
        public int I1 { get; private set; }
        public List<UIStyleChildSpec> ChildSpecs { get; private set; }

        public UIStyle() { }

        public UIStyle(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.S0 = stream.ReadString(512, true);
            this.DataElements = new DataElements(stream);
            this.I1 = stream.ReadValueS32();
            this.ChildSpecs = stream.ReadSerializedData<UIStyleChildSpec>();
            stream.Position += 24;

        }

        public void Read(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.S0 = stream.ReadString(512);
            this.DataElements = new DataElements(stream);
            this.I1 = stream.ReadValueS32();
            this.ChildSpecs = stream.ReadSerializedData<UIStyleChildSpec>();
            stream.Position += 24;
        }

    }

    public class UIStyleChildSpec : ISerializableData
    {
        public string S0 { get; private set; }
        public DataElements DataElements { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.S0 = stream.ReadString(512, true);
            this.DataElements = new DataElements(stream);
            stream.Position += 12;
        }

    }

    public class UIControlElement : ISerializableData
    {
        public int I0 { get; private set; }
        public string Name { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.Name = stream.ReadString(256, true);
        }

    }

    public class UIDataElementInt32 : ISerializableData
    {
        public int I0 { get; private set; }
        public int I1 { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
        }
    }

    public class UIDataElementFloat32 : ISerializableData
    {
        public int I0 { get; private set; }
        public float F0 { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
        }

    }

    public class UIAnimationElements
    {
        public int I0 { get; private set; }
        public List<UIAnimationElementInt32> Int32Elements { get; private set; }
        public int I1 { get; private set; }
        public List<UIAnimationElementFloat> FloatElements { get; private set; }
        public int I2 { get; private set; }
        public List<UIAnimationElementVector2D> Vector2DElements { get; private set; }
        public int I3 { get; private set; }
        public List<UIAnimationDescription> AnimationDescriptions { get; private set; }
        public int I4 { get; private set; }
        public List<UIControlAnimationBinding> AnimationBindings { get; private set; }

        public UIAnimationElements(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.Int32Elements = stream.ReadSerializedData<UIAnimationElementInt32>();
            stream.Position += 4;
            this.I1 = stream.ReadValueS32();
            this.FloatElements = stream.ReadSerializedData<UIAnimationElementFloat>();
            stream.Position += 4;
            this.I2 = stream.ReadValueS32();
            this.Vector2DElements = stream.ReadSerializedData<UIAnimationElementVector2D>();
            stream.Position += 4;
            this.I3 = stream.ReadValueS32();
            this.AnimationDescriptions = stream.ReadSerializedData<UIAnimationDescription>();
            stream.Position += 4;
            this.I4 = stream.ReadValueS32();
            this.AnimationBindings = stream.ReadSerializedData<UIControlAnimationBinding>();
            stream.Position += 4;
        }

    }

    public class UIAnimationElementInt32 : ISerializableData
    {
        public float F0 { get; private set; }
        public int I0 { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.F0 = stream.ReadValueF32();
            this.I0 = stream.ReadValueS32();
        }

    }

    public class UIAnimationElementFloat : ISerializableData
    {
        public float F0 { get; private set; }
        public float F1 { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
        }
    }

    public class UIAnimationElementVector2D : ISerializableData
    {
        public float F0 { get; private set; }
        public float[] F1 { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.F0 = stream.ReadValueF32();
            this.F1 = new float[2];
            for (int i = 0; i < 2; i++)
                this.F1[i] = stream.ReadValueF32();


        }

    }

    public class UIAnimationDescription : ISerializableData
    {
        public int I0 { get; private set; }
        public string S0 { get; private set; }
        public int I1 { get; private set; }
        public int[] I2 { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.S0 = stream.ReadString(512);
            this.I1 = stream.ReadValueS32();
            this.I2 = new int[2];
            for (int i = 0; i < 2; i++)
                this.I2[i] = stream.ReadValueS32();
        }
    }

    public class UIControlAnimationBinding : ISerializableData
    {
        public string S0 { get; private set; }
        public int I0 { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.S0 = stream.ReadString(512);
            this.I0 = stream.ReadValueS32();
        }

    }

    public class DataElements : ISerializableData
    {
        public int StringCount { get; private set; }
        public List<UIControlElement> StringElements { get; private set; }
        public int IntCount { get; private set; }
        public List<UIDataElementInt32> Int32Elements { get; private set; }
        public int FloatCount { get; private set; }
        public List<UIDataElementFloat32> Float32Elements { get; private set; }

        public DataElements() { }

        public DataElements(MpqFileStream stream)
        {
            this.StringCount = stream.ReadValueS32();
            this.StringElements = stream.ReadSerializedData<UIControlElement>();
            stream.Position += 4;
            this.IntCount = stream.ReadValueS32();
            this.Int32Elements = stream.ReadSerializedData<UIDataElementInt32>();
            stream.Position += 4;
            this.FloatCount = stream.ReadValueS32();
            this.Float32Elements = stream.ReadSerializedData<UIDataElementFloat32>();
            stream.Position += 4;
        }

        public void Read(MpqFileStream stream)
        {
            this.StringCount = stream.ReadValueS32();
            this.StringElements = stream.ReadSerializedData<UIControlElement>();
            stream.Position += 4;
            this.IntCount = stream.ReadValueS32();
            this.Int32Elements = stream.ReadSerializedData<UIDataElementInt32>();
            stream.Position += 4;
            this.FloatCount = stream.ReadValueS32();
            this.Float32Elements = stream.ReadSerializedData<UIDataElementFloat32>();
            stream.Position += 4;
        }
    }
}
