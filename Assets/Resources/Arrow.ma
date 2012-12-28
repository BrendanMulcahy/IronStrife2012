//Maya ASCII 2013 scene
//Name: Arrow.ma
//Last modified: Wed, Oct 03, 2012 10:07:45 AM
//Codeset: 1252
requires maya "2013";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2013";
fileInfo "version" "2013 x64";
fileInfo "cutIdentifier" "201202220241-825136";
fileInfo "osv" "Microsoft Windows 7 Ultimate Edition, 64-bit Windows 7 Service Pack 1 (Build 7601)\n";
fileInfo "license" "student";
createNode transform -s -n "persp";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 68.050864010151727 35.488298476276327 -59.596871087463597 ;
	setAttr ".r" -type "double3" -19.799999999997752 487.19999999991666 0 ;
	setAttr ".rp" -type "double3" 6.9388939039072284e-018 -2.2204460492503131e-016 1.4210854715202004e-014 ;
	setAttr ".rpt" -type "double3" -2.0118747266560759e-014 -1.1431553115553159e-015 
		-1.7041375877784488e-014 ;
createNode camera -s -n "perspShape" -p "persp";
	setAttr -k off ".v" no;
	setAttr ".pze" yes;
	setAttr ".fl" 34.999999999999979;
	setAttr ".coi" 104.76624066944467;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".tp" -type "double3" -10.465114057064056 0 0 ;
	setAttr ".hc" -type "string" "viewSet -p %camera";
createNode transform -s -n "top";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 25.55665631514913 100.1 -14.559627130351505 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 79.170465060660533;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
createNode transform -s -n "front";
	setAttr ".v" no;
	setAttr ".t" -type "double3" -29.932542908539642 18.360186230878089 100.1 ;
createNode camera -s -n "frontShape" -p "front";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 84.235908506215139;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
createNode transform -s -n "side";
	setAttr ".v" no;
	setAttr ".t" -type "double3" -38.649006207888576 -1.2998213117151711 0.058459627404774131 ;
	setAttr ".r" -type "double3" 0 -89.999999999999986 0 ;
	setAttr ".rpt" -type "double3" 9.3012767157733173e-016 0 1.5100680494511244e-015 ;
createNode camera -s -n "sideShape" -p "side";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 38.385306999501779;
	setAttr ".ow" 3.3399677139322241;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".tp" -type "double3" -0.26369920838680372 0 -8.5233674842343426e-015 ;
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
createNode transform -n "pCylinder1";
createNode transform -n "transform2" -p "pCylinder1";
	setAttr ".v" no;
createNode mesh -n "pCylinderShape1" -p "transform2";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 82 ".pt[0:81]" -type "float3"  -15.337452 -0.8483817 0 -15.123708 
		-0.3240557 0 -14.859521 0.32405487 0 -14.645777 0.84838086 0 -14.56414 1.0486575 
		0 -14.645777 0.84838063 0 -14.859521 0.32405487 0 -15.123708 -0.3240557 0 -15.337452 
		-0.8483817 0 -15.419088 -1.0486578 0 -6.412652 -0.65118468 -0.031806037 -6.3224006 
		-0.26473793 -0.011128594 -6.1834345 0.22283055 0.013799421 -6.0488405 0.62528443 
		0.033456609 -5.9700232 0.78890043 0.040334523 -5.9770918 0.65118462 0.031806037 -6.0673432 
		0.26473793 0.011128594 -6.2063093 -0.22283037 -0.013799416 -6.3409033 -0.62528431 
		-0.033456706 -6.4197202 -0.78890038 -0.040334508 -1.9410275 -0.43997204 4.6566129e-009 
		-1.889999 -0.16805431 -7.4505806e-009 -1.8269234 0.16805422 3.7252903e-009 -1.775895 
		0.43997177 0 -1.7564038 0.54383504 -1.8626451e-009 -1.7758954 0.43997201 -7.4505806e-009 
		-1.8269238 0.1680543 0 -1.8899994 -0.16805427 0 -1.9410278 -0.4399718 3.7252903e-009 
		-1.9605193 -0.54383504 -7.4505806e-009 -0.0038523183 -0.096617453 0 -0.0014714478 
		-4.2917781 -5.2368774 0.0014714478 -4.1634293 -5.2368774 0.0038523183 0.096617453 
		0 0.004761728 0.11942573 0 0.003852318 0.09661743 0 0.0014714478 -4.1634293 5.2368774 
		-0.0014714523 -4.2917781 5.2368774 -0.0038523183 -0.096617453 0 -0.004761728 -0.11942573 
		0 -0.0073690005 0.13353141 0 -0.0028147059 4.3058777 -5.2368774 0.0028147059 4.1284919 
		-5.2368774 0.0073690005 -0.13353141 0 0.0091085779 -0.16505386 0 0.0073689963 -0.13353138 
		0 0.0028147055 4.1284924 5.2368774 -0.0028147059 4.3058777 5.2368774 -0.0073690005 
		0.13353141 0 -0.0091085779 0.16505386 0 -1.8836372 0.24590322 0 -1.8680778 0.093926676 
		0 -1.848845 -0.093926691 0 -1.8332856 -0.24590322 0 -1.8273426 -0.30395311 0 -1.8332856 
		-0.24590316 0 -1.848845 -0.093926653 0 -1.8680778 0.093926698 0 -1.8836372 0.24590322 
		0 -1.8895801 0.30395311 0 -6.3186092 0.53386062 0 -6.2421346 0.20391665 0 -6.1476092 
		-0.20391686 0 -6.0711346 -0.5338608 0 -6.041925 -0.65988815 0 -6.0711346 -0.53386062 
		0 -6.1476092 -0.20391665 0 -6.2421346 0.20391676 0 -6.3186092 0.53386062 0 -6.3478189 
		0.65988803 0 -15.234209 0.72798932 0 -15.084267 0.27806711 0 -14.89893 -0.27806711 
		0 -14.748987 -0.72798938 0 -14.69171 -0.89984232 0 -14.748987 -0.72798896 0 -14.89893 
		-0.27806711 0 -15.084267 0.27806711 0 -15.234209 0.72798932 0 -15.291487 0.89984196 
		0 -22.425467 -8.1414957 0 -22.425451 8.1414957 0;
createNode transform -n "pCube1";
	setAttr ".t" -type "double3" -4.3827183249776285 -0.053816354369375326 1.922616102717414 ;
	setAttr ".r" -type "double3" 90 89.999999999999929 0 ;
	setAttr ".s" -type "double3" 0.56905833958977481 0.56905833958977481 0.56905833958977481 ;
	setAttr ".rp" -type "double3" 1.6715989112854004 0 4.258596658706665 ;
	setAttr ".rpt" -type "double3" 2.5207191881825439 0 -6.0900871376968215 ;
	setAttr ".sp" -type "double3" 1.6715989112854004 0 4.258596658706665 ;
createNode transform -n "transform1" -p "pCube1";
	setAttr ".v" no;
createNode mesh -n "pCubeShape1" -p "transform1";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 8 ".pt[0:7]" -type "float3"  0.4775998 0 1.3531995 -2.1492009 
		0 8.9151926 0.4775998 0 1.3531995 -2.1492009 0 8.9151926 5.5719929 0 -0.39799976 
		2.8655977 0 6.9251909 5.5719929 0 -0.39799976 2.8655977 0 6.9251909;
createNode transform -n "polySurface1";
createNode transform -n "transform5" -p "polySurface1";
	setAttr ".v" no;
createNode mesh -n "polySurfaceShape1" -p "transform5";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 8 ".pt[230:237]" -type "float3"  -0.24622206 -1.0066277 -0.57883745 
		-0.36538234 -0.94939148 -0.57883745 0.33862063 0.92307121 -0.58125359 0.22992724 
		1.006098 -0.57584125 -0.35463217 -0.95269531 0.57883745 0.25668895 1.0099313 0.58125359 
		0.36538234 0.92690361 0.57584125 -0.23547177 -1.0099313 0.57883745;
createNode transform -n "pSphere1";
	setAttr ".t" -type "double3" 0.018449588139030425 1.5341673658804431 0.028383380249072891 ;
	setAttr ".s" -type "double3" 0.045440335697874042 0.58734171892469711 0.49538077074422754 ;
createNode transform -n "transform4" -p "pSphere1";
	setAttr ".v" no;
