/*
    Copyright(c) 2017 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using RageLib.Hash;
using RageLib.Resources.Common;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // crBoneData
    public class Bone : ResourceSystemBlock
    {
        public override long BlockLength => 0x50;

        // structure data
        public Quaternion Rotation;
        public Vector3 Translation;
        public uint Unknown_1Ch; // 0x00000000
        public Vector3 Scale;
        public float Unknown_2Ch; // 1.0
        public short NextSiblingIndex;
        public short ParentIndex;
        public uint Unknown_34h; // 0x00000000
        public ulong NamePointer;
        public BoneFlags Flags;
        public short Index;
        public ushort BoneId;
        public ushort Unknown_46h;
        public uint Unknown_48h; // 0x00000000
        public uint Unknown_4Ch; // 0x00000000

        // reference data
        public string_r Name;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Rotation = reader.ReadQuaternion();
            this.Translation = reader.ReadVector3();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.Scale = reader.ReadVector3();
            this.Unknown_2Ch = reader.ReadSingle();
            this.NextSiblingIndex = reader.ReadInt16();
            this.ParentIndex = reader.ReadInt16();
            this.Unknown_34h = reader.ReadUInt32();
            this.NamePointer = reader.ReadUInt64();
            this.Flags = (BoneFlags)reader.ReadUInt16();
            this.Index = reader.ReadInt16();
            this.BoneId = reader.ReadUInt16();
            this.Unknown_46h = reader.ReadUInt16();
            this.Unknown_48h = reader.ReadUInt32();
            this.Unknown_4Ch = reader.ReadUInt32();

            // read reference data
            this.Name = reader.ReadBlockAt<string_r>(
                this.NamePointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.NamePointer = (ulong)(this.Name != null ? this.Name.BlockPosition : 0);

            // write structure data
            writer.Write(this.Rotation);
            writer.Write(this.Translation);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.Scale);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.NextSiblingIndex);
            writer.Write(this.ParentIndex);
            writer.Write(this.Unknown_34h);
            writer.Write(this.NamePointer);
            writer.Write((ushort)this.Flags);
            writer.Write(this.Index);
            writer.Write(this.BoneId);
            writer.Write(this.Unknown_46h);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_4Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Name != null) list.Add(Name);
            return list.ToArray();
        }

        public static ushort CalculateBoneIdFromName(string boneName)
        {
            return (ushort)(Elf.Hash(boneName.ToUpperInvariant()) % 0xFE8F + 0x170);
        }
    }

    [Flags]
    public enum BoneFlags : ushort
    {
        None = 0,
        RotX = 0x1,
        RotY = 0x2,
        RotZ = 0x4,
        LimitRotation = 0x8,
        TransX = 0x10,
        TransY = 0x20,
        TransZ = 0x40,
        LimitTranslation = 0x80,
        ScaleX = 0x100,
        ScaleY = 0x200,
        ScaleZ = 0x400,
        LimitScale = 0x800,
        Unk0 = 0x1000,
        Unk1 = 0x2000,
        Unk2 = 0x4000,
        Unk3 = 0x8000,
    }

    public enum PedBoneId : ushort
    {
        SKEL_ROOT = 0x0,
        SKEL_Pelvis = 0x2E28,
        SKEL_L_Thigh = 0xE39F,
        SKEL_L_Calf = 0xF9BB,
        SKEL_L_Foot = 0x3779,
        SKEL_L_Toe0 = 0x83C,
        EO_L_Foot = 0x84C5,
        EO_L_Toe = 0x68BD,
        IK_L_Foot = 0xFEDD,
        PH_L_Foot = 0xE175,
        MH_L_Knee = 0xB3FE,
        SKEL_R_Thigh = 0xCA72,
        SKEL_R_Calf = 0x9000,
        SKEL_R_Foot = 0xCC4D,
        SKEL_R_Toe0 = 0x512D,
        EO_R_Foot = 0x1096,
        EO_R_Toe = 0x7163,
        IK_R_Foot = 0x8AAE,
        PH_R_Foot = 0x60E6,
        MH_R_Knee = 0x3FCF,
        RB_L_ThighRoll = 0x5C57,
        RB_R_ThighRoll = 0x192A,
        SKEL_Spine_Root = 0xE0FD,
        SKEL_Spine0 = 0x5C01,
        SKEL_Spine1 = 0x60F0,
        SKEL_Spine2 = 0x60F1,
        SKEL_Spine3 = 0x60F2,
        SKEL_L_Clavicle = 0xFCD9,
        SKEL_L_UpperArm = 0xB1C5,
        SKEL_L_Forearm = 0xEEEB,
        SKEL_L_Hand = 0x49D9,
        SKEL_L_Finger00 = 0x67F2,
        SKEL_L_Finger01 = 0xFF9,
        SKEL_L_Finger02 = 0xFFA,
        SKEL_L_Finger10 = 0x67F3,
        SKEL_L_Finger11 = 0x1049,
        SKEL_L_Finger12 = 0x104A,
        SKEL_L_Finger20 = 0x67F4,
        SKEL_L_Finger21 = 0x1059,
        SKEL_L_Finger22 = 0x105A,
        SKEL_L_Finger30 = 0x67F5,
        SKEL_L_Finger31 = 0x1029,
        SKEL_L_Finger32 = 0x102A,
        SKEL_L_Finger40 = 0x67F6,
        SKEL_L_Finger41 = 0x1039,
        SKEL_L_Finger42 = 0x103A,
        PH_L_Hand = 0xEB95,
        IK_L_Hand = 0x8CBD,
        RB_L_ForeArmRoll = 0xEE4F,
        RB_L_ArmRoll = 0x1470,
        MH_L_Elbow = 0x58B7,
        SKEL_R_Clavicle = 0x29D2,
        SKEL_R_UpperArm = 0x9D4D,
        SKEL_R_Forearm = 0x6E5C,
        SKEL_R_Hand = 0xDEAD,
        SKEL_R_Finger00 = 0xE5F2,
        SKEL_R_Finger01 = 0xFA10,
        SKEL_R_Finger02 = 0xFA11,
        SKEL_R_Finger10 = 0xE5F3,
        SKEL_R_Finger11 = 0xFA60,
        SKEL_R_Finger12 = 0xFA61,
        SKEL_R_Finger20 = 0xE5F4,
        SKEL_R_Finger21 = 0xFA70,
        SKEL_R_Finger22 = 0xFA71,
        SKEL_R_Finger30 = 0xE5F5,
        SKEL_R_Finger31 = 0xFA40,
        SKEL_R_Finger32 = 0xFA41,
        SKEL_R_Finger40 = 0xE5F6,
        SKEL_R_Finger41 = 0xFA50,
        SKEL_R_Finger42 = 0xFA51,
        PH_R_Hand = 0x6F06,
        IK_R_Hand = 0x188E,
        RB_R_ForeArmRoll = 0xAB22,
        RB_R_ArmRoll = 0x90FF,
        MH_R_Elbow = 0xBB0,
        SKEL_Neck_1 = 0x9995,
        SKEL_Head = 0x796E,
        IK_Head = 0x322C,
        FACIAL_facialRoot = 0xFE2C,
        FB_L_Brow_Out_000 = 0xE3DB,
        FB_L_Lid_Upper_000 = 0xB2B6,
        FB_L_Eye_000 = 0x62AC,
        FB_L_CheekBone_000 = 0x542E,
        FB_L_Lip_Corner_000 = 0x74AC,
        FB_R_Lid_Upper_000 = 0xAA10,
        FB_R_Eye_000 = 0x6B52,
        FB_R_CheekBone_000 = 0x4B88,
        FB_R_Brow_Out_000 = 0x54C,
        FB_R_Lip_Corner_000 = 0x2BA6,
        FB_Brow_Centre_000 = 0x9149,
        FB_UpperLipRoot_000 = 0x4ED2,
        FB_UpperLip_000 = 0xF18F,
        FB_L_Lip_Top_000 = 0x4F37,
        FB_R_Lip_Top_000 = 0x4537,
        FB_Jaw_000 = 0xB4A0,
        FB_LowerLipRoot_000 = 0x4324,
        FB_LowerLip_000 = 0x508F,
        FB_L_Lip_Bot_000 = 0xB93B,
        FB_R_Lip_Bot_000 = 0xC33B,
        FB_Tongue_000 = 0xB987,
        RB_Neck_1 = 0x8B93,
        SPR_L_Breast = 0xFC8E,
        SPR_R_Breast = 0x885F,
        IK_Root = 0xDD1C,
        SKEL_Neck_2 = 0x5FD4,
        SKEL_Pelvis1 = 0xD003,
        SKEL_PelvisRoot = 0x45FC,
        SKEL_SADDLE = 0x9524,
        MH_L_CalfBack = 0x1013,
        MH_L_ThighBack = 0x600D,
        SM_L_Skirt = 0xC419,
        MH_R_CalfBack = 0xB013,
        MH_R_ThighBack = 0x51A3,
        SM_R_Skirt = 0x7712,
        SM_M_BackSkirtRoll = 0xDBB,
        SM_L_BackSkirtRoll = 0x40B2,
        SM_R_BackSkirtRoll = 0xC141,
        SM_M_FrontSkirtRoll = 0xCDBB,
        SM_L_FrontSkirtRoll = 0x9B69,
        SM_R_FrontSkirtRoll = 0x86F1,
        SM_CockNBalls_ROOT = 0xC67D,
        SM_CockNBalls = 0x9D34,
        MH_L_Finger00 = 0x8C63,
        MH_L_FingerBulge00 = 0x5FB8,
        MH_L_Finger10 = 0x8C53,
        MH_L_FingerTop00 = 0xA244,
        MH_L_HandSide = 0xC78A,
        MH_Watch = 0x2738,
        MH_L_Sleeve = 0x933C,
        MH_R_Finger00 = 0x2C63,
        MH_R_FingerBulge00 = 0x69B8,
        MH_R_Finger10 = 0x2C53,
        MH_R_FingerTop00 = 0xEF4B,
        MH_R_HandSide = 0x68FB,
        MH_R_Sleeve = 0x92DC,
        FACIAL_jaw = 0xB21,
        FACIAL_underChin = 0x8A95,
        FACIAL_L_underChin = 0x234E,
        FACIAL_chin = 0xB578,
        FACIAL_chinSkinBottom = 0x98BC,
        FACIAL_L_chinSkinBottom = 0x3E8F,
        FACIAL_R_chinSkinBottom = 0x9E8F,
        FACIAL_tongueA = 0x4A7C,
        FACIAL_tongueB = 0x4A7D,
        FACIAL_tongueC = 0x4A7E,
        FACIAL_tongueD = 0x4A7F,
        FACIAL_tongueE = 0x4A80,
        FACIAL_L_tongueE = 0x35F2,
        FACIAL_R_tongueE = 0x2FF2,
        FACIAL_L_tongueD = 0x35F1,
        FACIAL_R_tongueD = 0x2FF1,
        FACIAL_L_tongueC = 0x35F0,
        FACIAL_R_tongueC = 0x2FF0,
        FACIAL_L_tongueB = 0x35EF,
        FACIAL_R_tongueB = 0x2FEF,
        FACIAL_L_tongueA = 0x35EE,
        FACIAL_R_tongueA = 0x2FEE,
        FACIAL_chinSkinTop = 0x7226,
        FACIAL_L_chinSkinTop = 0x3EB3,
        FACIAL_chinSkinMid = 0x899A,
        FACIAL_L_chinSkinMid = 0x4427,
        FACIAL_L_chinSide = 0x4A5E,
        FACIAL_R_chinSkinMid = 0xF5AF,
        FACIAL_R_chinSkinTop = 0xF03B,
        FACIAL_R_chinSide = 0xAA5E,
        FACIAL_R_underChin = 0x2BF4,
        FACIAL_L_lipLowerSDK = 0xB9E1,
        FACIAL_L_lipLowerAnalog = 0x244A,
        FACIAL_L_lipLowerThicknessV = 0xC749,
        FACIAL_L_lipLowerThicknessH = 0xC67B,
        FACIAL_lipLowerSDK = 0x7285,
        FACIAL_lipLowerAnalog = 0xD97B,
        FACIAL_lipLowerThicknessV = 0xC5BB,
        FACIAL_lipLowerThicknessH = 0xC5ED,
        FACIAL_R_lipLowerSDK = 0xA034,
        FACIAL_R_lipLowerAnalog = 0xC2D9,
        FACIAL_R_lipLowerThicknessV = 0xC6E9,
        FACIAL_R_lipLowerThicknessH = 0xC6DB,
        FACIAL_nose = 0x20F1,
        FACIAL_L_nostril = 0x7322,
        FACIAL_L_nostrilThickness = 0xC15F,
        FACIAL_noseLower = 0xE05A,
        FACIAL_L_noseLowerThickness = 0x79D5,
        FACIAL_R_noseLowerThickness = 0x7975,
        FACIAL_noseTip = 0x6A60,
        FACIAL_R_nostril = 0x7922,
        FACIAL_R_nostrilThickness = 0x36FF,
        FACIAL_noseUpper = 0xA04F,
        FACIAL_L_noseUpper = 0x1FB8,
        FACIAL_noseBridge = 0x9BA3,
        FACIAL_L_nasolabialFurrow = 0x5ACA,
        FACIAL_L_nasolabialBulge = 0xCD78,
        FACIAL_L_cheekLower = 0x6907,
        FACIAL_L_cheekLowerBulge1 = 0xE3FB,
        FACIAL_L_cheekLowerBulge2 = 0xE3FC,
        FACIAL_L_cheekInner = 0xE7AB,
        FACIAL_L_cheekOuter = 0x8161,
        FACIAL_L_eyesackLower = 0x771B,
        FACIAL_L_eyeball = 0x1744,
        FACIAL_L_eyelidLower = 0x998C,
        FACIAL_L_eyelidLowerOuterSDK = 0xFE4C,
        FACIAL_L_eyelidLowerOuterAnalog = 0xB9AA,
        FACIAL_L_eyelashLowerOuter = 0xD7F6,
        FACIAL_L_eyelidLowerInnerSDK = 0xF151,
        FACIAL_L_eyelidLowerInnerAnalog = 0x8242,
        FACIAL_L_eyelashLowerInner = 0x4CCF,
        FACIAL_L_eyelidUpper = 0x97C1,
        FACIAL_L_eyelidUpperOuterSDK = 0xAF15,
        FACIAL_L_eyelidUpperOuterAnalog = 0x67FA,
        FACIAL_L_eyelashUpperOuter = 0x27B7,
        FACIAL_L_eyelidUpperInnerSDK = 0xD341,
        FACIAL_L_eyelidUpperInnerAnalog = 0xF092,
        FACIAL_L_eyelashUpperInner = 0x9B1F,
        FACIAL_L_eyesackUpperOuterBulge = 0xA559,
        FACIAL_L_eyesackUpperInnerBulge = 0x2F2A,
        FACIAL_L_eyesackUpperOuterFurrow = 0xC597,
        FACIAL_L_eyesackUpperInnerFurrow = 0x52A7,
        FACIAL_forehead = 0x9218,
        FACIAL_L_foreheadInner = 0x843,
        FACIAL_L_foreheadInnerBulge = 0x767C,
        FACIAL_L_foreheadOuter = 0x8DCB,
        FACIAL_skull = 0x4221,
        FACIAL_foreheadUpper = 0xF7D6,
        FACIAL_L_foreheadUpperInner = 0xCF13,
        FACIAL_L_foreheadUpperOuter = 0x509B,
        FACIAL_R_foreheadUpperInner = 0xCEF3,
        FACIAL_R_foreheadUpperOuter = 0x507B,
        FACIAL_L_temple = 0xAF79,
        FACIAL_L_ear = 0x19DD,
        FACIAL_L_earLower = 0x6031,
        FACIAL_L_masseter = 0x2810,
        FACIAL_L_jawRecess = 0x9C7A,
        FACIAL_L_cheekOuterSkin = 0x14A5,
        FACIAL_R_cheekLower = 0xF367,
        FACIAL_R_cheekLowerBulge1 = 0x599B,
        FACIAL_R_cheekLowerBulge2 = 0x599C,
        FACIAL_R_masseter = 0x810,
        FACIAL_R_jawRecess = 0x93D4,
        FACIAL_R_ear = 0x1137,
        FACIAL_R_earLower = 0x8031,
        FACIAL_R_eyesackLower = 0x777B,
        FACIAL_R_nasolabialBulge = 0xD61E,
        FACIAL_R_cheekOuter = 0xD32,
        FACIAL_R_cheekInner = 0x737C,
        FACIAL_R_noseUpper = 0x1CD6,
        FACIAL_R_foreheadInner = 0xE43,
        FACIAL_R_foreheadInnerBulge = 0x769C,
        FACIAL_R_foreheadOuter = 0x8FCB,
        FACIAL_R_cheekOuterSkin = 0xB334,
        FACIAL_R_eyesackUpperInnerFurrow = 0x9FAE,
        FACIAL_R_eyesackUpperOuterFurrow = 0x140F,
        FACIAL_R_eyesackUpperInnerBulge = 0xA359,
        FACIAL_R_eyesackUpperOuterBulge = 0x1AF9,
        FACIAL_R_nasolabialFurrow = 0x2CAA,
        FACIAL_R_temple = 0xAF19,
        FACIAL_R_eyeball = 0x1944,
        FACIAL_R_eyelidUpper = 0x7E14,
        FACIAL_R_eyelidUpperOuterSDK = 0xB115,
        FACIAL_R_eyelidUpperOuterAnalog = 0xF25A,
        FACIAL_R_eyelashUpperOuter = 0xE0A,
        FACIAL_R_eyelidUpperInnerSDK = 0xD541,
        FACIAL_R_eyelidUpperInnerAnalog = 0x7C63,
        FACIAL_R_eyelashUpperInner = 0x8172,
        FACIAL_R_eyelidLower = 0x7FDF,
        FACIAL_R_eyelidLowerOuterSDK = 0x1BD,
        FACIAL_R_eyelidLowerOuterAnalog = 0x457B,
        FACIAL_R_eyelashLowerOuter = 0xBE49,
        FACIAL_R_eyelidLowerInnerSDK = 0xF351,
        FACIAL_R_eyelidLowerInnerAnalog = 0xE13,
        FACIAL_R_eyelashLowerInner = 0x3322,
        FACIAL_L_lipUpperSDK = 0x8F30,
        FACIAL_L_lipUpperAnalog = 0xB1CF,
        FACIAL_L_lipUpperThicknessH = 0x37CE,
        FACIAL_L_lipUpperThicknessV = 0x38BC,
        FACIAL_lipUpperSDK = 0x1774,
        FACIAL_lipUpperAnalog = 0xE064,
        FACIAL_lipUpperThicknessH = 0x7993,
        FACIAL_lipUpperThicknessV = 0x7981,
        FACIAL_L_lipCornerSDK = 0xB1C,
        FACIAL_L_lipCornerAnalog = 0xE568,
        FACIAL_L_lipCornerThicknessUpper = 0x7BC,
        FACIAL_L_lipCornerThicknessLower = 0xDD42,
        FACIAL_R_lipUpperSDK = 0x7583,
        FACIAL_R_lipUpperAnalog = 0x51CF,
        FACIAL_R_lipUpperThicknessH = 0x382E,
        FACIAL_R_lipUpperThicknessV = 0x385C,
        FACIAL_R_lipCornerSDK = 0xB3C,
        FACIAL_R_lipCornerAnalog = 0xEE0E,
        FACIAL_R_lipCornerThicknessUpper = 0x54C3,
        FACIAL_R_lipCornerThicknessLower = 0x2BBA,
        MH_MulletRoot = 0x3E73,
        MH_MulletScaler = 0xA1C2,
        MH_Hair_Scale = 0xC664,
        MH_Hair_Crown = 0x1675,
        SM_Torch = 0x8D6,
        FX_Light = 0x8959,
        FX_Light_Scale = 0x5038,
        FX_Light_Switch = 0xE18E,
        BagRoot = 0xAD09,
        BagPivotROOT = 0xB836,
        BagPivot = 0x4D11,
        BagBody = 0xAB6D,
        BagBone_R = 0x937,
        BagBone_L = 0x991,
        SM_LifeSaver_Front = 0x9420,
        SM_R_Pouches_ROOT = 0x2962,
        SM_R_Pouches = 0x4141,
        SM_L_Pouches_ROOT = 0x2A02,
        SM_L_Pouches = 0x4B41,
        SM_Suit_Back_Flapper = 0xDA2D,
        SPR_CopRadio = 0x8245,
        SM_LifeSaver_Back = 0x2127,
        MH_BlushSlider = 0xA0CE,
        SKEL_Tail_01 = 0x347,
        SKEL_Tail_02 = 0x348,
        MH_L_Concertina_B = 0xC988,
        MH_L_Concertina_A = 0xC987,
        MH_R_Concertina_B = 0xC8E8,
        MH_R_Concertina_A = 0xC8E7,
        MH_L_ShoulderBladeRoot = 0x8711,
        MH_L_ShoulderBlade = 0x4EAF,
        MH_R_ShoulderBladeRoot = 0x3A0A,
        MH_R_ShoulderBlade = 0x54AF,
        FB_R_Ear_000 = 0x6CDF,
        SPR_R_Ear = 0x63B6,
        FB_L_Ear_000 = 0x6439,
        SPR_L_Ear = 0x5B10,
        FB_TongueA_000 = 0x4206,
        FB_TongueB_000 = 0x4207,
        FB_TongueC_000 = 0x4208,
        SKEL_L_Toe1 = 0x1D6B,
        SKEL_R_Toe1 = 0xB23F,
        SKEL_Tail_03 = 0x349,
        SKEL_Tail_04 = 0x34A,
        SKEL_Tail_05 = 0x34B,
        SPR_Gonads_ROOT = 0xBFDE,
        SPR_Gonads = 0x1C00,
        FB_L_Brow_Out_001 = 0xE3DB,
        FB_L_Lid_Upper_001 = 0xB2B6,
        FB_L_Eye_001 = 0x62AC,
        FB_L_CheekBone_001 = 0x542E,
        FB_L_Lip_Corner_001 = 0x74AC,
        FB_R_Lid_Upper_001 = 0xAA10,
        FB_R_Eye_001 = 0x6B52,
        FB_R_CheekBone_001 = 0x4B88,
        FB_R_Brow_Out_001 = 0x54C,
        FB_R_Lip_Corner_001 = 0x2BA6,
        FB_Brow_Centre_001 = 0x9149,
        FB_UpperLipRoot_001 = 0x4ED2,
        FB_UpperLip_001 = 0xF18F,
        FB_L_Lip_Top_001 = 0x4F37,
        FB_R_Lip_Top_001 = 0x4537,
        FB_Jaw_001 = 0xB4A0,
        FB_LowerLipRoot_001 = 0x4324,
        FB_LowerLip_001 = 0x508F,
        FB_L_Lip_Bot_001 = 0xB93B,
        FB_R_Lip_Bot_001 = 0xC33B,
        FB_Tongue_001 = 0xB987
    };
}
