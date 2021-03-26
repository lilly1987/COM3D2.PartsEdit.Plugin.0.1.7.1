﻿using System;
using System.Collections.Generic;

internal static class BoneUtil
{
	public static bool IsBodyBone(string boneName)
	{
		return BoneUtil.BodyBoneNameList.Contains(boneName);
	}

	public static bool IsAutoCalcBone(string boneName)
	{
		return BoneUtil.BodyBoneNameList.Contains(boneName);
	}

	private static HashSet<string> BodyBoneNameList = new HashSet<string>
	{
		"Bip01",
		"Bip01 Head",
		"Bip01 L Calf",
		"Bip01 L Calf_SCL_",
		"Bip01 L Clavicle",
		"Bip01 L Clavicle_SCL_",
		"Bip01 L Finger0",
		"Bip01 L Finger0_SCL_",
		"Bip01 L Finger01",
		"Bip01 L Finger01_SCL_",
		"Bip01 L Finger02",
		"Bip01 L Finger02_SCL_",
		"Bip01 L Finger1",
		"Bip01 L Finger1_SCL_",
		"Bip01 L Finger11",
		"Bip01 L Finger11_SCL_",
		"Bip01 L Finger12",
		"Bip01 L Finger12_SCL_",
		"Bip01 L Finger2",
		"Bip01 L Finger2_SCL_",
		"Bip01 L Finger21",
		"Bip01 L Finger21_SCL_",
		"Bip01 L Finger22",
		"Bip01 L Finger22_SCL_",
		"Bip01 L Finger3",
		"Bip01 L Finger3_SCL_",
		"Bip01 L Finger31",
		"Bip01 L Finger31_SCL_",
		"Bip01 L Finger32",
		"Bip01 L Finger32_SCL_",
		"Bip01 L Finger4",
		"Bip01 L Finger4_SCL_",
		"Bip01 L Finger41",
		"Bip01 L Finger41_SCL_",
		"Bip01 L Finger42",
		"Bip01 L Finger42_SCL_",
		"Bip01 L Foot",
		"Bip01 L Forearm",
		"Bip01 L Forearm_SCL_",
		"Bip01 L Hand",
		"Bip01 L Hand_SCL_",
		"Bip01 L Thigh",
		"Bip01 L Thigh_SCL_",
		"Bip01 L Toe0",
		"Bip01 L Toe01",
		"Bip01 L Toe1",
		"Bip01 L Toe11",
		"Bip01 L Toe2",
		"Bip01 L Toe21",
		"Bip01 L UpperArm",
		"Bip01 L UpperArm_SCL_",
		"Bip01 Neck",
		"Bip01 Neck_SCL_",
		"Bip01 Pelvis",
		"Bip01 Pelvis_SCL_",
		"Bip01 R Calf",
		"Bip01 R Calf_SCL_",
		"Bip01 R Clavicle",
		"Bip01 R Clavicle_SCL_",
		"Bip01 R Finger0",
		"Bip01 R Finger0_SCL_",
		"Bip01 R Finger01",
		"Bip01 R Finger01_SCL_",
		"Bip01 R Finger02",
		"Bip01 R Finger02_SCL_",
		"Bip01 R Finger1",
		"Bip01 R Finger1_SCL_",
		"Bip01 R Finger11",
		"Bip01 R Finger11_SCL_",
		"Bip01 R Finger12",
		"Bip01 R Finger12_SCL_",
		"Bip01 R Finger2",
		"Bip01 R Finger2_SCL_",
		"Bip01 R Finger21",
		"Bip01 R Finger21_SCL_",
		"Bip01 R Finger22",
		"Bip01 R Finger22_SCL_",
		"Bip01 R Finger3",
		"Bip01 R Finger3_SCL_",
		"Bip01 R Finger31",
		"Bip01 R Finger31_SCL_",
		"Bip01 R Finger32",
		"Bip01 R Finger32_SCL_",
		"Bip01 R Finger4",
		"Bip01 R Finger4_SCL_",
		"Bip01 R Finger41",
		"Bip01 R Finger41_SCL_",
		"Bip01 R Finger42",
		"Bip01 R Finger42_SCL_",
		"Bip01 R Foot",
		"Bip01 R Forearm",
		"Bip01 R Forearm_SCL_",
		"Bip01 R Hand",
		"Bip01 R Hand_SCL_",
		"Bip01 R Thigh",
		"Bip01 R Thigh_SCL_",
		"Bip01 R Toe0",
		"Bip01 R Toe01",
		"Bip01 R Toe1",
		"Bip01 R Toe11",
		"Bip01 R Toe2",
		"Bip01 R Toe21",
		"Bip01 R UpperArm",
		"Bip01 R UpperArm_SCL_",
		"Bip01 Spine",
		"Bip01 Spine_SCL_",
		"Bip01 Spine0a",
		"Bip01 Spine0a_SCL_",
		"Bip01 Spine1",
		"Bip01 Spine1_SCL_",
		"Bip01 Spine1a",
		"Bip01 Spine1a_SCL_",
		"Foretwist_L",
		"Foretwist_R",
		"Foretwist1_L",
		"Foretwist1_R",
		"Hip_L",
		"Hip_R",
		"Kata_L",
		"Kata_L_nub",
		"Kata_R",
		"momoniku_L",
		"momoniku_R",
		"momotwist_L",
		"momotwist_R",
		"momotwist2_L",
		"momotwist2_R",
		"Mune_L",
		"Mune_L_sub",
		"Mune_R",
		"Mune_R_sub",
		"Uppertwist_L",
		"Uppertwist_R",
		"Uppertwist1_L",
		"Uppertwist1_R"
	};

