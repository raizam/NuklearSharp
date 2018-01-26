using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public static unsafe partial class Nuklear
	{
		public delegate float NkTextWidthDelegate(float height, StringView text);

		public delegate void NkQueryFontGlyphDelegate(
			float height, nk_user_font_glyph* glyph, char codepoint, char next_codepoint);

		public delegate void NkCommandCustomCallback(
			nk_draw_list list, short x, short y, ushort w, ushort h, nk_handle callback_data);

		public delegate void NkPluginPaste(nk_handle handle, nk_text_edit text_edit);

		public delegate void NkPluginCopy(nk_handle handle, StringView text);

		public delegate void NkDrawNotify(nk_command_buffer buffer, nk_handle handle);

		public delegate int NkPluginFilter(nk_text_edit text_edit, char unicode);

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


		public static string nk_proggy_clean_ttf_compressed_data_base85 =
			"7])#######hV0qs'/###[),##/l:$#Q6>##5[n42>c-TH`->>#/e>11NNV=Bv(*:.F?uu#(gRU.o0XGH`$vhLG1hxt9?W`#,5LsCp#-i>.r$<$6pD>Lb';9Crc6tgXmKVeU2cD4Eo3R/2*>]b(MC;$jPfY.;h^`IWM9<Lh2TlS+f-s$o6Q<BWH`YiU.xfLq$N;$0iR/GX:U(jcW2p/W*q?-qmnUCI;jHSAiFWM.R*kU@C=GH?a9wp8f$e.-4^Qg1)Q-GL(lf(r/7GrRgwV%MS=C#`8ND>Qo#t'X#(v#Y9w0#1D$CIf;W'#pWUPXOuxXuU(H9M(1<q-UE31#^-V'8IRUo7Qf./L>=Ke$$'5F%)]0^#0X@U.a<r:QLtFsLcL6##lOj)#.Y5<-R&KgLwqJfLgN&;Q?gI^#DY2uLi@^rMl9t=cWq6##weg>$FBjVQTSDgEKnIS7EM9>ZY9w0#L;>>#Mx&4Mvt//L[MkA#W@lK.N'[0#7RL_&#w+F%HtG9M#XL`N&.,GM4Pg;-<nLENhvx>-VsM.M0rJfLH2eTM`*oJMHRC`NkfimM2J,W-jXS:)r0wK#@Fge$U>`w'N7G#$#fB#$E^$#:9:hk+eOe--6x)F7*E%?76%^GMHePW-Z5l'&GiF#$956:rS?dA#fiK:)Yr+`&#0j@'DbG&#^$PG.Ll+DNa<XCMKEV*N)LN/N*b=%Q6pia-Xg8I$<MR&,VdJe$<(7G;Ckl'&hF;;$<_=X(b.RS%%)###MPBuuE1V:v&cX&#2m#(&cV]`k9OhLMbn%s$G2,B$BfD3X*sp5#l,$R#]x_X1xKX%b5U*[r5iMfUo9U`N99hG)tm+/Us9pG)XPu`<0s-)WTt(gCRxIg(%6sfh=ktMKn3j)<6<b5Sk_/0(^]AaN#(p/L>&VZ>1i%h1S9u5o@YaaW$e+b<TWFn/Z:Oh(Cx2$lNEoN^e)#CFY@@I;BOQ*sRwZtZxRcU7uW6CXow0i(?$Q[cjOd[P4d)]>ROPOpxTO7Stwi1::iB1q)C_=dV26J;2,]7op$]uQr@_V7$q^%lQwtuHY]=DX,n3L#0PHDO4f9>dC@O>HBuKPpP*E,N+b3L#lpR/MrTEH.IAQk.a>D[.e;mc.x]Ip.PH^'/aqUO/$1WxLoW0[iLA<QT;5HKD+@qQ'NQ(3_PLhE48R.qAPSwQ0/WK?Z,[x?-J;jQTWA0X@KJ(_Y8N-:/M74:/-ZpKrUss?d#dZq]DAbkU*JqkL+nwX@@47`5>w=4h(9.`GCRUxHPeR`5Mjol(dUWxZa(>STrPkrJiWx`5U7F#.g*jrohGg`cg:lSTvEY/EV_7H4Q9[Z%cnv;JQYZ5q.l7Zeas:HOIZOB?G<Nald$qs]@]L<J7bR*>gv:[7MI2k).'2($5FNP&EQ(,)U]W]+fh18.vsai00);D3@4ku5P?DP8aJt+;qUM]=+b'8@;mViBKx0DE[-auGl8:PJ&Dj+M6OC]O^((##]`0i)drT;-7X`=-H3[igUnPG-NZlo.#k@h#=Ork$m>a>$-?Tm$UV(?#P6YY#'/###xe7q.73rI3*pP/$1>s9)W,JrM7SN]'/4C#v$U`0#V.[0>xQsH$fEmPMgY2u7Kh(G%siIfLSoS+MK2eTM$=5,M8p`A.;_R%#u[K#$x4AG8.kK/HSB==-'Ie/QTtG?-.*^N-4B/ZM_3YlQC7(p7q)&](`6_c)$/*JL(L-^(]$wIM`dPtOdGA,U3:w2M-0<q-]L_?^)1vw'.,MRsqVr.L;aN&#/EgJ)PBc[-f>+WomX2u7lqM2iEumMTcsF?-aT=Z-97UEnXglEn1K-bnEO`guFt(c%=;Am_Qs@jLooI&NX;]0#j4#F14;gl8-GQpgwhrq8'=l_f-b49'UOqkLu7-##oDY2L(te+Mch&gLYtJ,MEtJfLh'x'M=$CS-ZZ%P]8bZ>#S?YY#%Q&q'3^Fw&?D)UDNrocM3A76//oL?#h7gl85[qW/NDOk%16ij;+:1a'iNIdb-ou8.P*w,v5#EI$TWS>Pot-R*H'-SEpA:g)f+O$%%`kA#G=8RMmG1&O`>to8bC]T&$,n.LoO>29sp3dt-52U%VM#q7'DHpg+#Z9%H[K<L%a2E-grWVM3@2=-k22tL]4$##6We'8UJCKE[d_=%wI;'6X-GsLX4j^SgJ$##R*w,vP3wK#iiW&#*h^D&R?jp7+/u&#(AP##XU8c$fSYW-J95_-Dp[g9wcO&#M-h1OcJlc-*vpw0xUX&#OQFKNX@QI'IoPp7nb,QU//MQ&ZDkKP)X<WSVL(68uVl&#c'[0#(s1X&xm$Y%B7*K:eDA323j998GXbA#pwMs-jgD$9QISB-A_(aN4xoFM^@C58D0+Q+q3n0#3U1InDjF682-SjMXJK)(h$hxua_K]ul92%'BOU&#BRRh-slg8KDlr:%L71Ka:.A;%YULjDPmL<LYs8i#XwJOYaKPKc1h:'9Ke,g)b),78=I39B;xiY$bgGw-&.Zi9InXDuYa%G*f2Bq7mn9^#p1vv%#(Wi-;/Z5ho;#2:;%d&#x9v68C5g?ntX0X)pT`;%pB3q7mgGN)3%(P8nTd5L7GeA-GL@+%J3u2:(Yf>et`e;)f#Km8&+DC$I46>#Kr]]u-[=99tts1.qb#q72g1WJO81q+eN'03'eM>&1XxY-caEnOj%2n8)),?ILR5^.Ibn<-X-Mq7[a82Lq:F&#ce+S9wsCK*x`569E8ew'He]h:sI[2LM$[guka3ZRd6:t%IG:;$%YiJ:Nq=?eAw;/:nnDq0(CYcMpG)qLN4$##&J<j$UpK<Q4a1]MupW^-sj_$%[HK%'F####QRZJ::Y3EGl4'@%FkiAOg#p[##O`gukTfBHagL<LHw%q&OV0##F=6/:chIm0@eCP8X]:kFI%hl8hgO@RcBhS-@Qb$%+m=hPDLg*%K8ln(wcf3/'DW-$.lR?n[nCH-eXOONTJlh:.RYF%3'p6sq:UIMA945&^HFS87@$EP2iG<-lCO$%c`uKGD3rC$x0BL8aFn--`ke%#HMP'vh1/R&O_J9'um,.<tx[@%wsJk&bUT2`0uMv7gg#qp/ij.L56'hl;.s5CUrxjOM7-##.l+Au'A&O:-T72L]P`&=;ctp'XScX*rU.>-XTt,%OVU4)S1+R-#dg0/Nn?Ku1^0f$B*P:Rowwm-`0PKjYDDM'3]d39VZHEl4,.j']Pk-M.h^&:0FACm$maq-&sgw0t7/6(^xtk%LuH88Fj-ekm>GA#_>568x6(OFRl-IZp`&b,_P'$M<Jnq79VsJW/mWS*PUiq76;]/NM_>hLbxfc$mj`,O;&%W2m`Zh:/)Uetw:aJ%]K9h:TcF]u_-Sj9,VK3M.*'&0D[Ca]J9gp8,kAW]%(?A%R$f<->Zts'^kn=-^@c4%-pY6qI%J%1IGxfLU9CP8cbPlXv);C=b),<2mOvP8up,UVf3839acAWAW-W?#ao/^#%KYo8fRULNd2.>%m]UK:n%r$'sw]J;5pAoO_#2mO3n,'=H5(etHg*`+RLgv>=4U8guD$I%D:W>-r5V*%j*W:Kvej.Lp$<M-SGZ':+Q_k+uvOSLiEo(<aD/K<CCc`'Lx>'?;++O'>()jLR-^u68PHm8ZFWe+ej8h:9r6L*0//c&iH&R8pRbA#Kjm%upV1g:a_#Ur7FuA#(tRh#.Y5K+@?3<-8m0$PEn;J:rh6?I6uG<-`wMU'ircp0LaE_OtlMb&1#6T.#FDKu#1Lw%u%+GM+X'e?YLfjM[VO0MbuFp7;>Q&#WIo)0@F%q7c#4XAXN-U&VB<HFF*qL($/V,;(kXZejWO`<[5??ewY(*9=%wDc;,u<'9t3W-(H1th3+G]ucQ]kLs7df($/*JL]@*t7Bu_G3_7mp7<iaQjO@.kLg;x3B0lqp7Hf,^Ze7-##@/c58Mo(3;knp0%)A7?-W+eI'o8)b<nKnw'Ho8C=Y>pqB>0ie&jhZ[?iLR@@_AvA-iQC(=ksRZRVp7`.=+NpBC%rh&3]R:8XDmE5^V8O(x<<aG/1N$#FX$0V5Y6x'aErI3I$7x%E`v<-BY,)%-?Psf*l?%C3.mM(=/M0:JxG'?7WhH%o'a<-80g0NBxoO(GH<dM]n.+%q@jH?f.UsJ2Ggs&4<-e47&Kl+f//9@`b+?.TeN_&B8Ss?v;^Trk;f#YvJkl&w$]>-+k?'(<S:68tq*WoDfZu';mM?8X[ma8W%*`-=;D.(nc7/;)g:T1=^J$&BRV(-lTmNB6xqB[@0*o.erM*<SWF]u2=st-*(6v>^](H.aREZSi,#1:[IXaZFOm<-ui#qUq2$##Ri;u75OK#(RtaW-K-F`S+cF]uN`-KMQ%rP/Xri.LRcB##=YL3BgM/3MD?@f&1'BW-)Ju<L25gl8uhVm1hL$##*8###'A3/LkKW+(^rWX?5W_8g)a(m&K8P>#bmmWCMkk&#TR`C,5d>g)F;t,4:@_l8G/5h4vUd%&%950:VXD'QdWoY-F$BtUwmfe$YqL'8(PWX(P?^@Po3$##`MSs?DWBZ/S>+4%>fX,VWv/w'KD`LP5IbH;rTV>n3cEK8U#bX]l-/V+^lj3;vlMb&[5YQ8#pekX9JP3XUC72L,,?+Ni&co7ApnO*5NK,((W-i:$,kp'UDAO(G0Sq7MVjJsbIu)'Z,*[>br5fX^:FPAWr-m2KgL<LUN098kTF&#lvo58=/vjDo;.;)Ka*hLR#/k=rKbxuV`>Q_nN6'8uTG&#1T5g)uLv:873UpTLgH+#FgpH'_o1780Ph8KmxQJ8#H72L4@768@Tm&Qh4CB/5OvmA&,Q&QbUoi$a_%3M01H)4x7I^&KQVgtFnV+;[Pc>[m4k//,]1?#`VY[Jr*3&&slRfLiVZJ:]?=K3Sw=[$=uRB?3xk48@aeg<Z'<$#4H)6,>e0jT6'N#(q%.O=?2S]u*(m<-V8J'(1)G][68hW$5'q[GC&5j`TE?m'esFGNRM)j,ffZ?-qx8;->g4t*:CIP/[Qap7/9'#(1sao7w-.qNUdkJ)tCF&#B^;xGvn2r9FEPFFFcL@.iFNkTve$m%#QvQS8U@)2Z+3K:AKM5isZ88+dKQ)W6>J%CL<KE>`.d*(B`-n8D9oK<Up]c$X$(,)M8Zt7/[rdkqTgl-0cuGMv'?>-XV1q['-5k'cAZ69e;D_?$ZPP&s^+7])$*$#@QYi9,5P&#9r+$%CE=68>K8r0=dSC%%(@p7.m7jilQ02'0-VWAg<a/'\0'3u.=4L$Y)6k/K:_[3=&jvL<L0C/2'v:^;-DIBW,B4E68:kZ;%?8(Q8BH=kO65BW?xSG&#@uU,DS*,?.+(o(#1vCS8#CHF>TlGW'b)Tq7VT9q^*^$$.:&N@@$&)WHtPm*5_rO0&e%K&#-30j(E4#'Zb.o/(Tpm$>K'f@[PvFl,hfINTNU6u'0pao7%XUp9]5.>%h`8_=VYbxuel.NTSsJfLacFu3B'lQSu/m6-Oqem8T+oE--$0a/k]uj9EwsG>%veR*hv^BFpQj:K'#SJ,sB-'#](j.Lg92rTw-*n%@/;39rrJF,l#qV%OrtBeC6/,;qB3ebNW[?,Hqj2L.1NP&GjUR=1D8QaS3Up&@*9wP?+lo7b?@%'k4`p0Z$22%K3+iCZj?XJN4Nm&+YF]u@-W$U%VEQ/,,>>#)D<h#`)h0:<Q6909ua+&VU%n2:cG3FJ-%@Bj-DgLr`Hw&HAKjKjseK</xKT*)B,N9X3]krc12t'pgTV(Lv-tL[xg_%=M_q7a^x?7Ubd>#%8cY#YZ?=,`Wdxu/ae&#w6)R89tI#6@s'(6Bf7a&?S=^ZI_kS&ai`&=tE72L_D,;^R)7[$s<Eh#c&)q.MXI%#v9ROa5FZO%sF7q7Nwb&#ptUJ:aqJe$Sl68%.D###EC><?-aF&#RNQv>o8lKN%5/$(vdfq7+ebA#u1p]ovUKW&Y%q]'>$1@-[xfn$7ZTp7mM,G,Ko7a&Gu%G[RMxJs[0MM%wci.LFDK)(<c`Q8N)jEIF*+?P2a8g%)$q]o2aH8C&<SibC/q,(e:v;-b#6[$NtDZ84Je2KNvB#$P5?tQ3nt(0d=j.LQf./Ll33+(;q3L-w=8dX$#WF&uIJ@-bfI>%:_i2B5CsR8&9Z&#=mPEnm0f`<&c)QL5uJ#%u%lJj+D-r;BoF&#4DoS97h5g)E#o:&S4weDF,9^Hoe`h*L+_a*NrLW-1pG_&2UdB86e%B/:=>)N4xeW.*wft-;$'58-ESqr<b?UI(_%@[P46>#U`'6AQ]m&6/`Z>#S?YY#Vc;r7U2&326d=w&H####?TZ`*4?&.MK?LP8Vxg>$[QXc%QJv92.(Db*B)gb*BM9dM*hJMAo*c&#b0v=Pjer]$gG&JXDf->'StvU7505l9$AFvgYRI^&<^b68?j#q9QX4SM'RO#&sL1IM.rJfLUAj221]d##DW=m83u5;'bYx,*Sl0hL(W;;$doB&O/TQ:(Z^xBdLjL<Lni;'\0'X.`$#8+1GD:k$YUWsbn8ogh6rxZ2Z9]%nd+>V#*8U_72Lh+2Q8Cj0i:6hp&$C/:p(HK>T8Y[gHQ4`4)'$Ab(Nof%V'8hL&#<NEdtg(n'=S1A(Q1/I&4([%dM`,Iu'1:_hL>SfD07&6D<fp8dHM7/g+tlPN9J*rKaPct&?'uBCem^jn%9_K)<,C5K3s=5g&GmJb*[SYq7K;TRLGCsM-$$;S%:Y@r7AK0pprpL<Lrh,q7e/%KWK:50I^+m'vi`3?%Zp+<-d+$L-Sv:@.o19n$s0&39;kn;S%BSq*$3WoJSCLweV[aZ'MQIjO<7;X-X;&+dMLvu#^UsGEC9WEc[X(wI7#2.(F0jV*eZf<-Qv3J-c+J5AlrB#$p(H68LvEA'q3n0#m,[`*8Ft)FcYgEud]CWfm68,(aLA$@EFTgLXoBq/UPlp7:d[/;r_ix=:TF`S5H-b<LI&HY(K=h#)]Lk$K14lVfm:x$H<3^Ql<M`$OhapBnkup'D#L$Pb_`N*g]2e;X/Dtg,bsj&K#2[-:iYr'_wgH)NUIR8a1n#S?Yej'h8^58UbZd+^FKD*T@;6A7aQC[K8d-(v6GI$x:T<&'Gp5Uf>@M.*J:;$-rv29'M]8qMv-tLp,'886iaC=Hb*YJoKJ,(j%K=H`K.v9HggqBIiZu'QvBT.#=)0ukruV&.)3=(^1`o*Pj4<-<aN((^7('#Z0wK#5GX@7u][`*S^43933A4rl][`*O4CgLEl]v$1Q3AeF37dbXk,.)vj#x'd`;qgbQR%FW,2(?LO=s%Sc68%NP'##Aotl8x=BE#j1UD([3$M(]UI2LX3RpKN@;/#f'f/&_mt&F)XdF<9t4)Qa.*kTLwQ'(TTB9.xH'>#MJ+gLq9-##@HuZPN0]u:h7.T..G:;$/Usj(T7`Q8tT72LnYl<-qx8;-HV7Q-&Xdx%1a,hC=0u+HlsV>nuIQL-5<N?)NBS)QN*_I,?&)2'IM%L3I)X((e/dl2&8'<M:^#M*Q+[T.Xri.LYS3v%fF`68h;b-X[/En'CR.q7E)p'/kle2HM,u;^%OKC-N+Ll%F9CF<Nf'^#t2L,;27W:0O@6##U6W7:$rJfLWHj$#)woqBefIZ.PK<b*t7ed;p*_m;4ExK#h@&]>_>@kXQtMacfD.m-VAb8;IReM3$wf0'\0'hra*so568'Ip&vRs849'MRYSp%:t:h5qSgwpEr$B>Q,;s(C#$)`svQuF$##-D,##,g68@2[T;.XSdN9Qe)rpt._K-#5wF)sP'##p#C0c%-Gb%hd+<-j'Ai*x&&HMkT]C'OSl##5RG[JXaHN;d'uA#x._U;.`PU@(Z3dt4r152@:v,'R.Sj'w#0<-;kPI)FfJ&#AYJ&#//)>-k=m=*XnK$>=)72L]0I%>.G690a:$##<,);?;72#?x9+d;^V'9;jY@;)br#q^YQpx:X#Te$Z^'=-=bGhLf:D6&bNwZ9-ZD#n^9HhLMr5G;']d&6'wYmTFmL<LD)F^%[tC'8;+9E#C$g%#5Y>q9wI>P(9mI[>kC-ekLC/R&CH+s'B;K-M6$EB%is00:+A4[7xks.LrNk0&E)wILYF@2L'0Nb$+pv<(2.768/FrY&h$^3i&@+G%JT'<-,v`3;_)I9M^AE]CN?Cl2AZg+%4iTpT3<n-&%H%b<FDj2M<hH=&Eh<2Len$b*aTX=-8QxN)k11IM1c^j%9s<L<NFSo)B?+<-(GxsF,^-Eh@$4dXhN$+#rxK8'je'D7k`e;)2pYwPA'_p9&@^18ml1^[@g4t*[JOa*[=Qp7(qJ_oOL^('7fB&Hq-:sf,sNj8xq^>$U4O]GKx'm9)b@p7YsvK3w^YR-CdQ*:Ir<($u&)#(&?L9Rg3H)4fiEp^iI9O8KnTj,]H?D*r7'M;PwZ9K0E^k&-cpI;.p/6_vwoFMV<->#%Xi.LxVnrU(4&8/P+:hLSKj$#U%]49t'I:rgMi'FL@a:0Y-uA[39',(vbma*hU%<-SRF`Tt:542R_VV$p@[p8DV[A,?1839FWdF<TddF<9Ah-6&9tWoDlh]&1SpGMq>Ti1O*H&#(AL8[_P%.M>v^-))qOT*F5Cq0`Ye%+$B6i:7@0IX<N+T+0MlMBPQ*Vj>SsD<U4JHY8kD2)2fU/M#$e.)T4,_=8hLim[&);?UkK'-x?'(:siIfL<$pFM`i<?%W(mGDHM%>iWP,##P`%/L<eXi:@Z9C.7o=@(pXdAO/NLQ8lPl+HPOQa8wD8=^GlPa8TKI1CjhsCTSLJM'/Wl>-S(qw%sf/@%#B6;/U7K]uZbi^Oc^2n<bhPmUkMw>%t<)'mEVE'\0'n`WnJra$^TKvX5B>;_aSEK',(hwa0:i4G?.Bci.(X[?b*($,=-n<.Q%`(X=?+@Am*Js0&=3bh8K]mL<LoNs'6,'85`0?t/'_U59@]ddF<#LdF<eWdF<OuN/45rY<-L@&#+fm>69=Lb,OcZV/);TTm8VI;?%OtJ<(b4mq7M6:u?KRdF<gR@2L=FNU-<b[(9c/ML3m;Z[$oF3g)GAWqpARc=<ROu7cL5l;-[A]%/+fsd;l#SafT/f*W]0=O'$(Tb<[)*@e775R-:Yob%g*>l*:xP?Yb.5)%w_I?7uk5JC+FS(m#i'k.'a0i)9<7b'fs'59hq$*5Uhv##pi^8+hIEBF`nvo`;'l0.^S1<-wUK2/Coh58KKhLjM=SO*rfO`+qC`W-On.=AJ56>>i2@2LH6A:&5q`?9I3@@'04&p2/LVa*T-4<-i3;M9UvZd+N7>b*eIwg:CC)c<>nO&#<IGe;__.thjZl<%w(Wk2xmp4Q@I#I9,DF]u7-P=.-_:YJ]aS@V?6*C()dOp7:WL,b&3Rg/.cmM9&r^>$(>.Z-I&J(Q0Hd5Q%7Co-b`-c<N(6r@ip+AurK<m86QIth*#v;-OBqi+L7wDE-Ir8K['m+DDSLwK&/.?-V%U_%3:qKNu$_b*B-kp7NaD'QdWQPKYq[@>P)hI;*_F]u`Rb[.j8_Q/<&>uu+VsH$sM9TA%?)(vmJ80),P7E>)tjD%2L=-t#fK[%`v=Q8<FfNkgg^oIbah*#8/Qt$F&:K*-(N/'+1vMB,u()-a.VUU*#[e%gAAO(S>WlA2);Sa>gXm8YB`1d@K#n]76-a$U,mF<fX]idqd)<3,]J7JmW4`6]uks=4-72L(jEk+:bJ0M^q-8Dm_Z?0olP1C9Sa&H[d&c$ooQUj]Exd*3ZM@-WGW2%s',B-_M%>%Ul:#/'xoFM9QX-$.QN'>[%$Z$uF6pA6Ki2O5:8w*vP1<-1`[G,)-m#>0`P&#eb#.3i)rtB61(o'$?X3B</R90;eZ]%Ncq;-Tl]#F>2Qft^ae_5tKL9MUe9b*sLEQ95C&`=G?@Mj=wh*'3E>=-<)Gt*Iw)'QG:`@IwOf7&]1i'S01B+Ev/Nac#9S;=;YQpg_6U`*kVY39xK,[/6Aj7:'1Bm-_1EYfa1+o&o4hp7KN_Q(OlIo@S%;jVdn0'1<Vc52=u`3^o-n1'g4v58Hj&6_t7$##?M)c<$bgQ_'SY((-xkA#Y(,p'H9rIVY-b,'%bCPF7.J<Up^,(dU1VY*5#WkTU>h19w,WQhLI)3S#f$2(eb,jr*b;3Vw]*7NH%$c4Vs,eD9>XW8?N]o+(*pgC%/72LV-u<Hp,3@e^9UB1J+ak9-TN/mhKPg+AJYd$MlvAF_jCK*.O-^(63adMT->W%iewS8W6m2rtCpo'RS1R84=@paTKt)>=%&1[)*vp'u+x,VrwN;&]kuO9JDbg=pO$J*.jVe;u'm0dr9l,<*wMK*Oe=g8lV_KEBFkO'oU]^=[-792#ok,)i]lR8qQ2oA8wcRCZ^7w/Njh;?.stX?Q1>S1q4Bn$)K1<-rGdO'$Wr.Lc.CG)$/*JL4tNR/,SVO3,aUw'DJN:)Ss;wGn9A32ijw%FL+Z0Fn.U9;reSq)bmI32U==5ALuG&#Vf1398/pVo1*c-(aY168o<`JsSbk-,1N;$>0:OUas(3:8Z972LSfF8eb=c-;>SPw7.6hn3m`9^Xkn(r.qS[0;T%&Qc=+STRxX'q1BNk3&*eu2;&8q$&x>Q#Q7^Tf+6<(d%ZVmj2bDi%.3L2n+4W'$PiDDG)g,r%+?,$@?uou5tSe2aN_AQU*<h`e-GI7)?OK2A.d7_c)?wQ5AS@DL3r#7fSkgl6-++D:'A,uq7SvlB$pcpH'q3n0#_%dY#xCpr-l<F0NR@-##FEV6NTF6##$l84N1w?AO>'IAOURQ##V^Fv-XFbGM7Fl(N<3DhLGF%q.1rC$#:T__&Pi68%0xi_&[qFJ(77j_&JWoF.V735&T,[R*:xFR*K5>>#`bW-?4Ne_&6Ne_&6Ne_&n`kr-#GJcM6X;uM6X;uM(.a..^2TkL%oR(#;u.T%fAr%4tJ8&><1=GHZ_+m9/#H1F^R#SC#*N=BA9(D?v[UiFY>>^8p,KKF.W]L29uLkLlu/+4T<XoIB&hx=T1PcDaB&;HH+-AFr?(m9HZV)FKS8JCw;SD=6[^/DZUL`EUDf]GGlG&>w$)F./^n3+rlo+DB;5sIYGNk+i1t-69Jg--0pao7Sm#K)pdHW&;LuDNH@H>#/X-TI(;P>#,Gc>#0Su>#4`1?#8lC?#<xU?#@.i?#D:%@#HF7@#LRI@#P_[@#Tkn@#Xw*A#]-=A#a9OA#d<F&#*;G##.GY##2Sl##6`($#:l:$#>xL$#B.`$#F:r$#JF.%#NR@%#R_R%#Vke%#Zww%#_-4&#3^Rh%Sflr-k'MS.o?.5/sWel/wpEM0%3'/1)K^f1-d>G21&v(35>V`39V7A4=onx4A1OY5EI0;6Ibgr6M$HS7Q<)58C5w,;WoA*#[%T*#`1g*#d=#+#hI5+#lUG+#pbY+#tnl+#x$),#&1;,#*=M,#.I`,#2Ur,#6b.-#;w[H#iQtA#m^0B#qjBB#uvTB##-hB#'9$C#+E6C#/QHC#3^ZC#7jmC#;v)D#?,<D#C8ND#GDaD#KPsD#O]/E#g1A5#KA*1#gC17#MGd;#8(02#L-d3#rWM4#Hga1#,<w0#T.j<#O#'2#CYN1#qa^:#_4m3#o@/=#eG8=#t8J5#`+78#4uI-#m3B2#SB[8#Q0@8#i[*9#iOn8#1Nm;#^sN9#qh<9#:=x-#P;K2#$%X9#bC+.#Rg;<#mN=.#MTF.#RZO.#2?)4#Y#(/#[)1/#b;L/#dAU/#0Sv;#lY$0#n`-0#sf60#(F24#wrH0#%/e0#TmD<#%JSMFove:CTBEXI:<eh2g)B,3h2^G3i;#d3jD>)4kMYD4lVu`4m`:&5niUA5@(A5BA1]PBB:xlBCC=2CDLXMCEUtiCf&0g2'tN?PGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CPGT4CP-qekC`.9kEg^+F$kwViFJTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5KTB&5o,^<-28ZI'O?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xpO?;xp;7q-#lLYI:xvD=#";

		public static string nk_custom_cursor_data =
			"..-         -XXXXXXX-    X    -           X           -XXXXXXX          -          XXXXXXX..-         -X.....X-   X.X   -          X.X          -X.....X          -          X.....X---         -XXX.XXX-  X...X  -         X...X         -X....X           -           X....XX           -  X.X  - X.....X -        X.....X        -X...X            -            X...XXX          -  X.X  -X.......X-       X.......X       -X..X.X           -           X.X..XX.X         -  X.X  -XXXX.XXXX-       XXXX.XXXX       -X.X X.X          -          X.X X.XX..X        -  X.X  -   X.X   -          X.X          -XX   X.X         -         X.X   XXX...X       -  X.X  -   X.X   -    XX    X.X    XX    -      X.X        -        X.X      X....X      -  X.X  -   X.X   -   X.X    X.X    X.X   -       X.X       -       X.X       X.....X     -  X.X  -   X.X   -  X..X    X.X    X..X  -        X.X      -      X.X        X......X    -  X.X  -   X.X   - X...XXXXXX.XXXXXX...X -         X.X   XX-XX   X.X         X.......X   -  X.X  -   X.X   -X.....................X-          X.X X.X-X.X X.X          X........X  -  X.X  -   X.X   - X...XXXXXX.XXXXXX...X -           X.X..X-X..X.X           X.........X -XXX.XXX-   X.X   -  X..X    X.X    X..X  -            X...X-X...X            X..........X-X.....X-   X.X   -   X.X    X.X    X.X   -           X....X-X....X           X......XXXXX-XXXXXXX-   X.X   -    XX    X.X    XX    -          X.....X-X.....X          X...X..X    ---------   X.X   -          X.X          -          XXXXXXX-XXXXXXX          X..X X..X   -       -XXXX.XXXX-       XXXX.XXXX       ------------------------------------X.X  X..X   -       -X.......X-       X.......X       -    XX           XX    -           XX    X..X  -       - X.....X -        X.....X        -   X.X           X.X   -                 X..X          -  X...X  -         X...X         -  X..X           X..X  -                  XX           -   X.X   -          X.X          - X...XXXXXXXXXXXXX...X -           ------------        -    X    -           X           -X.....................X-                               ----------------------------------- X...XXXXXXXXXXXXX...X -                                                                 -  X..X           X..X  -                                                                 -   X.X           X.X   -                                                                 -    XX           XX    -           ";

		public class nk_buffer<T>
		{
			public T[] data;
		}

		public class nk_context
		{
			private readonly List<nk_window> _windows = new List<nk_window>();

			public List<nk_window> windows
			{
				get { return _windows; }
			}

			public nk_input input = new nk_input();
			public nk_style style = new nk_style();
			public nk_clipboard clip = new nk_clipboard();
			public uint last_widget_state;
			public int button_behavior;
			public nk_configuration_stacks stacks = new nk_configuration_stacks();
			public float delta_time_seconds;
			public nk_draw_list draw_list = new nk_draw_list();
			public nk_text_edit text_edit = new nk_text_edit();
			public nk_command_buffer overlay = new nk_command_buffer();
			public int build;
			public nk_window active;
			public nk_window current;
			public uint count;
			public uint seq;
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

		public class nk_user_font
		{
			public nk_handle userdata;
			public float height;
			public NkTextWidthDelegate WidthDelegate;
			public NkQueryFontGlyphDelegate query;
			/* font glyph callback to query drawing info */
			public nk_handle texture;

			public float width(float h, StringView text)
			{
				return WidthDelegate(h, text);
			}

			public float width(nk_handle handle, float h, StringView text, int length)
			{
				return WidthDelegate(h, new StringView(text, 0, length));
			}
		}

		public class nk_font
		{
			public nk_font next;
			public nk_user_font handle = new nk_user_font();
			public nk_baked_font info;
			public float scale;
			public nk_font_glyph* glyphs;
			public nk_font_glyph* fallback;
			public char fallback_codepoint;
			public nk_handle texture = new nk_handle();
			public nk_font_config* config;

			public float text_width(float height, StringView s)
			{
				return nk_font_text_width(this, height, s);
			}

			public void query_font_glyph(float height, nk_user_font_glyph* glyph, char codepoint, char next_codepoint)
			{
				nk_font_query_font_glyph(this, height, glyph, codepoint, next_codepoint);
			}
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
			public nk_font_config* config;
			public int font_num;
		}

		public class nk_tt_pack_context
		{
			public nk_rp_context pack_info;
			public int width;
			public int height;
			public int stride_in_bytes;
			public int padding;
			public uint h_oversample;
			public uint v_oversample;
			public byte* pixels;
			public void* nodes;
		}

		public class nk_font_baker
		{
			public nk_tt_pack_context spc;
			public nk_font_bake_data* build;
			public nk_tt_packedchar* packed_chars;
			public nk_rp_rect* rects;
			public nk_tt_pack_range* ranges;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct nk_tt__hheap
		{
			public nk_tt__hheap_chunk* head;
			public void* first_free;
			public int num_remaining_in_head_chunk;
		}

		public class nk_property_state
		{
			public int active;
			public int prev;
			public sbyte[] buffer = new sbyte[64];
			public int length;
			public int cursor;
			public int select_start;
			public int select_end;
			public uint name;
			public uint seq;
			public uint old;
			public int state;
		}

		public class nk_window
		{
			public uint seq;
			public uint name;
			public string name_string;
			public uint flags;
			public nk_rect bounds = new nk_rect();
			public nk_scroll scrollbar = new nk_scroll();
			public nk_command_buffer buffer = new nk_command_buffer();
			public nk_panel layout;
			public float scrollbar_hiding_timer;
			public nk_property_state property = new nk_property_state();
			public nk_popup_state popup = new nk_popup_state();
			public nk_edit_state edit;
			public uint scrolled;
			public uint table_count;
			public nk_window parent;
		}

		public class nk_popup_state
		{
			public nk_window win;
			public int type;
			public uint name;
			public int active;
			public uint combo_count;
			public uint con_count;
			public uint con_old;
			public uint active_con;
			public nk_rect header = new nk_rect();
		}

		public class nk_command_buffer
		{
			private readonly List<nk_command_base> _commands = new List<nk_command_base>();

			public List<nk_command_base> commands
			{
				get { return _commands; }
			}

			public nk_rect clip;
			public int use_clipping;
			public nk_handle userdata = new nk_handle();
		}

		public class nk_command_base
		{
			public nk_command header = new nk_command();
			public nk_handle userdata;
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
			public nk_color color = new nk_color();
			public ushort line_thickness;
			public ushort point_count;
			public nk_vec2i points = new nk_vec2i();
		}

		public class nk_command_polygon_filled : nk_command_base
		{
			public nk_color color = new nk_color();
			public ushort point_count;
			public nk_vec2i points = new nk_vec2i();
		}

		public class nk_command_polyline : nk_command_base
		{
			public nk_color color = new nk_color();
			public ushort line_thickness;
			public ushort point_count;
			public nk_vec2i points = new nk_vec2i();
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
			public StringView _string_;
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

		public class nk_clipboard
		{
			public nk_handle userdata;
			public NkPluginPaste paste;
			public NkPluginCopy CopyDelegate;

			public void copy(nk_handle handle, StringView text)
			{
				CopyDelegate(handle, text);
			}

			public void copy(nk_handle handle, StringView text, int length)
			{
				CopyDelegate(handle, new StringView(text, 0, length));
			}
		}

		public class nk_text_undo_state
		{
			public PinnedArray<nk_text_undo_record> undo_rec = new PinnedArray<nk_text_undo_record>(new nk_text_undo_record[99]);
			public PinnedArray<char> undo_char = new PinnedArray<char>(new char[999]);
			public short undo_point;
			public short redo_point;
			public short undo_char_point;
			public short redo_char_point;
		}

		public class nk_text_edit
		{
			public nk_clipboard clip;
			public StringView _string_;
			public NkPluginFilter filter;
			public nk_vec2 scrollbar;
			public int cursor;
			public int select_start;
			public int select_end;
			public byte mode;
			public byte cursor_at_end_of_line;
			public byte initialized;
			public byte has_preferred_x;
			public byte single_line;
			public byte active;
			public byte padding1;
			public float preferred_x;
			public nk_text_undo_state undo;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct nk_style_item
		{
			public int type;
			public nk_style_item_data data;
		}

		public class nk_style
		{
			public nk_user_font font;
			public nk_cursor[] cursors = new nk_cursor[7];
			public nk_cursor cursor_active;
			public nk_cursor cursor_last;
			public int cursor_visible;
			public nk_style_text text;
			public nk_style_button button;
			public nk_style_button contextual_button;
			public nk_style_button menu_button;
			public nk_style_toggle option;
			public nk_style_toggle checkbox;
			public nk_style_selectable selectable;
			public nk_style_slider slider;
			public nk_style_progress progress;
			public nk_style_property property;
			public nk_style_edit edit;
			public nk_style_chart chart;
			public nk_style_scrollbar scrollh;
			public nk_style_scrollbar scrollv;
			public nk_style_tab tab;
			public nk_style_combo combo;
			public nk_style_window window;
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

		[StructLayout(LayoutKind.Sequential)]
		public struct nk_command
		{
			public int type;
		}

		public class nk_draw_list
		{
			public nk_rect clip_rect;
			public nk_vec2[] circle_vtx = new nk_vec2[12];
			public nk_convert_config config = new nk_convert_config();
			public nk_buffer<nk_command_base> buffer;
			public nk_buffer<byte> vertices;
			public nk_buffer<ushort> elements;
			public nk_vec2[] path;
			public uint element_count;
			public uint vertex_count;
			public uint cmd_count;
			public uint path_count;
			public int line_AA;
			public int shape_AA;
			public nk_handle userdata;
		}

		public class nk_mouse
		{
			public nk_mouse_button[] buttons = new nk_mouse_button[NK_BUTTON_MAX];
			public nk_vec2 pos = new nk_vec2();
			public nk_vec2 prev = new nk_vec2();
			public nk_vec2 delta = new nk_vec2();
			public nk_vec2 scroll_delta = new nk_vec2();
			public byte grab;
			public byte grabbed;
			public byte ungrab;
		}

		public class nk_keyboard
		{
			public nk_key[] keys = new nk_key[NK_KEY_MAX];
			public char[] text = new char[16];
			public int text_len;
		}

		public class nk_input
		{
			public nk_keyboard keyboard = new nk_keyboard();
			public nk_mouse mouse = new nk_mouse();
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct nk_inv_sqrt_union
		{
			[FieldOffset(0)] public uint i;

			[FieldOffset(0)] public float f;
		}

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

		public static float nk_inv_sqrt(float number)
		{
			var threehalfs = 1.5f;
			var conv = new nk_inv_sqrt_union
			{
				i = 0,
			};
			var x2 = number*0.5f;
			conv.i = 0x5f375A84 - (conv.i >> 1);
			conv.f = conv.f*(threehalfs - (x2*conv.f*conv.f));
			return conv.f;
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

		public static int nk_utf_decode(StringView c, int pos, char* u, int clen)
		{
			*u = c[pos];

			return 1;
		}

		public static int nk_utf_decode(StringView c, char* u, int clen)
		{
			return nk_utf_decode(c, 0, u, clen);
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

		private static object nk_create_command<T>() where T : new()
		{
			return new T();
		}

		private static Func<object>[] _commandCreators =
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

		public static void nk_memset(void* ptr, int value, ulong size)
		{
			CRuntime.memset(ptr, value, size);
		}

		public static void nk_memcopy(void* a, void* b, ulong size)
		{
			CRuntime.memcpy(a, b, size);
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

		public static void nk_draw_list_setup(nk_draw_list canvas, nk_convert_config config, nk_buffer<nk_command_base> cmds,
			nk_buffer<byte> vertices, nk_buffer<ushort> elements, int line_aa, int shape_aa)
		{
			if (((((canvas == null) || (config == null)) || (cmds == null)) || (vertices == null)) || (elements == null)) return;
			canvas.buffer = cmds;
			canvas.config = config;
			canvas.elements = elements;
			canvas.vertices = vertices;
			canvas.line_AA = line_aa;
			canvas.shape_AA = shape_aa;
			canvas.clip_rect = nk_null_rect;
		}

		public static void nk_draw_list_clear(nk_draw_list list)
		{
			if (list == null) return;

			list.element_count = 0;
			list.vertex_count = 0;
			list.cmd_count = 0;
			list.path_count = 0;

			list.buffer = null;
			list.vertices = null;
			list.elements = null;
			list.clip_rect = nk_null_rect;
		}

		public static nk_vec2* nk_draw_list_alloc_path(nk_draw_list list, int count)
		{
			var point_align = 4;
			var point_size = (ulong) sizeof (nk_vec2);
			var points = (nk_vec2*)
				nk_buffer_alloc(list.buffer, NK_BUFFER_FRONT, point_size*(ulong) count, (ulong) point_align);
			if (points == null) return null;
			if (list.path_offset == 0)
			{
				void* memory = nk_buffer_memory(list.buffer);
				list.path_offset = ((uint) ((byte*) (points) - (byte*) (memory)));
			}

			list.path_count += ((uint) (count));
			return points;
		}

		public static nk_vec2 nk_draw_list_path_last(nk_draw_list list)
		{
			void* memory;
			nk_vec2* point;
			memory = nk_buffer_memory(list.buffer);
			point = ((nk_vec2*) ((void*) ((byte*) (memory) + (list.path_offset))));
			point += (list.path_count - 1);
			return (nk_vec2) (*point);
		}

		public static nk_draw_command nk_draw_list_push_command(nk_draw_list list, nk_rect clip, nk_handle texture)
		{
			ulong cmd_align = (ulong) (4);
			ulong cmd_size = (ulong) (sizeof (nk_draw_command));
			nk_draw_command cmd;
			cmd =
				(nk_draw_command) (nk_buffer_alloc(list.buffer, (int) (NK_BUFFER_BACK), (ulong) (cmd_size), (ulong) (cmd_align)));
			if (cmd == null) return (null);
			if (list.cmd_count == 0)
			{
				byte* memory = (byte*) (nk_buffer_memory(list.buffer));
				ulong total = (ulong) (nk_buffer_total(list.buffer));
				memory = ((byte*) ((void*) ((memory) + (total))));
				list.cmd_offset = ((ulong) (memory - (byte*) (cmd)));
			}

			cmd.elem_count = 0;
			cmd.clip_rect = clip;
			cmd.texture = texture;
			list.cmd_count++;
			list.clip_rect = clip;
			return cmd;
		}

		public static nk_draw_command nk_draw_list_command_last(nk_draw_list list)
		{
			void* memory;
			ulong size;
			nk_draw_command cmd;
			memory = nk_buffer_memory(list.buffer);
			size = (ulong) (nk_buffer_total(list.buffer));
			cmd = ((nk_draw_command) ((void*) ((byte*) (memory) + (size - list.cmd_offset))));
			return (cmd - (list.cmd_count - 1));
		}

		public static void* nk_buffer_alloc<T>(nk_buffer<T> buffer, int type, ulong size, ulong pnt_align)
		{
			return null;
		}

		public static void nk_buffer_mark<T>(nk_buffer<T> buffer, int type)
		{
		}

		public static void nk_buffer_reset<T>(nk_buffer<T> buffer, int type)
		{
		}

		public static void nk_draw_list_path_fill(nk_draw_list list, nk_color color)
		{

			if (list == null) return;

			fixed (byte* ptr = list.buffer)
			{
				nk_draw_list_fill_poly_convex(list, (nk_vec2*) ptr, list.path_count, color,
					list.config.shape_AA);
			}
			nk_draw_list_path_clear(list);
		}

		public static void nk_draw_list_path_stroke(nk_draw_list list, nk_color color, int closed, float thickness)
		{
			if (list == null) return;

			fixed (byte* ptr = list.buffer)
			{
				nk_draw_list_stroke_poly_line(list, (nk_vec2*) ptr, list.path_count, color, closed,
					thickness, list.config.line_AA);
			}
			nk_draw_list_path_clear(list);
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
				iter = iter._next_;
			}

			return iter;
		}

		public static void nk_input_glyph(nk_context ctx, char glyph)
		{
			if (ctx == null) return;
			var _in_ = ctx.input;
			if ((_in_.keyboard.text_len + 1 < 16))
			{
				_in_.keyboard.text[_in_.keyboard.text_len] = glyph;
				_in_.keyboard.text_len++;
			}
		}

		public static void nk_input_unicode(nk_context ctx, char unicode)
		{
			nk_input_glyph(ctx, unicode);
		}

		public static void nk_input_char(nk_context ctx, char c)
		{
			nk_input_glyph(ctx, c);
		}

		public static StringView nk_str_at_const(StringView str, int pos, char* unicode, int* len)
		{
			*unicode = str[pos];
			*len = 1;

			return str + pos;
		}

		public static StringView nk_str_get_const(StringView text)
		{
			return text;
		}

		public static int nk_str_len_char(StringView text)
		{
			return text.Length;
		}

		public static int nk_str_len(StringView text)
		{
			return text.Length;
		}

		public static int nk_strlen(StringView text)
		{
			return text.Length;
		}

		public static char nk_str_rune_at(StringView str, int pos)
		{
			return str[pos];
		}

		public static int nk_utf_len(char* str, int length)
		{
			return length;
		}

		public static int nk_utf_len(StringView ns, int length)
		{
			return length;
		}

		public static int nk_strtoi(StringView ns, int* endPos)
		{
			int result;

			if (ns.StartPos == 0)
			{
				int.TryParse(ns.Text, out result);
			}
			else
			{
				int.TryParse(ns.Text.Substring(ns.StartPos), out result);
			}

			return result;
		}

		public static float nk_strtof(StringView ns, int* endPos)
		{
			float result;

			if (ns.StartPos == 0)
			{
				float.TryParse(ns.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
			}
			else
			{
				float.TryParse(ns.Text.Substring(ns.StartPos), NumberStyles.Number, CultureInfo.InvariantCulture, out result);
			}

			return result;
		}

		public static double nk_strtod(StringView ns, int* endPos)
		{
			double result;

			if (ns.StartPos == 0)
			{
				double.TryParse(ns.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
			}
			else
			{
				double.TryParse(ns.Text.Substring(ns.StartPos), NumberStyles.Number, CultureInfo.InvariantCulture, out result);
			}

			return result;
		}

		public static int nk_str_insert_text_char(ref StringView str, int pos, char* text, int length)
		{
			str.Text = str.Text.Insert(str.StartPos + pos, new string(text, 0, length));

			return 1;
		}

		public static int nk_str_insert_text_char(ref StringView str, int pos, StringView text, int pos2, int length)
		{
			str.Text = str.Text.Insert(str.StartPos + pos, text.Text.Substring(text.StartPos + pos2, length));

			return 1;
		}

		public static int nk_str_insert_text_char(ref StringView str, int pos, StringView text, int length)
		{
			return nk_str_insert_text_char(ref str, pos, text, 0, length);
		}

		public static int nk_str_insert_text_char(ref StringView str, int pos, StringView text)
		{
			return nk_str_insert_text_char(ref str, pos, text, 0, text.Length);
		}

		public static int nk_str_insert_text_utf8(ref StringView str, int pos, StringView text, int pos2, int length)
		{
			return nk_str_insert_text_char(ref str, pos, text, pos2, length);
		}

		public static int nk_str_insert_text_runes(ref StringView str, int pos, char* text, int length)
		{
			return nk_str_insert_text_runes(ref str, pos, text, length);
		}

		public static int nk_str_insert_text_runes(ref StringView str, int pos, StringView text, int pos2, int length)
		{
			return nk_str_insert_text_char(ref str, pos, text, pos2, length);
		}

		public static int nk_str_insert_text_runes(ref StringView str, int pos, StringView text, int length)
		{
			return nk_str_insert_text_char(ref str, pos, text, length);
		}

		public static int nk_str_insert_text_runes(ref StringView str, int pos, StringView text)
		{
			return nk_str_insert_text_char(ref str, pos, text);
		}

		private static StringView nk_str_at_rune(StringView str, int pos, char* c, int* len)
		{
			if (str.IsNullOrEmpty || pos < 0 || pos >= str.Length)
			{
				return StringView.empty;
			}

			*c = str[pos];
			*len = 1;

			return str;
		}

		public static void nk_str_delete_runes(ref StringView str, int where, int length)
		{
			str.Text = str.Text.Remove(str.StartPos + where, length);
		}

		public static void nk_itoa(out StringView s, int value)
		{
			s = value.ToString();
		}

		public static void nk_dtoa(out StringView s, double value)
		{
			s = value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
		}

		public static int nk_string_float_limit(ref StringView s, int prec)
		{
			var dot = 0;
			var pos = 0;
			while (pos < s.Length)
			{
				var c = s[pos];
				if (c == '.')
				{
					dot = 1;
					pos++;
					continue;
				}
				if (dot == (prec + 1))
				{
					s.Length = pos;
					break;
				}

				if (dot > 0) dot++;
				++pos;
			}
			return pos;
		}

		public static uint nk_tt__find_table(byte* data, uint fontstart, string tag)
		{
			int num_tables = nk_ttUSHORT(data + fontstart + 4);
			var tabledir = fontstart + 12;
			int i;
			for (i = 0; i < num_tables; ++i)
			{
				var loc = tabledir + (uint) (16*i);
				if (((data + loc + 0)[0] == tag[0]) && ((data + loc + 0)[1] == tag[1]) &&
				    ((data + loc + 0)[2] == tag[2]) && ((data + loc + 0)[3] == tag[3]))
					return nk_ttULONG(data + loc + 8);
			}

			return 0;
		}

		public static void* nk_font_atlas_bake(nk_font_atlas atlas, int* width, int* height, int fmt)
		{
			int i;
			ulong img_size;
			nk_font font_iter;
			if ((atlas == null) || (width == null) || (height == null)) return null;
			if (atlas.font_num == 0) atlas.default_font = nk_font_atlas_add_default(atlas, 13.0f, null);
			if (atlas.font_num == 0) return null;
			var baker = new nk_font_baker();
			atlas.glyphs = (nk_font_glyph*) CRuntime.malloc((ulong) (sizeof (nk_font_glyph)*atlas.glyph_count));
			if (atlas.glyphs == null) goto failed;
			atlas.custom.w = 90*2 + 1;
			atlas.custom.h = 27 + 1;
			if (nk_font_bake_pack(baker, &img_size, width, height, &atlas.custom, atlas.config, atlas.font_num) == 0)
				goto failed;
			atlas.pixel = CRuntime.malloc(img_size);
			if (atlas.pixel == null) goto failed;
			nk_font_bake(baker, atlas.pixel, *width, *height, atlas.glyphs, atlas.glyph_count, atlas.config, atlas.font_num);
			nk_font_bake_custom_data(atlas.pixel, *width, *height, atlas.custom, nk_custom_cursor_data, 90, 27, (sbyte) '.',
				(sbyte) 'X');
			if (fmt == NK_FONT_ATLAS_RGBA32)
			{
				var img_rgba = CRuntime.malloc((ulong) ((*width)*(*height)*4));
				if (img_rgba == null) goto failed;
				nk_font_bake_convert(img_rgba, *width, *height, atlas.pixel);
				CRuntime.free(atlas.pixel);
				atlas.pixel = img_rgba;
			}

			atlas.tex_width = *width;
			atlas.tex_height = *height;
			for (font_iter = atlas.fonts; font_iter != null; font_iter = font_iter.next)
			{
				var font = font_iter;
				var config = font.config;
				nk_font_init(font, config->size, config->fallback_glyph, atlas.glyphs, config->font,
					nk_handle_ptr(null));
			}
			for (i = 0; i < NK_CURSOR_COUNT; ++i)
			{
				var cursor = atlas.cursors[i];
				cursor.img.w = (ushort) *width;
				cursor.img.h = (ushort) *height;
				cursor.img.region[0] = (ushort) (atlas.custom.x + nk_cursor_data[i, 0].x);
				cursor.img.region[1] = (ushort) (atlas.custom.y + nk_cursor_data[i, 0].y);
				cursor.img.region[2] = (ushort) nk_cursor_data[i, 1].x;
				cursor.img.region[3] = (ushort) nk_cursor_data[i, 1].y;
				cursor.size = nk_cursor_data[i, 1];
				cursor.offset = nk_cursor_data[i, 2];
			}
			return atlas.pixel;

			failed:
			if (atlas.glyphs != null)
			{
				CRuntime.free(atlas.glyphs);
				atlas.glyphs = null;
			}

			if (atlas.pixel != null)
			{
				CRuntime.free(atlas.pixel);
				atlas.pixel = null;
			}

			return null;
		}

		public static int nk_text_clamp(nk_user_font font, StringView text, float space, int* glyphs, float* text_width,
			uint* sep_list, int sep_count)
		{
			float last_width = 0;
			float width = 0;
			var len = 0;
			var g = 0;
			var sep_len = 0;
			var sep_g = 0;
			float sep_width = 0;
			sep_count = sep_count < 0 ? 0 : sep_count;
			var unicode = text[len];
			while ((width < space) && (len < text.Length))
			{
				len++;
				var s = font.width(font.userdata, font.height, text, len);
				int i;
				for (i = 0; i < sep_count; ++i)
				{
					if (unicode != sep_list[i]) continue;
					sep_width = last_width = width;
					sep_g = g + 1;
					sep_len = len;
					break;
				}
				if (i == sep_count)
				{
					last_width = sep_width = width;
					sep_g = g + 1;
				}
				width = s;
				unicode = text[len];
				g++;
			}
			if (len >= text.Length)
			{
				*glyphs = g;
				*text_width = last_width;
				return len;
			}
			*glyphs = sep_g;
			*text_width = sep_width;
			return sep_len == 0 ? len : sep_len;
		}

		public static nk_vec2 nk_text_calculate_text_bounds(nk_user_font font, StringView begin, float row_height,
			int* remaining, nk_vec2* out_offset, int* glyphs, int op)
		{
			var line_height = row_height;
			var text_size = nk_vec2_(0, 0);
			var line_width = 0.0f;
			var text_len = 0;
			if (begin.IsNullOrEmpty || (font == null))
				return nk_vec2_(0, row_height);
			var glyph_width = font.width(font.userdata, font.height, begin, 1);
			*glyphs = 0;

			var unicode = begin[text_len];
			while (text_len < begin.Length)
			{
				if (unicode == '\n')
				{
					text_size.x = text_size.x < line_width ? line_width : text_size.x;
					text_size.y += line_height;
					line_width = 0;
					*glyphs += 1;
					if (op == NK_STOP_ON_NEW_LINE) break;
					text_len++;
					unicode = begin[text_len];
					continue;
				}
				if (unicode == '\r')
				{
					text_len++;
					*glyphs += 1;
					unicode = begin[text_len];
					continue;
				}
				*glyphs = *glyphs + 1;
				text_len++;
				line_width += glyph_width;
				unicode = begin[text_len];
				glyph_width = font.width(font.userdata, font.height, begin + text_len, 1);
			}
			if (text_size.x < (line_width)) text_size.x = line_width;
			if (out_offset != null)
				*out_offset = nk_vec2_(line_width, text_size.y + line_height);
			if ((line_width > (0)) || (text_size.y == 0.0f)) text_size.y += line_height;
			if ((remaining) != null) *remaining = text_len;
			return text_size;
		}

		public static void nk_draw_text(nk_command_buffer b, nk_rect r, StringView _string_, nk_user_font font,
			nk_color bg,
			nk_color fg)
		{
			if ((b == null) || _string_.IsNullOrEmpty || ((bg.a == 0) && (fg.a == 0))) return;
			if (b.use_clipping != 0)
			{
				var c = &b.clip;
				if ((c->w == 0) || (c->h == 0) || (c->x > r.x + r.w) || (c->x + c->w < r.x) || (c->y > r.y + r.h) ||
				    (c->y + c->h < r.y))
					return;
			}

			float text_width = font.width(font.userdata, font.height, _string_);

			if (text_width > r.w)
			{
				var glyphs = 0;
				var txt_width = text_width;
				_string_.Length = nk_text_clamp(font, _string_, r.w, &glyphs, &txt_width, null, 0);
			}

			if (_string_.IsNullOrEmpty) return;
			var cmd = (nk_command_text) nk_command_buffer_push(b, NK_COMMAND_TEXT);
			if (cmd == null) return;
			cmd.x = (short) r.x;
			cmd.y = (short) r.y;
			cmd.w = (ushort) r.w;
			cmd.h = (ushort) r.h;
			cmd.background = bg;
			cmd.foreground = fg;
			cmd.font = font;
			cmd.length = _string_.Length;
			cmd._string_ = _string_;
		}

		public static void nk_draw_list_add_text(nk_draw_list list, nk_user_font font, nk_rect rect, StringView text,
			float font_height, nk_color fg)
		{
			var text_len = 0;
			var g = new nk_user_font_glyph();
			if ((list == null) || text.IsNullOrEmpty) return;
			if ((list.clip_rect.x > rect.x + rect.w) || (list.clip_rect.x + list.clip_rect.w < rect.x) ||
			    (list.clip_rect.y > rect.y + rect.h) || (list.clip_rect.y + list.clip_rect.h < rect.y)) return;
			nk_draw_list_push_image(list, font.texture);
			var x = rect.x;
			var unicode = text[text_len];
			fg.a = (byte) (fg.a*list.config.global_alpha);
			while ((text_len < text.Length))
			{
				if (unicode == 0xFFFD) break;
				var _next_ = text[text_len + 1];
				font.query(font.userdata, font_height, &g, unicode, _next_ == 0xFFFD ? ' ' : _next_);
				var gx = x + g.offset.x;
				var gy = rect.y + g.offset.y;
				var gw = g.width;
				var gh = g.height;
				var char_width = g.xadvance;
				nk_draw_list_push_rect_uv(list, nk_vec2_(gx, gy),
					nk_vec2_(gx + gw, gy + gh),
					nk_vec2_(g.uv_x[0], g.uv_y[0]),
					nk_vec2_(g.uv_x[1], g.uv_y[1]), fg);
				text_len++;
				x += char_width;
				unicode = _next_;
			}
		}

		public static void nk_widget_text(nk_command_buffer o, nk_rect b, StringView _string_, int length, nk_text* t,
			uint a,
			nk_user_font f)
		{
			nk_widget_text(o, b, new StringView(_string_, 0, length), t, a, f);
		}

		public static float nk_textedit_get_width(nk_text_edit edit, int line_start, int char_id, nk_user_font font)
		{
			return font.width(font.userdata, font.height, edit._string_ + line_start + char_id, 1);
		}

		public static void nk_textedit_layout_row(nk_text_edit_row* r, nk_text_edit edit, int line_start_id, float row_height,
			nk_user_font font)
		{
			var glyphs = 0;
			int remaining;
			var size = nk_text_calculate_text_bounds(font, edit._string_ + line_start_id, row_height, &remaining, null, &glyphs,
				NK_STOP_ON_NEW_LINE);
			r->x0 = 0.0f;
			r->x1 = size.x;
			r->baseline_y_delta = size.y;
			r->ymin = 0.0f;
			r->ymax = size.y;
			r->num_chars = glyphs;
		}

		public static int nk_textedit_paste(nk_text_edit state, StringView ctext)
		{
			if (state.mode == NK_TEXT_EDIT_MODE_VIEW) return 0;
			nk_textedit_clamp(state);
			nk_textedit_delete_selection(state);
			if (nk_str_insert_text_char(ref state._string_, state.cursor, ctext) != 0)
			{
				nk_textedit_makeundo_insert(state, state.cursor, ctext.Length);
				state.cursor += ctext.Length;
				state.has_preferred_x = 0;
				return 1;
			}

			if (state.undo.undo_point != 0) --state.undo.undo_point;
			return 0;
		}

		public static void nk_textedit_text(nk_text_edit state, StringView text)
		{
			var text_len = 0;
			if (text.IsNullOrEmpty || (state.mode == NK_TEXT_EDIT_MODE_VIEW)) return;
			var unicode = text[text_len];
			while ((text_len < text.Length))
			{
				if (unicode == 127) goto next;
				if ((unicode == '\n') && (state.single_line != 0)) goto next;
				if ((state.filter != null) && (state.filter(state, unicode) == 0)) goto next;
				if (state.select_start == state.select_end && (state.cursor < state._string_.Length))
				{
					if (state.mode == NK_TEXT_EDIT_MODE_REPLACE)
					{
						nk_textedit_makeundo_replace(state, state.cursor, 1, 1);
						nk_str_delete_runes(ref state._string_, state.cursor, 1);
					}
					if (nk_str_insert_text_utf8(ref state._string_, state.cursor, text, text_len, 1) != 0)
					{
						++state.cursor;
						state.has_preferred_x = 0;
					}
				}
				else
				{
					nk_textedit_delete_selection(state);
					if (nk_str_insert_text_utf8(ref state._string_, state.cursor, text, text_len, 1) != 0)
					{
						nk_textedit_makeundo_insert(state, state.cursor, 1);
						++state.cursor;
						state.has_preferred_x = 0;
					}
				}
				next:
				text_len++;
				unicode = text[text_len];
			}
		}

		public static void nk_textedit_text(nk_text_edit state, char[] text, int length)
		{
			nk_textedit_text(state, new StringView(new string(text, 0, length)));
		}

		public static void nk_textedit_text(nk_text_edit state, string text, int length)
		{
			nk_textedit_text(state, new StringView(text, 0, length));
		}

		public static void nk_edit_draw_text(nk_command_buffer _out_, nk_style_edit style, float pos_x, float pos_y,
			float x_offset, StringView text, int length, float row_height, nk_user_font font, nk_color background,
			nk_color foreground,
			int is_selected)
		{
			nk_edit_draw_text(_out_, style, pos_x, pos_y, x_offset, new StringView(text, 0, length), row_height, font,
				background, foreground, is_selected);
		}

		public static void nk_draw_property(nk_command_buffer _out_, nk_style_property style, nk_rect* bounds, nk_rect* label,
			uint state, StringView name, int length, nk_user_font font)
		{
			nk_draw_property(_out_, style, bounds, label, state, new StringView(name, 0, length), font);
		}

		public static string nk_style_get_color_by_name(int c)
		{
			return nk_color_names[c];
		}

		public static int nk_init(nk_context ctx, nk_allocator alloc, nk_user_font font)
		{
			if (alloc == null) return (int) (0);
			nk_setup(ctx, font);
			nk_buffer_init(ctx.memory, alloc, (ulong) (4*1024));
			nk_pool_init(ctx.pool, alloc, (uint) (16));
			ctx.use_pool = (int) (nk_true);
			return (int) (1);
		}

		public static void nk_clear(nk_context ctx)
		{
			nk_window iter;
			nk_window _next_;
			if (ctx == null) return;
			ctx.build = (int) (0);
			ctx.last_widget_state = (uint) (0);
			ctx.style.cursor_active = ctx.style.cursors[NK_CURSOR_ARROW];
			nk_draw_list_clear(ctx.draw_list);
			iter = ctx.begin;
			while ((iter) != null)
			{
				if ((((iter.flags & NK_WINDOW_MINIMIZED) != 0) && ((iter.flags & NK_WINDOW_CLOSED) == 0)) &&
				    ((iter.seq) == (ctx.seq)))
				{
					iter = iter._next_;
					continue;
				}
				if ((((iter.flags & NK_WINDOW_HIDDEN) != 0) || ((iter.flags & NK_WINDOW_CLOSED) != 0)) && ((iter) == (ctx.active)))
				{
					ctx.active = iter.prev;
					ctx.end = iter.prev;
					if ((ctx.active) != null) ctx.active.flags &= (uint) (~(uint) (NK_WINDOW_ROM));
				}
				if (((iter.popup.win) != null) && (iter.popup.win.seq != ctx.seq))
				{
					nk_free_window(ctx, iter.popup.win);
					iter.popup.win = (null);
				}
				{
					nk_table* n;
					nk_table* it = iter.tables;
					while ((it) != null)
					{
						n = it->_next_;
						if (it->seq != ctx.seq)
						{
							nk_remove_table(iter, it);
							nk_zero(it, (ulong) (sizeof (nk_table)));
							nk_free_table(ctx, it);
							if ((it) == (iter.tables)) iter.tables = n;
						}
						it = n;
					}
				}
				if ((iter.seq != ctx.seq) || ((iter.flags & NK_WINDOW_CLOSED) != 0))
				{
					_next_ = iter._next_;
					nk_remove_window(ctx, iter);
					nk_free_window(ctx, iter);
					iter = _next_;
				}
				else iter = iter._next_;
			}
			ctx.seq++;
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
				nk_draw_image(ctx.overlay, mouse_bounds, &cursor.img, nk_white);
				nk_finish_buffer(ctx, ctx.overlay);
			}

			foreach (var it in ctx.windows)
			{
				if (((it.flags & NK_WINDOW_HIDDEN) != 0) || (it.seq != ctx.seq))
					goto cont;
				cmd = ((nk_command*) ((void*) ((buffer) + (it.buffer.last))));
				while (((_next_) != null) &&
				       (((_next_.buffer.last) == (_next_.buffer.begin)) || ((_next_.flags & NK_WINDOW_HIDDEN) != 0)))
				{
					_next_ = _next_._next_;
				}
				if ((_next_) != null) cmd->_next_ = (ulong) (_next_.buffer.begin);
				cont:
				;
				it = _next_;
			}
			it = ctx.begin;
			while (it != 0)
			{
				nk_window _next_ = it._next_;
				nk_popup_buffer* buf;
				if (it.popup.buf.active == 0) goto skip;
				buf = &it.popup.buf;
				cmd->_next_ = (ulong) (buf->begin);
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
			}
		}

		public static nk_window nk_create_window(nk_context ctx)
		{
			nk_page_element elem;
			elem = nk_create_page_element(ctx);
			if (elem == null) return 0;
			elem.data.win.seq = (uint) (ctx.seq);
			return elem.data.win;
		}

		public static nk_window nk_find_window(nk_context ctx, uint hash, StringView name)
		{
			nk_window iter;
			iter = ctx.begin;
			while ((iter) != null)
			{
				if ((iter.name) == (hash))
				{
					int max_len = (int) (nk_strlen(iter.name_string));
					if (nk_stricmpn(iter.name_string, name, (int) (max_len)) == 0) return iter;
				}
				iter = iter._next_;
			}
			return 0;
		}

		public static void nk_insert_window(nk_context ctx, nk_window win, int loc)
		{
			nk_window iter;
			if ((win == null) || (ctx == null)) return;
			iter = ctx.begin;
			while ((iter) != null)
			{
				if ((iter) == (win)) return;
				iter = iter._next_;
			}
			if (ctx.begin == null)
			{
				win._next_ = 0;
				win.prev = 0;
				ctx.begin = win;
				ctx.end = win;
				ctx.count = (uint) (1);
				return;
			}

			if ((loc) == (NK_INSERT_BACK))
			{
				nk_window end;
				end = ctx.end;
				end.flags |= (uint) (NK_WINDOW_ROM);
				end._next_ = win;
				win.prev = ctx.end;
				win._next_ = 0;
				ctx.end = win;
				ctx.active = ctx.end;
				ctx.end.flags &= (uint) (~(uint) (NK_WINDOW_ROM));
			}
			else
			{
				ctx.begin.prev = win;
				win._next_ = ctx.begin;
				win.prev = 0;
				ctx.begin = win;
				ctx.begin.flags &= (uint) (~(uint) (NK_WINDOW_ROM));
			}

			ctx.count++;
		}

		public static void nk_remove_window(nk_context ctx, nk_window win)
		{
			if (((win) == (ctx.begin)) || ((win) == (ctx.end)))
			{
				if ((win) == (ctx.begin))
				{
					ctx.begin = win._next_;
					if ((win._next_) != null) win._next_.prev = 0;
				}
				if ((win) == (ctx.end))
				{
					ctx.end = win.prev;
					if ((win.prev) != null) win.prev._next_ = 0;
				}
			}
			else
			{
				if ((win._next_) != null) win._next_.prev = win.prev;
				if ((win.prev) != null) win.prev._next_ = win._next_;
			}

			if (((win) == (ctx.active)) || (ctx.active == null))
			{
				ctx.active = ctx.end;
				if ((ctx.end) != null) ctx.end.flags &= (uint) (~(uint) (NK_WINDOW_ROM));
			}

			win._next_ = 0;
			win.prev = 0;
			ctx.count--;
		}

		public static int nk_begin_titled(nk_context ctx, StringView name, StringView title, nk_rect bounds,
			uint flags)
		{
			if ((ctx == null) || (ctx.current != null) || (title == null) || (name == null)) return 0;
			var style = ctx.style;
			var title_hash = nk_murmur_hash(name, NK_WINDOW_TITLE);
			var win = nk_find_window(ctx, title_hash, name);
			if (win == null)
			{
				var name_length = nk_strlen(name);
				win = nk_create_window(ctx);
				if (win == null) return 0;
				if ((flags & NK_WINDOW_BACKGROUND) != 0) nk_insert_window(ctx, win, NK_INSERT_FRONT);
				else nk_insert_window(ctx, win, NK_INSERT_BACK);
				nk_command_buffer_init(win.buffer, NK_CLIPPING_ON);
				win.flags = flags;
				win.bounds = bounds;
				win.name = title_hash;
				name_length = name_length < 64 - 1 ? name_length : 64 - 1;
				win.name_string = name.Text.Substring(name.StartPos, name_length);
				win.popup.win = null;
				if (ctx.active == null) ctx.active = win;
			}
			else
			{
				win.flags &= ~(uint) (NK_WINDOW_PRIVATE - 1);
				win.flags |= flags;
				if ((win.flags & (NK_WINDOW_MOVABLE | NK_WINDOW_SCALABLE)) == 0) win.bounds = bounds;
				win.seq = ctx.seq;
				if ((ctx.active == null) && ((win.flags & NK_WINDOW_HIDDEN) == 0))
				{
					ctx.active = win;
//					ctx.end = win;
				}
			}

			if ((win.flags & NK_WINDOW_HIDDEN) != 0)
			{
				ctx.current = win;
				win.layout = null;
				return 0;
			}
			nk_start(ctx, win);
			if (((win.flags & NK_WINDOW_HIDDEN) == 0) && ((win.flags & NK_WINDOW_NO_INPUT) == 0))
			{
				var iter = win;
				var h = ctx.style.font.height + 2.0f*style.window.header.padding.y + 2.0f*style.window.header.label_padding.y;
				var win_bounds = (win.flags & NK_WINDOW_MINIMIZED) == 0
					? win.bounds
					: nk_rect_(win.bounds.x, win.bounds.y, win.bounds.w, h);
				var inpanel = nk_input_has_mouse_click_down_in_rect(ctx.input, NK_BUTTON_LEFT, win_bounds, nk_true);
				inpanel = (inpanel != 0) && (ctx.input.mouse.buttons[NK_BUTTON_LEFT].clicked != 0) ? 1 : 0;
				var ishovered = nk_input_is_mouse_hovering_rect(ctx.input, win_bounds);
				if ((win != ctx.active) && (ishovered != 0) && (ctx.input.mouse.buttons[NK_BUTTON_LEFT].down == 0))
				{
					iter = win._next_;
					while (iter != null)
					{
						var iter_bounds = !(iter.flags & NK_WINDOW_MINIMIZED)
							? iter.bounds
							: nk_rect_(iter.bounds.x, iter.bounds.y, iter.bounds.w, h);
						if (
							!((iter_bounds.x > win_bounds.x + win_bounds.w) || (iter_bounds.x + iter_bounds.w < win_bounds.x) ||
							  (iter_bounds.y > win_bounds.y + win_bounds.h) || (iter_bounds.y + iter_bounds.h < win_bounds.y)) &&
							!(iter.flags & NK_WINDOW_HIDDEN)) break;
						if ((iter.popup.win != null) && (iter.popup.active != 0) && ((iter.flags & NK_WINDOW_HIDDEN) == 0) &&
						    !((iter.popup.win.bounds.x > win.bounds.x + win_bounds.w) ||
						      (iter.popup.win.bounds.x + iter.popup.win.bounds.w < win.bounds.x) ||
						      (iter.popup.win.bounds.y > win_bounds.y + win_bounds.h) ||
						      (iter.popup.win.bounds.y + iter.popup.win.bounds.h < win_bounds.y))) break;
						iter = iter._next_;
					}
				}
				if ((iter != null) && (inpanel != 0) && (win != ctx.end))
				{
					iter = win._next_;
					while (iter != null)
					{
						var iter_bounds = !(iter.flags & NK_WINDOW_MINIMIZED)
							? iter.bounds
							: nk_rect_(iter.bounds.x, iter.bounds.y, iter.bounds.w, h);
						if ((iter_bounds.x <= ctx.input.mouse.pos.x) && (ctx.input.mouse.pos.x < iter_bounds.x + iter_bounds.w) &&
						    (iter_bounds.y <= ctx.input.mouse.pos.y) && (ctx.input.mouse.pos.y < iter_bounds.y + iter_bounds.h) &&
						    ((iter.flags & NK_WINDOW_HIDDEN) == 0)) break;
						if ((iter.popup.win != null) && (iter.popup.active != 0) && ((iter.flags & NK_WINDOW_HIDDEN) == 0) &&
						    !((iter.popup.win.bounds.x > win_bounds.x + win_bounds.w) ||
						      (iter.popup.win.bounds.x + iter.popup.win.bounds.w < win_bounds.x) ||
						      (iter.popup.win.bounds.y > win_bounds.y + win_bounds.h) ||
						      (iter.popup.win.bounds.y + iter.popup.win.bounds.h < win_bounds.y))) break;
						iter = iter._next_;
					}
				}
				if ((iter != null) && ((win.flags & NK_WINDOW_ROM) == 0) && ((win.flags & NK_WINDOW_BACKGROUND) != 0))
				{
					win.flags |= NK_WINDOW_ROM;
					iter.flags &= ~(uint) NK_WINDOW_ROM;
					ctx.active = iter;
					if ((iter.flags & NK_WINDOW_BACKGROUND) == 0)
					{
						nk_remove_window(ctx, iter);
						nk_insert_window(ctx, iter, NK_INSERT_BACK);
					}
				}
				else
				{
					if ((iter == null) && (ctx.end != win))
					{
						if ((win.flags & NK_WINDOW_BACKGROUND) == 0)
						{
							nk_remove_window(ctx, win);
							nk_insert_window(ctx, win, NK_INSERT_BACK);
						}
						win.flags &= ~(uint) NK_WINDOW_ROM;
						ctx.active = win;
					}
					if ((ctx.end != win) && ((win.flags & NK_WINDOW_BACKGROUND) == 0)) win.flags |= NK_WINDOW_ROM;
				}
			}

			win.layout = (nk_panel) nk_create_panel(ctx);
			ctx.current = win;
			var ret = nk_panel_begin(ctx, title, NK_PANEL_WINDOW);
			win.layout.offset_x = &win.scrollbar.x;
			win.layout.offset_y = &win.scrollbar.y;
			return ret;
		}

		public static void nk_font_atlas_begin(nk_font_atlas atlas)
		{
			if (atlas == null) return;
			if (atlas.glyphs != null)
			{
				CRuntime.free(atlas.glyphs);
				atlas.glyphs = null;
			}

			if (atlas.pixel != null)
			{
				CRuntime.free(atlas.pixel);
				atlas.pixel = null;
			}
		}
	}
}