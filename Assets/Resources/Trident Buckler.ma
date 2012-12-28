//Maya ASCII 2013 scene
//Name: Trident Buckler.ma
//Last modified: Thu, Oct 04, 2012 11:26:52 PM
//Codeset: 1252
requires maya "2013";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2013";
fileInfo "version" "2013 x64";
fileInfo "cutIdentifier" "201202220241-825136";
fileInfo "osv" "Microsoft Windows 7 Home Premium Edition, 64-bit Windows 7 Service Pack 1 (Build 7601)\n";
fileInfo "license" "student";
createNode transform -s -n "persp";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 48.191333672866904 113.54951458804493 175.33495010133944 ;
	setAttr ".r" -type "double3" 688.57976440569689 -1062.6000000001088 0 ;
createNode camera -s -n "perspShape" -p "persp";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999993;
	setAttr ".coi" 214.27085662802759;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".hc" -type "string" "viewSet -p %camera";
createNode transform -s -n "top";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 13.515305615197397 100.09999999999998 -39.007635789640204 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 36.487883623835224;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
createNode transform -s -n "front";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 0 100.1 ;
createNode camera -s -n "frontShape" -p "front";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
createNode transform -s -n "side";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 100.1 0.15544041450777357 0.55514433752775538 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
createNode camera -s -n "sideShape" -p "side";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
createNode transform -n "polySurface1";
createNode mesh -n "polySurfaceShape1" -p "polySurface1";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 15 ".pt";
	setAttr ".pt[1]" -type "float3" 3.1462135 0 0 ;
	setAttr ".pt[2]" -type "float3" 3.1462135 0 0 ;
	setAttr ".pt[13]" -type "float3" -5.8302689e-008 0 0 ;
	setAttr ".pt[14]" -type "float3" -3.1462135 0 0 ;
	setAttr ".pt[15]" -type "float3" -3.1462135 0 0 ;
	setAttr ".pt[27]" -type "float3" 3.1462135 0 0 ;
	setAttr ".pt[28]" -type "float3" 3.1462135 0 0 ;
	setAttr ".pt[49]" -type "float3" -3.1462135 0 0 ;
	setAttr ".pt[50]" -type "float3" -3.1462135 0 0 ;
	setAttr ".pt[67]" -type "float3" -2.3728633 0 0 ;
	setAttr ".pt[68]" -type "float3" -2.0771558 0 0 ;
	setAttr ".pt[69]" -type "float3" -1.3216877 0 0 ;
	setAttr ".pt[70]" -type "float3" 2.2274311 0 0 ;
	setAttr ".pt[71]" -type "float3" 2.3728631 0 0 ;
	setAttr ".pt[72]" -type "float3" 1.4719648 0 0 ;
createNode transform -n "pCube1";
	setAttr ".t" -type "double3" 0.097884030645268183 0.71933765890601942 -13.110155743467175 ;
createNode mesh -n "pCubeShape1" -p "pCube1";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 32 ".pt[0:31]" -type "float3"  0 0 -0.82281435 0 0 -0.82281429 
		0 0 -0.82281435 0 0 -0.82281429 0 0 0.59417725 0 0 0.54016113 0 0 0.59417725 0 0 
		0.54016113 0 0 1.9715881 0 0 1.9715881 0 0 -0.82281429 0 0 -0.82281429 0 0 1.9715881 
		0 0 1.9715881 0 0 -0.82281435 0 0 -0.82281435 0 0 -0.82281435 0 0 -0.82281435 0 0 
		1.9715881 0 0 0.59417725 0 0 0.59417725 0 0 1.9715881 0 0 -0.82281435 0 0 -0.82281435 
		0 0 1.9715881 0 0 -0.82281429 0 0 0.54016113 0 0 -0.82281429 0 0 -0.82281429 0 0 
		1.9715881 0 0 -0.82281429 0 0 0.54016113;
createNode transform -n "shield_handle:pCube1";
	setAttr ".t" -type "double3" -0.0016376831486619725 0.74237876913255851 16.172576774287847 ;
	setAttr ".s" -type "double3" 1.3097063243427762 1.7082054895886714 1 ;
createNode transform -n "shield_handle:transform4" -p "shield_handle:pCube1";
	setAttr ".v" no;
createNode mesh -n "shield_handle:pCubeShape1" -p "shield_handle:transform4";
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
	setAttr ".pt[18]" -type "float3"  0 0 5.2548809;
createNode transform -n "shield_handle:polySurface1";
	setAttr ".t" -type "double3" 0 0.24875322001213096 1.469335778997003 ;
	setAttr ".s" -type "double3" 1 0.63320904366495157 1.0703123411197315 ;
createNode transform -n "shield_handle:transform3" -p "shield_handle:polySurface1";
	setAttr ".v" no;
createNode mesh -n "shield_handle:polySurfaceShape1" -p "shield_handle:transform3";
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
createNode transform -n "shield_handle:Shield_Of_The_Round:pCylinder1";
	setAttr ".t" -type "double3" 0.1972945500150729 1.0193518431166035 0.54260490445020082 ;
createNode transform -n "shield_handle:Shield_Of_The_Round:transform2" -p "shield_handle:Shield_Of_The_Round:pCylinder1";
	setAttr ".v" no;
createNode mesh -n "shield_handle:Shield_Of_The_Round:pCylinderShape1" -p "shield_handle:Shield_Of_The_Round:transform2";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr -s 2 ".iog[0].og";
	setAttr -av ".iog[0].og[0].gco";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 2 ".pt[402:403]" -type "float3"  0 3.0167127 0 0 2.1483769 
		0;
createNode transform -n "shield_handle:Shield_Of_The_Round:polySurface1";
	setAttr ".t" -type "double3" -69.574172029773521 -1.889372347528596 -8.1258596702718418 ;
	setAttr ".r" -type "double3" 270 0 0 ;
	setAttr ".rp" -type "double3" 69.669452667236328 0 13.184549808502195 ;
	setAttr ".sp" -type "double3" 69.669452667236328 0 13.184549808502195 ;
createNode transform -n "shield_handle:Shield_Of_The_Round:transform1" -p "shield_handle:Shield_Of_The_Round:polySurface1";
	setAttr ".v" no;
createNode mesh -n "shield_handle:Shield_Of_The_Round:polySurfaceShape1" -p "shield_handle:Shield_Of_The_Round:transform1";
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
createNode transform -n "shield_handle:Shield_Of_The_Round:polySurface2";
	setAttr ".t" -type "double3" 67.574602186342133 0 0 ;
createNode transform -n "shield_handle:polySurface3" -p "shield_handle:Shield_Of_The_Round:polySurface2";
	setAttr ".t" -type "double3" -68.676762039807528 0.31518052922628037 0 ;
	setAttr ".r" -type "double3" 0 90 0 ;
	setAttr ".s" -type "double3" 0.72948596270654786 0.72948596270654786 0.72948596270654786 ;
	setAttr ".rp" -type "double3" 0.095279693603515639 -1.8893723487854004 1.1577942371368408 ;
	setAttr ".rpt" -type "double3" 1.0625145435333252 0 -1.2530739307403562 ;
	setAttr ".sp" -type "double3" 0.095279693603515639 -1.8893723487854004 1.1577942371368408 ;
createNode transform -n "shield_handle:transform2" -p "shield_handle:polySurface3";
	setAttr ".v" no;
createNode mesh -n "shield_handle:polySurfaceShape3" -p "shield_handle:transform2";
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
createNode transform -n "shield_handle:transform1" -p "shield_handle:Shield_Of_The_Round:polySurface2";
	setAttr ".v" no;
createNode mesh -n "shield_handle:Shield_Of_The_Round:polySurfaceShape2" -p "shield_handle:transform1";
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
createNode transform -n "shield_handle:polySurface4";
createNode transform -n "shield_handle:polySurface7" -p "shield_handle:polySurface4";
	setAttr ".t" -type "double3" 0 0 -12.651982293791672 ;
	setAttr ".s" -type "double3" 1.1916944297940439 1.1916944297940439 1.1916944297940439 ;
createNode mesh -n "shield_handle:polySurfaceShape7" -p "shield_handle:polySurface7";
	setAttr -k off ".v";
	setAttr -s 2 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
createNode transform -n "shield_handle:transform5" -p "shield_handle:polySurface4";
	setAttr ".v" no;
createNode mesh -n "shield_handle:polySurfaceShape4" -p "shield_handle:transform5";
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
createNode lightLinker -s -n "lightLinker1";
	setAttr -s 3 ".lnk";
	setAttr -s 3 ".slnk";
createNode displayLayerManager -n "layerManager";
createNode displayLayer -n "defaultLayer";
createNode renderLayerManager -n "renderLayerManager";
createNode renderLayer -n "defaultRenderLayer";
	setAttr ".g" yes;
createNode lambert -n "lambert2";
createNode shadingEngine -n "lambert2SG";
	setAttr ".ihi" 0;
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo1";
createNode file -n "file1";
	setAttr ".ftn" -type "string" "C:/Users/Joe/Desktop/IS_Weapons/SHIELD OF ASSETS.jpg";
