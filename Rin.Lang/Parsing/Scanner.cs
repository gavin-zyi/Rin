
#line 1 "Scanner.rl"

#line 366 "Scanner.rl"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Parsing
{
    public class Scanner
    {

#line 13 "Scanner.cs"
static readonly sbyte[] _scan_actions =  new sbyte [] {
	0, 1, 2, 1, 3, 1, 26, 1, 
	27, 1, 28, 1, 29, 1, 30, 1, 
	31, 1, 32, 1, 33, 1, 34, 1, 
	35, 1, 36, 1, 37, 1, 38, 1, 
	39, 1, 40, 1, 41, 1, 42, 1, 
	43, 1, 44, 1, 45, 1, 46, 1, 
	47, 1, 48, 1, 49, 1, 50, 1, 
	51, 1, 52, 2, 0, 1, 2, 3, 
	4, 2, 3, 5, 2, 3, 6, 2, 
	3, 7, 2, 3, 8, 2, 3, 9, 
	2, 3, 10, 2, 3, 11, 2, 3, 
	12, 2, 3, 13, 2, 3, 14, 2, 
	3, 15, 2, 3, 16, 2, 3, 17, 
	2, 3, 18, 2, 3, 19, 2, 3, 
	20, 2, 3, 21, 2, 3, 22, 2, 
	3, 23, 2, 3, 24, 2, 3, 25
	
};

static readonly short[] _scan_key_offsets =  new short [] {
	0, 0, 4, 8, 10, 12, 14, 16, 
	18, 22, 26, 28, 30, 32, 34, 36, 
	40, 42, 44, 46, 48, 54, 56, 58, 
	60, 62, 64, 66, 68, 126, 128, 129, 
	130, 132, 134, 135, 137, 140, 144, 146, 
	157, 162, 164, 166, 172, 189, 210, 229, 
	248, 266, 287, 307, 325, 343, 361, 379, 
	397, 415, 433, 452, 470, 488, 508, 526, 
	544, 562, 580, 598, 616, 635, 653, 671, 
	689, 707, 725, 744, 762, 780, 798, 818, 
	836, 854, 872, 890, 909, 927, 945, 963, 
	981, 999, 1017, 1035, 1053, 1071
};

static readonly char[] _scan_trans_keys =  new char [] {
	'\u000a', '\u000d', '\u0022', '\u005c', '\u000a', '\u000d', '\u0022', '\u005c', 
	'\u0000', '\u007f', '\u0022', '\u005c', '\u0022', '\u005c', '\u0022', '\u005c', 
	'\u0000', '\u007f', '\u000a', '\u000d', '\u0027', '\u005c', '\u000a', '\u000d', 
	'\u0027', '\u005c', '\u0000', '\u007f', '\u0027', '\u005c', '\u0027', '\u005c', 
	'\u0027', '\u005c', '\u0000', '\u007f', '\u002b', '\u002d', '\u0030', '\u0039', 
	'\u0030', '\u0039', '\u0030', '\u0039', '\u0030', '\u0031', '\u0030', '\u0037', 
	'\u0030', '\u0039', '\u0041', '\u0046', '\u0061', '\u0066', '\u0080', '\u00bf', 
	'\u00a0', '\u00bf', '\u0080', '\u00bf', '\u0080', '\u009f', '\u0090', '\u00bf', 
	'\u0080', '\u00bf', '\u0080', '\u008f', '\u0009', '\u000a', '\u000d', '\u0020', 
	'\u0022', '\u0023', '\u0027', '\u0028', '\u0029', '\u002a', '\u002b', '\u002c', 
	'\u002d', '\u002e', '\u002f', '\u0030', '\u003a', '\u003b', '\u003c', '\u003d', 
	'\u003e', '\u0042', '\u0052', '\u0055', '\u005b', '\u005d', '\u005f', '\u0061', 
	'\u0062', '\u0063', '\u0065', '\u0066', '\u0069', '\u006c', '\u006e', '\u006f', 
	'\u0070', '\u0072', '\u0074', '\u0075', '\u0076', '\u0077', '\u00e0', '\u00ed', 
	'\u00f0', '\u00f4', '\u0031', '\u0039', '\u0041', '\u005a', '\u0064', '\u007a', 
	'\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u0009', '\u0020', 
	'\u000a', '\u0022', '\u0022', '\u005c', '\u000a', '\u000d', '\u0027', '\u0027', 
	'\u005c', '\u002e', '\u0030', '\u0039', '\u0045', '\u0065', '\u0030', '\u0039', 
	'\u0030', '\u0039', '\u002e', '\u0042', '\u0045', '\u004f', '\u0058', '\u0062', 
	'\u0065', '\u006f', '\u0078', '\u0030', '\u0039', '\u002e', '\u0045', '\u0065', 
	'\u0030', '\u0039', '\u0030', '\u0031', '\u0030', '\u0037', '\u0030', '\u0039', 
	'\u0041', '\u0046', '\u0061', '\u0066', '\u005f', '\u00e0', '\u00ed', '\u00f0', 
	'\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', 
	'\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u0022', '\u0027', '\u0052', 
	'\u005f', '\u0072', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', 
	'\u00f1', '\u00f3', '\u0022', '\u0027', '\u005f', '\u00e0', '\u00ed', '\u00f0', 
	'\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', 
	'\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u006e', '\u0073', 
	'\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', 
	'\u005f', '\u0064', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', 
	'\u00f1', '\u00f3', '\u0022', '\u0027', '\u0052', '\u005f', '\u0072', '\u00e0', 
	'\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', 
	'\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u0022', 
	'\u0027', '\u005f', '\u0065', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', 
	'\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0061', '\u00e0', '\u00ed', '\u00f0', 
	'\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0062', '\u007a', '\u00c2', 
	'\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u006b', '\u00e0', 
	'\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', 
	'\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', 
	'\u006c', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', 
	'\u00f3', '\u005f', '\u0061', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u0062', '\u007a', '\u00c2', '\u00df', '\u00e1', 
	'\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0073', '\u00e0', '\u00ed', '\u00f0', 
	'\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', 
	'\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0073', '\u00e0', 
	'\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', 
	'\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', 
	'\u006c', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', 
	'\u00f3', '\u005f', '\u0069', '\u0073', '\u00e0', '\u00ed', '\u00f0', '\u00f4', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', 
	'\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0066', '\u00e0', '\u00ed', 
	'\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0065', 
	'\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', 
	'\u005f', '\u0061', '\u006f', '\u0075', '\u00e0', '\u00ed', '\u00f0', '\u00f4', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u0062', '\u007a', '\u00c2', '\u00df', 
	'\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u006c', '\u00e0', '\u00ed', 
	'\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0073', 
	'\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', 
	'\u005f', '\u0065', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', 
	'\u00f1', '\u00f3', '\u005f', '\u0072', '\u00e0', '\u00ed', '\u00f0', '\u00f4', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', 
	'\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u006e', '\u00e0', '\u00ed', 
	'\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0063', 
	'\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', 
	'\u005f', '\u0066', '\u006e', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', 
	'\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0065', '\u00e0', '\u00ed', '\u00f0', 
	'\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', 
	'\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0074', '\u00e0', 
	'\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', 
	'\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', 
	'\u006f', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', 
	'\u00f3', '\u005f', '\u006e', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', 
	'\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0065', '\u00e0', '\u00ed', '\u00f0', 
	'\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', 
	'\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0066', '\u0072', 
	'\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', 
	'\u005f', '\u0061', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u0062', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', 
	'\u00f1', '\u00f3', '\u005f', '\u0073', '\u00e0', '\u00ed', '\u00f0', '\u00f4', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', 
	'\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0073', '\u00e0', '\u00ed', 
	'\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u0022', '\u0027', 
	'\u005f', '\u0065', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', 
	'\u00f1', '\u00f3', '\u005f', '\u0074', '\u00e0', '\u00ed', '\u00f0', '\u00f4', 
	'\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', 
	'\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0075', '\u00e0', '\u00ed', 
	'\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', 
	'\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0072', 
	'\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', 
	'\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', 
	'\u005f', '\u006e', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', 
	'\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', 
	'\u00f1', '\u00f3', '\u005f', '\u0068', '\u0072', '\u00e0', '\u00ed', '\u00f0', 
	'\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', 
	'\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0069', '\u00e0', 
	'\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', 
	'\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', 
	'\u0073', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', 
	'\u00f3', '\u005f', '\u0075', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', 
	'\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0065', '\u00e0', '\u00ed', '\u00f0', 
	'\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', 
	'\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0061', '\u00e0', 
	'\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0062', 
	'\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', 
	'\u0072', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', 
	'\u00f3', '\u005f', '\u0068', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', 
	'\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', 
	'\u00ef', '\u00f1', '\u00f3', '\u005f', '\u0069', '\u00e0', '\u00ed', '\u00f0', 
	'\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', '\u007a', '\u00c2', 
	'\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', '\u006c', '\u00e0', 
	'\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', '\u005a', '\u0061', 
	'\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', '\u00f3', '\u005f', 
	'\u0065', '\u00e0', '\u00ed', '\u00f0', '\u00f4', '\u0030', '\u0039', '\u0041', 
	'\u005a', '\u0061', '\u007a', '\u00c2', '\u00df', '\u00e1', '\u00ef', '\u00f1', 
	'\u00f3', (char) 0
};

static readonly sbyte[] _scan_single_lengths =  new sbyte [] {
	0, 4, 4, 0, 2, 2, 2, 0, 
	4, 4, 0, 2, 2, 2, 0, 2, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 46, 2, 1, 1, 
	2, 2, 1, 2, 1, 2, 0, 9, 
	3, 0, 0, 0, 5, 9, 7, 7, 
	6, 9, 8, 6, 6, 6, 6, 6, 
	6, 6, 7, 6, 6, 8, 6, 6, 
	6, 6, 6, 6, 7, 6, 6, 6, 
	6, 6, 7, 6, 6, 6, 8, 6, 
	6, 6, 6, 7, 6, 6, 6, 6, 
	6, 6, 6, 6, 6, 6
};

static readonly sbyte[] _scan_range_lengths =  new sbyte [] {
	0, 0, 0, 1, 0, 0, 0, 1, 
	0, 0, 1, 0, 0, 0, 1, 1, 
	1, 1, 1, 1, 3, 1, 1, 1, 
	1, 1, 1, 1, 6, 0, 0, 0, 
	0, 0, 0, 0, 1, 1, 1, 1, 
	1, 1, 1, 3, 6, 6, 6, 6, 
	6, 6, 6, 6, 6, 6, 6, 6, 
	6, 6, 6, 6, 6, 6, 6, 6, 
	6, 6, 6, 6, 6, 6, 6, 6, 
	6, 6, 6, 6, 6, 6, 6, 6, 
	6, 6, 6, 6, 6, 6, 6, 6, 
	6, 6, 6, 6, 6, 6
};

static readonly short[] _scan_index_offsets =  new short [] {
	0, 0, 5, 10, 12, 15, 18, 21, 
	23, 28, 33, 35, 38, 41, 44, 46, 
	50, 52, 54, 56, 58, 62, 64, 66, 
	68, 70, 72, 74, 76, 129, 132, 134, 
	136, 139, 142, 144, 147, 150, 154, 156, 
	167, 172, 174, 176, 180, 192, 208, 222, 
	236, 249, 265, 280, 293, 306, 319, 332, 
	345, 358, 371, 385, 398, 411, 426, 439, 
	452, 465, 478, 491, 504, 518, 531, 544, 
	557, 570, 583, 597, 610, 623, 636, 651, 
	664, 677, 690, 703, 717, 730, 743, 756, 
	769, 782, 795, 808, 821, 834
};

static readonly byte[] _scan_indicies =  new byte [] {
	0, 0, 2, 3, 1, 0, 0, 4, 
	3, 1, 1, 0, 7, 8, 6, 9, 
	8, 6, 10, 8, 6, 6, 5, 0, 
	0, 12, 13, 11, 0, 0, 4, 13, 
	11, 11, 0, 15, 16, 14, 17, 16, 
	14, 18, 16, 14, 14, 5, 20, 20, 
	21, 19, 21, 19, 22, 19, 23, 19, 
	24, 19, 25, 25, 25, 19, 26, 0, 
	27, 0, 27, 0, 27, 0, 28, 0, 
	28, 0, 28, 0, 29, 31, 32, 29, 
	33, 34, 35, 36, 37, 38, 39, 40, 
	41, 42, 43, 44, 46, 47, 48, 49, 
	50, 51, 52, 51, 53, 54, 26, 55, 
	56, 57, 58, 59, 60, 61, 62, 63, 
	64, 65, 66, 51, 67, 68, 69, 70, 
	71, 73, 45, 26, 26, 27, 28, 72, 
	30, 29, 29, 74, 31, 75, 6, 76, 
	10, 8, 6, 77, 77, 34, 14, 76, 
	18, 16, 14, 79, 22, 78, 81, 81, 
	22, 80, 21, 80, 82, 83, 81, 84, 
	85, 83, 81, 84, 85, 45, 80, 82, 
	81, 81, 45, 80, 23, 80, 24, 80, 
	25, 25, 25, 80, 26, 69, 70, 71, 
	73, 26, 26, 26, 27, 28, 72, 0, 
	33, 35, 52, 26, 52, 69, 70, 71, 
	73, 26, 26, 26, 27, 28, 72, 86, 
	33, 35, 26, 69, 70, 71, 73, 26, 
	26, 26, 27, 28, 72, 86, 26, 87, 
	88, 69, 70, 71, 73, 26, 26, 26, 
	27, 28, 72, 86, 26, 89, 69, 70, 
	71, 73, 26, 26, 26, 27, 28, 72, 
	86, 33, 35, 52, 26, 90, 69, 70, 
	71, 73, 26, 26, 26, 27, 28, 72, 
	86, 33, 35, 26, 91, 69, 70, 71, 
	73, 26, 26, 26, 27, 28, 72, 86, 
	26, 92, 69, 70, 71, 73, 26, 26, 
	26, 27, 28, 72, 86, 26, 93, 69, 
	70, 71, 73, 26, 26, 26, 27, 28, 
	72, 86, 26, 94, 69, 70, 71, 73, 
	26, 26, 26, 27, 28, 72, 86, 26, 
	95, 69, 70, 71, 73, 26, 26, 26, 
	27, 28, 72, 86, 26, 96, 69, 70, 
	71, 73, 26, 26, 26, 27, 28, 72, 
	86, 26, 97, 69, 70, 71, 73, 26, 
	26, 26, 27, 28, 72, 86, 26, 98, 
	69, 70, 71, 73, 26, 26, 26, 27, 
	28, 72, 86, 26, 99, 100, 69, 70, 
	71, 73, 26, 26, 26, 27, 28, 72, 
	86, 26, 101, 69, 70, 71, 73, 26, 
	26, 26, 27, 28, 72, 86, 26, 102, 
	69, 70, 71, 73, 26, 26, 26, 27, 
	28, 72, 86, 26, 103, 104, 105, 69, 
	70, 71, 73, 26, 26, 26, 27, 28, 
	72, 86, 26, 106, 69, 70, 71, 73, 
	26, 26, 26, 27, 28, 72, 86, 26, 
	107, 69, 70, 71, 73, 26, 26, 26, 
	27, 28, 72, 86, 26, 108, 69, 70, 
	71, 73, 26, 26, 26, 27, 28, 72, 
	86, 26, 109, 69, 70, 71, 73, 26, 
	26, 26, 27, 28, 72, 86, 26, 110, 
	69, 70, 71, 73, 26, 26, 26, 27, 
	28, 72, 86, 26, 111, 69, 70, 71, 
	73, 26, 26, 26, 27, 28, 72, 86, 
	26, 112, 113, 69, 70, 71, 73, 26, 
	26, 26, 27, 28, 72, 86, 26, 114, 
	69, 70, 71, 73, 26, 26, 26, 27, 
	28, 72, 86, 26, 115, 69, 70, 71, 
	73, 26, 26, 26, 27, 28, 72, 86, 
	26, 116, 69, 70, 71, 73, 26, 26, 
	26, 27, 28, 72, 86, 26, 117, 69, 
	70, 71, 73, 26, 26, 26, 27, 28, 
	72, 86, 26, 118, 69, 70, 71, 73, 
	26, 26, 26, 27, 28, 72, 86, 26, 
	119, 120, 69, 70, 71, 73, 26, 26, 
	26, 27, 28, 72, 86, 26, 121, 69, 
	70, 71, 73, 26, 26, 26, 27, 28, 
	72, 86, 26, 122, 69, 70, 71, 73, 
	26, 26, 26, 27, 28, 72, 86, 26, 
	123, 69, 70, 71, 73, 26, 26, 26, 
	27, 28, 72, 86, 33, 35, 26, 124, 
	69, 70, 71, 73, 26, 26, 26, 27, 
	28, 72, 86, 26, 125, 69, 70, 71, 
	73, 26, 26, 26, 27, 28, 72, 86, 
	26, 126, 69, 70, 71, 73, 26, 26, 
	26, 27, 28, 72, 86, 26, 127, 69, 
	70, 71, 73, 26, 26, 26, 27, 28, 
	72, 86, 26, 128, 69, 70, 71, 73, 
	26, 26, 26, 27, 28, 72, 86, 26, 
	129, 130, 69, 70, 71, 73, 26, 26, 
	26, 27, 28, 72, 86, 26, 131, 69, 
	70, 71, 73, 26, 26, 26, 27, 28, 
	72, 86, 26, 132, 69, 70, 71, 73, 
	26, 26, 26, 27, 28, 72, 86, 26, 
	133, 69, 70, 71, 73, 26, 26, 26, 
	27, 28, 72, 86, 26, 134, 69, 70, 
	71, 73, 26, 26, 26, 27, 28, 72, 
	86, 26, 135, 69, 70, 71, 73, 26, 
	26, 26, 27, 28, 72, 86, 26, 136, 
	69, 70, 71, 73, 26, 26, 26, 27, 
	28, 72, 86, 26, 137, 69, 70, 71, 
	73, 26, 26, 26, 27, 28, 72, 86, 
	26, 138, 69, 70, 71, 73, 26, 26, 
	26, 27, 28, 72, 86, 26, 139, 69, 
	70, 71, 73, 26, 26, 26, 27, 28, 
	72, 86, 26, 140, 69, 70, 71, 73, 
	26, 26, 26, 27, 28, 72, 86, 0
};

static readonly sbyte[] _scan_trans_targs =  new sbyte [] {
	28, 2, 31, 3, 28, 28, 4, 5, 
	7, 6, 32, 9, 34, 10, 11, 12, 
	14, 13, 35, 28, 16, 38, 37, 41, 
	42, 43, 44, 21, 23, 29, 0, 28, 
	30, 1, 33, 8, 28, 28, 28, 28, 
	28, 28, 36, 28, 39, 40, 28, 28, 
	28, 28, 28, 45, 46, 28, 28, 47, 
	49, 53, 57, 61, 68, 69, 71, 74, 
	75, 78, 83, 88, 90, 22, 24, 25, 
	26, 27, 28, 28, 28, 28, 28, 28, 
	28, 15, 17, 18, 19, 20, 28, 48, 
	44, 44, 50, 51, 52, 44, 54, 55, 
	56, 44, 58, 59, 60, 44, 44, 62, 
	65, 66, 63, 64, 44, 44, 67, 44, 
	44, 44, 70, 44, 72, 73, 44, 44, 
	44, 76, 77, 44, 79, 80, 81, 82, 
	44, 84, 86, 85, 44, 87, 44, 89, 
	44, 91, 92, 93, 44
};

static readonly sbyte[] _scan_trans_actions =  new sbyte [] {
	57, 0, 3, 0, 5, 55, 0, 0, 
	0, 0, 3, 0, 3, 0, 0, 0, 
	0, 0, 3, 53, 0, 0, 3, 0, 
	0, 0, 125, 0, 0, 0, 0, 37, 
	0, 0, 0, 0, 15, 17, 11, 7, 
	25, 9, 0, 13, 3, 3, 27, 29, 
	33, 31, 35, 125, 125, 19, 21, 125, 
	125, 125, 125, 125, 125, 125, 125, 125, 
	125, 125, 125, 125, 125, 0, 0, 0, 
	0, 0, 49, 51, 43, 45, 47, 23, 
	41, 0, 0, 0, 0, 0, 39, 125, 
	74, 68, 125, 125, 125, 107, 125, 125, 
	125, 83, 125, 125, 125, 110, 113, 125, 
	125, 125, 125, 125, 122, 98, 125, 80, 
	95, 101, 125, 65, 125, 125, 116, 77, 
	71, 125, 125, 92, 125, 125, 125, 125, 
	89, 125, 125, 125, 86, 125, 119, 125, 
	62, 125, 125, 125, 104
};

static readonly sbyte[] _scan_to_state_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 59, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0
};

static readonly sbyte[] _scan_from_state_actions =  new sbyte [] {
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 1, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0, 0, 0, 
	0, 0, 0, 0, 0, 0
};

static readonly short[] _scan_eof_trans =  new short [] {
	0, 1, 1, 1, 6, 6, 6, 6, 
	1, 1, 1, 6, 6, 6, 6, 20, 
	20, 20, 20, 20, 20, 1, 1, 1, 
	1, 1, 1, 1, 0, 75, 76, 77, 
	77, 78, 77, 77, 79, 81, 81, 81, 
	81, 81, 81, 81, 1, 87, 87, 87, 
	87, 87, 87, 87, 87, 87, 87, 87, 
	87, 87, 87, 87, 87, 87, 87, 87, 
	87, 87, 87, 87, 87, 87, 87, 87, 
	87, 87, 87, 87, 87, 87, 87, 87, 
	87, 87, 87, 87, 87, 87, 87, 87, 
	87, 87, 87, 87, 87, 87
};

const int scan_start = 28;
const int scan_first_final = 28;
const int scan_error = 0;

const int scan_en_main = 28;


#line 378 "Scanner.rl"

        private readonly string data;

        private int cs;
        private int act;
        private int col;
        private int line;
        private int p;
        private int pe;
        private int eof;
        private int ts;
        private int te;
        private char it;
        private Stack<int> levels;
        private Queue<Token> pending;
        private bool finished;
        private int join;
        private Token last;
        private Token error;

        public Scanner(string data)
        {
            this.data = data;

            col = 1;
            line = 1;
            p = 0;
            pe = data.Length;
            eof = data.Length;
            it = (char)0;
            levels = new Stack<int>();
            pending = new Queue<Token>();
            finished = false;
            join = 0;

            last = Create(TokenType.Dummy);
            error = Create(TokenType.Dummy);

#line 472 "Scanner.cs"
	{
	cs = scan_start;
	ts = -1;
	te = -1;
	act = 0;
	}

#line 416 "Scanner.rl"
            
            levels.Push(0);
        }

        public Token Scan()
        {
            while (pending.Count == 0)
            {
                var token = Next();

                if (token == error)
                {
                    return null;
                }

                if (token == null)
                {
                    token = Create(TokenType.Eof);
                }

                switch (token.Type)
                {
                    case TokenType.Comment:
                    case TokenType.Ws:
                        break;
                    case TokenType.Eol:
                        {
                            var blankLine = last.Type == TokenType.Ws && last.Col == 1;
                            var commentLine = last.Type == TokenType.Comment && last.Col == 1;
                            if (token.Col > 1 && !blankLine && !commentLine && join == 0)
                            {
                                //Console.Write("[queued {0}]", token.Type);
                                pending.Enqueue(token);
                            }
                        }
                        break;
                    case TokenType.Eof:
                        {
                            if (last.Type != TokenType.Dummy && last.Type != TokenType.Eol)
                            {
                                var eol = Create(TokenType.Eol);
                                pending.Enqueue(eol);
                            }

                            while (levels.Peek() > 0) DoDedent(token);   

                            pending.Enqueue(token);
                        }
                        break;
                    case TokenType.LParen:
                    case TokenType.LBrack:
                        ++join;
                        pending.Enqueue(token);
                        break;
                    case TokenType.RParen:
                    case TokenType.RBrack:
                        --join;
                        pending.Enqueue(token);
                        break;                        
                    default:
                        {
                            if (last.Type == TokenType.Eol) 
                            {
                                while (levels.Peek() > 0) DoDedent(token);
                            } 
                            else if (last.Type == TokenType.Ws)
                            {                       
                                if (last.Col == 1 && join == 0)
                                {
                                    var level = last.Length;
                                    var curLevel = levels.Peek();
                                    if (level > curLevel)
                                    {         
                                        DoIndent(level, last);
                                    } 
                                    else if (level < curLevel)
                                    {
                                        do
                                        {
                                            DoDedent(token);
                                        }
                                        while (level < levels.Peek());
                                    }
                                }
                            }

                            pending.Enqueue(token);
                        }
                        break;
                }

                last = token;
            }

            return pending.Dequeue();
        }

        private void DoIndent(int level, Token source)
        {
            var s = source.Value;

            if (s.Distinct().Count() > 1)
            {
                Console.WriteLine("Indent not same!");
                throw new Exception();
            }

            if (it == 0)
            {
                it = s[0];
            }
            else if (it != s[0])
            {
                Console.WriteLine("Indent not same!");
                throw new Exception();
            }

            levels.Push(level);
            var ind = new Token();
            ind.Type = TokenType.Indent;
            ind.Col = source.Col;
            ind.Line = source.Line;
            pending.Enqueue(ind);
        }

        private void DoDedent(Token source)
        {
            levels.Pop();
            var ded = new Token();
            ded.Type = TokenType.Dedent;
            ded.Col = source.Col;
            ded.Line = source.Line;
            pending.Enqueue(ded);
        }

        private Token Next()
        {
            Token token = null;
            
#line 616 "Scanner.cs"
	{
	sbyte _klen;
	short _trans;
	int _acts;
	int _nacts;
	short _keys;

	if ( p == pe )
		goto _test_eof;
	if ( cs == 0 )
		goto _out;
_resume:
	_acts = _scan_from_state_actions[cs];
	_nacts = _scan_actions[_acts++];
	while ( _nacts-- > 0 ) {
		switch ( _scan_actions[_acts++] ) {
	case 2:
#line 1 "NONE"
	{ts = p;}
	break;
#line 635 "Scanner.cs"
		default: break;
		}
	}

	_keys = _scan_key_offsets[cs];
	_trans = (short)_scan_index_offsets[cs];

	_klen = _scan_single_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + _klen - 1);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + ((_upper-_lower) >> 1));
			if ( data[p] < _scan_trans_keys[_mid] )
				_upper = (short) (_mid - 1);
			else if ( data[p] > _scan_trans_keys[_mid] )
				_lower = (short) (_mid + 1);
			else {
				_trans += (short) (_mid - _keys);
				goto _match;
			}
		}
		_keys += (short) _klen;
		_trans += (short) _klen;
	}

	_klen = _scan_range_lengths[cs];
	if ( _klen > 0 ) {
		short _lower = _keys;
		short _mid;
		short _upper = (short) (_keys + (_klen<<1) - 2);
		while (true) {
			if ( _upper < _lower )
				break;

			_mid = (short) (_lower + (((_upper-_lower) >> 1) & ~1));
			if ( data[p] < _scan_trans_keys[_mid] )
				_upper = (short) (_mid - 2);
			else if ( data[p] > _scan_trans_keys[_mid+1] )
				_lower = (short) (_mid + 2);
			else {
				_trans += (short)((_mid - _keys)>>1);
				goto _match;
			}
		}
		_trans += (short) _klen;
	}