createNode mesh -n "pSphereShape1" -p "transform4";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 21 ".pt";
	setAttr ".pt[0]" -type "float3" 0 0.73349577 -1.7066027 ;
	setAttr ".pt[1]" -type "float3" 0 0.73349577 -1.7066035 ;
	setAttr ".pt[2]" -type "float3" 1.895813 0.73349577 -2.1616425e-007 ;
	setAttr ".pt[3]" -type "float3" 0 0.73349577 1.7066033 ;
	setAttr ".pt[4]" -type "float3" 0 0.73349577 1.7066033 ;
	setAttr ".pt[5]" -type "float3" 1.9956502 0.73349577 7.7480166e-008 ;
	setAttr ".pt[6]" -type "float3" 0 0 -2.0740583 ;
	setAttr ".pt[7]" -type "float3" 0 0 -2.0740588 ;
	setAttr ".pt[8]" -type "float3" 2.295074 0 -3.568704e-007 ;
	setAttr ".pt[9]" -type "float3" 0 0 2.0740585 ;
	setAttr ".pt[10]" -type "float3" 0 0 2.0740588 ;
	setAttr ".pt[11]" -type "float3" 3.0172045 0 -9.4933547e-017 ;
	setAttr ".pt[12]" -type "float3" 0 0 -0.71011007 ;
	setAttr ".pt[13]" -type "float3" 0 0 -0.71011031 ;
	setAttr ".pt[14]" -type "float3" 3.1585429 0 -8.994509e-008 ;
	setAttr ".pt[15]" -type "float3" 0 0 0.71011025 ;
	setAttr ".pt[16]" -type "float3" 0 0 0.71011025 ;
	setAttr ".pt[17]" -type "float3" 4.4485617 0 3.2239168e-008 ;
	setAttr ".pt[20]" -type "float3" 2.3492165 0 0 ;
	setAttr ".pt[23]" -type "float3" 2.7826316 0 0 ;
	setAttr ".pt[30]" -type "float3" 0 0.73349577 -6.9388932e-017 ;
createNode transform -n "pCube2";
	setAttr ".t" -type "double3" -19.599461889081944 -0.023570696640311972 0.016499487648218472 ;
	setAttr ".s" -type "double3" 1 1.2031992287037496 1 ;
createNode transform -n "transform3" -p "pCube2";
	setAttr ".v" no;
createNode mesh -n "pCubeShape2" -p "transform3";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode transform -n "pCylinder2";
	setAttr ".t" -type "double3" 15.497894384132811 0 0 ;
	setAttr ".r" -type "double3" 180 0 90 ;
	setAttr ".s" -type "double3" 0.29269108028230556 0.34406314250337217 0.29269108028230556 ;
createNode mesh -n "pCylinderShape2" -p "pCylinder2";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 23 ".pt";
	setAttr ".pt[75]" -type "float3" -1.9929307e-015 1.0840794 1.7977046e-016 ;
	setAttr ".pt[77]" -type "float3" -2.220446e-015 1.0840794 1.7977046e-016 ;
	setAttr ".pt[81]" -type "float3" -2.220446e-015 1.0840794 1.7977046e-016 ;
	setAttr ".pt[82]" -type "float3" 0 0 -0.21003628 ;
	setAttr ".pt[83]" -type "float3" 0 0 -0.21003628 ;
	setAttr ".pt[84]" -type "float3" 0 0 -0.21003628 ;
	setAttr ".pt[85]" -type "float3" 0 0 -0.21003628 ;
	setAttr ".pt[86]" -type "float3" 0 0 -0.21003628 ;
	setAttr ".pt[87]" -type "float3" 0 0 0.21003628 ;
	setAttr ".pt[88]" -type "float3" 0 0 0.21003628 ;
	setAttr ".pt[89]" -type "float3" 0 0 0.21003628 ;
	setAttr ".pt[90]" -type "float3" 0 0 0.21003628 ;
	setAttr ".pt[91]" -type "float3" 0 0 0.21003628 ;
	setAttr ".pt[92]" -type "float3" -2.2759572e-015 1.0840794 8.3266727e-017 ;
	setAttr ".pt[93]" -type "float3" -1.9929307e-015 1.0840794 8.3266727e-017 ;
	setAttr ".pt[94]" -type "float3" -2.2759572e-015 1.0840794 8.3266727e-017 ;
	setAttr ".pt[95]" -type "float3" -2.220446e-015 1.0840794 8.3266727e-017 ;
	setAttr ".pt[96]" -type "float3" -2.220446e-015 1.0840794 8.3266727e-017 ;
	setAttr ".pt[97]" -type "float3" -2.220446e-015 1.0840794 8.3266727e-017 ;
	setAttr ".pt[98]" -type "float3" -2.2759572e-015 1.0840794 8.3266727e-017 ;
	setAttr ".pt[99]" -type "float3" -1.9929307e-015 1.0840794 8.3266727e-017 ;
	setAttr ".pt[100]" -type "float3" -2.2759572e-015 1.0840794 8.3266727e-017 ;
	setAttr ".pt[101]" -type "float3" -2.220446e-015 1.0840794 8.3266727e-017 ;
createNode lightLinker -s -n "lightLinker1";
	setAttr -s 2 ".lnk";
	setAttr -s 2 ".slnk";
createNode displayLayerManager -n "layerManager";
createNode displayLayer -n "defaultLayer";
createNode renderLayerManager -n "renderLayerManager";
createNode renderLayer -n "defaultRenderLayer";
	setAttr ".g" yes;
createNode polyCylinder -n "polyCylinder1";
	setAttr ".r" 1.5;
	setAttr ".h" 80;
	setAttr ".sa" 10;
	setAttr ".sh" 7;
	setAttr ".sc" 1;
	setAttr ".cuv" 3;
createNode polyCube -n "polyCube1";
	setAttr ".w" 7.6686989600023168;
	setAttr ".h" 11.054079012851927;
	setAttr ".d" 5.6237533462057616;
	setAttr ".cuv" 4;
createNode polyBoolOp -n "polyBoolOp1";
	setAttr -s 2 ".ip";
	setAttr -s 2 ".im";
	setAttr ".op" 2;
	setAttr ".uth" yes;
createNode groupId -n "groupId1";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts1";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:89]";
createNode groupId -n "groupId2";
	setAttr ".ihi" 0;
createNode groupId -n "groupId3";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts2";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:5]";
createNode groupId -n "groupId4";
	setAttr ".ihi" 0;
createNode groupId -n "groupId5";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts3";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:97]";
createNode polySplit -n "polySplit1";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].t" 3;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[1].t" 2;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 0.5000002384185791 0.4999997615814209 ;
	setAttr ".sps[0].sp[2].t" 2;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0.49999994039535522 0.49999994039535522 
		1.1920928955078125e-007 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit2";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 94;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0.5 0 0.5 ;
	setAttr ".sps[0].sp[1].f" 94;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.49999994039535522 0 0.50000005960464478 ;
	setAttr ".sps[0].sp[2].f" 94;
	setAttr ".sps[0].sp[2].t" 1;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0.4999998807907105 0.50000011920928955 
		0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit3";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 88;
	setAttr ".sps[0].sp[0].t" 5;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0.49999982118606567 0.5000002384185791 
		-5.9604644775390625e-008 ;
	setAttr ".sps[0].sp[1].f" 86;
	setAttr ".sps[0].sp[1].t" 2;
	setAttr ".sps[0].sp[1].bc" -type "double3" 9.8617285004820587e-008 0.5 0.4999998807907105 ;
	setAttr ".sps[0].sp[2].f" 86;
	setAttr ".sps[0].sp[2].t" 2;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0.49999979138374329 1.9723457000964115e-007 
		0.50000005960464478 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit4";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 98;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0.49999994039535522 0.49999994039535522 
		1.1920928955078125e-007 ;
	setAttr ".sps[0].sp[1].f" 98;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.49999982118606567 0 0.50000017881393433 ;
	setAttr ".sps[0].sp[2].f" 80;
	setAttr ".sps[0].sp[2].t" 5;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0.4999989271163941 0.50000113248825073 
		-5.9604644775390625e-008 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit5";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 81;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 78;
	setAttr ".sps[0].sp[1].t" 3;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit6";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 92;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 86;
	setAttr ".sps[0].sp[1].t" 6;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 1 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit7";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 74;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 73;
	setAttr ".sps[0].sp[1].t" 2;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit8";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 104;
	setAttr ".sps[0].sp[0].t" 5;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0 1 ;
	setAttr ".sps[0].sp[1].f" 104;
	setAttr ".sps[0].sp[1].t" 5;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 1 0 ;
	setAttr ".c2v" yes;