	private static HashSet<string> BodyBonePathList = new HashSet<string>
	{
		"Bip01",
		"Bip01/Bip01 Pelvis",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/Bip01 L Calf",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/Bip01 L Calf/Bip01 L Calf_SCL_",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/Bip01 L Calf/Bip01 L Foot",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/Bip01 L Calf/Bip01 L Foot/Bip01 L Toe0",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/Bip01 L Calf/Bip01 L Foot/Bip01 L Toe0/Bip01 L Toe01",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/Bip01 L Calf/Bip01 L Foot/Bip01 L Toe1",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/Bip01 L Calf/Bip01 L Foot/Bip01 L Toe1/Bip01 L Toe11",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/Bip01 L Calf/Bip01 L Foot/Bip01 L Toe2",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/Bip01 L Calf/Bip01 L Foot/Bip01 L Toe2/Bip01 L Toe21",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/Bip01 L Thigh_SCL_",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/momotwist_L",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/momotwist_L/momoniku_L",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/momotwist_L/momotwist2_L",
		"Bip01/Bip01 Pelvis/Bip01 Pelvis_SCL_",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/Bip01 R Calf",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/Bip01 R Calf/Bip01 R Calf_SCL_",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/Bip01 R Calf/Bip01 R Foot",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/Bip01 R Calf/Bip01 R Foot/Bip01 R Toe0",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/Bip01 R Calf/Bip01 R Foot/Bip01 R Toe0/Bip01 R Toe01",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/Bip01 R Calf/Bip01 R Foot/Bip01 R Toe1",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/Bip01 R Calf/Bip01 R Foot/Bip01 R Toe1/Bip01 R Toe11",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/Bip01 R Calf/Bip01 R Foot/Bip01 R Toe2",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/Bip01 R Calf/Bip01 R Foot/Bip01 R Toe2/Bip01 R Toe21",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/Bip01 R Thigh_SCL_",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/momotwist_R",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/momotwist_R/momoniku_R",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/momotwist_R/momotwist2_R",
		"Bip01/Bip01 Pelvis/Hip_L",
		"Bip01/Bip01 Pelvis/Hip_L/Hip_L_nub",
		"Bip01/Bip01 Pelvis/Hip_R",
		"Bip01/Bip01 Pelvis/Hip_R/Hip_R_nub",
		"Bip01/Bip01 Spine",
		"Bip01/Bip01 Spine/Bip01 Spine_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine0a_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L Clavicle_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Forearm_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger0",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger0/Bip01 L Finger0_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger0/Bip01 L Finger01",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger0/Bip01 L Finger01/Bip01 L Finger01_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger0/Bip01 L Finger01/Bip01 L Finger02",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger0/Bip01 L Finger01/Bip01 L Finger02/Bip01 L Finger02_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger1",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger1/Bip01 L Finger1_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger1/Bip01 L Finger11",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger1/Bip01 L Finger11/Bip01 L Finger11_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger1/Bip01 L Finger11/Bip01 L Finger12",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger1/Bip01 L Finger11/Bip01 L Finger12/Bip01 L Finger12_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger2",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger2/Bip01 L Finger2_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger2/Bip01 L Finger21",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger2/Bip01 L Finger21/Bip01 L Finger21_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger2/Bip01 L Finger21/Bip01 L Finger22",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger2/Bip01 L Finger21/Bip01 L Finger22/Bip01 L Finger22_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger3",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger3/Bip01 L Finger3_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger3/Bip01 L Finger31",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger3/Bip01 L Finger31/Bip01 L Finger31_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger3/Bip01 L Finger31/Bip01 L Finger32",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger3/Bip01 L Finger31/Bip01 L Finger32/Bip01 L Finger32_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger4",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger4/Bip01 L Finger4_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger4/Bip01 L Finger41",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger4/Bip01 L Finger41/Bip01 L Finger41_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger4/Bip01 L Finger41/Bip01 L Finger42",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Finger4/Bip01 L Finger41/Bip01 L Finger42/Bip01 L Finger42_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Bip01 L Hand/Bip01 L Hand_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Foretwist_L",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Foretwist1_L",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L UpperArm_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Uppertwist_L",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Uppertwist1_L",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Kata_L",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Kata_L/Kata_L_nub",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 Neck",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 Neck/Bip01 Head",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 Neck/Bip01 Neck_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R Clavicle_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Forearm_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger0",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger0/Bip01 R Finger0_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger0/Bip01 R Finger01",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger0/Bip01 R Finger01/Bip01 R Finger01_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger0/Bip01 R Finger01/Bip01 R Finger02",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger0/Bip01 R Finger01/Bip01 R Finger02/Bip01 R Finger02_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger1",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger1/Bip01 R Finger1_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger1/Bip01 R Finger11",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger1/Bip01 R Finger11/Bip01 R Finger11_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger1/Bip01 R Finger11/Bip01 R Finger12",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger1/Bip01 R Finger11/Bip01 R Finger12/Bip01 R Finger12_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger2",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger2/Bip01 R Finger2_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger2/Bip01 R Finger21",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger2/Bip01 R Finger21/Bip01 R Finger21_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger2/Bip01 R Finger21/Bip01 R Finger22",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger2/Bip01 R Finger21/Bip01 R Finger22/Bip01 R Finger22_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger3",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger3/Bip01 R Finger3_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger3/Bip01 R Finger31",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger3/Bip01 R Finger31/Bip01 R Finger31_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger3/Bip01 R Finger31/Bip01 R Finger32",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger3/Bip01 R Finger31/Bip01 R Finger32/Bip01 R Finger32_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger4",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger4/Bip01 R Finger4_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger4/Bip01 R Finger41",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger4/Bip01 R Finger41/Bip01 R Finger41_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger4/Bip01 R Finger41/Bip01 R Finger42",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Finger4/Bip01 R Finger41/Bip01 R Finger42/Bip01 R Finger42_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand/Bip01 R Hand_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Foretwist_R",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Foretwist1_R",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R UpperArm_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Uppertwist_R",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Uppertwist1_R",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Kata_R",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Kata_R/Kata_R_nub",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 Spine1a_SCL_",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Mune_L",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Mune_L/Mune_L_sub",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Mune_R",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Mune_R/Mune_R_sub"
	};