createNode place2dTexture -n "place2dTexture1";
createNode polyCreateFace -n "polyCreateFace1";
	setAttr -s 14 ".v[0:13]" -type "float3"  0.05031842 0 -40.445759 14.840494 
		0 -40.445759 14.840494 0 -38.291847 5.8658729 0 -33.768639 6.7992334 0 9.4531364 
		26.040821 0 9.3095427 12.184007 0 22.879169 12.542992 0 13.760955 9.7429094 0 14.263534 
		9.0967369 0 21.586824 6.7274365 0 22.376591 6.7274365 0 25.822845 11.035254 0 25.822845 
		0.05031842 0 37.956535;
	setAttr ".l[0]"  14;
	setAttr ".tx" 1;
createNode polyMirror -n "polyMirror1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".p" -type "double3" 0.050318419933319092 0 -1.2446117401123047 ;
	setAttr ".d" 1;
	setAttr ".mm" 1;
createNode polyMergeVert -n "polyMergeVert1";
	setAttr ".ics" -type "componentList" 1 "vtx[0]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
createNode polyMergeVert -n "polyMergeVert2";
	setAttr ".ics" -type "componentList" 1 "vtx[13]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
createNode deleteComponent -n "deleteComponent1";
	setAttr ".dc" -type "componentList" 1 "e[13]";
createNode polyExtrudeFace -n "polyExtrudeFace1";
	setAttr ".ics" -type "componentList" 1 "f[0]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.050318718 0 -1.2446117 ;
	setAttr ".rs" 43570;
	setAttr ".lt" -type "double3" 0 1.3667263207536823e-017 -1.4365518814886373 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -25.940183639526367 0 -40.445758819580078 ;
	setAttr ".cbx" -type "double3" 26.040821075439453 0 37.956535339355469 ;
createNode polyMergeVert -n "polyMergeVert3";
	setAttr ".ics" -type "componentList" 2 "vtx[13]" "vtx[39]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
createNode polyTweak -n "polyTweak1";
	setAttr ".uopa" yes;
	setAttr -s 3 ".tk";
	setAttr ".tk[0]" -type "float3" 0 0 3.8146973e-006 ;
	setAttr ".tk[13]" -type "float3" 0 0.71827596 0 ;
	setAttr ".tk[39]" -type "float3" 0 -0.71827596 0 ;
createNode polySplit -n "polySplit1";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].t" 11;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].t" 12;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit2";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 16;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 10;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit3";
	setAttr -s 6 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 28;
	setAttr ".sps[0].sp[0].t" 16;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 28;
	setAttr ".sps[0].sp[1].t" 16;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 0.097345098853111267 0.90265488624572754 ;
	setAttr ".sps[0].sp[2].f" 28;
	setAttr ".sps[0].sp[2].t" 15;
	setAttr ".sps[0].sp[2].bc" -type "double3" 2.3180173513992486e-007 0.93888890743255615 
		0.061110854148864746 ;
	setAttr ".sps[0].sp[3].f" 28;
	setAttr ".sps[0].sp[3].t" 15;
	setAttr ".sps[0].sp[3].bc" -type "double3" 0.097345344722270966 0.90265464782714844 
		0 ;
	setAttr ".sps[0].sp[4].f" 28;
	setAttr ".sps[0].sp[4].t" 14;
	setAttr ".sps[0].sp[4].bc" -type "double3" 0.90265482664108276 0 0.097345173358917236 ;
	setAttr ".sps[0].sp[5].f" 28;
	setAttr ".sps[0].sp[5].t" 13;
	setAttr ".sps[0].sp[5].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit4";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 30;
	setAttr ".sps[0].sp[0].t" 15;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 13;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit5";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 29;
	setAttr ".sps[0].sp[0].t" 8;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 29;
	setAttr ".sps[0].sp[1].t" 9;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit6";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 6;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 3;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 0 1 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit7";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 33;
	setAttr ".sps[0].sp[0].t" 16;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 26;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit8";
	setAttr -s 6 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 34;
	setAttr ".sps[0].sp[0].t" 16;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 34;
	setAttr ".sps[0].sp[1].t" 17;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.90265488624572754 8.3711579890177745e-009 
		0.097345106303691864 ;
	setAttr ".sps[0].sp[2].f" 34;
	setAttr ".sps[0].sp[2].t" 17;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0.93888890743255615 0.061111051589250565 
		4.0978193283081055e-008 ;
	setAttr ".sps[0].sp[3].f" 34;
	setAttr ".sps[0].sp[3].t" 14;
	setAttr ".sps[0].sp[3].bc" -type "double3" 0 0.097345061600208282 0.90265494585037231 ;
	setAttr ".sps[0].sp[4].f" 34;
	setAttr ".sps[0].sp[4].t" 13;
	setAttr ".sps[0].sp[4].bc" -type "double3" 0 0.09734509140253067 0.90265488624572754 ;
	setAttr ".sps[0].sp[5].f" 14;
	setAttr ".sps[0].sp[5].t" 1;
	setAttr ".sps[0].sp[5].bc" -type "double3" 0 0 1 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit9";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 19;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0 1 ;
	setAttr ".sps[0].sp[1].f" 34;
	setAttr ".sps[0].sp[1].t" 16;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 0 1 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit10";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 34;
	setAttr ".sps[0].sp[0].t" 9;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 18;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polyExtrudeFace -n "polyExtrudeFace2";
	setAttr ".ics" -type "componentList" 2 "f[4]" "f[7:9]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.05031848 0.71827596 24.099718 ;
	setAttr ".rs" 63177;
	setAttr ".lt" -type "double3" -6.9388939039072284e-018 -1.5612055896462574e-016 
		0.70310449117796203 ;
	setAttr ".ls" -type "double3" 0.64380555978495635 1 1 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -6.6267995834350586 0 22.376590728759766 ;
	setAttr ".cbx" -type "double3" 6.7274365425109863 1.4365519285202026 25.822845458984375 ;
createNode polyExtrudeFace -n "polyExtrudeFace3";
	setAttr ".ics" -type "componentList" 3 "f[10]" "f[24]" "f[33:34]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.050318241 0.71827596 17.925179 ;
	setAttr ".rs" 37202;
	setAttr ".lt" -type "double3" 0 -4.2305534624368697e-015 -2.3401394442461263 ;
	setAttr ".ls" -type "double3" 1 0.24538870350693126 1.1641496783857621 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -9.64227294921875 0 14.263533592224121 ;
	setAttr ".cbx" -type "double3" 9.7429094314575195 1.4365519285202026 21.586824417114258 ;
createNode polySplit -n "polySplit11";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 27;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[1].f" 27;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 0.49999967217445374 0.50000035762786865 ;
	setAttr ".sps[0].sp[2].f" 26;
	setAttr ".sps[0].sp[2].t" 1;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit12";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[1].f" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 1 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit13";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 14;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".sps[0].sp[1].f" 14;
	setAttr ".sps[0].sp[1].bc" -type "double3" 3.9314333122320022e-008 0.50000005960464478 
		0.49999988079071045 ;
	setAttr ".sps[0].sp[2].f" 13;
	setAttr ".sps[0].sp[2].t" 1;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0 0.5 0.5 ;
	setAttr ".c2v" yes;
createNode polySplit -n "polySplit14";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 44;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0.5 0 0.5 ;
	setAttr ".sps[0].sp[1].f" 47;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polyCube -n "polyCube1";
	setAttr ".w" 34.65400775012867;
	setAttr ".h" 0.99455984778983175;
	setAttr ".d" 51.077180367856499;
	setAttr ".cuv" 4;
createNode polySplitRing -n "polySplitRing1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[0:3]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0.097884030645268183 0.71933765890601942 -13.110155743467175 1;
	setAttr ".wt" 0.88777673244476318;
	setAttr ".dr" no;
	setAttr ".re" 2;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polyTweak -n "polyTweak2";
	setAttr ".uopa" yes;
	setAttr -s 4 ".tk[0:3]" -type "float3"  -8.079921722 0 -2.25905657
		 8.079921722 0 -2.25905657 -8.079921722 0 -2.25905657 8.079921722 0 -2.25905657;
createNode polySplitRing -n "polySplitRing2";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[0:3]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0.097884030645268183 0.71933765890601942 -13.110155743467175 1;
	setAttr ".wt" 0.12495951354503632;
	setAttr ".re" 2;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polyExtrudeFace -n "polyExtrudeFace4";
	setAttr ".ics" -type "componentList" 4 "f[1]" "f[3]" "f[7]" "f[9]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0.097884030645268183 0.71933765890601942 -13.110155743467175 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.097884029 0.71933764 -14.239684 ;
	setAttr ".rs" 55469;
	setAttr ".lt" -type "double3" 0 1.1340546069450173e-016 0.51073279052553744 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -25.309041170770747 0.22205774667266676 -38.648745221006237 ;
	setAttr ".cbx" -type "double3" 25.504809232061284 1.2166175711393721 10.169377642763294 ;
createNode groupParts -n "shield_handle:groupParts8";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:30]";
createNode polySeparate -n "shield_handle:polySeparate2";
	setAttr ".ic" 3;
createNode groupParts -n "shield_handle:groupParts5";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:172]";
createNode polyUnite -n "shield_handle:polyUnite1";
	setAttr -s 3 ".ip";
	setAttr -s 3 ".im";