createNode polyExtrudeFace -n "polyExtrudeFace1";
	setAttr ".ics" -type "componentList" 4 "f[92:93]" "f[97]" "f[99:100]" "f[105]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 4.7624111e-005 2.0977914 0.091125727 ;
	setAttr ".rs" 56320;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -1.491450309753418 -0.00059795379638671875 -2.7702980041503902 ;
	setAttr ".cbx" -type "double3" 1.491545557975769 4.1961808204650879 2.9525494575500488 ;
createNode polyExtrudeFace -n "polyExtrudeFace2";
	setAttr ".ics" -type "componentList" 4 "f[92:93]" "f[97]" "f[99:100]" "f[105]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 4.7624111e-005 2.0977914 0.091125727 ;
	setAttr ".rs" 56277;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -1.491450309753418 -0.00059795379638671875 -2.7702980041503902 ;
	setAttr ".cbx" -type "double3" 1.491545557975769 4.1961808204650879 2.9525494575500488 ;
createNode polyExtrudeFace -n "polyExtrudeFace3";
	setAttr ".ics" -type "componentList" 4 "f[92:93]" "f[97]" "f[99:100]" "f[105]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 4.7624111e-005 2.0977914 0.091125727 ;
	setAttr ".rs" 46135;
	setAttr ".lt" -type "double3" 0 -2.2204460492503131e-016 0.27960593548436913 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.40478956699371338 -0.00059801340103149414 -2.7702980041503902 ;
	setAttr ".cbx" -type "double3" 0.40488481521606445 4.1961808204650879 2.9525494575500488 ;
createNode polyTweak -n "polyTweak1";
	setAttr ".uopa" yes;
	setAttr -s 16 ".tk[126:141]" -type "float3"  0.68527317 -5.9604645e-008
		 0 0.78233409 -5.9604645e-008 0 -0.7822842 -5.9604645e-008 0 -0.68520737 -5.9604645e-008
		 0 0.87939525 -5.9604645e-008 0 -0.87936109 -5.9604645e-008 0 -0.70234907 -5.9604645e-008
		 0 0.70241445 -5.9604645e-008 0 -0.79087496 -5.9604645e-008 0 0.7909258 -5.9604645e-008
		 0 1.074139118 -5.9604645e-008 0 -1.07412827 -5.9604645e-008 0 0.87943685 -5.9604645e-008
		 0 -0.87940097 -5.9604645e-008 0 -1.086660743 -5.9604645e-008 0 1.086660743 -5.9604645e-008
		 0;
createNode polySphere -n "polySphere1";
	setAttr ".r" 3.0319456365267783;
	setAttr ".sa" 6;
	setAttr ".sh" 6;
createNode polyExtrudeFace -n "polyExtrudeFace4";
	setAttr ".ics" -type "componentList" 5 "f[69:70]" "f[72]" "f[82]" "f[86]" "f[88]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -3.0875206e-005 -2.1374779 0.091125727 ;
	setAttr ".rs" 35052;
	setAttr ".lt" -type "double3" 2.1175823681357508e-022 3.3306690738754696e-016 0.93013400662327084 ;
	setAttr ".ls" -type "double3" 0.15706425139892966 1 1 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -1.4946855306625366 -4.3038134574890137 -2.7702980041503902 ;
	setAttr ".cbx" -type "double3" 1.4946237802505491 0.028857707977294918 2.9525494575500488 ;
createNode polyTweak -n "polyTweak2";
	setAttr ".uopa" yes;
	setAttr -s 8 ".tk";
	setAttr ".tk[142]" -type "float3" 0 0.15698916 -0.41322026 ;
	setAttr ".tk[145]" -type "float3" 0 0.15698916 -0.41322026 ;
	setAttr ".tk[148]" -type "float3" 0 0.15698916 0.41322026 ;
	setAttr ".tk[149]" -type "float3" 0 0.15698916 0.41322026 ;
	setAttr ".tk[152]" -type "float3" 0 -0.8181082 0.23604392 ;
	setAttr ".tk[153]" -type "float3" 0 -0.8181082 0.23604392 ;
	setAttr ".tk[156]" -type "float3" 0 -0.75161105 -0.43263608 ;
	setAttr ".tk[157]" -type "float3" 0 -0.75161105 -0.43263608 ;
createNode polySplit -n "polySplit9";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 88;
	setAttr ".sps[0].sp[0].t" 3;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 169;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 1 0 ;
	setAttr ".c2v" yes;
createNode polyTweak -n "polyTweak3";
	setAttr ".uopa" yes;
	setAttr -s 16 ".tk[158:173]" -type "float3"  0 -0.71136183 -0.50585729
		 0 -0.71136183 -0.50585729 0 0.55328143 1.1175871e-008 0 0.55328143 1.1175871e-008
		 0 1.72307408 0.56908947 0 2.40282083 0.094848216 0 2.40282083 0.094848216 0 1.72307408
		 0.56908947 0 -0.67974579 0.34777683 0 -0.67974579 0.34777683 0 0.56908947 -0.12646429
		 0 0.56908947 -0.12646429 0 1.4701463 -0.52166533 0 1.4701463 -0.52166533 0 2.2763567
		 -0.93267447 0 2.2763567 -0.93267447;
createNode script -n "uiConfigurationScriptNode";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"top\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n"
		+ "                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -objectFilterShowInHUD 1\n                -isFiltered 0\n"
		+ "                -colorResolution 4 4 \n                -bumpResolution 4 4 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 0\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n"
		+ "                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n"
		+ "            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 4 4 \n            -bumpResolution 4 4 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n"
		+ "            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 0\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n"
		+ "            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -shadows 0\n            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"side\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n"
		+ "                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 4 4 \n                -bumpResolution 4 4 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n"
		+ "                -lowQualityLighting 0\n                -maximumNumHardwareLights 0\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n"
		+ "                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n"
		+ "            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 4 4 \n            -bumpResolution 4 4 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 0\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n"
		+ "            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -shadows 0\n            $editorName;\n"
		+ "modelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"front\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n"
		+ "                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 4 4 \n                -bumpResolution 4 4 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 0\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n"
		+ "                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n"
		+ "                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n"
		+ "            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 4 4 \n            -bumpResolution 4 4 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 0\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n"
		+ "            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -shadows 0\n            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"persp\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 1\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n"
		+ "                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n"
		+ "                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -shadows 0\n                $editorName;\n"
		+ "modelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 1\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n"
		+ "            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n"
		+ "            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -shadows 0\n            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t\t$editorName = $panelName;\n            outlinerEditor -e \n                -showShapes 0\n                -showReferenceNodes 1\n                -showReferenceMembers 1\n                -showAttributes 0\n                -showConnected 0\n                -showAnimCurvesOnly 0\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 1\n                -showAssets 1\n                -showContainedOnly 1\n                -showPublishedAsConnected 0\n                -showContainerContents 1\n                -ignoreDagHierarchy 0\n                -expandConnections 0\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 0\n                -highlightActive 1\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n"
		+ "                -transmitFilters 0\n                -setFilter \"defaultSetFilter\" \n                -showSetMembers 1\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showReferenceNodes 1\n            -showReferenceMembers 1\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n"
		+ "            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"graphEditor\" -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n"
		+ "            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n"
		+ "                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n"
		+ "                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n"
		+ "                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n"
		+ "                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n"
		+ "                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dopeSheetPanel\" -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n"
		+ "                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n"
		+ "                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n"
		+ "                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n"
		+ "                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n"
		+ "                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"clipEditorPanel\" -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n"
		+ "                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"sequenceEditorPanel\" -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperGraphPanel\" -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n"
		+ "                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n"
		+ "                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperShadePanel\" -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"visorPanel\" -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"nodeEditorPanel\" -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -ignoreAssets 1\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -island 0\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -syncedSelection 1\n"
		+ "                -extendToShapes 1\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -ignoreAssets 1\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -island 0\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -syncedSelection 1\n                -extendToShapes 1\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n"
		+ "\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"createNodePanel\" -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Texture Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"polyTexturePlacementPanel\" -l (localizedPanelLabel(\"UV Texture Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Texture Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n"
		+ "\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"renderWindowPanel\" -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"blendShapePanel\" (localizedPanelLabel(\"Blend Shape\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\tblendShapePanel -unParent -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels ;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tblendShapePanel -edit -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynRelEdPanel\" -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"relationshipPanel\" -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"referenceEditorPanel\" -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"componentEditorPanel\" -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynPaintScriptedPanelType\" -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"scriptEditorPanel\" -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-defaultImage \"\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"single\\\" -ps 1 100 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap true\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 1\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -maxConstantTransparency 1\\n    -rendererName \\\"base_OpenGL_Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -shadows 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 1\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -maxConstantTransparency 1\\n    -rendererName \\\"base_OpenGL_Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -shadows 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        setFocus `paneLayout -q -p1 $gMainPane`;\n        sceneUIReplacement -deleteRemaining;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 5 -size 56 -divisions 2 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 24 -ast 1 -aet 48 ";
	setAttr ".st" 6;