_match:
	_trans = (short)_scan_indicies[_trans];
_eof_trans:
	cs = _scan_trans_targs[_trans];

	if ( _scan_trans_actions[_trans] == 0 )
		goto _again;

	_acts = _scan_trans_actions[_trans];
	_nacts = _scan_actions[_acts++];
	while ( _nacts-- > 0 )
	{
		switch ( _scan_actions[_acts++] )
		{
	case 3:
#line 1 "NONE"
	{te = p+1;}
	break;
	case 4:
#line 5 "Scanner.rl"
	{act = 1;}
	break;
	case 5:
#line 10 "Scanner.rl"
	{act = 2;}
	break;
	case 6:
#line 15 "Scanner.rl"
	{act = 3;}
	break;
	case 7:
#line 20 "Scanner.rl"
	{act = 4;}
	break;
	case 8:
#line 25 "Scanner.rl"
	{act = 5;}
	break;
	case 9:
#line 30 "Scanner.rl"
	{act = 6;}
	break;
	case 10:
#line 35 "Scanner.rl"
	{act = 7;}
	break;
	case 11:
#line 40 "Scanner.rl"
	{act = 8;}
	break;
	case 12:
#line 45 "Scanner.rl"
	{act = 9;}
	break;
	case 13:
#line 50 "Scanner.rl"
	{act = 10;}
	break;
	case 14:
#line 55 "Scanner.rl"
	{act = 11;}
	break;
	case 15:
#line 60 "Scanner.rl"
	{act = 12;}
	break;
	case 16:
#line 65 "Scanner.rl"
	{act = 13;}
	break;
	case 17:
#line 70 "Scanner.rl"
	{act = 14;}
	break;
	case 18:
#line 75 "Scanner.rl"
	{act = 15;}
	break;
	case 19:
#line 80 "Scanner.rl"
	{act = 16;}
	break;
	case 20:
#line 85 "Scanner.rl"
	{act = 17;}
	break;
	case 21:
#line 90 "Scanner.rl"
	{act = 18;}
	break;
	case 22:
#line 95 "Scanner.rl"
	{act = 19;}
	break;
	case 23:
#line 100 "Scanner.rl"
	{act = 20;}
	break;
	case 24:
#line 105 "Scanner.rl"
	{act = 21;}
	break;
	case 25:
#line 110 "Scanner.rl"
	{act = 22;}
	break;
	case 26:
#line 120 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.String);
    {p++; if (true) goto _out; }
}}
	break;
	case 27:
#line 140 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.Add);
    {p++; if (true) goto _out; }
}}
	break;
	case 28:
#line 145 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.Sub);
    {p++; if (true) goto _out; }
}}
	break;
	case 29:
#line 150 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.Mul);
    {p++; if (true) goto _out; }
}}
	break;
	case 30:
#line 155 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.Div);
    {p++; if (true) goto _out; }
}}
	break;
	case 31:
#line 160 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.LParen);
    {p++; if (true) goto _out; }
}}
	break;
	case 32:
#line 165 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.RParen);
    {p++; if (true) goto _out; }
}}
	break;
	case 33:
#line 170 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.LBrack);
    {p++; if (true) goto _out; }
}}
	break;
	case 34:
#line 175 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.RBrack);
    {p++; if (true) goto _out; }
}}
	break;
	case 35:
#line 180 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.Range);
    {p++; if (true) goto _out; }
}}
	break;
	case 36:
#line 190 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.Comma);
    {p++; if (true) goto _out; }
}}
	break;
	case 37:
#line 195 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.Colon);
    {p++; if (true) goto _out; }
}}
	break;
	case 38:
#line 200 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.SemiColon);
    {p++; if (true) goto _out; }
}}
	break;
	case 39:
#line 205 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.Equal);
    {p++; if (true) goto _out; }
}}
	break;
	case 40:
#line 210 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.Less);
    {p++; if (true) goto _out; }
}}
	break;
	case 41:
#line 215 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.Great);
    {p++; if (true) goto _out; }
}}
	break;
	case 42:
#line 135 "Scanner.rl"
	{te = p+1;{
    token = Create(TokenType.Eol);
    {p++; if (true) goto _out; }
}}
	break;
	case 43:
#line 110 "Scanner.rl"
	{te = p;p--;{
    token = Create(TokenType.Member);
    {p++; if (true) goto _out; }
}}
	break;
	case 44:
#line 115 "Scanner.rl"
	{te = p;p--;{
    token = Create(TokenType.Number);
    {p++; if (true) goto _out; }
}}
	break;
	case 45:
#line 120 "Scanner.rl"
	{te = p;p--;{
    token = Create(TokenType.String);
    {p++; if (true) goto _out; }
}}
	break;
	case 46:
#line 125 "Scanner.rl"
	{te = p;p--;{
    token = Create(TokenType.Comment);
    {p++; if (true) goto _out; }
}}
	break;
	case 47:
#line 185 "Scanner.rl"
	{te = p;p--;{
    token = Create(TokenType.Dot);
    {p++; if (true) goto _out; }
}}
	break;
	case 48:
#line 130 "Scanner.rl"
	{te = p;p--;{
    token = Create(TokenType.Ws);
    {p++; if (true) goto _out; }
}}
	break;
	case 49:
#line 135 "Scanner.rl"
	{te = p;p--;{
    token = Create(TokenType.Eol);
    {p++; if (true) goto _out; }
}}
	break;
	case 50:
#line 115 "Scanner.rl"
	{{p = ((te))-1;}{
    token = Create(TokenType.Number);
    {p++; if (true) goto _out; }
}}
	break;
	case 51:
#line 120 "Scanner.rl"
	{{p = ((te))-1;}{
    token = Create(TokenType.String);
    {p++; if (true) goto _out; }
}}
	break;
	case 52:
#line 1 "NONE"
	{	switch( act ) {
	case 0:
	{{cs = 0; if (true) goto _again;}}
	break;
	case 1:
	{{p = ((te))-1;}
    token = Create(TokenType.Var);
    {p++; if (true) goto _out; }
}
	break;
	case 2:
	{{p = ((te))-1;}
    token = Create(TokenType.Let);
    {p++; if (true) goto _out; }
}
	break;
	case 3:
	{{p = ((te))-1;}
    token = Create(TokenType.And);
    {p++; if (true) goto _out; }
}
	break;
	case 4:
	{{p = ((te))-1;}
    token = Create(TokenType.Or);
    {p++; if (true) goto _out; }
}
	break;
	case 5:
	{{p = ((te))-1;}
    token = Create(TokenType.As);
    {p++; if (true) goto _out; }
}
	break;
	case 6:
	{{p = ((te))-1;}
    token = Create(TokenType.Of);
    {p++; if (true) goto _out; }
}
	break;
	case 7:
	{{p = ((te))-1;}
    token = Create(TokenType.Func);
    {p++; if (true) goto _out; }
}
	break;
	case 8:
	{{p = ((te))-1;}
    token = Create(TokenType.Class);
    {p++; if (true) goto _out; }
}
	break;
	case 9:
	{{p = ((te))-1;}
    token = Create(TokenType.This);
    {p++; if (true) goto _out; }
}
	break;
	case 10:
	{{p = ((te))-1;}
    token = Create(TokenType.Return);
    {p++; if (true) goto _out; }
}
	break;
	case 11:
	{{p = ((te))-1;}
    token = Create(TokenType.Pass);
    {p++; if (true) goto _out; }
}
	break;
	case 12:
	{{p = ((te))-1;}
    token = Create(TokenType.If);
    {p++; if (true) goto _out; }
}
	break;
	case 13:
	{{p = ((te))-1;}
    token = Create(TokenType.For);
    {p++; if (true) goto _out; }
}
	break;
	case 14:
	{{p = ((te))-1;}
    token = Create(TokenType.In);
    {p++; if (true) goto _out; }
}
	break;
	case 15:
	{{p = ((te))-1;}
    token = Create(TokenType.While);
    {p++; if (true) goto _out; }
}
	break;
	case 16:
	{{p = ((te))-1;}
    token = Create(TokenType.Break);
    {p++; if (true) goto _out; }
}
	break;
	case 17:
	{{p = ((te))-1;}
    token = Create(TokenType.Elif);
    {p++; if (true) goto _out; }
}
	break;
	case 18:
	{{p = ((te))-1;}
    token = Create(TokenType.Else);
    {p++; if (true) goto _out; }
}
	break;
	case 19:
	{{p = ((te))-1;}
    token = Create(TokenType.None);
    {p++; if (true) goto _out; }
}
	break;
	case 20:
	{{p = ((te))-1;}
    token = Create(TokenType.True);
    {p++; if (true) goto _out; }
}
	break;
	case 21:
	{{p = ((te))-1;}
    token = Create(TokenType.False);
    {p++; if (true) goto _out; }
}
	break;
	case 22:
	{{p = ((te))-1;}
    token = Create(TokenType.Member);
    {p++; if (true) goto _out; }
}
	break;
	}
	}
	break;
