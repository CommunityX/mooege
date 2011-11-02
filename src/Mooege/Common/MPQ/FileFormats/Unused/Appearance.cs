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
using Mooege.Core.GS.Common.Types.Collusion;
using Mooege.Core.GS.Common.Types.Misc;

namespace Mooege.Common.MPQ.FileFormats
{
    //[FileFormat(SNOGroup.Appearance)]
    public class Appearance : FileFormat
    {
        public Header Header;
        public Structure Structure; //16
        public int AppearanceLookCount; //1456
        public int AppearanceMaterialCount; //1460
        public List<AppearanceMaterial> AppearanceMaterials; //1464
        public List<AppearanceLook> AppearanceLooks; //1480
        public int StaticLightCount; //1496
        public List<StaticLight> StaticLights; //1500
        public int I2; //1520
        public int Time0; //1524
        public int Time1; //1528
        public int Time2; //1532
        public int Time3; //1536
        public int Time4; //1540
        public int I3; //1544
        public float F0; //1548
        public int I4; //1552
        public int I5; //1556
        public long L0; //1560
        public int I6; //1568

        public Appearance(MpqFile file)
        {
            var stream = file.Open();
            this.Header = new Header(stream);
            stream.Position += 4;
            this.Structure = new Structure(stream);
            this.AppearanceLookCount = stream.ReadValueS32();
            this.AppearanceMaterialCount = stream.ReadValueS32();
            this.AppearanceMaterials = stream.ReadSerializedData<AppearanceMaterial>();
            stream.Position += 8;
            this.AppearanceLooks = stream.ReadSerializedData<AppearanceLook>();
            stream.Position += 8;
            this.StaticLightCount = stream.ReadValueS32();
            if (this.StaticLightCount != 0)
                stream.Position += 0;
            this.StaticLights = stream.ReadSerializedData<StaticLight>();
            stream.Position += 12;
            this.I2 = stream.ReadValueS32();
            this.Time0 = stream.ReadValueS32();
            this.Time1 = stream.ReadValueS32();
            this.Time2 = stream.ReadValueS32();
            this.Time3 = stream.ReadValueS32();
            this.Time4 = stream.ReadValueS32();
            this.I3 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
            this.I4 = stream.ReadValueS32();
            this.I5 = stream.ReadValueS32();
            this.L0 = stream.ReadValueS64();
            this.I6 = stream.ReadValueS32();
            stream.Position += 4;
            stream.Close();

            this.AppearanceLooks = null;
            this.StaticLights = null;
            this.AppearanceMaterials = null;
            this.Structure = null;
        }
    }

    public class AppearanceMaterial : ISerializableData
    {
        public string S0;
        public List<SubObjectAppearance> SOAs;

        public void Read(MpqFileStream stream)
        {
            this.S0 = stream.ReadString(128, true);
            this.SOAs = stream.ReadSerializedData<SubObjectAppearance>();
            stream.Position += 4;
        }
    }

    public class SubObjectAppearance : ISerializableData
    {
        public int I0;
        public int SNOCloth;
        public List<TagMap> TagMap;
        public UberMaterial Material;
        public int SNOMaterial;

        public void Read(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.SNOCloth = stream.ReadValueS32();
            this.TagMap = stream.ReadSerializedData<TagMap>();
            stream.Position += 8;
            this.Material = new UberMaterial(stream);
            this.SNOMaterial = stream.ReadValueS32();
            stream.Position += 116;
        }
    }

    public class AppearanceLook : ISerializableData
    {
        public string S0;

        public void Read(MpqFileStream stream)
        {
            this.S0 = stream.ReadString(64);
        }
    }

    public class StaticLight : ISerializableData
    {
        public int CastsShadows;
        public int Type;
        public Vector3D P;
        public Vector3D V;
        public float Radius;
        public int AttenType;
        public float[] F0; //3
        public float AttenNearStart;
        public float AttenNearEnd;
        public float AttenFarStart;
        public float AttenFarEnd;
        public int Hotspot;
        public int Falloff;
        public RGBAColorValue RGBAValDiffuse;

        public void Read(MpqFileStream stream)
        {
            this.CastsShadows = stream.ReadValueS32();
            this.Type = stream.ReadValueS32();
            this.P = new Vector3D(stream);
            this.V = new Vector3D(stream);
            this.Radius = stream.ReadValueF32();
            this.AttenType = stream.ReadValueS32();
            this.F0 = new float[3];
            for (int i = 0; i < 3; i++)
                this.F0[i] = stream.ReadValueF32();
            this.AttenNearStart = stream.ReadValueF32();
            this.AttenNearEnd = stream.ReadValueF32();
            this.AttenFarStart = stream.ReadValueF32();
            this.AttenFarEnd = stream.ReadValueF32();
            this.Hotspot = stream.ReadValueS32();
            this.Falloff = stream.ReadValueS32();
            this.RGBAValDiffuse = new RGBAColorValue(stream);
        }
    }

