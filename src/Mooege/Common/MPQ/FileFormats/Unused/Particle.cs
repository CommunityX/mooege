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
using Mooege.Core.GS.Common.Types.Misc;

namespace Mooege.Common.MPQ.FileFormats
{
    //[FileFormat(SNOGroup.Particle)]
    public class Particle : FileFormat
    {
        public Header Header;
        public int I0; //12
        public int I1; //16
        public int Time0; //20
        public int Time1; //24
        public int Time2; //28
        public InterpolationScalar Scalar; //32
        public FloatPath FP0; //44
        public IntPath IP; //84
        public FloatPath FP1; //124
        public TimePath TP; //164
        public FloatPath FP2; //204
        public AnglePath AP0; //244
        public VelocityVectorPath VVP0; //284
        public VelocityVectorPath VVP1; //324
        public VelocityPath VelP0; //364
        public VelocityPath VelP1; //404
        public VelocityPath VelP2; //444
        public VectorPath VecP3; //484
        public VelocityPath VelP6; //524
        public float F7; //564
        public UberMaterial UberMaterial0; //568
        public int SNOPhysics; //672
        public int Mass; //676 float
        public int I2; //680
        public float F0; //684
        public float F1; //688
        public float F2; //692
        public float F3; //696
        public float F4; //700
        public float F5; //704
        public float F6; //708
        public int SNOActor; //712
        public EmitterParams Params; //716
        public ColorPath CP0; //968
        public FloatPath FP3; //1008
        public FloatPath FP4; //1048
        public FloatPath FP5; //1088
        public AnglePath AP1; //1128
        public AngularVelocityPath AngVP0; //1168
        public AngularVelocityPath AngVP1; //1208
        public AnglePath AP2; //1248
        public VectorPath VecP0; //1288
        public FloatPath FP6; //1328
        public VelocityPath VelP3; //1368
        public AngularVelocityPath AngVP2; //1408
        public FloatPath FP7; //1448
        public VelocityPath VelP4; //1488
        public VectorPath VecP1; //1528
        public VelocityVectorPath VVP2; //1568
        public AccelVectorPath AccVP0; //1608
        public VectorPath VecP2; //1684
        public VelocityVectorPath VVP3; //1688
        public AccelVectorPath AccVP1; //1728
        public VelocityPath VelP5; //1768
        public FloatPath FP8; //1808
        public int I3; //1848
        public float F8; //1852
        public float F9; //1856
        public List<MsgTriggeredEvent> MsgTriggeredEvents; //1860
        public int I4; //1868

        public Particle(MpqFile file)
        {
            var stream = file.Open();
            this.Header = new Header(stream);
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
            this.Time0 = stream.ReadValueS32();
            this.Time1 = stream.ReadValueS32();
            this.Time2 = stream.ReadValueS32();
            this.Scalar = new InterpolationScalar(stream);
            this.FP0 = new FloatPath(stream);
            this.IP = new IntPath(stream);
            this.FP1 = new FloatPath(stream);
            this.TP = new TimePath(stream);
            this.FP2 = new FloatPath(stream);
            this.AP0 = new AnglePath(stream);
            this.VVP0 = new VelocityVectorPath(stream);
            this.VVP1 = new VelocityVectorPath(stream);
            this.VelP0 = new VelocityPath(stream);
            this.VelP1 = new VelocityPath(stream);
            this.VelP2 = new VelocityPath(stream);
            this.VecP3 = new VectorPath(stream);
            this.VelP6 = new VelocityPath(stream);
            this.F7 = stream.ReadValueF32();
            this.UberMaterial0 = new UberMaterial(stream);
            this.SNOPhysics = stream.ReadValueS32();
            this.Mass = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
            this.F2 = stream.ReadValueF32();
            this.F3 = stream.ReadValueF32();
            this.F4 = stream.ReadValueF32();
            this.F5 = stream.ReadValueF32();
            this.F6 = stream.ReadValueF32();
            this.SNOActor = stream.ReadValueS32(); //good pos
            this.Params = new EmitterParams(stream);
            this.CP0 = new ColorPath(stream);
            this.FP3 = new FloatPath(stream);
            this.FP4 = new FloatPath(stream);
            this.FP5 = new FloatPath(stream);
            this.AP1 = new AnglePath(stream);
            this.AngVP0 = new AngularVelocityPath(stream);
            this.AngVP1 = new AngularVelocityPath(stream);
            this.AP2 = new AnglePath(stream);
            this.VecP0 = new VectorPath(stream);
            this.FP6 = new FloatPath(stream);
            this.VelP3 = new VelocityPath(stream);
            this.AngVP2 = new AngularVelocityPath(stream);
            this.FP7 = new FloatPath(stream);
            this.VelP4 = new VelocityPath(stream);
            this.VecP1 = new VectorPath(stream);
            this.VVP2 = new VelocityVectorPath(stream);
            this.AccVP0 = new AccelVectorPath(stream);
            this.VecP2 = new VectorPath(stream);
            this.VVP3 = new VelocityVectorPath(stream);
            this.AccVP1 = new AccelVectorPath(stream);
            this.VelP5 = new VelocityPath(stream);
            this.FP8 = new FloatPath(stream);
            this.I2 = stream.ReadValueS32();
            this.F8 = stream.ReadValueF32();
            this.F9 = stream.ReadValueF32();
            this.MsgTriggeredEvents = stream.ReadSerializedData<MsgTriggeredEvent>();
            this.I3 = stream.ReadValueS32();
            stream.Close();
        }
    }