createNode polyExtrudeFace -n "polyExtrudeFace5";
	setAttr ".ics" -type "componentList" 2 "f[69]" "f[79]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -4.8726797e-006 -0.25837365 0.013529897 ;
	setAttr ".rs" 36418;
	setAttr ".lt" -type "double3" 1.1382005228729659e-021 -0.011885297732122269 0.3905622513501058 ;
	setAttr ".lr" -type "double3" 11.250334791471742 0 0 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.17064294219017029 -0.37748545408248901 -1.6457409858703611 ;
	setAttr ".cbx" -type "double3" 0.17063319683074951 -0.13926184177398682 1.6728007793426514 ;
createNode polyTweak -n "polyTweak4";
	setAttr ".uopa" yes;
	setAttr -s 2 ".tk[160:161]" -type "float3"  0 0.10682326 0.091671459 0
		 0.10682326 0.091671459;
createNode polySplit -n "polySplit10";
	setAttr -s 7 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 168;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0.5 0 0.5 ;
	setAttr ".sps[0].sp[1].f" 164;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.79677081108093262 0.20322908461093905 
		1.0430812835693359e-007 ;
	setAttr ".sps[0].sp[2].f" 164;
	setAttr ".sps[0].sp[2].t" 1;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[3].f" 159;
	setAttr ".sps[0].sp[3].t" 1;
	setAttr ".sps[0].sp[3].bc" -type "double3" 0.5000002384185791 0.49999979138374329 
		-2.9802322387695313e-008 ;
	setAttr ".sps[0].sp[4].f" 159;
	setAttr ".sps[0].sp[4].t" 1;
	setAttr ".sps[0].sp[4].bc" -type "double3" 0.5 0 0.5 ;
	setAttr ".sps[0].sp[5].f" 165;
	setAttr ".sps[0].sp[5].t" 1;
	setAttr ".sps[0].sp[5].bc" -type "double3" 0.19001138210296631 0.80998867750167836 
		-5.9604644775390625e-008 ;
	setAttr ".sps[0].sp[6].f" 165;
	setAttr ".sps[0].sp[6].bc" -type "double3" 0.4999828040599823 0.4999828040599823 
		3.4362077713012695e-005 ;
	setAttr ".c2v" yes;
createNode polyTweak -n "polyTweak5";
	setAttr ".uopa" yes;
	setAttr -s 87 ".tk";
	setAttr ".tk[28]" -type "float3" 0 6.8602114 0 ;
	setAttr ".tk[29]" -type "float3" 0 6.8306341 0 ;
	setAttr ".tk[30]" -type "float3" 0 9.5986633 0 ;
	setAttr ".tk[31]" -type "float3" 0 9.5583296 0 ;
	setAttr ".tk[32]" -type "float3" 0 6.8306341 0 ;
	setAttr ".tk[33]" -type "float3" 0 9.5583296 0 ;
	setAttr ".tk[34]" -type "float3" 0 6.7532001 0 ;
	setAttr ".tk[35]" -type "float3" 0 9.4527397 0 ;
	setAttr ".tk[36]" -type "float3" 0 6.6574879 0 ;
	setAttr ".tk[37]" -type "float3" 0 9.3222227 0 ;
	setAttr ".tk[38]" -type "float3" 0 6.5800538 0 ;
	setAttr ".tk[39]" -type "float3" 0 9.2166328 0 ;
	setAttr ".tk[40]" -type "float3" 0 6.5504775 0 ;
	setAttr ".tk[41]" -type "float3" 0 9.176301 0 ;
	setAttr ".tk[42]" -type "float3" 0 6.5800538 0 ;
	setAttr ".tk[43]" -type "float3" 0 9.2166328 0 ;
	setAttr ".tk[44]" -type "float3" 0 6.6574879 0 ;
	setAttr ".tk[45]" -type "float3" 0 9.3222227 0 ;
	setAttr ".tk[46]" -type "float3" 0 6.7532001 0 ;
	setAttr ".tk[47]" -type "float3" 0 9.4527397 0 ;
	setAttr ".tk[48]" -type "float3" 0 4.0945401 0 ;
	setAttr ".tk[49]" -type "float3" 0 4.0809169 0 ;
	setAttr ".tk[50]" -type "float3" 0 4.0809169 0 ;
	setAttr ".tk[51]" -type "float3" 0 4.0452504 0 ;
	setAttr ".tk[52]" -type "float3" 0 4.0011635 0 ;
	setAttr ".tk[53]" -type "float3" 0 3.9654963 0 ;
	setAttr ".tk[54]" -type "float3" 0 3.9518728 0 ;
	setAttr ".tk[55]" -type "float3" 0 3.9654963 0 ;
	setAttr ".tk[56]" -type "float3" 0 4.0011635 0 ;
	setAttr ".tk[57]" -type "float3" 0 4.0452504 0 ;
	setAttr ".tk[64]" -type "float3" 0 -4.1508374 0 ;
	setAttr ".tk[66]" -type "float3" 0 -4.126462 0 ;
	setAttr ".tk[67]" -type "float3" 0 -4.126462 0 ;
	setAttr ".tk[68]" -type "float3" 0 -4.0626464 0 ;
	setAttr ".tk[70]" -type "float3" 0 -3.9837656 0 ;
	setAttr ".tk[72]" -type "float3" 0 -3.9199505 0 ;
	setAttr ".tk[73]" -type "float3" 0 -3.895575 0 ;
	setAttr ".tk[74]" -type "float3" 0 -3.9199505 0 ;
	setAttr ".tk[76]" -type "float3" 0 -3.9837656 0 ;
	setAttr ".tk[79]" -type "float3" 0 -4.0626464 0 ;
	setAttr ".tk[80]" -type "float3" 0 -6.8904896 0 ;
	setAttr ".tk[81]" -type "float3" 0 -6.8581696 0 ;
	setAttr ".tk[82]" -type "float3" 0 -6.8520894 0 ;
	setAttr ".tk[83]" -type "float3" 0 -6.7576385 0 ;
	setAttr ".tk[84]" -type "float3" 0 -6.6432133 0 ;
	setAttr ".tk[85]" -type "float3" 0 -6.5525193 0 ;
	setAttr ".tk[86]" -type "float3" 0 -6.5201993 0 ;
	setAttr ".tk[87]" -type "float3" 0 -6.5585971 0 ;
	setAttr ".tk[88]" -type "float3" 0 -6.653048 0 ;
	setAttr ".tk[89]" -type "float3" 0 -6.7674751 0 ;
	setAttr ".tk[90]" -type "float3" 0 -9.6335869 0 ;
	setAttr ".tk[91]" -type "float3" 0 -9.586586 0 ;
	setAttr ".tk[92]" -type "float3" 0 -9.586586 0 ;
	setAttr ".tk[93]" -type "float3" 0 -9.4635324 0 ;
	setAttr ".tk[94]" -type "float3" 0 -9.3114309 0 ;
	setAttr ".tk[95]" -type "float3" 0 -9.1883755 0 ;
	setAttr ".tk[96]" -type "float3" 0 -9.1413755 0 ;
	setAttr ".tk[97]" -type "float3" 0 -9.1883755 0 ;
	setAttr ".tk[98]" -type "float3" 0 -9.3114309 0 ;
	setAttr ".tk[99]" -type "float3" 0 -9.4635324 0 ;
	setAttr ".tk[100]" -type "float3" 0 11.298186 0 ;
	setAttr ".tk[101]" -type "float3" 0 -11.298186 0 ;
	setAttr ".tk[105]" -type "float3" 0 -0.37968621 0 ;
	setAttr ".tk[158]" -type "float3" 0 -0.13798866 0.30281997 ;
	setAttr ".tk[159]" -type "float3" 0 -0.13798866 0.30281997 ;
	setAttr ".tk[160]" -type "float3" 0 -0.37968621 0 ;
	setAttr ".tk[161]" -type "float3" 0 -0.37968621 0 ;
	setAttr ".tk[162]" -type "float3" 0 -0.37968621 0 ;
	setAttr ".tk[163]" -type "float3" 0 -0.37968621 0 ;
	setAttr ".tk[164]" -type "float3" 0 -0.37968621 0 ;
	setAttr ".tk[165]" -type "float3" 0 -0.37968621 0 ;
	setAttr ".tk[166]" -type "float3" 0 -0.13798866 -0.30281997 ;
	setAttr ".tk[167]" -type "float3" 0 -0.13798866 -0.30281997 ;
	setAttr ".tk[168]" -type "float3" 0 -0.37968621 0 ;
	setAttr ".tk[169]" -type "float3" 0 -0.37968621 0 ;
	setAttr ".tk[170]" -type "float3" 0 -0.37968621 0 ;
	setAttr ".tk[171]" -type "float3" 0 -0.37968621 0 ;
	setAttr ".tk[172]" -type "float3" 0 -0.37968621 0 ;
	setAttr ".tk[173]" -type "float3" 0 -0.37968621 0 ;
	setAttr ".tk[174]" -type "float3" 0 0.32820395 -0.17562287 ;
	setAttr ".tk[175]" -type "float3" 0 0.32820395 -0.17562287 ;
	setAttr ".tk[176]" -type "float3" 0 0.32820395 -0.12212861 ;
	setAttr ".tk[177]" -type "float3" 0 0.32820395 -0.12212861 ;
	setAttr ".tk[178]" -type "float3" 0 0.19156504 0.13979229 ;
	setAttr ".tk[179]" -type "float3" 0 0.19156516 0.13979223 ;
	setAttr ".tk[180]" -type "float3" 0 0.37239185 0.15430862 ;
	setAttr ".tk[181]" -type "float3" 0 0.37239167 0.15430848 ;