createNode groupId -n "shield_handle:groupId3";
	setAttr ".ihi" 0;
createNode groupParts -n "shield_handle:groupParts3";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:19]";
createNode polySplit -n "shield_handle:polySplit5";
	setAttr -s 4 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 17;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0 1 ;
	setAttr ".sps[0].sp[1].f" 17;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.44681146740913391 0.55318856239318848 
		0 ;
	setAttr ".sps[0].sp[2].f" 4;
	setAttr ".sps[0].sp[2].t" 1;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0.43583574891090393 0 0.56416428089141846 ;
	setAttr ".sps[0].sp[3].f" 7;
	setAttr ".sps[0].sp[3].t" 2;
	setAttr ".sps[0].sp[3].bc" -type "double3" 0 0 1 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:polySplit4";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 4;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 5;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:polySplit3";
	setAttr ".e[0]"  0.95645767;
	setAttr ".d[0]"  -2147483647;
createNode polyTweak -n "shield_handle:polyTweak6";
	setAttr ".uopa" yes;
	setAttr -s 8 ".tk[9:16]" -type "float3"  0 -0.28910506 0 0 -0.28910506
		 0 0 -0.28910506 0 0 -0.28910506 0 0 -0.28910506 0 0 -0.28910506 0 0 -0.28910506 0
		 0 -0.28910506 0;
createNode polySoftEdge -n "shield_handle:polySoftEdge1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[*]";
	setAttr ".ix" -type "matrix" 1.3097063243427762 0 0 0 0 1 0 0 0 0 1 0 -0.0016376831486619725 0.68525106117299395 16.172576774287847 1;
	setAttr ".a" 0;
createNode polyTweak -n "shield_handle:polyTweak3";
	setAttr ".uopa" yes;
	setAttr -s 8 ".tk[9:16]" -type "float3"  -2.3841858e-007 0 -16.77170181
		 -2.3841858e-007 0 -16.77170181 -2.3841858e-007 0 -16.77170181 -2.3841858e-007 0 -16.77170181
		 1.1920929e-006 0 -16.77172089 1.1920929e-006 0 -16.77172089 1.1920929e-006 0 -16.77172089
		 1.1920929e-006 0 -16.77172089;
createNode polyExtrudeFace -n "shield_handle:polyExtrudeFace2";
	setAttr ".ics" -type "componentList" 2 "f[2]" "f[8]";
	setAttr ".ix" -type "matrix" 1.3097063243427762 0 0 0 0 1 0 0 0 0 1 0 -0.0016376831486619725 0.68525106117299395 16.172576774287847 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -1.2472883 0.68525106 -1.2294503 ;
	setAttr ".rs" 53621;
	setAttr ".lt" -type "double3" -14.458969450432626 3.9968028886505628e-015 3.5724892142511382 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -2.4931188784113285 4.0250142108178011e-009 -2.6411420214885197 ;
	setAttr ".cbx" -type "double3" -0.0014578223558292548 1.3705021183209736 0.18224130981030839 ;
createNode polyExtrudeFace -n "shield_handle:polyExtrudeFace1";
	setAttr ".ics" -type "componentList" 1 "f[6:7]";
	setAttr ".ix" -type "matrix" 1.3097063243427762 0 0 0 0 1 0 0 0 0 1 0 -0.0016376831486619725 0.68525106117299395 16.172576774287847 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 1.2440137 0.68525106 -1.2294503 ;
	setAttr ".rs" 33367;
	setAttr ".lt" -type "double3" 14.453330448875745 -2.8484159475539172e-015 3.6033136707554201 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.0018161387790506846 4.0250142108178011e-009 -2.6411420214885197 ;
	setAttr ".cbx" -type "double3" 2.4898433559848439 1.3705021183209736 0.18224130981030839 ;
createNode polyTweak -n "shield_handle:polyTweak2";
	setAttr ".uopa" yes;
	setAttr ".tk[8]" -type "float3"  0 0 -2.82338262;
createNode polySplit -n "shield_handle:polySplit2";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 3;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0 1 ;
	setAttr ".sps[0].sp[1].f" 2;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.49999994039535522 0.50000005960464478 
		0 ;
	setAttr ".sps[0].sp[2].f" 5;
	setAttr ".sps[0].sp[2].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:polySplit1";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 2;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 1 0 ;
	setAttr ".sps[0].sp[1].f" 5;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polyTweak -n "shield_handle:polyTweak1";
	setAttr ".uopa" yes;
	setAttr -s 8 ".tk[0:7]" -type "float3"  1.90245771 0 0 0 0.74882418
		 0 0 -0.60303473 0 -1.90245664 0 0 0 -0.60303473 0 -1.90245664 0 0 1.90245771 0 0
		 0 0.74882418 0;
createNode polyCube -n "shield_handle:polyCube1";
	setAttr ".w" 3.8046408119399442;
	setAttr ".h" 1.3705021223459879;
	setAttr ".d" 31.980670540089065;
	setAttr ".cuv" 4;
createNode groupId -n "shield_handle:groupId4";
	setAttr ".ihi" 0;
createNode groupId -n "shield_handle:groupId5";
	setAttr ".ihi" 0;
createNode groupParts -n "shield_handle:groupParts4";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:121]";
createNode polyBevel -n "shield_handle:polyBevel1";
	setAttr ".ics" -type "componentList" 1 "e[0:59]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0.90250087995727468 0 1;
	setAttr ".ws" yes;
	setAttr ".oaf" yes;
	setAttr ".o" 0.5;
	setAttr ".at" 180;
	setAttr ".fn" yes;
	setAttr ".mv" yes;
	setAttr ".mvt" 0.0001;
	setAttr ".sa" 30;
	setAttr ".ma" 180;
createNode polyExtrudeFace -n "shield_handle:polyExtrudeFace3";
	setAttr ".ics" -type "componentList" 1 "f[0]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0.90250087995727468 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.0087842941 0.90250087 5.9797516 ;
	setAttr ".rs" 62579;
	setAttr ".lt" -type "double3" 0 -1.8402890719057952e-016 1.1712074821511065 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -14.931818008422852 0.90250087995727468 -17.557207107543945 ;
	setAttr ".cbx" -type "double3" 14.949386596679688 0.90250087995727468 29.51671028137207 ;
createNode deleteComponent -n "shield_handle:deleteComponent1";
	setAttr ".dc" -type "componentList" 1 "e[3]";
createNode polyMergeVert -n "shield_handle:polyMergeVert2";
	setAttr ".ics" -type "componentList" 1 "vtx[4]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
createNode polyMergeVert -n "shield_handle:polyMergeVert1";
	setAttr ".ics" -type "componentList" 2 "vtx[3]" "vtx[14]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
createNode polyTweak -n "shield_handle:polyTweak5";
	setAttr ".uopa" yes;
	setAttr -s 2 ".tk";
	setAttr ".tk[3]" -type "float3" -0.095661677 0 0 ;
	setAttr ".tk[14]" -type "float3" 0.095661677 0 0 ;
createNode polyMirror -n "shield_handle:polyMirror1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".p" -type "double3" 0.0087844962254166603 0 5.9797515869140625 ;
	setAttr ".d" 1;
	setAttr ".mm" 1;
createNode polyTweak -n "shield_handle:polyTweak4";
	setAttr ".uopa" yes;
	setAttr -s 9 ".tk";
	setAttr ".tk[0]" -type "float3" -3.3410332 0 -0.73056769 ;
	setAttr ".tk[1]" -type "float3" -2.1406994 0 -0.21843877 ;
	setAttr ".tk[4]" -type "float3" 0 0 -1.8091978 ;
	setAttr ".tk[5]" -type "float3" -0.17300293 0 -6.0266843 ;
	setAttr ".tk[6]" -type "float3" -0.94824278 0 -4.9768939 ;
	setAttr ".tk[7]" -type "float3" -1.0414178 0 -3.5860689 ;
	setAttr ".tk[8]" -type "float3" -0.9407829 0 -3.6907635 ;
	setAttr ".tk[9]" -type "float3" -1.0855187 0 -0.28947169 ;
	setAttr ".tk[10]" -type "float3" -1.8001914 0 -1.112864 ;
createNode polyCreateFace -n "shield_handle:polyCreateFace1";
	setAttr -s 11 ".v[0:10]" -type "float3"  14.836344 0 -16.217943 9.9575987 
		0 -17.078899 3.2612815 0 -17.557207 0.10444617 0 -17.557207 0.0087844962 0 31.325909 
		6.9920869 0 29.412676 10.914215 0 23.38599 13.497081 0 16.689672 15.027667 0 8.8454151 
		15.984284 0 -4.6428809 16.749578 0 -15.548312;
	setAttr ".l[0]"  11;
	setAttr ".tx" 1;
createNode groupId -n "shield_handle:groupId6";
	setAttr ".ihi" 0;
createNode groupParts -n "shield_handle:groupParts2";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:30]";
createNode polySeparate -n "shield_handle:polySeparate1";
	setAttr ".ic" 2;
createNode groupParts -n "shield_handle:Shield_Of_The_Round:groupParts3";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:415]";
createNode polyUnite -n "shield_handle:Shield_Of_The_Round:polyUnite1";
	setAttr -s 2 ".ip";
	setAttr -s 2 ".im";