    public class InterpolationScalar
    {
        public int I0;
        public float F0;
        public float F1;

        public InterpolationScalar(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
        }
    }

    public class InterpolationPathHeader
    {
        public int I0;
        public float F0;
        public float F1;
        public int I1;
        public InterpolationScalar Scalar;

        public InterpolationPathHeader(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
            this.I1 = stream.ReadValueS32();
            this.Scalar = new InterpolationScalar(stream);
        }
    } //size=28

    public class FloatPath //size = 40
    {
        public InterpolationPathHeader Header;
        public List<FloatNode> Nodes;

        public FloatPath(MpqFileStream stream)
        {
            this.Header = new InterpolationPathHeader(stream);
            stream.Position += 4;
            this.Nodes = stream.ReadSerializedData<FloatNode>();
        }
    }

    public class FloatNode : ISerializableData
    {
        public float F0;
        public float F1;
        public float F2;

        public void Read(MpqFileStream stream)
        {
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
            this.F2 = stream.ReadValueF32();
        }
    }

    public class IntPath
    {
        public InterpolationPathHeader Header;
        public List<IntNode> Nodes;

        public IntPath(MpqFileStream stream)
        {
            this.Header = new InterpolationPathHeader(stream);
            stream.Position += 4;
            this.Nodes = stream.ReadSerializedData<IntNode>();
        }
    }

    public class IntNode : ISerializableData
    {
        public float I0;
        public float I1;
        public float F0;

        public void Read(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
        }
    }

    public class TimePath
    {
        public InterpolationPathHeader Header;
        public List<TimeNode> Nodes;

        public TimePath(MpqFileStream stream)
        {
            this.Header = new InterpolationPathHeader(stream);
            stream.Position += 4;
            this.Nodes = stream.ReadSerializedData<TimeNode>();
        }
    }

    public class TimeNode : ISerializableData
    {
        public int Ticks0;
        public int Ticks1;
        public float F0;

        public void Read(MpqFileStream stream)
        {
            this.Ticks0 = stream.ReadValueS32();
            this.Ticks1 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
        }
    }

    public class VelocityPath
    {
        public InterpolationPathHeader Header;
        public List<VelocityNode> Nodes;

        public VelocityPath(MpqFileStream stream)
        {
            this.Header = new InterpolationPathHeader(stream);
            stream.Position += 4;
            this.Nodes = stream.ReadSerializedData<VelocityNode>();
        }
    }

    public class VelocityNode : ISerializableData
    {
        public float Velocity0;
        public float Velocity1;
        public float F2;

        public void Read(MpqFileStream stream)
        {
            this.Velocity0 = stream.ReadValueF32();
            this.Velocity1 = stream.ReadValueF32();
            this.F2 = stream.ReadValueF32();
        }
    }

    public class AccelPath
    {
        public InterpolationPathHeader Header;
        public List<AccelNode> Nodes;

        public AccelPath(MpqFileStream stream)
        {
            this.Header = new InterpolationPathHeader(stream);
            stream.Position += 4;
            this.Nodes = stream.ReadSerializedData<AccelNode>();
        }
    }

    public class AccelNode : ISerializableData
    {
        public float Accel0;
        public float Accel1;
        public float F2;

        public void Read(MpqFileStream stream)
        {
            this.Accel0 = stream.ReadValueF32();
            this.Accel1 = stream.ReadValueF32();
            this.F2 = stream.ReadValueF32();
        }
    }

    public class AnglePath
    {
        public InterpolationPathHeader Header;
        public List<AngleNode> Nodes;

        public AnglePath(MpqFileStream stream)
        {
            this.Header = new InterpolationPathHeader(stream);
            stream.Position += 4;
            this.Nodes = stream.ReadSerializedData<AngleNode>();
        }
    }

    public class AngleNode : ISerializableData
    {
        public float Angle0;
        public float Angle1;
        public float F0;