createNode polySplit -n "polySplit11";
	setAttr -s 7 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 148;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 90;
	setAttr ".sps[0].sp[1].t" 4;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.3671323955059051 0.63286638259887695 
		1.1920928955078125e-006 ;
	setAttr ".sps[0].sp[2].f" 75;
	setAttr ".sps[0].sp[2].t" 1;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0.5 0 0.5 ;
	setAttr ".sps[0].sp[3].f" 75;
	setAttr ".sps[0].sp[3].bc" -type "double3" 1.8377679111836187e-007 0.50384688377380371 
		0.49615293741226191 ;
	setAttr ".sps[0].sp[4].f" 152;
	setAttr ".sps[0].sp[4].bc" -type "double3" 0.50000005960464478 0.5 -5.9604644775390625e-008 ;
	setAttr ".sps[0].sp[5].f" 152;
	setAttr ".sps[0].sp[5].bc" -type "double3" 0 0.35927769541740417 0.64072227478027344 ;
	setAttr ".sps[0].sp[6].f" 146;
	setAttr ".sps[0].sp[6].t" 1;
	setAttr ".sps[0].sp[6].bc" -type "double3" 0 1 0 ;
	setAttr ".c2v" yes;
createNode polyTweak -n "polyTweak6";
	setAttr ".uopa" yes;
	setAttr -s 2 ".tk[183:184]" -type "float3"  0 -0.095316827 0 0 -0.095316827
		 0;
createNode polySplit -n "polySplit12";
	setAttr -s 7 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 124;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 141;
	setAttr ".sps[0].sp[1].bc" -type "double3" 5.5410872334960004e-008 0.36327946186065674 
		0.63672047853469849 ;
	setAttr ".sps[0].sp[2].f" 64;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0.5 0 0.5 ;
	setAttr ".sps[0].sp[3].f" 64;
	setAttr ".sps[0].sp[3].t" 1;
	setAttr ".sps[0].sp[3].bc" -type "double3" 0.50384676456451416 0.4961530864238739 
		1.4901161193847656e-007 ;
	setAttr ".sps[0].sp[4].f" 74;
	setAttr ".sps[0].sp[4].bc" -type "double3" 0.5 0 0.5 ;
	setAttr ".sps[0].sp[5].f" 74;
	setAttr ".sps[0].sp[5].bc" -type "double3" 0 0.63300532102584839 0.36699467897415161 ;
	setAttr ".sps[0].sp[6].f" 122;
	setAttr ".sps[0].sp[6].t" 1;
	setAttr ".sps[0].sp[6].bc" -type "double3" 0 1 0 ;
	setAttr ".c2v" yes;
createNode polyExtrudeFace -n "polyExtrudeFace6";
	setAttr ".ics" -type "componentList" 1 "f[58:59]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -4.0078435 28.28932 -5.9604645e-008 ;
	setAttr ".rs" 50456;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -6.6111350059509277 21.050094604492188 -1.4265850782394407 ;
	setAttr ".cbx" -type "double3" -1.4045522212982178 35.528545379638672 1.4265849590301514 ;
createNode polyTweak -n "polyTweak7";
	setAttr ".uopa" yes;
	setAttr -s 4 ".tk[186:189]" -type "float3"  0 0 -2.19865656 0 0 -2.19865656
		 0 0 2.19865656 0 0 2.19865608;
createNode polyExtrudeFace -n "polyExtrudeFace7";
	setAttr ".ics" -type "componentList" 1 "f[58:59]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -4.0078435 28.28932 -1.1920929e-007 ;
	setAttr ".rs" 40387;
	setAttr ".lt" -type "double3" -1.1731637930043577e-015 3.5527136788005009e-015 0.93069753577561298 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -6.2683305740356445 22.003364562988281 -2.0814039707183838 ;
	setAttr ".cbx" -type "double3" -1.7473567724227903 34.575275421142578 2.0814037322998047 ;
createNode polyTweak -n "polyTweak8";
	setAttr ".uopa" yes;
	setAttr -s 8 ".tk[190:197]" -type "float3"  -0.34280455 0.92272735 0.65481877
		 -0.22326185 0.9532696 0.65481883 0.2331765 -0.9532696 0.65481877 0.34280452 -0.88696253
		 0.65481883 -0.22326183 0.9532696 -0.65481877 -0.34280455 0.92272735 -0.65481883 0.34280455
		 -0.88696253 -0.65481877 0.2331765 -0.9532696 -0.65481883;
createNode polyExtrudeFace -n "polyExtrudeFace8";
	setAttr ".ics" -type "componentList" 1 "f[58:59]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -4.0078435 28.289322 0 ;
	setAttr ".rs" 44998;
	setAttr ".lt" -type "double3" -2.2007137814693137e-015 -7.1054273576010019e-015 
		0.46571428091966766 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -5.9082808494567871 23.00459098815918 -2.5323343276977539 ;
	setAttr ".cbx" -type "double3" -2.1074063777923584 33.574050903320313 2.5323343276977539 ;
createNode polyTweak -n "polyTweak9";
	setAttr ".uopa" yes;
	setAttr -s 8 ".tk[198:205]" -type "float3"  -0.36004987 0.96914768 -0.47976696
		 -0.23449372 1.0012259483 -0.47976696 0.24490683 -1.0012259483 -0.47976696 0.36004981
		 -0.9315834 -0.47976696 -0.23449339 1.0012259483 0.47976696 -0.36004984 0.96914768
		 0.47976696 0.3600499 -0.9315834 0.47976696 0.24490683 -1.0012259483 0.47976696;
createNode polyExtrudeFace -n "polyExtrudeFace9";
	setAttr ".ics" -type "componentList" 2 "f[19]" "f[24]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -4.0367169 -28.281105 -5.9604645e-008 ;
	setAttr ".rs" 34783;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -6.6469602584838867 -35.603641510009766 -1.437713623046875 ;
	setAttr ".cbx" -type "double3" -1.4264736175537107 -20.958568572998047 1.4377135038375854 ;