    public class Structure
    {
        public int Flags; //0
        public int BoneCount; //4
        public List<BoneStructure> BoneStructure; //8
        public LookAtData LookAt; //24
        public int I0; //116
        public List<BonePulseData> BonePulses; //128
        public GeoSet[] Geoset; //136
        public Sphere Sphere; //184
        public int CollisionCapsuleCount; //200
        public List<CollisionCapsule> CollisionCapsules; //204
        public int HardpointCount; //224
        public List<HardPoint> HardPoints; //228
        public Vector3D V0; //248
        public Octree OctreeVisualMesh; //264
        public AABB AABBBounds; //336
        public int LoopContraintCount; //360
        public List<ConstraintParameters> LoopConstraints; //364
        public int RagdollDegrade; //384
        public string S0; //400
        public string S1; //656
        public string S2; //912
        public string S3; //1168
        public int I1; //1424
        public float F0; //1428
        public int I2; //1432

        public Structure(MpqFileStream stream)
        {
            this.Flags = stream.ReadValueS32();
            this.BoneCount = stream.ReadValueS32();
            this.BoneStructure = stream.ReadSerializedData<BoneStructure>();
            stream.Position += 8;
            this.LookAt = new LookAtData(stream);
            this.I0 = stream.ReadValueS32(); //116
            stream.Position += 8;
            this.BonePulses = stream.ReadSerializedData<BonePulseData>();
            this.Geoset = new GeoSet[2]; //136
            this.Geoset[0] = new GeoSet(stream); //+24
            this.Geoset[1] = new GeoSet(stream); //+24
            this.Sphere = new Sphere(stream); //184 + 16
            this.CollisionCapsuleCount = stream.ReadValueS32(); //200
            this.CollisionCapsules = stream.ReadSerializedData<CollisionCapsule>();
            stream.Position += 12;
            this.HardpointCount = stream.ReadValueS32(); //224
            this.HardPoints = stream.ReadSerializedData<HardPoint>();
            stream.Position += 12;
            this.V0 = new Vector3D(stream); //248 +12
            stream.Position += 4;
            this.OctreeVisualMesh = new Octree(stream); //264 + 72
            this.AABBBounds = new AABB(stream); //336 + 24
            this.LoopContraintCount = stream.ReadValueS32(); //360
            this.LoopConstraints = stream.ReadSerializedData<ConstraintParameters>();
            stream.Position += 12;
            this.RagdollDegrade = stream.ReadValueS32(); //384
            stream.Position += 12;
            this.S0 = stream.ReadString(256, true); //400
            this.S1 = stream.ReadString(256, true); //656
            this.S2 = stream.ReadString(256, true); //912
            this.S3 = stream.ReadString(256, true); //1168
            this.I1 = stream.ReadValueS32(); //1424
            this.F0 = stream.ReadValueF32(); //1428
            this.I2 = stream.ReadValueS32(); //1432
            stream.Position += 4;
        }

    }

    public class BoneStructure : ISerializableData
    {
        public string S0;
        public int I0;
        public AABB AABBBounds;
        public Sphere Bounds;
        public PRSTransform Transform0;
        public PRSTransform Transform1;
        public PRSTransform Transform2;
        public int I1;
        public List<CollisionShape> Shapes;
        public List<ConstraintParameters> Constraint;
        public int SNOParticleSystem;

        public void Read(MpqFileStream stream)
        {
            this.S0 = stream.ReadString(64, true);
            this.I0 = stream.ReadValueS32();
            this.AABBBounds = new AABB(stream);
            this.Bounds = new Sphere(stream);
            this.Transform0 = new PRSTransform(stream);
            this.Transform1 = new PRSTransform(stream);
            this.Transform2 = new PRSTransform(stream);
            this.I1 = stream.ReadValueS32();
            this.Shapes = stream.ReadSerializedData<CollisionShape>();
            stream.Position += 4;
            this.Constraint = stream.ReadSerializedData<ConstraintParameters>();
            stream.Position += 4;
            this.SNOParticleSystem = stream.ReadValueS32();
        }
    }

    public class PRSTransform
    {
        public Quaternion Q0;
        public Vector3D V0;
        public float F0;