createNode groupId -n "shield_handle:Shield_Of_The_Round:groupId1";
	setAttr ".ihi" 0;
createNode groupParts -n "shield_handle:Shield_Of_The_Round:groupParts1";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:384]";
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit26";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 378;
	setAttr ".sps[0].sp[0].t" 3;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 1 0 ;
	setAttr ".sps[0].sp[1].f" 372;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.5 0.49999997019767761 2.9802322387695313e-008 ;
	setAttr ".sps[0].sp[2].f" 380;
	setAttr ".sps[0].sp[2].t" 2;
	setAttr ".sps[0].sp[2].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit25";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 343;
	setAttr ".sps[0].sp[0].t" 3;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 329;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.5 0.49999997019767761 2.9802322387695313e-008 ;
	setAttr ".sps[0].sp[2].f" 345;
	setAttr ".sps[0].sp[2].t" 3;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0 0 1 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit24";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 373;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 379;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.50000011920928955 0.49999991059303278 
		-2.9802322387695313e-008 ;
	setAttr ".sps[0].sp[2].f" 377;
	setAttr ".sps[0].sp[2].t" 4;
	setAttr ".sps[0].sp[2].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit23";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 269;
	setAttr ".sps[0].sp[0].t" 4;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 268;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0.50000005960464478 0.49999994039535522 
		0 ;
	setAttr ".sps[0].sp[2].f" 268;
	setAttr ".sps[0].sp[2].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode deleteComponent -n "shield_handle:Shield_Of_The_Round:deleteComponent4";
	setAttr ".dc" -type "componentList" 0;
createNode polyTweak -n "shield_handle:Shield_Of_The_Round:polyTweak2";
	setAttr ".uopa" yes;
	setAttr -s 7 ".tk";
	setAttr ".tk[4]" -type "float3" -0.0033042373 0 0.019858524 ;
	setAttr ".tk[84]" -type "float3" 0 0 -1.1292286e-008 ;
	setAttr ".tk[289]" -type "float3" -7.4505806e-008 0 0 ;
	setAttr ".tk[309]" -type "float3" -0.058344085 0 0.019858524 ;
	setAttr ".tk[310]" -type "float3" -0.10040869 0 0.0060447566 ;
	setAttr ".tk[369]" -type "float3" 0 0 -1.1292286e-008 ;
	setAttr ".tk[388]" -type "float3" -7.4505806e-008 0 0 ;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit22";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 374;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 1 0 ;
	setAttr ".sps[0].sp[1].f" 375;
	setAttr ".sps[0].sp[1].t" 6;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit21";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 369;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 1 0 ;
	setAttr ".sps[0].sp[1].f" 376;
	setAttr ".sps[0].sp[1].t" 4;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit20";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 295;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0 1 ;
	setAttr ".sps[0].sp[1].f" 287;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 1 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit19";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 374;
	setAttr ".sps[0].sp[0].t" 4;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 364;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 1 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit18";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 330;
	setAttr ".sps[0].sp[0].t" 4;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 330;
	setAttr ".sps[0].sp[1].t" 3;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit17";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 372;
	setAttr ".sps[0].sp[0].t" 4;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 369;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 1 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit16";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 302;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0 1 ;
	setAttr ".sps[0].sp[1].f" 303;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 0 1 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit15";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 370;
	setAttr ".sps[0].sp[0].t" 4;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 370;
	setAttr ".sps[0].sp[1].t" 2;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit14";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 304;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 1 0 ;
	setAttr ".sps[0].sp[1].f" 369;
	setAttr ".sps[0].sp[1].t" 3;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit13";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 337;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 1 0 ;
	setAttr ".sps[0].sp[1].f" 326;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 1 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit12";
	setAttr -s 6 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 356;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 356;
	setAttr ".sps[0].sp[1].bc" -type "double3" 3.0304547635751078e-006 0.021202092990279201 
		0.97879487276077259 ;
	setAttr ".sps[0].sp[2].f" 356;
	setAttr ".sps[0].sp[2].t" 1;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0.97901064157485973 1.523964965599589e-005 
		0.020974118262529373 ;
	setAttr ".sps[0].sp[3].f" 314;
	setAttr ".sps[0].sp[3].t" 1;
	setAttr ".sps[0].sp[3].bc" -type "double3" 0.99699991941452037 0.003000049851834774 
		3.0733644962310791e-008 ;
	setAttr ".sps[0].sp[4].f" 314;
	setAttr ".sps[0].sp[4].t" 1;
	setAttr ".sps[0].sp[4].bc" -type "double3" 0.99648177623748779 0 0.003518223762512207 ;
	setAttr ".sps[0].sp[5].f" 311;
	setAttr ".sps[0].sp[5].t" 1;
	setAttr ".sps[0].sp[5].bc" -type "double3" 0 1 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit11";
	setAttr -s 7 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 222;
	setAttr ".sps[0].sp[0].t" 48;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 222;
	setAttr ".sps[0].sp[1].t" 44;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[2].f" 222;
	setAttr ".sps[0].sp[2].t" 45;
	setAttr ".sps[0].sp[2].bc" -type "double3" 0.8297431468963623 0.1702544093132019 
		2.4437904357910156e-006 ;
	setAttr ".sps[0].sp[3].f" 222;
	setAttr ".sps[0].sp[3].t" 46;
	setAttr ".sps[0].sp[3].bc" -type "double3" 0.83455312252044667 0.16544638574123385 
		4.9173831939697266e-007 ;
	setAttr ".sps[0].sp[4].f" 222;
	setAttr ".sps[0].sp[4].t" 47;
	setAttr ".sps[0].sp[4].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[5].f" 222;
	setAttr ".sps[0].sp[5].t" 49;
	setAttr ".sps[0].sp[5].bc" -type "double3" 0 0 1 ;
	setAttr ".sps[0].sp[6].f" 222;
	setAttr ".sps[0].sp[6].t" 48;
	setAttr ".sps[0].sp[6].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit10";
	setAttr -s 3 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 358;
	setAttr ".sps[0].sp[0].t" 2;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0 1 ;
	setAttr ".sps[0].sp[1].f" 349;
	setAttr ".sps[0].sp[1].t" 2;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[2].f" 355;
	setAttr ".sps[0].sp[2].t" 5;
	setAttr ".sps[0].sp[2].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit9";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 297;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 342;
	setAttr ".sps[0].sp[1].t" 9;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit8";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 348;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0 1 ;
	setAttr ".sps[0].sp[1].f" 356;
	setAttr ".sps[0].sp[1].t" 4;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit7";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 319;
	setAttr ".sps[0].sp[0].t" 7;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 319;
	setAttr ".sps[0].sp[1].t" 8;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit6";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 354;
	setAttr ".sps[0].sp[0].t" 2;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 344;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 0 1 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit5";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 340;
	setAttr ".sps[0].sp[0].t" 7;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 340;
	setAttr ".sps[0].sp[1].t" 8;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit4";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 343;
	setAttr ".sps[0].sp[0].t" 1;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0 1 ;
	setAttr ".sps[0].sp[1].f" 353;
	setAttr ".sps[0].sp[1].t" 4;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit3";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 347;
	setAttr ".sps[0].sp[0].t" 7;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 347;
	setAttr ".sps[0].sp[1].t" 8;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit2";
	setAttr -s 4 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 347;
	setAttr ".sps[0].sp[0].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[1].f" 329;
	setAttr ".sps[0].sp[1].t" 1;
	setAttr ".sps[0].sp[1].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[2].f" 339;
	setAttr ".sps[0].sp[2].bc" -type "double3" 1 0 0 ;
	setAttr ".sps[0].sp[3].f" 348;
	setAttr ".sps[0].sp[3].t" 23;
	setAttr ".sps[0].sp[3].bc" -type "double3" 1 0 0 ;
	setAttr ".c2v" yes;
createNode polySplit -n "shield_handle:Shield_Of_The_Round:polySplit1";
	setAttr -s 2 ".sps[0].sp";
	setAttr ".sps[0].sp[0].f" 241;
	setAttr ".sps[0].sp[0].t" 49;
	setAttr ".sps[0].sp[0].bc" -type "double3" 0 0 1 ;
	setAttr ".sps[0].sp[1].f" 241;
	setAttr ".sps[0].sp[1].t" 49;
	setAttr ".sps[0].sp[1].bc" -type "double3" 0 1 0 ;
	setAttr ".c2v" yes;
createNode deleteComponent -n "shield_handle:Shield_Of_The_Round:deleteComponent3";
	setAttr ".dc" -type "componentList" 1 "e[741]";
createNode deleteComponent -n "shield_handle:Shield_Of_The_Round:deleteComponent2";
	setAttr ".dc" -type "componentList" 41 "e[544]" "e[546]" "e[550:551]" "e[553]" "e[555]" "e[557]" "e[560]" "e[562]" "e[574:575]" "e[577]" "e[580:581]" "e[583]" "e[586]" "e[588]" "e[605]" "e[614]" "e[623:630]" "e[635]" "e[638:646]" "e[657]" "e[664:665]" "e[679]" "e[681]" "e[691:692]" "e[703:704]" "e[712:714]" "e[719:721]" "e[728:736]" "e[738]" "e[741:742]" "e[748:753]" "e[755:757]" "e[762]" "e[776]" "e[791]" "e[794:795]" "e[800]" "e[804]" "e[813]" "e[821]" "e[826:827]";