createNode polyTweak -n "polyTweak10";
	setAttr ".uopa" yes;
	setAttr -s 8 ".tk[206:213]" -type "float3"  -0.1830387 0.49268541 -0.28875408
		 -0.1192095 0.50899333 -0.28875408 0.12450337 -0.50899327 -0.28875408 0.18303868 -0.47358876
		 -0.28875408 -0.11920945 0.50899333 0.28875408 -0.18303864 0.49268541 0.28875408 0.1830387
		 -0.47358876 0.28875408 0.12450337 -0.50899327 0.28875408;
createNode polyExtrudeFace -n "polyExtrudeFace10";
	setAttr ".ics" -type "componentList" 2 "f[19]" "f[24]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -4.0367169 -28.281105 -1.1920929e-007 ;
	setAttr ".rs" 51189;
	setAttr ".lt" -type "double3" -2.6055546609171643e-015 -5.3767754137901136e-015 
		0.74248199913376189 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -6.3795413970947266 -34.853446960449219 -2.031437873840332 ;
	setAttr ".cbx" -type "double3" -1.6938927173614502 -21.708761215209961 2.0314376354217529 ;
createNode polyTweak -n "polyTweak11";
	setAttr ".uopa" yes;
	setAttr -s 8 ".tk[214:221]" -type "float3"  -0.17890482 -0.750193 0.58912838
		 -0.26741907 -0.70767707 0.58912832 0.25552547 0.6832177 0.59372407 0.17478631 0.74489152
		 0.58342963 -0.26741907 -0.70767707 -0.58912843 0.18667988 0.750193 -0.59372413 0.26741907
		 0.68851858 -0.58342963 -0.17890486 -0.750193 -0.58912832;
createNode polyExtrudeFace -n "polyExtrudeFace11";
	setAttr ".ics" -type "componentList" 2 "f[19]" "f[24]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -4.0367475 -28.276596 2.3841858e-007 ;
	setAttr ".rs" 52090;
	setAttr ".lt" -type "double3" -2.4980018054066022e-016 4.59094567917262e-015 0.90212659332625889 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -6.144477367401123 -34.152240753173828 -2.4797260761260982 ;
	setAttr ".cbx" -type "double3" -1.9290174245834353 -22.400951385498047 2.4797265529632568 ;
createNode polyTweak -n "polyTweak12";
	setAttr ".uopa" yes;
	setAttr -s 8 ".tk[222:229]" -type "float3"  -0.16771963 -0.69573289 -0.29236436
		 -0.24992216 -0.65624893 -0.29236436 0.235733 0.63546634 -0.29403129 0.16075104 0.69274217
		 -0.29029745 -0.24677847 -0.65721488 0.29236439 0.17494015 0.69669902 0.29403129 0.24992216
		 0.63942277 0.29029745 -0.16457604 -0.69669902 0.29236436;
createNode polyCube -n "polyCube2";
	setAttr ".w" 0.17759391438577765;
	setAttr ".h" 95.034023962951281;
	setAttr ".d" 0.17911476239481061;
	setAttr ".cuv" 4;
createNode polyCylinder -n "polyCylinder2";
	setAttr ".r" 1.429416067662453;
	setAttr ".h" 15.136129762320436;
	setAttr ".sa" 8;
	setAttr ".sc" 1;
	setAttr ".cuv" 3;
createNode polyCut -n "polyCut1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:23]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -34.029701827117719 7.5680648811602174 36.351364193107464 1;
	setAttr ".pc" -type "double3" -34.029701232910156 23.932617369104985 39.566220006750214 ;
	setAttr ".ro" -type "double3" -90 90 0 ;
	setAttr ".ps" -type "double2" 2.8588333129882813 29.143241882324219 ;
createNode polyTweak -n "polyTweak13";
	setAttr ".uopa" yes;
	setAttr -s 2 ".tk";
	setAttr ".tk[17]" -type "float3" 0 14.007111 0 ;
	setAttr ".tk[41]" -type "float3" 0 -9.5367432e-007 0 ;
createNode polyCut -n "polyCut2";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:31]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -34.029701827117719 7.5680648811602174 36.351364193107464 1;
	setAttr ".pc" -type "double3" -34.029701232910156 21.068311303535197 39.88059506272738 ;
	setAttr ".ro" -type "double3" -90 90 0 ;
	setAttr ".ps" -type "double2" 2.8588333129882813 29.143241882324219 ;
createNode polyCut -n "polyCut3";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:39]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -34.029701827117719 7.5680648811602174 36.351364193107464 1;
	setAttr ".pc" -type "double3" -34.029701232910156 17.330741193584377 39.496358883199726 ;
	setAttr ".ro" -type "double3" -90 90 0 ;
	setAttr ".ps" -type "double2" 2.8588333129882813 29.143241882324219 ;
createNode polyCut -n "polyCut4";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:47]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -34.029701827117719 7.5680648811602174 36.351364193107464 1;
	setAttr ".pc" -type "double3" -34.029701232910156 11.354632350925108 39.725539852024298 ;
	setAttr ".ro" -type "double3" -90 90 0 ;
	setAttr ".ps" -type "double2" 2.8588333129882813 29.143241882324219 ;
createNode polyCut -n "polyCut5";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:55]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -34.029701827117719 7.5680648811602174 36.351364193107464 1;
	setAttr ".pc" -type "double3" -34.029701232910156 6.4308775535622651 40.301043659508267 ;
	setAttr ".ro" -type "double3" -90 90 0 ;
	setAttr ".ps" -type "double2" 2.8588333129882813 29.143241882324219 ;
createNode polyCut -n "polyCut6";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:63]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -34.029701827117719 7.5680648811602174 36.351364193107464 1;
	setAttr ".pc" -type "double3" -34.029701232910156 2.8819374074111241 39.949346888268067 ;
	setAttr ".ro" -type "double3" -90 90 0 ;
	setAttr ".ps" -type "double2" 2.8588333129882813 29.143241882324219 ;
createNode polyExtrudeFace -n "polyExtrudeFace12";
	setAttr ".ics" -type "componentList" 1 "f[8:15]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -34.029701827117719 7.5680648811602174 36.351364193107464 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -34.029701 1.9152399e-007 36.351364 ;
	setAttr ".rs" 60198;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -35.285854576179304 1.9152398778032875e-007 35.09521144404588 ;
	setAttr ".cbx" -type "double3" -32.773549078056135 1.9152398778032875e-007 37.607516942169049 ;
createNode polyTweak -n "polyTweak14";
	setAttr ".uopa" yes;
	setAttr -s 66 ".tk[0:65]" -type "float3"  -0.12251606 0 0.12251606 0
		 0 0.17326394 0.12251606 0 0.12251606 0.17326394 0 0 0.12251606 0 -0.12251606 0 0
		 -0.17326394 -0.12251606 0 -0.12251606 -0.17326394 0 0 -0.69425076 0 0.69425076 0
		 0 0.98181945 0.69425076 0 0.69425076 0.98181945 0 0 0.69425076 0 -0.69425076 0 0
		 -0.98181945 -0.69425076 0 -0.69425076 -0.98181945 0 0 0 0 0 0 -2.97343493 0 -0.1370659
		 -1.023117542 -0.21919045 0 -1.023117542 -0.3099817 0.13706727 -1.023117542 -0.21919045
		 0.19384189 -1.023117542 -1.111903e-006 0.13706727 -1.023117542 0.21919045 0 -1.023117542
		 0.3099817 -0.1370659 -1.023117542 0.21919045 -0.19384052 -1.023117542 -1.111903e-006
		 -0.21241218 -1.98228979 -0.55438739 0 -1.98228979 -0.78402179 0.21241352 -1.98228979
		 -0.55438739 0.30039784 -1.98228979 -1.8147175e-006 0.21241352 -1.98228979 0.55438739
		 0 -1.98228979 0.78402179 -0.21241218 -1.98228979 0.55438739 -0.30039638 -1.98228979
		 -1.8147175e-006 -0.31072992 0 -0.43060771 0 0 -0.60897279 0.31072992 0 -0.43060771
		 0.43943983 0 0 0.31072992 0 0.43060771 0 0 0.60897279 -0.31072992 0 0.43060771 -0.43943983
		 0 0 -0.35004592 0 0.35004592 0 0 0.49503985 0.35004592 0 0.35004592 0.49503985 0
		 0 0.35004592 0 -0.35004592 0 0 -0.49503985 -0.35004592 0 -0.35004592 -0.49503985
		 0 0 -0.19252524 0 0.19252524 0 0 0.2722719 0.19252524 0 0.19252524 0.2722719 0 0
		 0.19252524 0 -0.19252524 0 0 -0.2722719 -0.19252524 0 -0.19252524 -0.2722719 0 0
		 -0.10501376 0 0.10501376 0 0 0.14851195 0.10501376 0 0.10501376 0.14851195 0 0 0.10501376
		 0 -0.10501376 0 0 -0.14851195 -0.10501376 0 -0.10501376 -0.14851195 0 0;