        public PRSTransform(MpqFileStream stream)
        {
            this.Q0 = new Quaternion(stream);
            this.V0 = new Vector3D(stream);
            this.F0 = stream.ReadValueF32();
        }
    }

    public class CollisionShape : ISerializableData
    {
        public int I0;
        public int I1;
        public int I2;
        public int I3;
        public float F0;
        public float F1;
        public float F2;
        public List<PolytopeData> Polytope;
        public Vector3D V0;
        public Vector3D V1;
        public float F3;

        public void Read(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
            this.I2 = stream.ReadValueS32();
            this.I3 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
            this.F2 = stream.ReadValueF32();
            this.Polytope = stream.ReadSerializedData<PolytopeData>();
            stream.Position += 4;
            this.V0 = new Vector3D(stream);
            this.V1 = new Vector3D(stream);
            this.F3 = stream.ReadValueF32();
        }
    }

    public class ConstraintParameters : ISerializableData
    {
        public string S0;
        public int I0;
        public int I1;
        public int I2;
        public int I3;
        public PRTransform Transform0;
        public Vector3D V0;
        public PRTransform Transform1;
        public PRTransform Transform2;
        public float F0;
        public float F1;
        public float F2;
        public float F3;
        public float F4;
        public float F5;
        public float F6;
        public float F7;
        public float F8;
        public string S1;

        public void Read(MpqFileStream stream)
        {
            this.S0 = stream.ReadString(64, true);
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
            this.I2 = stream.ReadValueS32();
            this.I3 = stream.ReadValueS32();
            this.Transform0 = new PRTransform(stream);
            this.V0 = new Vector3D(stream);
            this.Transform1 = new PRTransform(stream);
            this.Transform2 = new PRTransform(stream);
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
            this.F2 = stream.ReadValueF32();
            this.F3 = stream.ReadValueF32();
            this.F4 = stream.ReadValueF32();
            this.F5 = stream.ReadValueF32();
            this.F6 = stream.ReadValueF32();
            this.F7 = stream.ReadValueF32();
            this.F8 = stream.ReadValueF32();
            this.S1 = stream.ReadString(64, true);
        }
    }

    public class PolytopeData : ISerializableData
    {
        public Float3 FFF0;
        public List<Float3> Vertices;
        public List<Plane> Planes;
        public List<SubEdge> SubEdges;
        public List<sbyte> FaceSubEdges;
        public int I0;
        public int I1;
        public int I2;
        public float F0;
        public float F1;

        public void Read(MpqFileStream stream)
        {
            stream.Position += 32;
            this.FFF0 = new Float3(stream); //32
            this.I0 = stream.ReadValueS32(); //44
            this.I1 = stream.ReadValueS32(); //48
            this.I2 = stream.ReadValueS32(); //52
            this.F0 = stream.ReadValueF32(); //56
            this.F1 = stream.ReadValueF32(); //60
            this.Vertices = stream.ReadSerializedData<Float3>(); //64
            this.Planes = stream.ReadSerializedData<Plane>(); //72
            this.SubEdges = stream.ReadSerializedData<SubEdge>(); //80
            this.FaceSubEdges = stream.ReadSerializedBytes(); //88
        }
    }

    public class LookAtData
    {
        public int I0;
        public string S0;
        public float Angle0;
        public float Angle1;
        public float Angle2;
        public float Angle3;
        public float AngularVelocity;
        public float F0;

        public LookAtData(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.S0 = stream.ReadString(64, true);
            this.Angle0 = stream.ReadValueF32();
            this.Angle1 = stream.ReadValueF32();
            this.Angle2 = stream.ReadValueF32();
            this.Angle3 = stream.ReadValueF32();
            this.AngularVelocity = stream.ReadValueF32();
            this.F0 = stream.ReadValueF32();
        }
    }

    public class BonePulseData : ISerializableData
    {
        public string S0;
        public float F0;
        public float F1;
        public float Angle;

        public void Read(MpqFileStream stream)
        {
            this.S0 = stream.ReadString(64);
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
            this.Angle = stream.ReadValueF32();
        }
    }

    public class CollisionCapsule: ISerializableData
    {
        public float F0;
        public float F1;
        public HardPoint HP;

        public void Read(MpqFileStream stream)
        {
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
            this.HP = new HardPoint(stream);
        }
    }

    public class GeoSet
    {
        public int ObjectCount;
        public List<SubObject> SubObjects;

        public GeoSet(MpqFileStream stream)
        {
            this.ObjectCount = stream.ReadValueS32();
            this.SubObjects = stream.ReadSerializedData<SubObject>();
            stream.Position += 12;
        }
    }