createNode deleteComponent -n "shield_handle:Shield_Of_The_Round:deleteComponent1";
	setAttr ".dc" -type "componentList" 46 "e[541]" "e[544]" "e[566:567]" "e[569]" "e[572:573]" "e[575]" "e[578]" "e[580]" "e[603]" "e[605]" "e[609:610]" "e[612]" "e[614]" "e[617]" "e[627]" "e[637]" "e[651]" "e[654:662]" "e[680:687]" "e[694:701]" "e[703]" "e[706]" "e[712:718]" "e[726:729]" "e[740:743]" "e[760:761]" "e[774:775]" "e[783:785]" "e[793:795]" "e[797]" "e[800:802]" "e[819:824]" "e[826:828]" "e[833]" "e[850:857]" "e[862]" "e[867:868]" "e[872:877]" "e[880:881]" "e[884:885]" "e[890]" "e[897]" "e[905:911]" "e[926:927]" "e[932:933]" "e[950:953]";
createNode polyExtrudeFace -n "shield_handle:Shield_Of_The_Round:polyExtrudeFace3";
	setAttr ".ics" -type "componentList" 45 "f[0]" "f[2:5]" "f[8:10]" "f[13:15]" "f[17:19]" "f[24]" "f[29]" "f[34]" "f[39]" "f[64]" "f[69]" "f[74]" "f[79]" "f[84]" "f[89]" "f[94]" "f[99]" "f[101:104]" "f[107:110]" "f[112:120]" "f[122:125]" "f[128:131]" "f[133:139]" "f[261]" "f[264]" "f[266]" "f[268:270]" "f[272:278]" "f[281:287]" "f[295]" "f[297]" "f[299]" "f[302:315]" "f[322]" "f[324]" "f[327:328]" "f[330]" "f[333:338]" "f[340:345]" "f[347]" "f[350]" "f[353]" "f[355]" "f[357:366]" "f[369:378]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0.1972945500150729 1.0193518431166035 0.54260490445020082 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.19729169 2.3553462 0.54260015 ;
	setAttr ".rs" 33369;
	setAttr ".lt" -type "double3" 2.4153313897545825e-015 -8.3266726846886741e-017 1.3390224773512005 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -23.152829809115783 3.0973774656217756e-009 -22.807525176726557 ;
	setAttr ".cbx" -type "double3" 23.54741318710003 4.7106922895887715 23.892725448883795 ;
createNode polyCut -n "shield_handle:Shield_Of_The_Round:polyCut4";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 5 "f[0:39]" "f[60:139]" "f[212]" "f[252]" "f[260:347]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0.1972945500150729 1.0193518431166035 0.54260490445020082 1;
	setAttr ".pc" -type "double3" -4.371054314164966 1.8448970913887024 -5.8139266120446882 ;
	setAttr ".ro" -type "double3" -180 -90 0 ;
	setAttr ".ps" -type "double2" 46.70024299621582 5.7315906286239624 ;
createNode polyCut -n "shield_handle:Shield_Of_The_Round:polyCut3";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 5 "f[0:39]" "f[60:139]" "f[212]" "f[252]" "f[260:315]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0.1972945500150729 1.0193518431166035 0.54260490445020082 1;
	setAttr ".pc" -type "double3" 4.8803033604754473 1.8448970913887024 -5.474427247837701 ;
	setAttr ".ro" -type "double3" -180 -90 0 ;
	setAttr ".ps" -type "double2" 46.70024299621582 5.7315906286239624 ;
createNode polyCut -n "shield_handle:Shield_Of_The_Round:polyCut2";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 5 "f[0:39]" "f[60:139]" "f[212]" "f[252]" "f[260:287]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0.1972945500150729 1.0193518431166035 0.54260490445020082 1;
	setAttr ".pc" -type "double3" -5.9836762941481583 1.8448970913887024 5.1349278836306684 ;
	setAttr ".ro" -type "double3" -180 0 0 ;
	setAttr ".ps" -type "double2" 46.70024299621582 5.7315906286239624 ;
createNode polyCut -n "shield_handle:Shield_Of_The_Round:polyCut1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 4 "f[0:39]" "f[60:139]" "f[212]" "f[252]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0.1972945500150729 1.0193518431166035 0.54260490445020082 1;
	setAttr ".pc" -type "double3" -6.0685511351999049 1.8448970913887024 -3.8618052678545078 ;
	setAttr ".ro" -type "double3" -180 0 0 ;
	setAttr ".ps" -type "double2" 46.70024299621582 5.7315906286239624 ;
createNode polyExtrudeFace -n "shield_handle:Shield_Of_The_Round:polyExtrudeFace2";
	setAttr ".ics" -type "componentList" 1 "f[140:179]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0.1972945500150729 1.0193518431166035 0.54260490445020082 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.19729169 1.0193518 0.54260015 ;
	setAttr ".rs" 52454;
	setAttr ".lt" -type "double3" 0 -2.2668493199466149e-016 1.0208981752616637 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -25.968820257113833 3.0973774656217756e-009 -25.623515624724604 ;
	setAttr ".cbx" -type "double3" 26.363403635098081 2.0387036831358296 26.708715896881841 ;
createNode polyExtrudeFace -n "shield_handle:Shield_Of_The_Round:polyExtrudeFace1";
	setAttr ".ics" -type "componentList" 1 "f[40:59]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0.1972945500150729 1.0193518431166035 0.54260490445020082 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.19729169 1.0193518 0.54260015 ;
	setAttr ".rs" 65107;
	setAttr ".lt" -type "double3" -2.2204460492503131e-015 -1.2711336046495091e-016 
		2.8510911524292526 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -23.152829809115783 3.0973774656217756e-009 -22.807525176726557 ;
	setAttr ".cbx" -type "double3" 23.54741318710003 2.0387036831358296 23.892725448883795 ;
createNode polyTweak -n "shield_handle:Shield_Of_The_Round:polyTweak1";
	setAttr ".uopa" yes;
	setAttr -s 82 ".tk";
	setAttr ".tk[0]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[1]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[2]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[3]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[4]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[5]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[6]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[7]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[8]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[9]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[10]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[11]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[12]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[13]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[14]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[15]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[16]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[17]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[18]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[19]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[20]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[21]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[22]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[23]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[24]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[25]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[26]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[27]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[28]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[29]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[30]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[31]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[32]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[33]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[34]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[35]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[36]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[37]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[38]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[39]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[80]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[81]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[82]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[83]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[84]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[85]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[86]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[87]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[88]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[89]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[90]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[91]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[92]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[93]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[94]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[95]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[96]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[97]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[98]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[99]" -type "float3" 0 1.2390461 0 ;
	setAttr ".tk[100]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[101]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[102]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[103]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[104]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[105]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[106]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[107]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[108]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[109]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[110]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[111]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[112]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[113]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[114]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[115]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[116]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[117]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[118]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[119]" -type "float3" 0 2.1586959 0 ;
	setAttr ".tk[120]" -type "float3" 0 2.6719887 0 ;
	setAttr ".tk[121]" -type "float3" 0 2.6719887 0 ;
createNode polyCylinder -n "shield_handle:Shield_Of_The_Round:polyCylinder1";
	setAttr ".r" 23.350118445614669;
	setAttr ".h" 2.0387036862332071;
	setAttr ".sc" 3;
	setAttr ".cuv" 3;
createNode groupId -n "shield_handle:Shield_Of_The_Round:groupId2";
	setAttr ".ihi" 0;
createNode groupId -n "shield_handle:Shield_Of_The_Round:groupId3";
	setAttr ".ihi" 0;
createNode groupParts -n "shield_handle:Shield_Of_The_Round:groupParts2";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[0:30]";
createNode polyExtrudeFace -n "shield_handle:Shield_Of_The_Round:polyExtrudeFace4";
	setAttr ".ics" -type "componentList" 1 "f[0]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 69.669456 0 13.18455 ;
	setAttr ".rs" 46281;
	setAttr ".lt" -type "double3" 0 -4.4011096148049298e-017 7.8017916437874781 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" 59.338607788085938 0 10.061270713806152 ;
	setAttr ".cbx" -type "double3" 80.000297546386719 0 16.307828903198242 ;
createNode polyTweak -n "shield_handle:Shield_Of_The_Round:polyTweak3";
	setAttr ".uopa" yes;
	setAttr -s 4 ".tk";
	setAttr ".tk[16]" -type "float3" 0 0 -0.10491524 ;
	setAttr ".tk[24]" -type "float3" 0.31474575 0 -0.28851694 ;
	setAttr ".tk[27]" -type "float3" -0.078686431 0 0.31474572 ;
	setAttr ".tk[28]" -type "float3" -0.18360168 0 0 ;