        public void Read(MpqFileStream stream)
        {
            this.Angle0 = stream.ReadValueF32();
            this.Angle1 = stream.ReadValueF32();
            this.F0 = stream.ReadValueF32();
        }
    }

    public class ColorPath
    {
        public InterpolationPathHeader Header;
        public List<ColorNode> Nodes;

        public ColorPath(MpqFileStream stream)
        {
            this.Header = new InterpolationPathHeader(stream);
            stream.Position += 4;
            this.Nodes = stream.ReadSerializedData<ColorNode>();
        }
    }

    public class ColorNode : ISerializableData
    {
        public RGBAColor Color0;
        public RGBAColor Color1;
        public float F0;

        public void Read(MpqFileStream stream)
        {
            this.Color0 = new RGBAColor(stream);
            this.Color1 = new RGBAColor(stream);
            this.F0 = stream.ReadValueF32();
        }
    }
    
    public class VelocityVectorPath
    {
        public InterpolationPathHeader Header;
        public List<VelocityVectorNode> Nodes;

        public VelocityVectorPath(MpqFileStream stream)
        {
            this.Header = new InterpolationPathHeader(stream);
            stream.Position += 4;
            this.Nodes = stream.ReadSerializedData<VelocityVectorNode>();
        }
    }

    public class VelocityVectorNode : ISerializableData
    {
        public VelocityVector3D VelocityVector0; //in anim file
        public VelocityVector3D VelocityVector1;
        public float F0;

        public void Read(MpqFileStream stream)
        {
            this.VelocityVector0 = new VelocityVector3D(stream);
            this.VelocityVector1 = new VelocityVector3D(stream);
            this.F0 = stream.ReadValueF32();
        }
    }

    public class AngularVelocityNode : ISerializableData
    {
        public float AngularVelocity0;
        public float AngularVelocity1;
        public float F0;

        public void Read(MpqFileStream stream)
        {
            this.AngularVelocity0 = stream.ReadValueF32();
            this.AngularVelocity1 = stream.ReadValueF32();
            this.F0 = stream.ReadValueF32();
        }
    }

    public class AngularVelocityPath
    {
        public InterpolationPathHeader Header;
        public List<AngularVelocityNode> Nodes;

        public AngularVelocityPath(MpqFileStream stream)
        {
            this.Header = new InterpolationPathHeader(stream);
            stream.Position += 4;
            this.Nodes = stream.ReadSerializedData<AngularVelocityNode>();
        }
    }

    public class AccelVectorPath
    {
        public InterpolationPathHeader Header;
        public List<AccelVectorNode> Nodes;

        public AccelVectorPath(MpqFileStream stream)
        {
            this.Header = new InterpolationPathHeader(stream);
            stream.Position += 4;
            this.Nodes = stream.ReadSerializedData<AccelVectorNode>();
        }
    }

    public class AccelVectorNode : ISerializableData
    {
        public AccelVector3D AccelVector0;
        public AccelVector3D AccelVector1;
        public float F0;

        public void Read(MpqFileStream stream)
        {
            this.AccelVector0 = new AccelVector3D(stream);
            this.AccelVector1 = new AccelVector3D(stream);
            this.F0 = stream.ReadValueF32();
        }
    }

    public class AccelVector3D
    {
        public float Accel0;
        public float Accel1;
        public float Accel2;

        public AccelVector3D(MpqFileStream stream)
        {
            this.Accel0 = stream.ReadValueF32();
            this.Accel1 = stream.ReadValueF32();
            this.Accel2 = stream.ReadValueF32();
        }
    }

    public class VectorPath
    {
        public InterpolationPathHeader Header;
        public List<VectorNode> Nodes;

        public VectorPath(MpqFileStream stream)
        {
            this.Header = new InterpolationPathHeader(stream);
            stream.Position += 4;
            this.Nodes = stream.ReadSerializedData<VectorNode>();
        }
    }

    public class VectorNode : ISerializableData
    {
        public Vector3D V0;
        public Vector3D V1;
        public float F0;

        public void Read(MpqFileStream stream)
        {
            this.V0 = new Vector3D(stream);
            this.V1 = new Vector3D(stream);
            this.F0 = stream.ReadValueF32();
        }
    }
    
    public class UberMaterial //size 104
    {
        public int SNOShaderMap;
        public ParticleMaterial Material;
        public List<MaterialTextureEntry> MatTexList;

        public UberMaterial(MpqFileStream stream)
        {
            this.SNOShaderMap = stream.ReadValueS32();
            this.Material = new ParticleMaterial(stream);
            this.MatTexList = stream.ReadSerializedData<MaterialTextureEntry>();
            stream.Position += 20;
        }
    }