    public class SubObject : ISerializableData
    {
        public int I0;
        public int VertCount;
        public List<FatVertex> VertList;
        public List<VertInfluences> InfluenceList;
        public int IndexCount;
        public List<int> IndexList;
        public List<ClothStructure> ClothStructure;
        public int SNOSurface;
        public int I1;
        public float F0;
        public string S0;
        public string S1;
        public AABB AABBBounds;
        public int ShapeCount;
        public List<CollisionShape> Shapes;

        public void Read(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.VertCount = stream.ReadValueS32();
            this.VertList = stream.ReadSerializedData<FatVertex>();
            stream.Position += 4;
            this.InfluenceList = stream.ReadSerializedData<VertInfluences>();
            stream.Position += 4;
            this.IndexCount = stream.ReadValueS32();
            this.IndexList = stream.ReadSerializedInts();
            stream.Position += 4;
            this.ClothStructure = stream.ReadSerializedData<ClothStructure>();
            stream.Position += 4;
            this.SNOSurface = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
            this.S0 = stream.ReadString(128, true);
            this.S1 = stream.ReadString(128, true);
            this.AABBBounds = new AABB(stream);
            this.ShapeCount = stream.ReadValueS32();
            this.Shapes = stream.ReadSerializedData<CollisionShape>();
            stream.Position += 8;
        }
    }

    public class FatVertex : ISerializableData
    {
        public Vector3D V0;
        public RGBAColor Color0;
        public RGBAColor[] Color1;
        public RGBAColor[] Color2;
        public RGBAColor Color3;
        public RGBAColor Color4;
        public int I0;

        public void Read(MpqFileStream stream)
        {
            this.V0 = new Vector3D(stream);
            this.Color0 = new RGBAColor(stream);
            this.Color1 = new RGBAColor[2];
            this.Color1[0] = new RGBAColor(stream);
            this.Color1[1] = new RGBAColor(stream);
            this.Color2 = new RGBAColor[2];
            this.Color2[0] = new RGBAColor(stream);
            this.Color2[1] = new RGBAColor(stream);
            this.Color3 = new RGBAColor(stream);
            this.Color4 = new RGBAColor(stream);
            this.I0 = stream.ReadValueS32();
        }
    }

    public class VertInfluences : ISerializableData
    {
        public Influence[] Influences;

        public void Read(MpqFileStream stream)
        {
            this.Influences = new Influence[3];
            for (int i = 0; i < 3; i++)
                this.Influences[i] = new Influence(stream);
        }
    }

    public class Influence
    {
        public int I0;
        public float F0;

        public Influence(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
        }
    }

    public class ClothStructure : ISerializableData
    {
        public int VerticeCount; //0
        public List<ClothVertex> Vertices; //4
        public int FaceCount; //16
        public List<ClothFace> Faces; //20
        public int StapleCount; //32
        public List<ClothStaple> Staples;  //36
        public int DistanceConstraintCount; //48
        public List<ClothConstraint> DistanceConstraints; //52
        public int BendingConstraintCount; //64
        public List<ClothConstraint> BendingConstraints; //68
        public int I5; //80
        public float F0; //84

        public void Read(MpqFileStream stream)
        {
            this.VerticeCount = stream.ReadValueS32();
            this.Vertices = stream.ReadSerializedData<ClothVertex>();
            stream.Position += 4;
            this.FaceCount = stream.ReadValueS32();
            this.Faces = stream.ReadSerializedData<ClothFace>();
            stream.Position += 4;
            this.StapleCount = stream.ReadValueS32();
            this.Staples = stream.ReadSerializedData<ClothStaple>();
            stream.Position += 4;
            this.DistanceConstraintCount = stream.ReadValueS32();
            this.DistanceConstraints = stream.ReadSerializedData<ClothConstraint>();
            stream.Position += 4;
            this.BendingConstraintCount = stream.ReadValueS32();
            this.BendingConstraints = stream.ReadSerializedData<ClothConstraint>();
            stream.Position += 4;
            this.I5 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
        }
    }

    public class ClothVertex : ISerializableData
    {
        public Vector3D V0;
        public Vector3D V1;
        public Vector3D V2;
        public Vector3D V3;
        public float F0;
        public int I0;
        public int I1;
        public int I2;
        public int I3;
        public Vector3D V4;
        public int I4;