createNode polyExtrudeFace -n "polyExtrudeFace13";
	setAttr ".ics" -type "componentList" 1 "f[8:15]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -34.029701827117719 7.5680648811602174 36.351364193107464 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -34.029701 1.9152399e-007 36.351364 ;
	setAttr ".rs" 61712;
	setAttr ".lt" -type "double3" 0 1.5310638888165211e-015 57.104701240845493 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -35.04358327204875 1.9152398778032875e-007 35.337482748176434 ;
	setAttr ".cbx" -type "double3" -33.015820382186689 1.9152398778032875e-007 37.365245638038495 ;
createNode polyTweak -n "polyTweak15";
	setAttr ".uopa" yes;
	setAttr -s 9 ".tk[65:73]" -type "float3"  -0.17131172 0 0.17131172 0
		 0 0.24227166 0 0 0 0.17131172 0 0.17131172 0.24227166 0 0 0.17131172 0 -0.17131172
		 0 0 -0.24227166 -0.17131172 0 -0.17131172 -0.24227166 0 0;
createNode polyCut -n "polyCut7";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[8:15]";
	setAttr ".ix" -type "matrix" 6.4990475286365159e-017 0.29269108028230556 0 0 0.34406314250337217 -7.639736454642602e-017 4.2135582617133444e-017 0
		 3.5844319460670651e-017 -7.2153922044254814e-033 -0.29269108028230556 0 15.497894384132811 -1.4535233722997385 0.046746947329069144 1;
	setAttr ".pc" -type "double3" -19.781761169434503 -1.3622836578119977 0.12092197350159856 ;
	setAttr ".ro" -type "double3" -1.2723348244361296e-014 0 -90 ;
createNode polyTweak -n "polyTweak16";
	setAttr ".uopa" yes;
	setAttr -s 33 ".tk";
	setAttr ".tk[0]" -type "float3" 7.2164497e-015 -4.7792354 0 ;
	setAttr ".tk[1]" -type "float3" 2.9973432e-015 -4.7792354 0 ;
	setAttr ".tk[2]" -type "float3" 7.2164497e-015 -4.7792354 0 ;
	setAttr ".tk[3]" -type "float3" 8.437695e-015 -4.7792354 -2.9493647e-015 ;
	setAttr ".tk[4]" -type "float3" 7.2164497e-015 -4.7792354 0 ;
	setAttr ".tk[5]" -type "float3" 2.9973432e-015 -4.7792354 0 ;
	setAttr ".tk[6]" -type "float3" 7.2164497e-015 -4.7792354 0 ;
	setAttr ".tk[7]" -type "float3" 8.437695e-015 -4.7792354 -2.9493647e-015 ;
	setAttr ".tk[57]" -type "float3" 7.2164497e-015 -3.1094475 0 ;
	setAttr ".tk[58]" -type "float3" 2.9973432e-015 -3.1094475 0 ;
	setAttr ".tk[59]" -type "float3" 7.2164497e-015 -3.1094475 0 ;
	setAttr ".tk[60]" -type "float3" 8.437695e-015 -3.1094475 -2.9493647e-015 ;
	setAttr ".tk[61]" -type "float3" 7.2164497e-015 -3.1094475 0 ;
	setAttr ".tk[62]" -type "float3" 2.9973432e-015 -3.1094475 0 ;
	setAttr ".tk[63]" -type "float3" 7.2164497e-015 -3.1094475 0 ;
	setAttr ".tk[64]" -type "float3" 8.437695e-015 -3.1094475 -2.9493647e-015 ;
	setAttr ".tk[65]" -type "float3" 7.2164497e-015 -4.7792354 0 ;
	setAttr ".tk[66]" -type "float3" 2.9973432e-015 -4.7792354 0 ;
	setAttr ".tk[67]" -type "float3" 7.2164497e-015 -4.7792354 0 ;
	setAttr ".tk[68]" -type "float3" 8.437695e-015 -4.7792354 -2.9493647e-015 ;
	setAttr ".tk[69]" -type "float3" 7.2164497e-015 -4.7792354 0 ;
	setAttr ".tk[70]" -type "float3" 2.9973432e-015 -4.7792354 0 ;
	setAttr ".tk[71]" -type "float3" 7.2164497e-015 -4.7792354 0 ;
	setAttr ".tk[72]" -type "float3" 8.437695e-015 -4.7792354 -2.9493647e-015 ;
	setAttr ".tk[73]" -type "float3" 7.2164497e-015 -37.865547 0 ;
	setAttr ".tk[74]" -type "float3" 2.9973432e-015 -37.865547 0 ;
	setAttr ".tk[75]" -type "float3" 2.9973432e-015 -37.865547 -2.9493647e-015 ;
	setAttr ".tk[76]" -type "float3" 7.2164497e-015 -37.865547 0 ;
	setAttr ".tk[77]" -type "float3" 8.437695e-015 -37.865547 -2.9493647e-015 ;
	setAttr ".tk[78]" -type "float3" 7.2164497e-015 -37.865547 0 ;
	setAttr ".tk[79]" -type "float3" 2.9973432e-015 -37.865547 0 ;
	setAttr ".tk[80]" -type "float3" 7.2164497e-015 -37.865547 0 ;
	setAttr ".tk[81]" -type "float3" 8.437695e-015 -37.865547 -2.9493647e-015 ;
createNode polyCut -n "polyCut8";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 2 "f[8:15]" "f[88:91]";
	setAttr ".ix" -type "matrix" 6.4990475286365159e-017 0.29269108028230556 0 0 0.34406314250337217 -7.639736454642602e-017 4.2135582617133444e-017 0
		 3.5844319460670651e-017 -7.2153922044254814e-033 -0.29269108028230556 0 15.497894384132811 -1.4535233722997385 0.046746947329069144 1;
	setAttr ".pc" -type "double3" -19.781761169434503 -1.4188533297487465 -0.01814513667624286 ;
	setAttr ".ro" -type "double3" -1.2721273074020585e-014 0 -90 ;
createNode polyCut -n "polyCut9";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 2 "f[8:15]" "f[88:95]";
	setAttr ".ix" -type "matrix" 6.4990475286365159e-017 0.29269108028230556 0 0 0.34406314250337217 -7.639736454642602e-017 4.2135582617133444e-017 0
		 3.5844319460670651e-017 -7.2153922044254814e-033 -0.29269108028230556 0 15.497894384132811 -1.4535233722997385 0.046746947329069144 1;
	setAttr ".pc" -type "double3" -19.781761169434503 -1.3434271004997478 0.10442248585338 ;
	setAttr ".ro" -type "double3" -1.2720634560069597e-014 0 -90 ;
createNode polyCut -n "polyCut10";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 2 "f[8:15]" "f[88:99]";
	setAttr ".ix" -type "matrix" 6.4990475286365159e-017 0.29269108028230556 0 0 0.34406314250337217 -7.639736454642602e-017 4.2135582617133444e-017 0
		 3.5844319460670651e-017 -7.2153922044254814e-033 -0.29269108028230556 0 15.497894384132811 -1.4535233722997385 0.046746947329069144 1;
	setAttr ".pc" -type "double3" -19.781761169434503 -1.4164962600847153 0.00071142063600676542 ;
	setAttr ".ro" -type "double3" -1.2720794188557344e-014 0 -90 ;
createNode groupId -n "groupId6";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts4";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:35]";
createNode groupId -n "groupId7";
	setAttr ".ihi" 0;