createNode polyCreateFace -n "shield_handle:Shield_Of_The_Round:polyCreateFace1";
	setAttr -s 29 ".v[0:28]" -type "float3"  80.000298 0 15.106567 78.799034 
		0 15.106567 77.437607 0 14.786231 76.476601 0 14.225643 75.595673 0 13.18455 74.074081 
		0 11.743036 72.552483 0 10.461692 70.390213 0 10.141356 67.26693 0 10.061271 65.585167 
		0 10.782028 64.063568 0 12.223541 63.102558 0 13.504887 62.461887 0 14.385811 61.260628 
		0 14.9464 59.338608 0 15.266736 59.418694 0 16.147661 61.260628 0 16.227745 63.26273 
		0 15.426904 64.143654 0 14.545979 65.184746 0 13.264634 66.145752 0 12.383709 67.427101 
		0 11.743036 70.230042 0 11.743036 72.472397 0 12.143457 73.273239 0 13.344718 74.714752 
		0 14.225643 75.755844 0 15.34682 77.517693 0 15.74724 80.160469 0 16.307829;
	setAttr ".l[0]"  29;
	setAttr ".tx" 1;
createNode groupId -n "shield_handle:Shield_Of_The_Round:groupId4";
	setAttr ".ihi" 0;
createNode groupId -n "shield_handle:Shield_Of_The_Round:groupId5";
	setAttr ".ihi" 0;
createNode groupId -n "shield_handle:groupId2";
	setAttr ".ihi" 0;
createNode groupId -n "shield_handle:groupId7";
	setAttr ".ihi" 0;
createNode groupId -n "shield_handle:groupId10";
	setAttr ".ihi" 0;
createNode script -n "uiConfigurationScriptNode";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"top\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n"
		+ "                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n"
		+ "                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n"
		+ "                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n"
		+ "            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n"
		+ "            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n"
		+ "            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -shadows 0\n            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"side\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n"
		+ "                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n"
		+ "                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n"
		+ "                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n"
		+ "            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n"
		+ "            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n"
		+ "            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -shadows 0\n            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"front\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n"
		+ "                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n"
		+ "                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n"
		+ "                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n"
		+ "            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n"
		+ "            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n"
		+ "            -motionTrails 1\n            -clipGhosts 1\n            -shadows 0\n            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"persp\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n"
		+ "                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 1\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n"
		+ "                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n"
		+ "                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n"
		+ "            -activeComponentsXray 0\n            -displayTextures 1\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n"
		+ "            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -shadows 0\n            $editorName;\n"
		+ "modelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            outlinerEditor -e \n                -showShapes 0\n                -showReferenceNodes 1\n                -showReferenceMembers 1\n                -showAttributes 0\n                -showConnected 0\n                -showAnimCurvesOnly 0\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 1\n                -showAssets 1\n                -showContainedOnly 1\n                -showPublishedAsConnected 0\n                -showContainerContents 1\n                -ignoreDagHierarchy 0\n"
		+ "                -expandConnections 0\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 0\n                -highlightActive 1\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"defaultSetFilter\" \n                -showSetMembers 1\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n"
		+ "                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showReferenceNodes 1\n            -showReferenceMembers 1\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n"
		+ "            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n"
		+ "\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"graphEditor\" -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n"
		+ "                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n"
		+ "                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n"
		+ "                -classicMode 1\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n"
		+ "                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n"
		+ "                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dopeSheetPanel\" -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n"
		+ "                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n"
		+ "                -showPinIcons 0\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n"
		+ "                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n"
		+ "                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n"
		+ "                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"clipEditorPanel\" -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n"
		+ "\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"sequenceEditorPanel\" -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n"
		+ "                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperGraphPanel\" -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n"
		+ "                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n"
		+ "                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperShadePanel\" -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"visorPanel\" -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"nodeEditorPanel\" -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -ignoreAssets 1\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n"
		+ "                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -island 0\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -syncedSelection 1\n                -extendToShapes 1\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -ignoreAssets 1\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n"
		+ "                -island 0\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -syncedSelection 1\n                -extendToShapes 1\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"createNodePanel\" -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Texture Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"polyTexturePlacementPanel\" -l (localizedPanelLabel(\"UV Texture Editor\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Texture Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"renderWindowPanel\" -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"blendShapePanel\" (localizedPanelLabel(\"Blend Shape\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\tblendShapePanel -unParent -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels ;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\tblendShapePanel -edit -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynRelEdPanel\" -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"relationshipPanel\" -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"referenceEditorPanel\" -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"componentEditorPanel\" -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynPaintScriptedPanelType\" -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"scriptEditorPanel\" -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-defaultImage \"\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"single\\\" -ps 1 100 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 1\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -maxConstantTransparency 1\\n    -rendererName \\\"base_OpenGL_Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -shadows 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 1\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -maxConstantTransparency 1\\n    -rendererName \\\"base_OpenGL_Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -shadows 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        setFocus `paneLayout -q -p1 $gMainPane`;\n        sceneUIReplacement -deleteRemaining;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 5 -size 56 -divisions 2 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 24 -ast 1 -aet 48 ";
	setAttr ".st" 6;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :renderPartition;
	setAttr -s 3 ".st";
select -ne :initialShadingGroup;
	setAttr -s 14 ".dsm";
	setAttr ".ro" yes;
	setAttr -s 12 ".gn";
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultShaderList1;
	setAttr -s 3 ".s";
select -ne :defaultTextureList1;
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderUtilityList1;
select -ne :defaultRenderingList1;
select -ne :renderGlobalsList1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :defaultHardwareRenderGlobals;
	setAttr ".fn" -type "string" "im";
	setAttr ".res" -type "string" "ntsc_4d 646 485 1.333";
select -ne :ikSystem;
	setAttr -s 4 ".sol";
connectAttr "polySplit14.out" "polySurfaceShape1.i";
connectAttr "polyExtrudeFace4.out" "pCubeShape1.i";
connectAttr "shield_handle:groupId3.id" "shield_handle:pCubeShape1.iog.og[0].gid"
		;
connectAttr ":initialShadingGroup.mwc" "shield_handle:pCubeShape1.iog.og[0].gco"
		;
connectAttr "shield_handle:groupParts3.og" "shield_handle:pCubeShape1.i";
connectAttr "shield_handle:groupId4.id" "shield_handle:pCubeShape1.ciog.cog[0].cgid"
		;
connectAttr "shield_handle:groupId5.id" "shield_handle:polySurfaceShape1.iog.og[0].gid"
		;
connectAttr ":initialShadingGroup.mwc" "shield_handle:polySurfaceShape1.iog.og[0].gco"
		;
connectAttr "shield_handle:groupParts4.og" "shield_handle:polySurfaceShape1.i";
connectAttr "shield_handle:groupId6.id" "shield_handle:polySurfaceShape1.ciog.cog[0].cgid"
		;
connectAttr ":initialShadingGroup.mwc" "shield_handle:Shield_Of_The_Round:pCylinderShape1.iog.og[0].gco"
		;
connectAttr "shield_handle:Shield_Of_The_Round:groupId1.id" "shield_handle:Shield_Of_The_Round:pCylinderShape1.iog.og[0].gid"
		;
connectAttr "shield_handle:Shield_Of_The_Round:groupParts1.og" "shield_handle:Shield_Of_The_Round:pCylinderShape1.i"
		;
connectAttr "shield_handle:Shield_Of_The_Round:groupId2.id" "shield_handle:Shield_Of_The_Round:pCylinderShape1.ciog.cog[0].cgid"
		;
connectAttr "shield_handle:Shield_Of_The_Round:groupId3.id" "shield_handle:Shield_Of_The_Round:polySurfaceShape1.iog.og[0].gid"
		;
connectAttr ":initialShadingGroup.mwc" "shield_handle:Shield_Of_The_Round:polySurfaceShape1.iog.og[0].gco"
		;
connectAttr "shield_handle:Shield_Of_The_Round:groupParts2.og" "shield_handle:Shield_Of_The_Round:polySurfaceShape1.i"
		;
connectAttr "shield_handle:Shield_Of_The_Round:groupId4.id" "shield_handle:Shield_Of_The_Round:polySurfaceShape1.ciog.cog[0].cgid"
		;
connectAttr "shield_handle:groupParts2.og" "shield_handle:polySurfaceShape3.i";
connectAttr "shield_handle:groupId2.id" "shield_handle:polySurfaceShape3.iog.og[0].gid"
		;
connectAttr ":initialShadingGroup.mwc" "shield_handle:polySurfaceShape3.iog.og[0].gco"
		;
connectAttr "shield_handle:Shield_Of_The_Round:groupParts3.og" "shield_handle:Shield_Of_The_Round:polySurfaceShape2.i"
		;
connectAttr "shield_handle:Shield_Of_The_Round:groupId5.id" "shield_handle:Shield_Of_The_Round:polySurfaceShape2.iog.og[0].gid"
		;
connectAttr ":initialShadingGroup.mwc" "shield_handle:Shield_Of_The_Round:polySurfaceShape2.iog.og[0].gco"
		;
connectAttr "shield_handle:groupParts8.og" "shield_handle:polySurfaceShape7.i";
connectAttr "shield_handle:groupId10.id" "shield_handle:polySurfaceShape7.iog.og[0].gid"
		;
connectAttr ":initialShadingGroup.mwc" "shield_handle:polySurfaceShape7.iog.og[0].gco"
		;