        public void Read(MpqFileStream stream)
        {
            this.V0 = new Vector3D(stream);
            this.V1 = new Vector3D(stream);
            this.V2 = new Vector3D(stream);
            this.V3 = new Vector3D(stream);
            this.F0 = stream.ReadValueF32();
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
            this.I2 = stream.ReadValueS32();
            this.I3 = stream.ReadValueS32();
            this.V4 = new Vector3D(stream);
            this.I4 = stream.ReadValueS32();
        }
    }

    public class ClothFace : ISerializableData
    {
        public Vector3D V0;
        public float F0;
        public int I0;
        public int I1;
        public int I2;

        public void Read(MpqFileStream stream)
        {
            this.V0 = new Vector3D(stream);
            this.F0 = stream.ReadValueF32();
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
            this.I2 = stream.ReadValueS32();
        }
    }

    public class ClothStaple : ISerializableData
    {
        public int I0;
        public int[] I1;
        public float[] F0;

        public void Read(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.I1 = new int[3];
            for ( int i = 0; i < 3; i++)
                this.I1[i] = stream.ReadValueS32();
            this.F0 = new float[3];
            for (int i = 0; i < 3; i++)
                this.F0[i] = stream.ReadValueF32();
        }
    }

    public class ClothConstraint : ISerializableData
    {
        public int I0;
        public int I1;
        public float F0;
        public float F1;
        public float F2;
        public float F3;

        public void Read(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
            this.F2 = stream.ReadValueF32();
            this.F3 = stream.ReadValueF32();
        }

    }

    public class HardPoint : ISerializableData
    {
        public string S0;
        public int I0;
        public PRTransform Transform;

        public HardPoint() { }

        public HardPoint(MpqFileStream stream)
        {
            this.S0 = stream.ReadString(64, true);
            this.I0 = stream.ReadValueS32();
            this.Transform = new PRTransform(stream);
        }

        public void Read(MpqFileStream stream)
        {
            this.S0 = stream.ReadString(64, true);
            this.I0 = stream.ReadValueS32();
            this.Transform = new PRTransform(stream);
        }
    }

    public class Octree
    {
        public int MaxNodes;
        public int MaxLeaves;
        public int MaxPrimitives;
        public int NodeCount;
        public int LeafCount;
        public int PrimitivesCount;
        public List<OctreeNode> Nodes;
        public List<OctreeLeaf> Leaves;
        public List<OctreePrimitive> Primitives;

        public Octree(MpqFileStream stream)
        {
            this.MaxNodes = stream.ReadValueS32();
            this.MaxLeaves = stream.ReadValueS32();
            this.MaxPrimitives = stream.ReadValueS32();
            this.NodeCount = stream.ReadValueS32();
            this.LeafCount = stream.ReadValueS32();
            this.PrimitivesCount = stream.ReadValueS32();
            this.Nodes = stream.ReadSerializedData<OctreeNode>();
            stream.Position += 8;
            this.Leaves = stream.ReadSerializedData<OctreeLeaf>();
            stream.Position += 8;
            this.Primitives = stream.ReadSerializedData<OctreePrimitive>();
            stream.Position += 8;
        }
    }

    public class OctreeNode : ISerializableData
    {
        public OctreeCube Cube;
        public int[] I0;

        public void Read(MpqFileStream stream)
        {
            this.Cube = new OctreeCube(stream);
            this.I0 = new int[8];
            for (int i = 0; i < 8; i++)
                this.I0[i] = stream.ReadValueS32();
        }

    }

    public class OctreeLeaf : ISerializableData
    {
        public OctreeCube Cube;
        public int I0;
        public int I1;

        public void Read(MpqFileStream stream)
        {
            this.Cube = new OctreeCube(stream);
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
        }

    }

    public class OctreePrimitive : ISerializableData
    {
        public int I0;
        public int I1;

        public void Read(MpqFileStream stream)
        {
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
        }
    }

    public class OctreeCube
    {
        public Vector3D V0;
        public float F0;

        public OctreeCube(MpqFileStream stream)
        {
            this.V0 = new Vector3D(stream);
            this.F0 = stream.ReadValueF32();
        }
    }

    public class Plane : ISerializableData
    {
        public Float3 FFF0;
        public float F0;

        public void Read(MpqFileStream stream)
        {
            this.FFF0 = new Float3(stream);
            this.F0 = stream.ReadValueF32();
        }
    }

    public class SubEdge : ISerializableData
    {
        public sbyte C0;
        public sbyte C1;
        public sbyte C2;
        public sbyte C3;

        public void Read(MpqFileStream stream)
        {
            this.C0 = stream.ReadValueS8();
            this.C1 = stream.ReadValueS8();
            this.C2 = stream.ReadValueS8();
            this.C3 = stream.ReadValueS8();
        }
    }


}