#line 1066 "Scanner.cs"
		default: break;
		}
	}

_again:
	_acts = _scan_to_state_actions[cs];
	_nacts = _scan_actions[_acts++];
	while ( _nacts-- > 0 ) {
		switch ( _scan_actions[_acts++] ) {
	case 0:
#line 1 "NONE"
	{ts = -1;}
	break;
	case 1:
#line 1 "NONE"
	{act = 0;}
	break;
#line 1081 "Scanner.cs"
		default: break;
		}
	}

	if ( cs == 0 )
		goto _out;
	if ( ++p != pe )
		goto _resume;
	_test_eof: {}
	if ( p == eof )
	{
	if ( _scan_eof_trans[cs] > 0 ) {
		_trans = (short) (_scan_eof_trans[cs] - 1);
		goto _eof_trans;
	}
	}

	_out: {}
	}

#line 555 "Scanner.rl"

            if (cs == scan_error && data[p] != 0)
            {
                Console.WriteLine("Unexpected {0}", data[p]);   
                token = error;  
            }

            return token;
        }

        private Token Create(TokenType type)
        {
            var token = new Token();
            token.Type = type;
            token.Col = col;
            token.Line = line;

            switch (type)
            {
                case TokenType.Dummy:
                case TokenType.Indent:
                case TokenType.Dedent:
                case TokenType.Eof:
                    token.Length = -1;
                    token.Size = -1;
                    break;
                case TokenType.Ws:
                    {
                        token.Value = GetTokenString(ts, te);
                        token.Length = token.Value.Length;
                        var width = 4;
                        foreach (var c in token.Value)
                        {
                            switch (c)
                            {
                            case ' ':
                                col++;
                                break;
                            case '\t':
                                col = ((col + width) & ~(width - 1)) + 1;
                                break;
                            }
                        }
                        break;
                    }
                case TokenType.Eol:
                    col = 1;
                    line++;
                    break;
                case TokenType.String:
                    token.Value = GetTokenString(ts, te);
                    token.Length = token.Value.Length;
                    for (var i = ts; i != te; i++)
                    {
                        switch (data[i])
                        {
                            case '\r':
                                col = 1;
                                line++;
                                if (data[i + 1] == '\n') i++;  
                                break;          
                            case '\n':
                                col = 1;
                                line++;
                                break;
                            default:
                                col++;  
                                break;  
                        }           
                    }
                    break;
                default:
                    token.Value = GetTokenString(ts, te);
                    token.Length = token.Value.Length;
                    col += token.Length;
                    break;
            }

            return token;
        }

        private string GetTokenString(int start, int end)
        {
            return data.Substring(start, end - start);
        }

    }
}