connectAttr "shield_handle:groupParts5.og" "shield_handle:polySurfaceShape4.i";
connectAttr "shield_handle:groupId7.id" "shield_handle:polySurfaceShape4.iog.og[0].gid"
		;
connectAttr ":initialShadingGroup.mwc" "shield_handle:polySurfaceShape4.iog.og[0].gco"
		;
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "lambert2SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "lambert2SG.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "file1.oc" "lambert2.c";
connectAttr "lambert2.oc" "lambert2SG.ss";
connectAttr "lambert2SG.msg" "materialInfo1.sg";
connectAttr "lambert2.msg" "materialInfo1.m";
connectAttr "file1.msg" "materialInfo1.t" -na;
connectAttr "place2dTexture1.c" "file1.c";
connectAttr "place2dTexture1.tf" "file1.tf";
connectAttr "place2dTexture1.rf" "file1.rf";
connectAttr "place2dTexture1.mu" "file1.mu";
connectAttr "place2dTexture1.mv" "file1.mv";
connectAttr "place2dTexture1.s" "file1.s";
connectAttr "place2dTexture1.wu" "file1.wu";
connectAttr "place2dTexture1.wv" "file1.wv";
connectAttr "place2dTexture1.re" "file1.re";
connectAttr "place2dTexture1.of" "file1.of";
connectAttr "place2dTexture1.r" "file1.ro";
connectAttr "place2dTexture1.n" "file1.n";
connectAttr "place2dTexture1.vt1" "file1.vt1";
connectAttr "place2dTexture1.vt2" "file1.vt2";
connectAttr "place2dTexture1.vt3" "file1.vt3";
connectAttr "place2dTexture1.vc1" "file1.vc1";
connectAttr "place2dTexture1.o" "file1.uv";
connectAttr "place2dTexture1.ofs" "file1.fs";
connectAttr "polyCreateFace1.out" "polyMirror1.ip";
connectAttr "polySurfaceShape1.wm" "polyMirror1.mp";
connectAttr "polyMirror1.out" "polyMergeVert1.ip";
connectAttr "polySurfaceShape1.wm" "polyMergeVert1.mp";
connectAttr "polyMergeVert1.out" "polyMergeVert2.ip";
connectAttr "polySurfaceShape1.wm" "polyMergeVert2.mp";
connectAttr "polyMergeVert2.out" "deleteComponent1.ig";
connectAttr "deleteComponent1.og" "polyExtrudeFace1.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace1.mp";
connectAttr "polyTweak1.out" "polyMergeVert3.ip";
connectAttr "polySurfaceShape1.wm" "polyMergeVert3.mp";
connectAttr "polyExtrudeFace1.out" "polyTweak1.ip";
connectAttr "polyMergeVert3.out" "polySplit1.ip";
connectAttr "polySplit1.out" "polySplit2.ip";
connectAttr "polySplit2.out" "polySplit3.ip";
connectAttr "polySplit3.out" "polySplit4.ip";
connectAttr "polySplit4.out" "polySplit5.ip";
connectAttr "polySplit5.out" "polySplit6.ip";
connectAttr "polySplit6.out" "polySplit7.ip";
connectAttr "polySplit7.out" "polySplit8.ip";
connectAttr "polySplit8.out" "polySplit9.ip";
connectAttr "polySplit9.out" "polySplit10.ip";
connectAttr "polySplit10.out" "polyExtrudeFace2.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace2.mp";
connectAttr "polyExtrudeFace2.out" "polyExtrudeFace3.ip";
connectAttr "polySurfaceShape1.wm" "polyExtrudeFace3.mp";
connectAttr "polyExtrudeFace3.out" "polySplit11.ip";
connectAttr "polySplit11.out" "polySplit12.ip";
connectAttr "polySplit12.out" "polySplit13.ip";
connectAttr "polySplit13.out" "polySplit14.ip";
connectAttr "polyTweak2.out" "polySplitRing1.ip";
connectAttr "pCubeShape1.wm" "polySplitRing1.mp";
connectAttr "polyCube1.out" "polyTweak2.ip";
connectAttr "polySplitRing1.out" "polySplitRing2.ip";
connectAttr "pCubeShape1.wm" "polySplitRing2.mp";
connectAttr "polySplitRing2.out" "polyExtrudeFace4.ip";
connectAttr "pCubeShape1.wm" "polyExtrudeFace4.mp";
connectAttr "shield_handle:polySeparate2.out[2]" "shield_handle:groupParts8.ig";
connectAttr "shield_handle:groupId10.id" "shield_handle:groupParts8.gi";
connectAttr "shield_handle:polySurfaceShape4.o" "shield_handle:polySeparate2.ip"
		;
connectAttr "shield_handle:polyUnite1.out" "shield_handle:groupParts5.ig";
connectAttr "shield_handle:groupId7.id" "shield_handle:groupParts5.gi";
connectAttr "shield_handle:pCubeShape1.o" "shield_handle:polyUnite1.ip[0]";
connectAttr "shield_handle:polySurfaceShape1.o" "shield_handle:polyUnite1.ip[1]"
		;
connectAttr "shield_handle:polySurfaceShape3.o" "shield_handle:polyUnite1.ip[2]"
		;
connectAttr "shield_handle:pCubeShape1.wm" "shield_handle:polyUnite1.im[0]";
connectAttr "shield_handle:polySurfaceShape1.wm" "shield_handle:polyUnite1.im[1]"
		;
connectAttr "shield_handle:polySurfaceShape3.wm" "shield_handle:polyUnite1.im[2]"
		;
connectAttr "shield_handle:polySplit5.out" "shield_handle:groupParts3.ig";
connectAttr "shield_handle:groupId3.id" "shield_handle:groupParts3.gi";
connectAttr "shield_handle:polySplit4.out" "shield_handle:polySplit5.ip";
connectAttr "shield_handle:polySplit3.out" "shield_handle:polySplit4.ip";
connectAttr "shield_handle:polyTweak6.out" "shield_handle:polySplit3.ip";
connectAttr "shield_handle:polySoftEdge1.out" "shield_handle:polyTweak6.ip";
connectAttr "shield_handle:polyTweak3.out" "shield_handle:polySoftEdge1.ip";
connectAttr "shield_handle:pCubeShape1.wm" "shield_handle:polySoftEdge1.mp";
connectAttr "shield_handle:polyExtrudeFace2.out" "shield_handle:polyTweak3.ip";
connectAttr "shield_handle:polyExtrudeFace1.out" "shield_handle:polyExtrudeFace2.ip"
		;
connectAttr "shield_handle:pCubeShape1.wm" "shield_handle:polyExtrudeFace2.mp";
connectAttr "shield_handle:polyTweak2.out" "shield_handle:polyExtrudeFace1.ip";
connectAttr "shield_handle:pCubeShape1.wm" "shield_handle:polyExtrudeFace1.mp";
connectAttr "shield_handle:polySplit2.out" "shield_handle:polyTweak2.ip";
connectAttr "shield_handle:polySplit1.out" "shield_handle:polySplit2.ip";
connectAttr "shield_handle:polyTweak1.out" "shield_handle:polySplit1.ip";
connectAttr "shield_handle:polyCube1.out" "shield_handle:polyTweak1.ip";
connectAttr "shield_handle:polyBevel1.out" "shield_handle:groupParts4.ig";
connectAttr "shield_handle:groupId5.id" "shield_handle:groupParts4.gi";
connectAttr "shield_handle:polyExtrudeFace3.out" "shield_handle:polyBevel1.ip";
connectAttr "shield_handle:polySurfaceShape1.wm" "shield_handle:polyBevel1.mp";
connectAttr "shield_handle:deleteComponent1.og" "shield_handle:polyExtrudeFace3.ip"
		;
connectAttr "shield_handle:polySurfaceShape1.wm" "shield_handle:polyExtrudeFace3.mp"
		;
connectAttr "shield_handle:polyMergeVert2.out" "shield_handle:deleteComponent1.ig"
		;
connectAttr "shield_handle:polyMergeVert1.out" "shield_handle:polyMergeVert2.ip"
		;
connectAttr "shield_handle:polySurfaceShape1.wm" "shield_handle:polyMergeVert2.mp"
		;
connectAttr "shield_handle:polyTweak5.out" "shield_handle:polyMergeVert1.ip";
connectAttr "shield_handle:polySurfaceShape1.wm" "shield_handle:polyMergeVert1.mp"
		;
connectAttr "shield_handle:polyMirror1.out" "shield_handle:polyTweak5.ip";
connectAttr "shield_handle:polyTweak4.out" "shield_handle:polyMirror1.ip";
connectAttr "shield_handle:polySurfaceShape1.wm" "shield_handle:polyMirror1.mp";
connectAttr "shield_handle:polyCreateFace1.out" "shield_handle:polyTweak4.ip";
connectAttr "shield_handle:polySeparate1.out[1]" "shield_handle:groupParts2.ig";
connectAttr "shield_handle:groupId2.id" "shield_handle:groupParts2.gi";
connectAttr "shield_handle:Shield_Of_The_Round:polySurfaceShape2.o" "shield_handle:polySeparate1.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyUnite1.out" "shield_handle:Shield_Of_The_Round:groupParts3.ig"
		;