	private static HashSet<string> AutoCalcBoneNameList = new HashSet<string>
	{
		"Foretwist_L",
		"Foretwist_R",
		"Foretwist1_L",
		"Foretwist1_R",
		"Hip_L",
		"Hip_R",
		"Kata_L",
		"Kata_L_nub",
		"Kata_R",
		"momoniku_L",
		"momoniku_R",
		"momotwist_L",
		"momotwist_R",
		"momotwist2_L",
		"momotwist2_R",
		"Mune_L",
		"Mune_L_sub",
		"Mune_R",
		"Mune_R_sub",
		"Uppertwist_L",
		"Uppertwist_R",
		"Uppertwist1_L",
		"Uppertwist1_R"
	};

	private static HashSet<string> AutoCalcBonePathList = new HashSet<string>
	{
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/momotwist_L",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/momotwist_L/momoniku_L",
		"Bip01/Bip01 Pelvis/Bip01 L Thigh/momotwist_L/momotwist2_L",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/momotwist_R",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/momotwist_R/momoniku_R",
		"Bip01/Bip01 Pelvis/Bip01 R Thigh/momotwist_R/momotwist2_R",
		"Bip01/Bip01 Pelvis/Hip_L",
		"Bip01/Bip01 Pelvis/Hip_L/Hip_L_nub",
		"Bip01/Bip01 Pelvis/Hip_R",
		"Bip01/Bip01 Pelvis/Hip_R/Hip_R_nub",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Foretwist_L",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Bip01 L Forearm/Foretwist1_L",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Uppertwist_L",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Bip01 L UpperArm/Uppertwist1_L",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Kata_L",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 L Clavicle/Kata_L/Kata_L_nub",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Foretwist_R",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Foretwist1_R",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Uppertwist_R",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Bip01 R UpperArm/Uppertwist1_R",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Kata_R",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Bip01 R Clavicle/Kata_R/Kata_R_nub",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Mune_L",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Mune_L/Mune_L_sub",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Mune_R",
		"Bip01/Bip01 Spine/Bip01 Spine0a/Bip01 Spine1/Bip01 Spine1a/Mune_R/Mune_R_sub"
	};
}
