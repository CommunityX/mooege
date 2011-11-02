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
    //[FileFormat(SNOGroup.Sound)]
    public class Sound : FileFormat
    {
        public Header Header { get; private set; }
        public int I0 { get; private set; }
        public int I1 { get; private set; }
        public float F0 { get; private set; }
        public float F1 { get; private set; }
        public float F2 { get; private set; }
        public int I2 { get; private set; }
        public float F3 { get; private set; }
        public float F4 { get; private set; }
        public float F5 { get; private set; }
        public int Impulse0 { get; private set; }
        public int Impulse1 { get; private set; }
        public float F6 { get; private set; }
        public float F7 { get; private set; }
        public int I3 { get; private set; }
        public int I4 { get; private set; }
        public int I5 { get; private set; }
        public int Ticks { get; private set; }
        public List<SoundPermutation> Permutations { get; private set; }
        public List<DspEffect> DspEffects { get; private set; }

        public Sound(MpqFile file)
        {
            var stream = file.Open();
            this.Header = new Header(stream);
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
            this.F2 = stream.ReadValueF32();
            this.I2 = stream.ReadValueS32();
            this.F3 = stream.ReadValueF32();
            this.F4 = stream.ReadValueF32();
            this.F5 = stream.ReadValueF32();
            this.Impulse0 = stream.ReadValueS32();
            this.Impulse1 = stream.ReadValueS32();
            this.F6 = stream.ReadValueF32();
            this.F7 = stream.ReadValueF32();
            this.Permutations = stream.ReadSerializedData<SoundPermutation>();
            this.I3 = stream.ReadValueS32();
            stream.Position += 4;
            this.I4 = stream.ReadValueS32();
            this.I5 = stream.ReadValueS32(); //84
            stream.Position += 4; //before or after dpseffects?
            this.DspEffects = stream.ReadSerializedData<DspEffect>();
            this.Ticks = stream.ReadValueS32(); //100
            stream.Position += 20;
            stream.Close();
        }
    }

    public class SoundPermutation : ISerializableData
    {
        public int SNOSoundBank { get; private set; }
        public string S0 { get; private set; }
        public int I0 { get; private set; }
        public float F0 { get; private set; }
        public float F1 { get; private set; }
        public float F2 { get; private set; }
        public float F3 { get; private set; }
        public int Tick1 { get; private set; } //sound length?
        public int Tick2 { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.SNOSoundBank = stream.ReadValueS32();
            this.S0 = stream.ReadString(129, true);
            stream.Position += 3;
            this.I0 = stream.ReadValueS32();
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
            this.F2 = stream.ReadValueF32();
            this.F3 = stream.ReadValueF32();
            this.Tick1 = stream.ReadValueS32();
            this.Tick2 = stream.ReadValueS32();
            stream.Position += 8;
        }
    }

    public class DspParam
    {
        public float F0 { get; private set; }
        public float F1 { get; private set; }
        public int I0 { get; private set; }
        public int I1 { get; private set; }

        public DspParam(MpqFileStream stream)
        {
            this.F0 = stream.ReadValueF32();
            this.F1 = stream.ReadValueF32();
            this.I0 = stream.ReadValueS32();
            this.I1 = stream.ReadValueS32();
        }
    }

    public class DspEffect : ISerializableData
    {
        public Effect Effect { get; private set; }
        public DspParam Param0 { get; private set; }
        public DspParam Param1 { get; private set; }
        public DspParam Param2 { get; private set; }
        public DspParam Param3 { get; private set; }
        public DspParam Param4 { get; private set; }
        public DspParam Param5 { get; private set; }
        public DspParam Param6 { get; private set; }
        public DspParam Param7 { get; private set; }

        public void Read(MpqFileStream stream)
        {
            this.Effect = (Effect)stream.ReadValueS32();
            stream.Position += 4;
            this.Param0 = new DspParam(stream);
            this.Param1 = new DspParam(stream);
            this.Param2 = new DspParam(stream);
            this.Param3 = new DspParam(stream);
            this.Param4 = new DspParam(stream);
            this.Param5 = new DspParam(stream);
            this.Param6 = new DspParam(stream);
            this.Param7 = new DspParam(stream);
        }
    }

    public enum Effect : int
    {
        Chorus = 0,
        Compressor,
        Distortion,
        Echo,
        Flange,
        HighPassFilter,
        LowPassFilter,
        LowPassFilterSimple,
        ParametricEQ,
        Reverb,
    }


}