createNode groupId -n "groupId8";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts5";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:5]";
createNode groupId -n "groupId9";
	setAttr ".ihi" 0;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :renderPartition;
	setAttr -s 2 ".st";
select -ne :initialShadingGroup;
	setAttr -s 10 ".dsm";
	setAttr ".ro" yes;
	setAttr -s 9 ".gn";
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultShaderList1;
	setAttr -s 2 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderingList1;
select -ne :renderGlobalsList1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :defaultHardwareRenderGlobals;
	setAttr ".fn" -type "string" "im";
	setAttr ".res" -type "string" "ntsc_4d 646 485 1.333";
connectAttr "groupId1.id" "pCylinderShape1.iog.og[1].gid";
connectAttr ":initialShadingGroup.mwc" "pCylinderShape1.iog.og[1].gco";
connectAttr "groupParts1.og" "pCylinderShape1.i";
connectAttr "groupId2.id" "pCylinderShape1.ciog.cog[1].cgid";
connectAttr "groupId3.id" "pCubeShape1.iog.og[1].gid";
connectAttr ":initialShadingGroup.mwc" "pCubeShape1.iog.og[1].gco";
connectAttr "groupParts2.og" "pCubeShape1.i";
connectAttr "groupId4.id" "pCubeShape1.ciog.cog[1].cgid";
connectAttr "polyExtrudeFace11.out" "polySurfaceShape1.i";
connectAttr "groupId5.id" "polySurfaceShape1.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "polySurfaceShape1.iog.og[0].gco";
connectAttr "groupId6.id" "pSphereShape1.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "pSphereShape1.iog.og[0].gco";
connectAttr "groupParts4.og" "pSphereShape1.i";
connectAttr "groupId7.id" "pSphereShape1.ciog.cog[0].cgid";
connectAttr "groupId8.id" "pCubeShape2.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "pCubeShape2.iog.og[0].gco";
connectAttr "groupParts5.og" "pCubeShape2.i";
connectAttr "groupId9.id" "pCubeShape2.ciog.cog[0].cgid";
connectAttr "polyCut10.out" "pCylinderShape2.i";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "pCylinderShape1.o" "polyBoolOp1.ip[0]";
connectAttr "pCubeShape1.o" "polyBoolOp1.ip[1]";
connectAttr "pCylinderShape1.wm" "polyBoolOp1.im[0]";
connectAttr "pCubeShape1.wm" "polyBoolOp1.im[1]";
connectAttr "polyCylinder1.out" "groupParts1.ig";
connectAttr "groupId1.id" "groupParts1.gi";
connectAttr "polyCube1.out" "groupParts2.ig";
connectAttr "groupId3.id" "groupParts2.gi";
connectAttr "polyBoolOp1.out" "groupParts3.ig";
connectAttr "groupId5.id" "groupParts3.gi";
connectAttr "groupParts3.og" "polySplit1.ip";
connectAttr "polySplit1.out" "polySplit2.ip";
connectAttr "polySplit2.out" "polySplit3.ip";
connectAttr "polySplit3.out" "polySplit4.ip";
connectAttr "polySplit4.out" "polySplit5.ip";
connectAttr "polySplit5.out" "polySplit6.ip";
connectAttr "polySplit6.out" "polySplit7.ip";
connectAttr "polySplit7.out" "polySplit8.ip";
connectAttr "polySplit8.out" "polyExtrudeFace1.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace1.mp";
connectAttr "polyExtrudeFace1.out" "polyExtrudeFace2.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace2.mp";
connectAttr "polyTweak1.out" "polyExtrudeFace3.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace3.mp";
connectAttr "polyExtrudeFace2.out" "polyTweak1.ip";
connectAttr "polyTweak2.out" "polyExtrudeFace4.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace4.mp";
connectAttr "polyExtrudeFace3.out" "polyTweak2.ip";
connectAttr "polyTweak3.out" "polySplit9.ip";
connectAttr "polyExtrudeFace4.out" "polyTweak3.ip";
connectAttr "polyTweak4.out" "polyExtrudeFace5.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace5.mp";
connectAttr "polySplit9.out" "polyTweak4.ip";
connectAttr "polyTweak5.out" "polySplit10.ip";
connectAttr "polyExtrudeFace5.out" "polyTweak5.ip";
connectAttr "polyTweak6.out" "polySplit11.ip";
connectAttr "polySplit10.out" "polyTweak6.ip";
connectAttr "polySplit11.out" "polySplit12.ip";
connectAttr "polyTweak7.out" "polyExtrudeFace6.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace6.mp";
connectAttr "polySplit12.out" "polyTweak7.ip";
connectAttr "polyTweak8.out" "polyExtrudeFace7.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace7.mp";
connectAttr "polyExtrudeFace6.out" "polyTweak8.ip";
connectAttr "polyTweak9.out" "polyExtrudeFace8.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace8.mp";
connectAttr "polyExtrudeFace7.out" "polyTweak9.ip";
connectAttr "polyTweak10.out" "polyExtrudeFace9.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace9.mp";
connectAttr "polyExtrudeFace8.out" "polyTweak10.ip";
connectAttr "polyTweak11.out" "polyExtrudeFace10.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace10.mp";
connectAttr "polyExtrudeFace9.out" "polyTweak11.ip";
connectAttr "polyTweak12.out" "polyExtrudeFace11.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace11.mp";
connectAttr "polyExtrudeFace10.out" "polyTweak12.ip";
connectAttr "polyTweak13.out" "polyCut1.ip";
connectAttr "pCylinderShape2.wm" "polyCut1.mp";
connectAttr "polyCylinder2.out" "polyTweak13.ip";
connectAttr "polyCut1.out" "polyCut2.ip";
connectAttr "pCylinderShape2.wm" "polyCut2.mp";
connectAttr "polyCut2.out" "polyCut3.ip";
connectAttr "pCylinderShape2.wm" "polyCut3.mp";
connectAttr "polyCut3.out" "polyCut4.ip";
connectAttr "pCylinderShape2.wm" "polyCut4.mp";
connectAttr "polyCut4.out" "polyCut5.ip";
connectAttr "pCylinderShape2.wm" "polyCut5.mp";
connectAttr "polyCut5.out" "polyCut6.ip";
connectAttr "pCylinderShape2.wm" "polyCut6.mp";
connectAttr "polyTweak14.out" "polyExtrudeFace12.ip";
connectAttr "pCylinderShape2.wm" "polyExtrudeFace12.mp";
connectAttr "polyCut6.out" "polyTweak14.ip";
connectAttr "polyTweak15.out" "polyExtrudeFace13.ip";
connectAttr "pCylinderShape2.wm" "polyExtrudeFace13.mp";
connectAttr "polyExtrudeFace12.out" "polyTweak15.ip";
connectAttr "polyTweak16.out" "polyCut7.ip";
connectAttr "pCylinderShape2.wm" "polyCut7.mp";
connectAttr "polyExtrudeFace13.out" "polyTweak16.ip";
connectAttr "polyCut7.out" "polyCut8.ip";
connectAttr "pCylinderShape2.wm" "polyCut8.mp";
connectAttr "polyCut8.out" "polyCut9.ip";
connectAttr "pCylinderShape2.wm" "polyCut9.mp";
connectAttr "polyCut9.out" "polyCut10.ip";
connectAttr "pCylinderShape2.wm" "polyCut10.mp";
connectAttr "polySphere1.out" "groupParts4.ig";
connectAttr "groupId6.id" "groupParts4.gi";
connectAttr "polyCube2.out" "groupParts5.ig";
connectAttr "groupId8.id" "groupParts5.gi";
connectAttr "pCylinderShape1.iog.og[1]" ":initialShadingGroup.dsm" -na;
connectAttr "pCylinderShape1.ciog.cog[1]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape1.iog.og[1]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape1.ciog.cog[1]" ":initialShadingGroup.dsm" -na;
connectAttr "polySurfaceShape1.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCylinderShape2.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pSphereShape1.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pSphereShape1.ciog.cog[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape2.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape2.ciog.cog[0]" ":initialShadingGroup.dsm" -na;
connectAttr "groupId1.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId2.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId3.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId4.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId5.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId6.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId7.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId8.msg" ":initialShadingGroup.gn" -na;
connectAttr "groupId9.msg" ":initialShadingGroup.gn" -na;
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
// End of Arrow.ma
