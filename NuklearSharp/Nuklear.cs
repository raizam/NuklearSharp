using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public static unsafe partial class Nuklear
	{
		public delegate float NkTextWidthDelegate(nk_handle handle, float height, char* text, int length);

		public delegate void NkQueryFontGlyphDelegate(nk_handle handle,
			float height, nk_user_font_glyph* glyph, char codepoint, char next_codepoint);

		public delegate void NkCommandCustomCallback(
			nk_draw_list list, short x, short y, ushort w, ushort h, nk_handle callback_data);

		public delegate void NkPluginPaste(nk_handle handle, nk_text_edit text_edit);

		public delegate void NkPluginCopy(nk_handle handle, char* text, int length);

		public delegate void NkDrawNotify(nk_command_buffer buffer, nk_handle handle);

		public delegate int NkPluginFilter(nk_text_edit text_edit, char unicode);

		public delegate float NkFloatValueGetter(void* handle, int index);

		public delegate float NkComboCallback(void* handle, int index, char** item);

		public delegate int QSortComparer(void* a, void* b);

		public static nk_vec2[,] nk_cursor_data =
		{
			{new nk_vec2 {x = 0, y = 3}, new nk_vec2 {x = 12, y = 19}, new nk_vec2 {x = 0, y = 0}},
			{new nk_vec2 {x = 13, y = 0}, new nk_vec2 {x = 7, y = 16}, new nk_vec2 {x = 4, y = 8}},
			{new nk_vec2 {x = 31, y = 0}, new nk_vec2 {x = 23, y = 23}, new nk_vec2 {x = 11, y = 11}},
			{new nk_vec2 {x = 21, y = 0}, new nk_vec2 {x = 9, y = 23}, new nk_vec2 {x = 5, y = 11}},
			{new nk_vec2 {x = 55, y = 18}, new nk_vec2 {x = 23, y = 9}, new nk_vec2 {x = 11, y = 5}},
			{new nk_vec2 {x = 73, y = 0}, new nk_vec2 {x = 17, y = 17}, new nk_vec2 {x = 9, y = 9}},
			{new nk_vec2 {x = 55, y = 0}, new nk_vec2 {x = 17, y = 17}, new nk_vec2 {x = 9, y = 9}}
		};

		public static nk_rect nk_null_rect = new nk_rect {x = -8192.0f, y = -8192.0f, w = 16384, h = 16384};
		public static nk_color nk_red = new nk_color {r = 255, g = 0, b = 0, a = 255};
		public static nk_color nk_green = new nk_color {r = 0, g = 255, b = 0, a = 255};
		public static nk_color nk_blue = new nk_color {r = 0, g = 0, b = 255, a = 255};
		public static nk_color nk_white = new nk_color {r = 255, g = 255, b = 255, a = 255};
		public static nk_color nk_black = new nk_color {r = 0, g = 0, b = 0, a = 255};
		public static nk_color nk_yellow = new nk_color {r = 255, g = 255, b = 0, a = 255};
		public static nk_color nk_black_trans = new nk_color {r = 0, g = 0, b = 0, a = 0};

		public static nk_color[] hue_colors =
		{
			new nk_color {r = 255, g = 0, b = 0, a = 255},
			new nk_color {r = 255, g = 255, b = 0, a = 255},
			new nk_color {r = 0, g = 255, b = 0, a = 255},
			new nk_color {r = 0, g = 255, b = 255, a = 255},
			new nk_color {r = 0, g = 0, b = 255, a = 255},
			new nk_color {r = 255, g = 0, b = 255, a = 255},
			new nk_color {r = 255, g = 0, b = 0, a = 255}
		};

		public static nk_color[] nk_default_color_style =
		{
			new nk_color {r = 175, g = 175, b = 175, a = 255},
			new nk_color {r = 45, g = 45, b = 45, a = 255},
			new nk_color {r = 40, g = 40, b = 40, a = 255},
			new nk_color {r = 65, g = 65, b = 65, a = 255},
			new nk_color {r = 50, g = 50, b = 50, a = 255},
			new nk_color {r = 40, g = 40, b = 40, a = 255},
			new nk_color {r = 35, g = 35, b = 35, a = 255},
			new nk_color {r = 100, g = 100, b = 100, a = 255},
			new nk_color {r = 120, g = 120, b = 120, a = 255},
			new nk_color {r = 45, g = 45, b = 45, a = 255},
			new nk_color {r = 45, g = 45, b = 45, a = 255},
			new nk_color {r = 35, g = 35, b = 35, a = 255},
			new nk_color {r = 38, g = 38, b = 38, a = 255},
			new nk_color {r = 100, g = 100, b = 100, a = 255},
			new nk_color {r = 120, g = 120, b = 120, a = 255},
			new nk_color {r = 150, g = 150, b = 150, a = 255},
			new nk_color {r = 38, g = 38, b = 38, a = 255},
			new nk_color {r = 38, g = 38, b = 38, a = 255},
			new nk_color {r = 175, g = 175, b = 175, a = 255},
			new nk_color {r = 45, g = 45, b = 45, a = 255},
			new nk_color {r = 120, g = 120, b = 120, a = 255},
			new nk_color {r = 45, g = 45, b = 45, a = 255},
			new nk_color {r = 255, g = 0, b = 0, a = 255},
			new nk_color {r = 40, g = 40, b = 40, a = 255},
			new nk_color {r = 100, g = 100, b = 100, a = 255},
			new nk_color {r = 120, g = 120, b = 120, a = 255},
			new nk_color {r = 150, g = 150, b = 150, a = 255},
			new nk_color {r = 40, g = 40, b = 40, a = 255}
		};

		public static string[] nk_color_names =
		{
			"NK_COLOR_TEXT", "NK_COLOR_WINDOW", "NK_COLOR_HEADER", "NK_COLOR_BORDER", "NK_COLOR_BUTTON", "NK_COLOR_BUTTON_HOVER",
			"NK_COLOR_BUTTON_ACTIVE", "NK_COLOR_TOGGLE", "NK_COLOR_TOGGLE_HOVER", "NK_COLOR_TOGGLE_CURSOR", "NK_COLOR_SELECT",
			"NK_COLOR_SELECT_ACTIVE", "NK_COLOR_SLIDER", "NK_COLOR_SLIDER_CURSOR", "NK_COLOR_SLIDER_CURSOR_HOVER",
			"NK_COLOR_SLIDER_CURSOR_ACTIVE", "NK_COLOR_PROPERTY", "NK_COLOR_EDIT", "NK_COLOR_EDIT_CURSOR", "NK_COLOR_COMBO",
			"NK_COLOR_CHART", "NK_COLOR_CHART_COLOR", "NK_COLOR_CHART_COLOR_HIGHLIGHT", "NK_COLOR_SCROLLBAR",
			"NK_COLOR_SCROLLBAR_CURSOR", "NK_COLOR_SCROLLBAR_CURSOR_HOVER", "NK_COLOR_SCROLLBAR_CURSOR_ACTIVE",
			"NK_COLOR_TAB_HEADER"
		};

		public static string nk_proggy_clean_ttf_compressed_data_base85 =
			"7])#######hV0qs'/###[),##/l:$#Q6>##5[n42>c-TH`->>#/e>11NNV=Bv(*:.F?uu#(gRU.o0XGH`$vhLG1hxt9?W`#,5LsCp#-i>.r$<$6pD>Lb';9Crc6tgXmKVeU2cD4Eo3R/2*>]b(MC;$jPfY.;h^`IWM9<Lh2TlS+f-s$o6Q<BWH`YiU.xfLq$N;$0iR/GX:U(jcW2p/W*q?-qmnUCI;jHSAiFWM.R*kU@C=GH?a9wp8f$e.-4^Qg1)Q-GL(lf(r/7GrRgwV%MS=C#`8ND>Qo#t'X#(v#Y9w0#1D$CIf;W'#pWUPXOuxXuU(H9M(1<q-UE31#^-V'8IRUo7Qf./L>=Ke$$'5F%)]0^#0X@U.a<r:QLtFsLcL6##lOj)#.Y5<-R&KgLwqJfLgN&;Q?gI^#DY2uLi@^rMl9t=cWq6##weg>$FBjVQTSDgEKnIS7EM9>ZY9w0#L;>>#Mx&4Mvt//L[MkA#W@lK.N'[0#7RL_&#w+F%HtG9M#XL`N&.,GM4Pg;-<nLENhvx>-VsM.M0rJfLH2eTM`*oJMHRC`NkfimM2J,W-jXS:)r0wK#@Fge$U>`w'N7G#$#fB#$E^$#:9:hk+eOe--6x)F7*E%?76%^GMHePW-Z5l'&GiF#$956:rS?dA#fiK:)Yr+`&#0j@'DbG&#^$PG.Ll+DNa<XCMKEV*N)LN/N*b=%Q6pia-Xg8I$<MR&,VdJe$<(7G;Ckl'&hF;;$<_=X(b.RS%%)###MPBuuE1V:v&cX&#2m#(&cV]`k9OhLMbn%s$G2,B$BfD3X*sp5#l,$R#]x_X1xKX%b5U*[r5iMfUo9U`N99hG)tm+/Us9pG)XPu`<0s-)WTt(gCRxIg(%6sfh=ktMKn3j)<6<b5Sk_/0(^]AaN#(p/L>&VZ>1i%h1S9u5o@YaaW$e+b<TWFn/Z:Oh(Cx2$lNEoN^e)#CFY@@I;BOQ*sRwZtZxRcU7uW6CXow0i(?$Q[cjOd[P4d)]>ROPOpxTO7Stwi1::iB1q)C_=dV26J;2,]7op$]uQr@_V7$q^%lQwtuHY]=DX,n3L#0PHDO4f9>dC@O>HBuKPpP*E,N+b3L#lpR/MrTEH.IAQk.a>D[.e;mc.x]Ip.PH^'/aqUO/$1WxLoW0[iLA<QT;5HKD+@qQ'NQ(3_PLhE48R.qAPSwQ0/WK?Z,[x?-J;jQTWA0X@KJ(_Y8N-:/M74:/-ZpKrUss?d#dZq]DAbkU*JqkL+nwX@@47`5>w=4h(9.`GCRUxHPeR`5Mjol(dUWxZa(>STrPkrJiWx`5U7F#.g*jrohGg`cg:lSTvEY/EV_7H4Q9[Z%cnv;JQYZ5q.l7Zeas:HOIZOB?G<Nald$qs]@]L<J7bR*>gv:[7MI2k).'2($5FNP&EQ(,)U]W]+fh18.vsai00);D3@4ku5P?DP8aJt+;qUM]=+b'8@;mViBKx0DE[-auGl8:PJ&Dj+M6OC]O^((##]`0i)drT;-7X`=-H3[igUnPG-NZlo.#k@h#=Ork$m>a>$-?Tm$UV(?#P6YY#'/###xe7q.73rI3*pP/$1>s9)W,JrM7SN]'/4C#v$U`0#V.[0>xQsH$fEmPMgY2u7Kh(G%siIfLSoS+MK2eTM$=5,M8p`A.;_R%#u[K#$x4AG8.kK/HSB==-'Ie/QTtG?-.*^N-4B/ZM_3YlQC7(p7q)&](`6_c)$/*JL(L-^(]$wIM`dPtOdGA,U3:w2M-0<q-]L_?^)1vw'.,MRsqVr.L;aN&#/EgJ)PBc[-f>+WomX2u7lqM2iEumMTcsF?-aT=Z-97UEnXglEn1K-bnEO`guFt(c%=;Am_Qs@jLooI&NX;]0#j4#F14;gl8-GQpgwhrq8'=l_f-b49'UOqkLu7-##oDY2L(te+Mch&gLYtJ,MEtJfLh'x'M=$CS-ZZ%P]8bZ>#S?YY#%Q&q'3^Fw&?D)UDNrocM3A76//oL?#h7gl85[qW/NDOk%16ij;+:1a'iNIdb-ou8.P*w,v5#EI$TWS>Pot-R*H'-SEpA:g)f+O$%%`kA#G=8RMmG1&O`>to8bC]T&$,n.LoO>29sp3dt-52U%VM#q7'DHpg+#Z9%H[K<L%a2E-grWVM3@2=-k22tL]4$##6We'8UJCKE[d_=%wI;'6X-GsLX4j^SgJ$##R*w,vP3wK#iiW&#*h^D&R?jp7+/u&#(AP##XU8c$fSYW-J95_-Dp[g9wcO&#M-h1OcJlc-*vpw0xUX&#OQFKNX@QI'IoPp7nb,QU//MQ&ZDkKP)X<WSVL(68uVl&#c'[0#(s1X&xm$Y%B7*K:eDA323j998GXbA#pwMs-jgD$9QISB-A_(aN4xoFM^@C58D0+Q+q3n0#3U1InDjF682-SjMXJK)(h$hxua_K]ul92%'BOU&#BRRh-slg8KDlr:%L71Ka:.A;%YULjDPmL<LYs8i#XwJOYaKPKc1h:'9Ke,g)b),78=I39B;xiY$bgGw-&.Zi9InXDuYa%G*f2Bq7mn9^#p1vv%#(Wi-;/Z5ho;#2:;%d&#x9v68C5g?ntX0X)pT`;%pB3q7mgGN)3%(P8nTd5L7GeA-GL@+%J3u2:(Yf>et`e;)f#Km8&+DC$I46>#Kr]]u-[=99tts1.qb#q72g1WJO81q+eN'03'eM>&1XxY-caEnOj%2n8)),?ILR5^.Ibn<-X-Mq7[a82Lq:F&#ce+S9wsCK*x`569E8ew'He]h:sI[2LM$[guka3ZRd6:t%IG:;$%YiJ:Nq=?eAw;/:nnDq0(CYcMpG)qLN4$##&J<j$UpK<Q4a1]MupW^-sj_$%[HK%'F####QRZJ::Y3EGl4'@%FkiAOg#p[##O`gukTfBHagL<LHw%q&OV0##F=6/:chIm0@eCP8X]:kFI%hl8hgO@RcBhS-@Qb$%+m=hPDLg*%K8ln(wcf3/'DW-$.lR?n[nCH-eXOONTJlh:.RYF%3'p6sq:UIMA945&^HFS87@$EP2iG<-lCO$%c`uKGD3rC$x0BL8aFn--`ke%#HMP'vh1/R&O_J9'um,.<tx[@%wsJk&bUT2`0uMv7gg#qp/ij.L56'hl;.s5CUrxjOM7-##.l+Au'A&O:-T72L]P`&=;ctp'XScX*rU.>-XTt,%OVU4)S1+R-#dg0/Nn?Ku1^0f$B*P:Rowwm-`0PKjYDDM'3]d39VZHEl4,.j']Pk-M.h^&:0FACm$maq-&sgw0t7/6(^xtk%LuH88Fj-ekm>GA#_>568x6(OFRl-IZp`&b,_P'$M<Jnq79VsJW/mWS*PUiq76;]/NM_>hLbxfc$mj`,O;&%W2m`Zh:/)Uetw:aJ%]K9h:TcF]u_-Sj9,VK3M.*'&0D[Ca]J9gp8,kAW]%(?A%R$f<->Zts'^kn=-^@c4%-pY6qI%J%1IGxfLU9CP8cbPlXv);C=b),<2mOvP8up,UVf3839acAWAW-W?#ao/^#%KYo8fRULNd2.>%m]UK:n%r$'sw]J;5pAoO_#2mO3n,'=H5(etHg*`+RLgv>=4U8guD$I%D:W>-r5V*%j*W:Kvej.Lp$<M-SGZ':+Q_k+uvOSLiEo(<aD/K<CCc`'Lx>'?;++O'>()jLR-^u68PHm8ZFWe+ej8h:9r6L*0//c&iH&R8pRbA#Kjm%upV1g:a_#Ur7FuA#(tRh#.Y5K+@?3<-8m0$PEn;J:rh6?I6uG<-`wMU'ircp0LaE_OtlMb&1#6T.#FDKu#1Lw%u%+GM+X'e?YLfjM[VO0MbuFp7;>Q&#WIo)0@F%q7c#4XAXN-U&VB<HFF*qL($/V,;(kXZejWO`<[5??ewY(*9=%wDc;,u<'9t3W-(H1th3+G]ucQ]kLs7df($/*JL]@*t7Bu_G3_7mp7<iaQjO@.kLg;x3B0lqp7Hf,^Ze7-##@/c58Mo(3;knp0%)A7?-W+eI'o8)b<nKnw'Ho8C=Y>pqB>0ie&jhZ[?iLR@@_AvA-iQC(=ksRZRVp7`.=+NpBC%rh&3]R:8XDmE5^V8O(x<<aG/1N$#FX$0V5Y6x'aErI3I$7x%E`v<-BY,)%-?Psf*l?%C3.mM(=/M0:JxG'?7WhH%o'a<-80g0NBxoO(GH<dM]n.+%q@jH?f.UsJ2Ggs&4<-e47&Kl+f//9@`b+?.TeN_&B8Ss?v;^Trk;f#YvJkl&w$]>-+k?'(<S:68tq*WoDfZu';mM?8X[ma8W%*`-=;D.(nc7/;)g:T1=^J$&BRV(-lTmNB6xqB[@0*o.erM*<SWF]u2=st-*(6v>^](H.aREZSi,#1:[IXaZFOm<-ui#qUq2$##Ri;u75OK#(RtaW-K-F`S+cF]uN`-KMQ%rP/Xri.LRcB##=YL3BgM/3MD?@f&1'BW-)Ju<L25gl8uhVm1hL$##*8###'A3/LkKW+(^rWX?5W_8g)a(m&K8P>#bmmWCMkk&#TR`C,5d>g)F;t,4:@_l8G/5h4vUd%&%950:VXD'QdWoY-F$BtUwmfe$YqL'8(PWX(P?^@Po3$##`MSs?DWBZ/S>+4%>fX,VWv/w'KD`LP5IbH;rTV>n3cEK8U#bX]l-/V+^lj3;vlMb&[5YQ8#pekX9JP3XUC72L,,?+Ni&co7ApnO*5NK,((W-i:$,kp'UDAO(G0Sq7MVjJsbIu)'Z,*[>br5fX^:FPAWr-m2KgL<LUN098kTF&#lvo58=/vjDo;.;)Ka*hLR#/k=rKbxuV`>Q_nN6'8uTG&#1T5g)uLv:873UpTLgH+#FgpH'_o1780Ph8KmxQJ8#H72L4@768@Tm&Qh4CB/5OvmA&,Q&QbUoi$a_%3M01H)4x7I^&KQVgtFnV+;[Pc>[m4k//,]1?#`VY[Jr*3&&slRfLiVZJ:]?=K3Sw=[$=uRB?3xk48@aeg<Z'<$#4H)6,>e0jT6'N#(q%.O=?2S]u*(m<-V8J'(1)G][68hW$5'q[GC&5j`TE?m'esFGNRM)j,ffZ?-qx8;->g4t*:CIP/[Qap7/9'#(1sao7w-.qNUdkJ)tCF&#B^;xGvn2r9FEPFFFcL@.iFNkTve$m%#QvQS8U@)2Z+3K:AKM5isZ88+dKQ)W6>J%CL<KE>`.d*(B`-n8D9oK<Up]c$X$(,)M8Zt7/[rdkqTgl-0cuGMv'?>-XV1q['-5k'cAZ69e;D_?$ZPP&s^+7])$*$#@QYi9,5P&#9r+$%CE=68>K8r0=dSC%%(@p7.m7jilQ02'0-VWAg<a/'\0'3u.=4L$Y)6k/K:_[3=&jvL<L0C/2'v:^;-DIBW,B4E68:kZ;%?8(Q8BH=kO65BW?xSG&#@uU,DS*,?.+(o(#1vCS8#CHF>TlGW'b)Tq7VT9q^*^$$.:&N@@$&)WHtPm*5_rO0&e%K&#-30j(E4#'Zb.o/(Tpm$>K'f@[PvFl,hfINTNU6u'0pao7%XUp9]5.>%h`8_=VYbxuel.NTSsJfLacFu3B'lQSu/m6-Oqem8T+oE--$0a/k]uj9EwsG>%veR*hv^BFpQj:K'#SJ,sB-'#](j.Lg92rTw-*n%@/;39rrJF,l#qV%OrtBeC6/,;qB3ebNW[?,Hqj2L.1NP&GjUR=1D8QaS3Up&@*9wP?+lo7b?@%'k4`p0Z$22%K3+iCZj?XJN4Nm&+YF]u@-W$U%VEQ/,,>>#)D<h#`)h0:<Q6909ua+&VU%n2:cG3FJ-%@Bj-DgLr`Hw&HAKjKjseK</xKT*)B,N9X3]krc12t'pgTV(Lv-tL[xg_%=M_q7a^x?7Ubd>#%8cY#YZ?=,`Wdxu/ae&#w6)R89tI#6@s'(6Bf7a&?S=^ZI_kS&ai`&=tE72L_D,;^R)7[$s<Eh#c&)q.MXI%#v9ROa5FZO%sF7q7Nwb&#ptUJ:aqJe$Sl68%.D###EC><?-aF&#RNQv>o8lKN%5/$(vdfq7+ebA#u1p]ovUKW&Y%q]'>$1@-[xfn$7ZTp7mM,G,Ko7a&Gu%G[RMxJs[0MM%wci.LFDK)(<c`Q8N)jEIF*+?P2a8g%)$q]o2aH8C&<SibC/q,(e:v;-b#6[$NtDZ84Je2KNvB#$P5?tQ3nt(0d=j.LQf./Ll33+(;q3L-w=8dX$#WF&uIJ@-bfI>%:_i2B5CsR8&9Z&#=mPEnm0f`<&c)QL5uJ#%u%lJj+D-r;BoF&#4DoS97h5g)E#o:&S4weDF,9^Hoe`h*L+_a*NrLW-1pG_&2UdB86e%B/:=>)N4xeW.*wft-;$'58-ESqr<b?UI(_%@[P46>#U`'6AQ]m&6/`Z>#S?YY#Vc;r7U2&326d=w&H####?TZ`*4?&.MK?LP8Vxg>$[QXc%QJv92.(Db*B)gb*BM9dM*hJMAo*c&#b0v=Pjer]$gG&JXDf->'StvU7505l9$AFvgYRI^&<^b68?j#q9QX4SM'RO#&sL1IM.rJfLUAj221]d##DW=m83u5;'bYx,*Sl0hL(W;;$doB&O/TQ:(Z^xBdLjL<Lni;'\0'X.`$#8+1GD:k$YUWsbn8ogh6rxZ2Z9]%nd+>V#*8U_72Lh+2Q8Cj0i:6hp&$C/:p(HK>T8Y[gHQ4`4)'$Ab(Nof%V'8hL&#<NEdtg(n'=S1A(Q1/I&4([%dM`,Iu'1:_hL>SfD07&6D<fp8dHM7/g+tlPN9J*rKaPct&?'uBCem^jn%9_K)<,C5K3s=5g&GmJb*[SYq7K;TRLGCsM-$$;S%:Y@r7AK0pprpL<Lrh,q7e/%KWK:50I^+m'vi`3?%Zp+<-d+$L-Sv:@.o19n$s0&39;kn;S%BSq*$3WoJSCLweV[aZ'MQIjO<7;X-X;&+dMLvu#^UsGEC9WEc[X(wI7#2.(F0jV*eZf<-Qv3J-c+J5AlrB#$p(H68LvEA'q3n0#m,[`*8Ft)FcYgEud]CWfm68,(aLA$@EFTgLXoBq/UPlp7:d[/;r_ix=:TF`S5H-b<LI&HY(K=h#)]Lk$K14lVfm:x$H<3^Ql<M`$OhapBnkup'D#L$Pb_`N*g]2e;X/Dtg,bsj&K#2[-:iYr'_wgH)NUIR8a1n#S?Yej'h8^58UbZd+^FKD*T@;6A7aQC[K8d-(v6GI$x:T<&'Gp5Uf>@M.*J:;$-rv29'M]8qMv-tLp,'886iaC=Hb*YJoKJ,(j%K=H`K.v9HggqBIiZu'QvBT.#=)0ukruV&.)3=(^1`o*Pj4<-<aN((^7('#Z0wK#5GX@7u][`*S^43933A4rl][`*O4CgLEl]v$1Q3AeF37dbXk,.)vj#x'd`;qgbQR%FW,2(?LO=s%Sc68%NP'##Aotl8x=BE#j1UD([3$M(]UI2LX3RpKN@;/#f'f/&_mt&F)XdF<9t4)Qa.*kTLwQ'(TTB9.xH'>#MJ+gLq9-##@HuZPN0]u:h7.T..G:;$/Usj(T7`Q8tT72LnYl<-qx8;-HV7Q-&Xdx%1a,hC=0u+HlsV>nuIQL-5<N?)NBS)QN*_I,?&)2'IM%L3I)X((e/dl2&8'<M:^#M*Q+[T.Xri.LYS3v%fF`68h;b-X[/En'CR.q7E)p'/kle2HM,u;^%OKC-N+Ll%F9CF<Nf'^#t2L,;27W:0O@6##U6W7:$rJfLWHj$#)woqBefIZ.PK<b*t7ed;p*_m;4ExK#h@&]>_>@kXQtMacfD.m-VAb8;IReM3$wf0'\0'hra*so568'Ip&vRs849'MRYSp%:t:h5qSgwpEr$B>Q,;s(C#$)`svQuF$##-D,##,g68@2[T;.XSdN9Qe)rpt._K-#5wF)sP'##p#C0c%-Gb%hd+<-j'Ai*x&&HMkT]C'OSl##5RG[JXaHN;d'uA#x._U;.`PU@(Z3dt4r152@:v,'R.Sj'w#0<-;kPI)FfJ&#AYJ&#//)>-k=m=*XnK$>=)72L]0I%>.G690a:$##<,);?;72#?x9+d;^V'9;jY@;)br#q^YQpx:X#Te$Z^'=-=bGhLf:D6&bNwZ9-ZD#n^9HhLMr5G;']d&6'wYmTFmL<LD)F^%[tC'8;+9E#C$g%#5Y>q9wI>P(9mI[>kC-ekLC/R&CH+s'B;K-M6$EB%is00:+A4[7xks.LrNk0&E)wILYF@2L'0Nb$+pv<(2.768/FrY&h$^3i&@+G%JT'<-,v`3;_)I9M^AE]CN?Cl2AZg+%4iTpT3<n-&%H%b<FDj2M<hH=&Eh<2Len$b*aTX=-8QxN)k11IM1c^j%9s<L<NFSo)B?+<-(GxsF,^-Eh@$4dXhN$+#rxK8'je'D7k`e;)2pYwPA'_p9&@^18ml1^[@g4t*[JOa*[=Qp7(qJ_oOL^('7fB&Hq-:sf,sNj8xq^>$U4O]GKx'm9)b@p7YsvK3w^YR-CdQ*:Ir<($u&)#(&?L9Rg3H)4fiEp^iI9O8KnTj,]H?D*r7'M;PwZ9K0E^k&-cpI;.p/6_vwoFMV<->#%Xi.LxVnrU(4&8/P+:hLSKj$#U%]49t'I:rgMi'FL@a:0Y-uA[39',(vbma*hU%<-SRF`Tt:542R_VV$p@[p8DV[A,?1839FWdF<TddF<9Ah-6&9tWoDlh]&1SpGMq>Ti1O*H&#(AL8[_P%.M>v^-))qOT*F5Cq0`Ye%+$B6i:7@0IX<N+T+0MlMBPQ*Vj>SsD<U4JHY8kD2)2fU/M#$e.)T4,_=8hLim[&);?UkK'-x?'(:siIfL<$pFM`i<?%W(mGDHM%>iWP,##P`%/L<eXi:@Z9C.7o=@(pXdAO/NLQ8lPl+HPOQa8wD8=^GlPa8TKI1CjhsCTSLJM'/Wl>-S(qw%sf/@%#B6;/U7K]uZbi^Oc^2n<bhPmUkMw>%t<)'mEVE'\0'n`WnJra$^TKvX5B>;_aSEK',(hwa0:i4G?.Bci.(X[?b*($,=-n<.Q%`(X=?+@Am*Js0&=3bh8K]mL<LoNs'6,'85`0?t/'_U59@]ddF<#LdF<eWdF<OuN/45rY<-L@&#+fm>69=Lb,OcZV/);TTm8VI;?%OtJ<(b4mq7M6:u?KRdF<gR@2L=FNU-<b[(9c/ML3m;Z[$oF3g)GAWqpARc=<ROu7cL5l;-[A]%/+fsd;l#SafT/f*W]0=O'$(Tb<[)*@e775R-:Yob%g*>l*:xP?Yb.5)%w_I?7uk5JC+FS(m#i'k.'a0i)9<7b'fs'59hq$*5Uhv##pi^8+hIEBF`nvo`;'l0.^S1<-wUK2/Coh58KKhLjM=SO*rfO`+qC`W-On.=AJ56>>i2@2LH6A:&5q`?9I3@@'04&p2/LVa*T-4<-i3;M9UvZd+N7>b*eIwg:CC)c<>nO&#<IGe;__.thjZl<%w(Wk2xmp4Q@I#I9,DF]u7-P=.-_:YJ]aS@V?6*C()dOp7:WL,b&3Rg/.cmM9&r^>$(>.Z-I&J(Q0Hd5Q%7Co-b`-c<N(6r@ip+AurK<m86QIth*#v;-OBqi+L7wDE-Ir8K['m+DDSLwK&/.?-V%U_%3:qKNu$_b*B-kp7NaD'QdWQPKYq[@>P)hI;*_F]u`Rb[.j8_Q/<&>uu+VsH$sM9TA%?)(vmJ80),P7E>)tjD%2L=-t#fK[%`v=Q8<FfNkgg^oIbah*#8/Qt$F&:K*-(N/'+1vMB,u()-a.VUU*#[e%gAAO(S>WlA2);Sa>gXm8YB`1d@K#n]76-a$U,mF<fX]idqd)<3,]J7JmW4`6]uks=4-72L(jEk+:bJ0M^q-8Dm_Z?0olP1C9Sa&H[d&c$ooQUj]Exd*3ZM@-WGW2%s',B-_M%>%Ul:#/'xoFM9QX-$.QN'>[%$Z$uF6pA6Ki2O5:8w*vP1<-1`[G,)-m#>0`P&#eb#.3i)rtB61(o'$?X3B</R90;eZ]%Ncq;-Tl]#F>2Qft^ae_5tKL9MUe9b*sLEQ95C&`=G?@Mj=wh*'3E>=-<)Gt*Iw)'QG:`@IwOf7&]1i'S01B+Ev/Nac#9S;=;YQpg_6U`*kVY39xK,[/6Aj7:'1Bm-_1EYfa1+o&o4hp7KN_Q(OlIo@S%;jVdn0'1<Vc52=u`3^o-n1'g4v58Hj&6_t7$##?M)c<$bgQ_'SY((-xkA#Y(,p'H9rIVY-b,'%bCPF7.J<Up^,(dU1VY*5#WkTU>h19w,WQhLI)3S#f$2(eb,jr*b;3Vw]*7NH%$c4Vs,eD9>XW8?N]o+(*pgC%/72LV-u<Hp,3@e^9UB1J+ak9-TN/mhKPg+AJYd$MlvAF_jCK*.O-^(63adMT->W%iewS8W6m2rtCpo'RS1R84=@paTKt)>=%&1[)*vp'u+x,VrwN;&]kuO9JDbg=pO$J*.jVe;u'm0dr9l,<*wMK*Oe=g8lV_KEBFkO'oU]^=[-792#ok,)i]lR8qQ2oA8wcRCZ^7w/Njh;?.stX?Q1>S1q4Bn$)K1<-rGdO'$Wr.Lc.CG)$/*JL4tNR/,SVO3,aUw'DJN:)Ss;wGn9A32ijw%FL+Z0Fn.U9;reSq)bmI32U==5ALuG&#Vf1398/pVo1*c-(aY168o<`JsSbk-,1N;$>0:OUas(3:8Z972LSfF8eb=c-;>SPw7.6hn3m`9^Xkn(r.qS[0;T%&Qc=+STRxX'q1BNk3&*eu2;&8q$&x>Q#Q7^Tf+6<(d%ZVmj2bDi%.3L2n+4W'$PiDDG)g,r%+?,$@?uou5tSe2aN_AQU*<h`e-GI7)?OK2A.d7_c)?wQ5AS@DL3r#7fSkgl6-++D:'A,uq7SvlB$pcpH'q3n0#_%dY#xCpr-l<F0NR@-##FEV6NTF6##$l84N1w?AO>'IAOURQ##V^Fv-XFbGM7Fl(N<3DhLGF%q.1rC$#:T__&Pi68%0xi_&[qFJ(77j_&JWoF.V735&T,[R*:xFR*K5>>#`bW-?4Ne_&6Ne_&6Ne_&n`kr-#GJcM6X;uM6X;uM(.a..^2TkL%oR(#;u.T%fAr%4tJ8&><1=GHZ_+m9/#H1F^R#SC#*N=BA9(D?v[UiFY>>^8p,KKF.W]L29uLkLlu/+4T<XoIB&hx=T1PcDaB&;HH+-AFr?(m9HZV)FKS8JCw;SD=6[^/DZUL`EUDf]GGlG&>w$)F./^n3+rlo+DB;5sIYGNk+i1t-69Jg--0pao7Sm#K)pdHW&;LuDNH@H>#/X-TI(;P>#,Gc>#0Su>#4`1?#8lC?#<xU?#@.i?#D:%@#HF7@#LRI@#P_[@#Tkn@#Xw*A#]-=A#a9OA#d<F&#*;G##.GY##2Sl##6`($#:l:$#>xL$#B.`$#F:r$#JF.%#NR@%#R_R%#Vke%#Zww%#_-4&#3^Rh%Sflr-k'MS.o?.5/sWel/wpEM0%3'/1)K^f1-d>G21&v(35>V`39V7A4=onx4A1OY5EI0;6Ibgr6M$HS7Q<)58C5w,;WoA*#[%T*#`1g*#d=#+#hI5+#lUG+#pbY+#tnl+#x$),#&1;,#*=M,#.I`,#2Ur,#6b.-#;w[H#iQtA#m^0B#qjBB#uvTB##-hB#'9$C#+E6C#/QHC#3^ZC#7jmC#;v)D#?,<D#C8ND#GDaD#KPsD#O]/E#g1A5#KA*1#gC17#MGd;#8(02#L-d3#rWM4#Hga1#,<w0#T.j<#O#'2#CYN1#qa^:#_4m3#o@/=#eG8=#t8J5#`+78#4uI-#m3B2#SB[8#Q0@8#i[*9#iOn8#1Nm;#^sN9#qh<9#:=x-#P;K2#$%X9#bC+.#Rg;<#mN=.#MTF.#RZO.#2?)4#Y#(/#[)1/#b;L/#dAU/#0Sv;#lY$0#n`-0#sf60#(F24#wrH0#%/e0#TmD<#%JSMFove:CTBEXI:<eh2g)B,3h2^G3i;#d3jD>)4kMYD4lVu`4m`:&5niUA5@(A5BA1]PBB:xlBCC=2CDLXMCEUtiCf&0g2'tN?PGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CP-qekC`.9kEg^+F$kwViFJTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5o,^<-28ZI'O?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xp;7q-#lLYI:xvD=#";

		public static string nk_custom_cursor_data =
			"..-         -XXXXXXX-    X    -           X           -XXXXXXX          -          XXXXXXX..-         -X.....X-   X.X   -          X.X          -X.....X          -          X.....X---         -XXX.XXX-  X...X  -         X...X         -X....X           -           X....XX           -  X.X  - X.....X -        X.....X        -X...X            -            X...XXX          -  X.X  -X.......X-       X.......X       -X..X.X           -           X.X..XX.X         -  X.X  -XXXX.XXXX-       XXXX.XXXX       -X.X X.X          -          X.X X.XX..X        -  X.X  -   X.X   -          X.X          -XX   X.X         -         X.X   XXX...X       -  X.X  -   X.X   -    XX    X.X    XX    -      X.X        -        X.X      X....X      -  X.X  -   X.X   -   X.X    X.X    X.X   -       X.X       -       X.X       X.....X     -  X.X  -   X.X   -  X..X    X.X    X..X  -        X.X      -      X.X        X......X    -  X.X  -   X.X   - X...XXXXXX.XXXXXX...X -         X.X   XX-XX   X.X         X.......X   -  X.X  -   X.X   -X.....................X-          X.X X.X-X.X X.X          X........X  -  X.X  -   X.X   - X...XXXXXX.XXXXXX...X -           X.X..X-X..X.X           X.........X -XXX.XXX-   X.X   -  X..X    X.X    X..X  -            X...X-X...X            X..........X-X.....X-   X.X   -   X.X    X.X    X.X   -           X....X-X....X           X......XXXXX-XXXXXXX-   X.X   -    XX    X.X    XX    -          X.....X-X.....X          X...X..X    ---------   X.X   -          X.X          -          XXXXXXX-XXXXXXX          X..X X..X   -       -XXXX.XXXX-       XXXX.XXXX       ------------------------------------X.X  X..X   -       -X.......X-       X.......X       -    XX           XX    -           XX    X..X  -       - X.....X -        X.....X        -   X.X           X.X   -                 X..X          -  X...X  -         X...X         -  X..X           X..X  -                  XX           -   X.X   -          X.X          - X...XXXXXXXXXXXXX...X -           ------------        -    X    -           X           -X.....................X-                               ----------------------------------- X...XXXXXXXXXXXXX...X -                                                                 -  X..X           X..X  -                                                                 -   X.X           X.X   -                                                                 -    XX           XX    -           ";

		private static PinnedArray<uint> ranges = new PinnedArray<uint>(new uint[] {0x0020, 0x00FF, 0});

		[StructLayout(LayoutKind.Explicit)]
		public struct nk_handle
		{
			[FieldOffset(0)] public void* ptr;

			[FieldOffset(0)] public int id;
		}

		public class nk_user_font
		{
			public nk_handle userdata;
			public float height;
			public nk_handle texture;

			public NkTextWidthDelegate width;
			public NkQueryFontGlyphDelegate query;
		}

		public class nk_font
		{
			public nk_font next;
			public nk_user_font handle = new nk_user_font();
			public nk_baked_font info = new nk_baked_font();
			public float scale;
			public nk_font_glyph* glyphs;
			public nk_font_glyph* fallback;
			public char fallback_codepoint;
			public nk_handle texture = new nk_handle();
			public nk_font_config config;

			public float text_width(nk_handle handle, float height, char* s, int length)
			{
				return nk_font_text_width(this, height, s, length);
			}

			public void query_font_glyph(nk_handle handle, float height, nk_user_font_glyph* glyph, char codepoint,
				char next_codepoint)
			{
				nk_font_query_font_glyph(this, height, glyph, codepoint, next_codepoint);
			}
		}

		public class nk_clipboard
		{
			public nk_handle userdata;
			public NkPluginPaste paste;
			public NkPluginCopy copy;
		}

		public class nk_keyboard
		{
			public PinnedArray<nk_key> keys = new PinnedArray<nk_key>(new nk_key[NK_KEY_MAX]);
			public PinnedArray<char> text = new PinnedArray<char>(new char[16]);
			public int text_len;
		}

		public class nk_mouse
		{
			public PinnedArray<nk_mouse_button> buttons = new PinnedArray<nk_mouse_button>(new nk_mouse_button[NK_BUTTON_MAX]);
			public nk_vec2 pos;
			public nk_vec2 prev;
			public nk_vec2 delta;
			public nk_vec2 scroll_delta;
			public byte grab;
			public byte grabbed;
			public byte ungrab;
		}

		public class nk_buffer
		{
			public nk_buffer_marker[] marker = new nk_buffer_marker[2];
			public int type;
			public nk_memory memory;
			public float grow_factor;
			public ulong allocated;
			public ulong needed;
			public ulong calls;
			public ulong size;
		}

		public class nk_context
		{
			public nk_input input = new nk_input();
			public nk_style style = new nk_style();
			public nk_buffer memory = new nk_buffer();
			public nk_clipboard clip = new nk_clipboard();
			public uint last_widget_state;
			public int button_behavior;
			public nk_configuration_stacks stacks = new nk_configuration_stacks();
			public float delta_time_seconds;
			public nk_draw_list draw_list = new nk_draw_list();
			public nk_handle userdata = new nk_handle();
			public nk_text_edit text_edit = new nk_text_edit();
			public nk_command_buffer overlay = new nk_command_buffer();
			public int build;
			public nk_window begin;
			public nk_window end;
			public nk_window active;
			public nk_window current;
			public uint count;
			public uint seq;
		}

		public class nk_panel
		{
			public int type;
			public uint flags;
			public nk_rect bounds = new nk_rect();
			public nk_scroll offset;
			public float at_x;
			public float at_y;
			public float max_x;
			public float footer_height;
			public float header_height;
			public float border;
			public uint has_scrolling;
			public nk_rect clip = new nk_rect();
			public nk_menu_state menu = new nk_menu_state();
			public nk_row_layout row = new nk_row_layout();
			public nk_chart chart = new nk_chart();
			public nk_command_buffer buffer;
			public nk_panel parent;
		}

		public class nk_window
		{
			public uint seq;
			public uint name;
			public PinnedArray<char> name_string = new PinnedArray<char>(64);
			public uint flags;
			public nk_rect bounds = new nk_rect();
			public nk_scroll scrollbar = new nk_scroll();
			public nk_command_buffer buffer = new nk_command_buffer();
			public nk_panel layout;
			public float scrollbar_hiding_timer;
			public nk_property_state property = new nk_property_state();
			public nk_popup_state popup = new nk_popup_state();
			public nk_edit_state edit = new nk_edit_state();
			public uint scrolled;
			public nk_table tables;
			public uint table_count;
			public nk_window next;
			public nk_window prev;
			public nk_window parent;
		}

		public class nk_draw_list
		{
			public nk_rect clip_rect;
			public nk_vec2[] circle_vtx = new nk_vec2[12];
			public nk_convert_config config;
			public nk_buffer buffer;
			public nk_buffer vertices;
			public nk_buffer elements;
			public uint element_count;
			public uint vertex_count;
			public uint cmd_count;
			public ulong cmd_offset;
			public uint path_count;
			public uint path_offset;
			public int line_AA;
			public int shape_AA;
			public nk_handle userdata;
		}

		public class nk_style_item_data
		{
			public nk_image image;
			public nk_color color;
		}

		public class nk_style_item
		{
			public int type;
			public nk_style_item_data data = new nk_style_item_data();
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct nk_rp_context
		{
			public int width;
			public int height;
			public int align;
			public int init_mode;
			public int heuristic;
			public int num_nodes;
			public nk_rp_node* active_head;
			public nk_rp_node* free_head;
			public nk_rp_node extra_0, extra_1;
		}

		public class nk_font_atlas
		{
			public void* pixel;
			public int tex_width;
			public int tex_height;
			public nk_recti custom;
			public nk_cursor[] cursors = new nk_cursor[NK_CURSOR_COUNT];
			public int glyph_count;
			public nk_font_glyph* glyphs;
			public nk_font default_font;
			public nk_font fonts;
			public nk_font_config config;
			public int font_num;

			public nk_font_atlas()
			{
				for (var i = 0; i < cursors.Length; ++i)
				{
					cursors[i] = new nk_cursor();
				}
			}
		}

		public class nk_text_undo_state
		{
			public PinnedArray<nk_text_undo_record> undo_rec = new PinnedArray<nk_text_undo_record>(new nk_text_undo_record[99]);
			public PinnedArray<uint> undo_char = new PinnedArray<uint>(new uint[999]);
			public short undo_point;
			public short redo_point;
			public short undo_char_point;
			public short redo_char_point;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct nk_property
		{
			[FieldOffset(0)] public int i;

			[FieldOffset(0)] public float f;

			[FieldOffset(0)] public double d;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct nk_property_variant
		{
			public int kind;
			public nk_property value;
			public nk_property min_value;
			public nk_property max_value;
			public nk_property step;
		}

		public class nk_style
		{
			public nk_user_font font;
			public nk_cursor[] cursors = new nk_cursor[NK_CURSOR_COUNT];
			public nk_cursor cursor_active;
			public nk_cursor cursor_last;
			public int cursor_visible;
			public nk_style_text text = new nk_style_text();
			public nk_style_button button = new nk_style_button();
			public nk_style_button contextual_button = new nk_style_button();
			public nk_style_button menu_button = new nk_style_button();
			public nk_style_toggle option = new nk_style_toggle();
			public nk_style_toggle checkbox = new nk_style_toggle();
			public nk_style_selectable selectable = new nk_style_selectable();
			public nk_style_slider slider = new nk_style_slider();
			public nk_style_progress progress = new nk_style_progress();
			public nk_style_property property = new nk_style_property();
			public nk_style_edit edit = new nk_style_edit();
			public nk_style_chart chart = new nk_style_chart();
			public nk_style_scrollbar scrollh = new nk_style_scrollbar();
			public nk_style_scrollbar scrollv = new nk_style_scrollbar();
			public nk_style_tab tab = new nk_style_tab();
			public nk_style_combo combo = new nk_style_combo();
			public nk_style_window window = new nk_style_window();
		}

		public class nk_chart
		{
			public int slot;
			public float x;
			public float y;
			public float w;
			public float h;
			public nk_chart_slot[] slots = new nk_chart_slot[4];
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct nk_inv_sqrt_union
		{
			[FieldOffset(0)] public uint i;

			[FieldOffset(0)] public float f;
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct nk_murmur_hash_union
		{
			[FieldOffset(0)] public uint* i;

			[FieldOffset(0)] public byte* b;

			public nk_murmur_hash_union(void* ptr)
			{
				i = (uint*) ptr;
				b = (byte*) ptr;
			}
		}

		public class nk_command_base
		{
			public nk_command header;
			public nk_handle userdata;
			public nk_command_base next;
		}

		public class nk_command_scissor : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
		}

		public class nk_command_line : nk_command_base
		{
			public ushort line_thickness;
			public nk_vec2i begin = new nk_vec2i();
			public nk_vec2i end = new nk_vec2i();
			public nk_color color = new nk_color();
		}

		public class nk_command_curve : nk_command_base
		{
			public ushort line_thickness;
			public nk_vec2i begin = new nk_vec2i();
			public nk_vec2i end = new nk_vec2i();
			public nk_vec2i ctrl_0 = new nk_vec2i();
			public nk_vec2i ctrl_1 = new nk_vec2i();
			public nk_color color = new nk_color();
		}

		public class nk_command_rect : nk_command_base
		{
			public ushort rounding;
			public ushort line_thickness;
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_color color = new nk_color();
		}

		public class nk_command_rect_filled : nk_command_base
		{
			public ushort rounding;
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_color color = new nk_color();
		}

		public class nk_command_rect_multi_color : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_color left = new nk_color();
			public nk_color top = new nk_color();
			public nk_color bottom = new nk_color();
			public nk_color right = new nk_color();
		}

		public class nk_command_triangle : nk_command_base
		{
			public ushort line_thickness;
			public nk_vec2i a = new nk_vec2i();
			public nk_vec2i b = new nk_vec2i();
			public nk_vec2i c = new nk_vec2i();
			public nk_color color = new nk_color();
		}

		public class nk_command_triangle_filled : nk_command_base
		{
			public nk_vec2i a = new nk_vec2i();
			public nk_vec2i b = new nk_vec2i();
			public nk_vec2i c = new nk_vec2i();
			public nk_color color = new nk_color();
		}

		public class nk_command_circle : nk_command_base
		{
			public short x;
			public short y;
			public ushort line_thickness;
			public ushort w;
			public ushort h;
			public nk_color color = new nk_color();
		}

		public class nk_command_circle_filled : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_color color = new nk_color();
		}

		public class nk_command_arc : nk_command_base
		{
			public short cx;
			public short cy;
			public ushort r;
			public ushort line_thickness;
			public PinnedArray<float> a = new PinnedArray<float>(2);
			public nk_color color = new nk_color();
		}

		public class nk_command_arc_filled : nk_command_base
		{
			public short cx;
			public short cy;
			public ushort r;
			public PinnedArray<float> a = new PinnedArray<float>(2);
			public nk_color color = new nk_color();
		}

		public class nk_command_polygon : nk_command_base
		{
			public nk_color color;
			public ushort line_thickness;
			public ushort point_count;
			public nk_vec2i[] points;
		}

		public class nk_command_polygon_filled : nk_command_base
		{
			public nk_color color;
			public ushort point_count;
			public nk_vec2i[] points;
		}

		public class nk_command_polyline : nk_command_base
		{
			public nk_color color;
			public ushort line_thickness;
			public ushort point_count;
			public nk_vec2i[] points;
		}

		public class nk_command_image : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_image img = new nk_image();
			public nk_color col = new nk_color();
		}

		public class nk_command_text : nk_command_base
		{
			public nk_user_font font;
			public nk_color background;
			public nk_color foreground;
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public float height;
			public char* _string_;
			public int length;
		}

		public class nk_command_custom : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_handle callback_data;
			public NkCommandCustomCallback callback;
		}

		public class nk_command_buffer
		{
			private readonly List<nk_command_base> _commands = new List<nk_command_base>();

			public List<nk_command_base> commands
			{
				get { return _commands; }
			}

			public nk_command_base begin
			{
				get { return _commands[0]; }
			}

			public nk_command_base last
			{
				get { return _commands[_commands.Count - 1]; }
			}

			public nk_rect clip;
			public int use_clipping;
			public nk_handle userdata = new nk_handle();
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct nk_config_stack_button_behavior_element
		{
			public int old_value;
		}

		public class nk_convert_config
		{
			public float global_alpha;
			public int line_AA;
			public int shape_AA;
			public uint circle_segment_count;
			public uint arc_segment_count;
			public uint curve_segment_count;
			public nk_draw_null_texture _null_ = new nk_draw_null_texture();
			public nk_draw_vertex_layout_element[] vertex_layout;
			public ulong vertex_size;
			public ulong vertex_alignment;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct nk_user_font_glyph
		{
			public fixed float uv_x [2];
			public fixed float uv_y [2];
			public nk_vec2 offset;
			public float width;
			public float height;
			public float xadvance;
		}


		private static readonly Func<object>[] _commandCreators =
		{
			null,
			() => nk_create_command<nk_command_scissor>(),
			() => nk_create_command<nk_command_line>(),
			() => nk_create_command<nk_command_curve>(),
			() => nk_create_command<nk_command_rect>(),
			() => nk_create_command<nk_command_rect_filled>(),
			() => nk_create_command<nk_command_rect_multi_color>(),
			() => nk_create_command<nk_command_circle>(),
			() => nk_create_command<nk_command_circle_filled>(),
			() => nk_create_command<nk_command_arc>(),
			() => nk_create_command<nk_command_arc_filled>(),
			() => nk_create_command<nk_command_triangle>(),
			() => nk_create_command<nk_command_triangle_filled>(),
			() => nk_create_command<nk_command_polygon>(),
			() => nk_create_command<nk_command_polygon_filled>(),
			() => nk_create_command<nk_command_polyline>(),
			() => nk_create_command<nk_command_text>(),
			() => nk_create_command<nk_command_image>(),
			() => nk_create_command<nk_command_custom>()
		};

		private static object nk_create_command<T>() where T : new()
		{
			return new T();
		}

		public static void nk_command_buffer_init(nk_command_buffer cmdbuf, int clip)
		{
			cmdbuf.use_clipping = clip;
			cmdbuf.commands.Clear();
		}

		public static void nk_command_buffer_reset(nk_command_buffer buffer)
		{
			if (buffer == null) return;
			buffer.commands.Clear();
			buffer.clip = nk_null_rect;
		}

		public static nk_command_base nk_command_buffer_push(nk_command_buffer b, int t)
		{
			if (b == null || t < 0 || t >= _commandCreators.Length || _commandCreators[t] == null) return null;

			var creator = _commandCreators[t];

			var command = (nk_command_base) creator();

			command.header = new nk_command
			{
				type = t
			};

			b.commands.Add(command);

			return command;
		}

		public static nk_window nk__begin(nk_context ctx)
		{
			if (ctx == null || ctx.count == 0) return null;
			if (ctx.build == 0)
			{
				nk_build(ctx);
				ctx.build = nk_true;
			}

			var iter = ctx.begin;
			while ((iter != null) &&
			       ((iter.buffer.commands.Count == 0) || (iter.flags & NK_WINDOW_HIDDEN) != 0 || (iter.seq != ctx.seq)))
			{
				iter = iter.next;
			}

			return iter;
		}


		public static void nk_build(nk_context ctx)
		{
			if (ctx.style.cursor_active == null) ctx.style.cursor_active = ctx.style.cursors[NK_CURSOR_ARROW];
			if ((ctx.style.cursor_active != null) && (ctx.input.mouse.grabbed == 0) && ((ctx.style.cursor_visible) != 0))
			{
				var mouse_bounds = new nk_rect();
				var cursor = ctx.style.cursor_active;
				nk_command_buffer_init(ctx.overlay, NK_CLIPPING_OFF);
				nk_start_buffer(ctx, ctx.overlay);
				mouse_bounds.x = ctx.input.mouse.pos.x - cursor.offset.x;
				mouse_bounds.y = ctx.input.mouse.pos.y - cursor.offset.y;
				mouse_bounds.w = cursor.size.x;
				mouse_bounds.h = cursor.size.y;
				nk_draw_image(ctx.overlay, mouse_bounds, cursor.img, nk_white);
				nk_finish_buffer(ctx, ctx.overlay);
			}

			var it = ctx.begin;
			nk_command_base cmd = null;
			for (; it != null;)
			{
				var next = it.next;
				if (((it.flags & NK_WINDOW_HIDDEN) != 0) || (it.seq != ctx.seq))
					goto cont;
				cmd = it.buffer.last;

				while ((next != null) &&
				       ((next.buffer.last == next.buffer.begin) || ((next.flags & NK_WINDOW_HIDDEN) != 0)))
				{
					next = next.next;
				}
				if (next != null) cmd.next = next.buffer.begin;
				cont:
				it = next;
			}
			/*it = ctx.begin;

			while (it != null)
			{
				nk_window _next_ = it.next;
				nk_popup_buffer buf;

				if (it.popup.buf.active == 0) goto skip;
				buf = it.popup.buf;
				cmd.next = buf.begin;
				cmd = ((nk_command*) ((void*) ((buffer) + (buf->last))));
				buf->active = (int) (nk_false);
				skip:
				;
				it = _next_;
			}
			if ((cmd) != null)
			{
				if (ctx.overlay.end != ctx.overlay.begin) cmd->_next_ = (ulong) (ctx.overlay.begin);
				else cmd->_next_ = (ulong) (ctx.memory.allocated);
			}*/
		}



		public static float nk_inv_sqrt(float number)
		{
			var threehalfs = 1.5f;
			var conv = new nk_inv_sqrt_union
			{
				i = 0,
			};
			conv.f = number;
			var x2 = number*0.5f;
			conv.i = 0x5f375A84 - (conv.i >> 1);
			conv.f = conv.f*(threehalfs - (x2*conv.f*conv.f));
			return conv.f;
		}

		public static int nk_utf_decode(char* c, int pos, char* u, int clen)
		{
			*u = c[pos];

			return 1;
		}

		public static int nk_utf_decode(char* c, char* u, int clen)
		{
			return nk_utf_decode(c, 0, u, clen);
		}

		public static int nk_utf_encode(char c, char* u, int clen)
		{
			*u = c;

			return 1;
		}

		public static int nk_utf_len(char* str, int len)
		{
			return len;
		}

		public static void nk_textedit_text(nk_text_edit state, string text, int total_len)
		{
			fixed (char* p = text)
			{
				nk_textedit_text(state, text, total_len);
			}
		}

		public static string nk_style_get_color_by_name(int c)
		{
			return nk_color_names[c];
		}

		public static int nk_init_fixed(nk_context ctx, void* memory, ulong size, nk_user_font font)
		{
			if (memory == null) return 0;
			nk_setup(ctx, font);
			nk_buffer_init_fixed(ctx.memory, memory, size);
			return 1;
		}

		public static void nk_buffer_init_default(nk_buffer buffer)
		{
			nk_buffer_init(buffer, 4*1024);
		}

		public static int nk_init(nk_context ctx, nk_user_font font)
		{
			nk_setup(ctx, font);
			nk_buffer_init(ctx.memory, 4*1024);
			return 1;
		}

		public static void nk_free(nk_context ctx)
		{
			if (ctx == null) return;
			nk_buffer_free(ctx.memory);

			ctx.seq = 0;
			ctx.build = 0;
			ctx.begin = null;
			ctx.end = null;
			ctx.active = null;
			ctx.current = null;
			ctx.count = 0;
		}

		public static nk_table nk_create_table(nk_context ctx)
		{
			var result = new nk_table();

			return result;
		}

		public static nk_window nk_create_window(nk_context ctx)
		{
			var result = new nk_window {seq = ctx.seq};

			return result;
		}

		public static void nk_free_window(nk_context ctx, nk_window win)
		{
			nk_table it = win.tables;
			if (win.popup.win != null)
			{
				nk_free_window(ctx, win.popup.win);
				win.popup.win = null;
			}

			win.next = null;
			win.prev = null;
			while (it != null)
			{
				var n = it.next;
				nk_remove_table(win, it);
				if (it == win.tables) win.tables = n;
				it = n;
			}
		}

		public static nk_panel nk_create_panel(nk_context ctx)
		{
			var result = new nk_panel();

			return result;
		}

		public static void nk_free_panel(nk_context ctx, nk_panel panel)
		{
		}

		public static int nk_popup_begin(nk_context ctx, int type, string title, uint flags, nk_rect rect)
		{
			fixed (char* ptr = title)
			{
				return nk_popup_begin(ctx, type, ptr, flags, rect);
			}
		}

		public static void nk_str_init_default(nk_str str)
		{
			nk_buffer_init(str.buffer, 32);
			str.len = 0;
		}

		public static void nk_str_init(nk_str str, ulong size)
		{
			nk_buffer_init(str.buffer, size);
			str.len = 0;
		}

		public static int nk_init_default(nk_context ctx, nk_user_font font)
		{
			return nk_init(ctx, font);
		}

		public static nk_font nk_font_atlas_add_default(nk_font_atlas atlas, float pixel_height, nk_font_config config)
		{
			fixed (char* ptr = nk_proggy_clean_ttf_compressed_data_base85)
			{
				return nk_font_atlas_add_compressed_base85(atlas, ptr, pixel_height, config);
			}
		}

		public static void nk_property_(nk_context ctx, char* name, nk_property_variant* variant, float inc_per_pixel,
			int filter)
		{
			var bounds = new nk_rect();
			uint hash;
			char* dummy_buffer = stackalloc char[64];
			var dummy_state = NK_PROPERTY_DEFAULT;
			var dummy_length = 0;
			var dummy_cursor = 0;
			var dummy_select_begin = 0;
			var dummy_select_end = 0;
			if ((ctx == null) || (ctx.current == null) || (ctx.current.layout == null)) return;
			var win = ctx.current;
			var layout = win.layout;
			var style = ctx.style;
			var s = nk_widget(&bounds, ctx);
			if (s == 0) return;
			if (name[0] == '#')
			{
				hash = nk_murmur_hash(name, nk_strlen(name), win.property.seq++);
				name++;
			}
			else hash = nk_murmur_hash(name, nk_strlen(name), 42);

			var _in_ = ((s == NK_WIDGET_ROM) && (win.property.active == 0)) || ((layout.flags & NK_WINDOW_ROM) != 0)
				? null
				: ctx.input;

			int old_state, state;
			char* buffer;
			int len, cursor, select_begin, select_end;
			if ((win.property.active != 0) && (hash == win.property.name))
			{
				old_state = win.property.state;
				nk_do_property(ref ctx.last_widget_state, win.buffer, bounds, name, variant, inc_per_pixel,
					win.property.buffer, ref win.property.length, ref win.property.state, ref win.property.cursor,
					ref win.property.select_start, ref win.property.select_end, style.property, filter, _in_, style.font,
					ctx.text_edit, ctx.button_behavior);
				state = win.property.state;
				buffer = win.property.buffer;
				len = win.property.length;
				cursor = win.property.cursor;
				select_begin = win.property.select_start;
				select_end = win.property.select_end;
			}
			else
			{
				old_state = dummy_state;
				nk_do_property(ref ctx.last_widget_state, win.buffer, bounds, name, variant, inc_per_pixel,
					dummy_buffer, ref dummy_length, ref dummy_state, ref dummy_cursor,
					ref dummy_select_begin, ref dummy_select_end, style.property, filter, _in_, style.font,
					ctx.text_edit, ctx.button_behavior);
				state = dummy_state;
				buffer = dummy_buffer;
				len = dummy_length;
				cursor = dummy_cursor;
				select_begin = dummy_select_begin;
				select_end = dummy_select_end;
			}

			ctx.text_edit.clip = ctx.clip;
			if ((_in_ != null) && (state != NK_PROPERTY_DEFAULT) && (win.property.active == 0))
			{
				win.property.active = 1;
				nk_memcopy(win.property.buffer, buffer, (ulong) len);
				win.property.length = len;
				win.property.cursor = cursor;
				win.property.state = state;
				win.property.name = hash;
				win.property.select_start = select_begin;
				win.property.select_end = select_end;
				if (state == NK_PROPERTY_DRAG)
				{
					ctx.input.mouse.grab = nk_true;
					ctx.input.mouse.grabbed = nk_true;
				}
			}

			if ((state == NK_PROPERTY_DEFAULT) && (old_state != NK_PROPERTY_DEFAULT))
			{
				if (old_state == NK_PROPERTY_DRAG)
				{
					ctx.input.mouse.grab = nk_false;
					ctx.input.mouse.grabbed = nk_false;
					ctx.input.mouse.ungrab = nk_true;
				}
				win.property.select_start = 0;
				win.property.select_end = 0;
				win.property.active = 0;
			}
		}

		public static void nk_stroke_polygon(nk_command_buffer b, float* points, int point_count, float line_thickness,
			nk_color col)
		{
			if ((b == null) || (col.a == 0) || (line_thickness <= 0)) return;
			var cmd = (nk_command_polygon) nk_command_buffer_push(b, NK_COMMAND_POLYGON);
			if (cmd == null) return;
			cmd.color = col;
			cmd.line_thickness = (ushort) line_thickness;
			cmd.point_count = (ushort) point_count;
			cmd.points = new nk_vec2i[point_count];
			for (var i = 0; i < point_count; ++i)
			{
				cmd.points[i].x = (short) points[i*2];
				cmd.points[i].y = (short) points[i*2 + 1];
			}
		}

		public static void nk_fill_polygon(nk_command_buffer b, float* points, int point_count, nk_color col)
		{
			nk_command_polygon_filled cmd;
			if ((b == null) || (col.a == 0)) return;
			cmd = (nk_command_polygon_filled) nk_command_buffer_push(b, NK_COMMAND_POLYGON_FILLED);
			if (cmd == null) return;
			cmd.color = col;
			cmd.point_count = (ushort) point_count;
			cmd.points = new nk_vec2i[point_count];
			for (var i = 0; i < point_count; ++i)
			{
				cmd.points[i].x = (short) points[i*2 + 0];
				cmd.points[i].y = (short) points[i*2 + 1];
			}
		}

		public static void nk_stroke_polyline(nk_command_buffer b, float* points, int point_count, float line_thickness,
			nk_color col)
		{
			if ((b == null) || (col.a == 0) || (line_thickness <= 0)) return;
			var cmd = (nk_command_polyline) nk_command_buffer_push(b, NK_COMMAND_POLYLINE);
			if (cmd == null) return;
			cmd.color = col;
			cmd.point_count = (ushort) point_count;
			cmd.line_thickness = (ushort) line_thickness;
			cmd.points = new nk_vec2i[point_count];
			for (var i = 0; i < point_count; ++i)
			{
				cmd.points[i].x = (short) points[i*2];
				cmd.points[i].y = (short) points[i*2 + 1];
			}
		}

		public static nk_font_config nk_font_config_clone(nk_font_config src)
		{
			return new nk_font_config
			{
				next = src.next,
				ttf_blob = src.ttf_blob,
				ttf_size = src.ttf_size,
				ttf_data_owned_by_atlas = src.ttf_data_owned_by_atlas,
				merge_mode = src.merge_mode,
				pixel_snap = src.pixel_snap,
				oversample_v = src.oversample_v,
				oversample_h = src.oversample_h,
				padding = src.padding,
				size = src.size,
				coord_type = src.coord_type,
				spacing = src.spacing,
				range = src.range,
				font = src.font,
				fallback_glyph = src.fallback_glyph,
				n = src.n,
				p = src.p,
			};
		}
	}
}