connectAttr "shield_handle:Shield_Of_The_Round:groupId5.id" "shield_handle:Shield_Of_The_Round:groupParts3.gi"
		;
connectAttr "shield_handle:Shield_Of_The_Round:pCylinderShape1.o" "shield_handle:Shield_Of_The_Round:polyUnite1.ip[0]"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySurfaceShape1.o" "shield_handle:Shield_Of_The_Round:polyUnite1.ip[1]"
		;
connectAttr "shield_handle:Shield_Of_The_Round:pCylinderShape1.wm" "shield_handle:Shield_Of_The_Round:polyUnite1.im[0]"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySurfaceShape1.wm" "shield_handle:Shield_Of_The_Round:polyUnite1.im[1]"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit26.out" "shield_handle:Shield_Of_The_Round:groupParts1.ig"
		;
connectAttr "shield_handle:Shield_Of_The_Round:groupId1.id" "shield_handle:Shield_Of_The_Round:groupParts1.gi"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit25.out" "shield_handle:Shield_Of_The_Round:polySplit26.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit24.out" "shield_handle:Shield_Of_The_Round:polySplit25.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit23.out" "shield_handle:Shield_Of_The_Round:polySplit24.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:deleteComponent4.og" "shield_handle:Shield_Of_The_Round:polySplit23.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyTweak2.out" "shield_handle:Shield_Of_The_Round:deleteComponent4.ig"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit22.out" "shield_handle:Shield_Of_The_Round:polyTweak2.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit21.out" "shield_handle:Shield_Of_The_Round:polySplit22.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit20.out" "shield_handle:Shield_Of_The_Round:polySplit21.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit19.out" "shield_handle:Shield_Of_The_Round:polySplit20.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit18.out" "shield_handle:Shield_Of_The_Round:polySplit19.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit17.out" "shield_handle:Shield_Of_The_Round:polySplit18.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit16.out" "shield_handle:Shield_Of_The_Round:polySplit17.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit15.out" "shield_handle:Shield_Of_The_Round:polySplit16.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit14.out" "shield_handle:Shield_Of_The_Round:polySplit15.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit13.out" "shield_handle:Shield_Of_The_Round:polySplit14.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit12.out" "shield_handle:Shield_Of_The_Round:polySplit13.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit11.out" "shield_handle:Shield_Of_The_Round:polySplit12.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit10.out" "shield_handle:Shield_Of_The_Round:polySplit11.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit9.out" "shield_handle:Shield_Of_The_Round:polySplit10.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit8.out" "shield_handle:Shield_Of_The_Round:polySplit9.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit7.out" "shield_handle:Shield_Of_The_Round:polySplit8.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit6.out" "shield_handle:Shield_Of_The_Round:polySplit7.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit5.out" "shield_handle:Shield_Of_The_Round:polySplit6.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit4.out" "shield_handle:Shield_Of_The_Round:polySplit5.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit3.out" "shield_handle:Shield_Of_The_Round:polySplit4.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit2.out" "shield_handle:Shield_Of_The_Round:polySplit3.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySplit1.out" "shield_handle:Shield_Of_The_Round:polySplit2.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:deleteComponent3.og" "shield_handle:Shield_Of_The_Round:polySplit1.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:deleteComponent2.og" "shield_handle:Shield_Of_The_Round:deleteComponent3.ig"
		;
connectAttr "shield_handle:Shield_Of_The_Round:deleteComponent1.og" "shield_handle:Shield_Of_The_Round:deleteComponent2.ig"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyExtrudeFace3.out" "shield_handle:Shield_Of_The_Round:deleteComponent1.ig"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyCut4.out" "shield_handle:Shield_Of_The_Round:polyExtrudeFace3.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:pCylinderShape1.wm" "shield_handle:Shield_Of_The_Round:polyExtrudeFace3.mp"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyCut3.out" "shield_handle:Shield_Of_The_Round:polyCut4.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:pCylinderShape1.wm" "shield_handle:Shield_Of_The_Round:polyCut4.mp"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyCut2.out" "shield_handle:Shield_Of_The_Round:polyCut3.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:pCylinderShape1.wm" "shield_handle:Shield_Of_The_Round:polyCut3.mp"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyCut1.out" "shield_handle:Shield_Of_The_Round:polyCut2.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:pCylinderShape1.wm" "shield_handle:Shield_Of_The_Round:polyCut2.mp"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyExtrudeFace2.out" "shield_handle:Shield_Of_The_Round:polyCut1.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:pCylinderShape1.wm" "shield_handle:Shield_Of_The_Round:polyCut1.mp"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyExtrudeFace1.out" "shield_handle:Shield_Of_The_Round:polyExtrudeFace2.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:pCylinderShape1.wm" "shield_handle:Shield_Of_The_Round:polyExtrudeFace2.mp"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyTweak1.out" "shield_handle:Shield_Of_The_Round:polyExtrudeFace1.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:pCylinderShape1.wm" "shield_handle:Shield_Of_The_Round:polyExtrudeFace1.mp"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyCylinder1.out" "shield_handle:Shield_Of_The_Round:polyTweak1.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyExtrudeFace4.out" "shield_handle:Shield_Of_The_Round:groupParts2.ig"
		;
connectAttr "shield_handle:Shield_Of_The_Round:groupId3.id" "shield_handle:Shield_Of_The_Round:groupParts2.gi"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyTweak3.out" "shield_handle:Shield_Of_The_Round:polyExtrudeFace4.ip"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polySurfaceShape1.wm" "shield_handle:Shield_Of_The_Round:polyExtrudeFace4.mp"
		;
connectAttr "shield_handle:Shield_Of_The_Round:polyCreateFace1.out" "shield_handle:Shield_Of_The_Round:polyTweak3.ip"
		;
connectAttr "lambert2SG.pa" ":renderPartition.st" -na;
connectAttr "polySurfaceShape1.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape1.iog" ":initialShadingGroup.dsm" -na;
connectAttr "shield_handle:Shield_Of_The_Round:pCylinderShape1.iog.og[0]" ":initialShadingGroup.dsm"
		 -na;
connectAttr "shield_handle:Shield_Of_The_Round:pCylinderShape1.ciog.cog[0]" ":initialShadingGroup.dsm"
		 -na;
connectAttr "shield_handle:Shield_Of_The_Round:polySurfaceShape1.iog.og[0]" ":initialShadingGroup.dsm"
		 -na;
connectAttr "shield_handle:Shield_Of_The_Round:polySurfaceShape1.ciog.cog[0]" ":initialShadingGroup.dsm"
		 -na;
connectAttr "shield_handle:Shield_Of_The_Round:polySurfaceShape2.iog.og[0]" ":initialShadingGroup.dsm"
		 -na;
connectAttr "shield_handle:polySurfaceShape3.iog.og[0]" ":initialShadingGroup.dsm"
		 -na;
connectAttr "shield_handle:pCubeShape1.iog.og[0]" ":initialShadingGroup.dsm" -na
		;
connectAttr "shield_handle:pCubeShape1.ciog.cog[0]" ":initialShadingGroup.dsm" -na
		;
connectAttr "shield_handle:polySurfaceShape1.iog.og[0]" ":initialShadingGroup.dsm"
		 -na;
connectAttr "shield_handle:polySurfaceShape1.ciog.cog[0]" ":initialShadingGroup.dsm"
		 -na;
connectAttr "shield_handle:polySurfaceShape4.iog.og[0]" ":initialShadingGroup.dsm"
		 -na;
connectAttr "shield_handle:polySurfaceShape7.iog.og[0]" ":initialShadingGroup.dsm"
		 -na;
connectAttr "shield_handle:Shield_Of_The_Round:groupId1.msg" ":initialShadingGroup.gn"
		 -na;
connectAttr "shield_handle:Shield_Of_The_Round:groupId2.msg" ":initialShadingGroup.gn"
		 -na;
connectAttr "shield_handle:Shield_Of_The_Round:groupId3.msg" ":initialShadingGroup.gn"
		 -na;
connectAttr "shield_handle:Shield_Of_The_Round:groupId4.msg" ":initialShadingGroup.gn"
		 -na;
connectAttr "shield_handle:Shield_Of_The_Round:groupId5.msg" ":initialShadingGroup.gn"
		 -na;
connectAttr "shield_handle:groupId2.msg" ":initialShadingGroup.gn" -na;
connectAttr "shield_handle:groupId3.msg" ":initialShadingGroup.gn" -na;
connectAttr "shield_handle:groupId4.msg" ":initialShadingGroup.gn" -na;
connectAttr "shield_handle:groupId5.msg" ":initialShadingGroup.gn" -na;
connectAttr "shield_handle:groupId6.msg" ":initialShadingGroup.gn" -na;
connectAttr "shield_handle:groupId7.msg" ":initialShadingGroup.gn" -na;
connectAttr "shield_handle:groupId10.msg" ":initialShadingGroup.gn" -na;
connectAttr "lambert2.msg" ":defaultShaderList1.s" -na;
connectAttr "file1.msg" ":defaultTextureList1.tx" -na;
connectAttr "place2dTexture1.msg" ":defaultRenderUtilityList1.u" -na;
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
// End of Trident Buckler.ma