    public class MaterialTextureEntry : ISerializableData
    {
        public int I0;
        public MaterialTexture Texture;

        public void Read(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.Texture = new MaterialTexture(stream);
        }
    }

    public class MaterialTexture
    {
        public int SNOTex;
        public TexAnimParams Params;

        public MaterialTexture(MpqFileStream stream)
        {
            this.SNOTex = stream.ReadValueS32();
            this.Params = new TexAnimParams(stream);
        }
    }

    public class TexAnimParams
    {
        public int I0;
        public Matrix4x4 Matrix;
        public float F0;
        public float Velocity0;
        public float F1;
        public float Velocity1;
        public float Angle0;
        public float AngularVelocity0;
        public float Angle1;
        public float F2;
        public float Velocity2;
        public float F3;
        public float Velocity3;
        public float Angle2;
        public float AngularVelocity1;
        public float Angle3;
        public float Angle4;
        public float AngularVelocity2;
        public float AngularVelocity3;
        public int I1;
        public int I2;
        public int I3;
        public int I4;
        public FrameAnim Anim; //from anim2d
        public int I5;

        public TexAnimParams(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.Matrix = new Matrix4x4(stream);
            this.F0 = stream.ReadValueF32();
            this.Velocity0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
            this.Velocity1 = stream.ReadValueF32();
            this.Angle0 = stream.ReadValueF32();
            this.AngularVelocity0 = stream.ReadValueF32();
            this.Angle1 = stream.ReadValueF32();
            this.F2 = stream.ReadValueF32();
            this.Velocity2 = stream.ReadValueF32();
            this.F3 = stream.ReadValueF32();
            this.Velocity3 = stream.ReadValueF32();
            this.Angle2 = stream.ReadValueF32();
            this.AngularVelocity1 = stream.ReadValueF32();
            this.Angle3 = stream.ReadValueF32();
            this.Angle4 = stream.ReadValueF32();
            this.AngularVelocity2 = stream.ReadValueF32();
            this.AngularVelocity3 = stream.ReadValueF32();
            this.I1 = stream.ReadValueS32();
            this.I2 = stream.ReadValueS32();
            this.I3 = stream.ReadValueS32();
            this.I4 = stream.ReadValueS32();
            this.Anim = new FrameAnim(stream);
            this.I5 = stream.ReadValueS32();
        }
    }

    public class ParticleMaterial //size 72
    {
        public RGBAColorValue Color0;
        public RGBAColorValue Color1;
        public RGBAColorValue Color2;
        public RGBAColorValue Color3;
        public float F0;
        public int I0;

        public ParticleMaterial(MpqFileStream stream)
        {
            this.Color0 = new RGBAColorValue(stream);
            this.Color1 = new RGBAColorValue(stream);
            this.Color2 = new RGBAColorValue(stream);
            this.Color3 = new RGBAColorValue(stream);
            this.F0 = stream.ReadValueF32();
            this.I0 = stream.ReadValueS32();
        }

    } //size 72

    public class RGBAColorValue
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public RGBAColorValue(MpqFileStream stream)
        {
            this.R = stream.ReadValueF32();
            this.G = stream.ReadValueF32();
            this.B = stream.ReadValueF32();
            this.A = stream.ReadValueF32();
        }
    }

    public class EmitterParams
    {
        public int I0;
        public FloatPath FP0;
        public FloatPath FP1;
        public VectorPath VP0;
        public string S0;

        public EmitterParams(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.FP0 = new FloatPath(stream);
            this.FP1 = new FloatPath(stream);
            this.VP0 = new VectorPath(stream);
            this.S0 = stream.ReadString(128, true);
        }
    }

    public class Matrix4x4
    {
        public float F0;
        public float F1;
        public float F2;
        public float F3;
        public float F4;
        public float F5;
        public float F6;
        public float F7;
        public float F8;
        public float F9;
        public float F10;
        public float F11;
        public float F12;
        public float F13;
        public float F14;
        public float F15;

        public Matrix4x4(MpqFileStream stream)
        {
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
            this.F2 = stream.ReadValueF32();
            this.F3 = stream.ReadValueF32();
            this.F4 = stream.ReadValueF32();
            this.F5 = stream.ReadValueF32();
            this.F6 = stream.ReadValueF32();
            this.F7 = stream.ReadValueF32();
            this.F8 = stream.ReadValueF32();
            this.F9 = stream.ReadValueF32();
            this.F10 = stream.ReadValueF32();
            this.F11 = stream.ReadValueF32();
            this.F12 = stream.ReadValueF32();
            this.F13 = stream.ReadValueF32();
            this.F14 = stream.ReadValueF32();
            this.F15 = stream.ReadValueF32();
        }
    }

}
