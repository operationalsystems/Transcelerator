/*
 *  Port of Snowball stemmers on C#
 *  Original stemmers can be found on http://snowball.tartarus.org
 *  Licence still BSD: http://snowball.tartarus.org/license.php
 *  
 *  Most of stemmers are ported from Java by Iveonik Systems ltd. (www.iveonik.com)
 */

namespace SIL.Stemmers
{
	[Stemmer("cs")]
	public class CzechStemmer : StemmerOperations, IStemmer
	{
		public bool StagedStemming { get; set; }

		public string Stem(string input)
		{
			setCurrent(input.ToLowerInvariant());
			// stemming...
			//removes case endings from nouns and adjectives
			removeCase();
			//removes possesive endings from names -ov- and -in-
			removePossessives();
			//removes comparative endings
			removeComparative();
			//removes diminutive endings
			removeDiminutive();
			//removes augmentatives endings
			removeAugmentative();
			//removes derivational sufixes from nouns
			removeDerivational();

			//result = sb.toString();
			return getCurrent(); //sb.ToString();
		}

		private void removeDerivational()
		{
			int len = current.Length;
			if ((len > 8) &&
				current.ToString().Substring(len - 6, 6).Equals("obinec"))
			{
				current = current.Remove(len - 6, 6);
				return;
			} //len >8
			if (len > 7)
			{
				if (current.ToString().Substring(len - 5, 5).Equals("ion\u00e1\u0159"))
				{
					// -ionář 

					current = current.Remove(len - 4, 4);
					palatalise();
					return;
				}
				if (current.ToString().Substring(len - 5, 5).Equals("ovisk") ||
					current.ToString().Substring(len - 5, 5).Equals("ovstv") ||
					current.ToString().Substring(len - 5, 5).Equals("ovi\u0161t") || //-ovišt
					current.ToString().Substring(len - 5, 5).Equals("ovn\u00edk"))
				{
					//-ovník

					current = current.Remove(len - 5, 5);
					return;
				}
			} //len>7
			if (len > 6)
			{
				if (current.ToString().Substring(len - 4, 4).Equals("\u00e1sek") || // -ásek 
					current.ToString().Substring(len - 4, 4).Equals("loun") ||
					current.ToString().Substring(len - 4, 4).Equals("nost") ||
					current.ToString().Substring(len - 4, 4).Equals("teln") ||
					current.ToString().Substring(len - 4, 4).Equals("ovec") ||
					current.ToString().Substring(len - 5, 5).Equals("ov\u00edk") || //-ovík
					current.ToString().Substring(len - 4, 4).Equals("ovtv") ||
					current.ToString().Substring(len - 4, 4).Equals("ovin") ||
					current.ToString().Substring(len - 4, 4).Equals("\u0161tin"))
				{
					//-štin

					current = current.Remove(len - 4, 4);
					return;
				}
				if (current.ToString().Substring(len - 4, 4).Equals("enic") ||
					current.ToString().Substring(len - 4, 4).Equals("inec") ||
					current.ToString().Substring(len - 4, 4).Equals("itel"))
				{

					current = current.Remove(len - 3, 3);
					palatalise();
					return;
				}
			} //len>6
			if (len > 5)
			{
				if (current.ToString().Substring(len - 3, 3).Equals("\u00e1rn"))
				{
					//-árn
					current = current.Remove(len - 3, 3);
					return;
				}
				if (current.ToString().Substring(len - 3, 3).Equals("\u011bnk"))
				{
					//-ěnk

					current = current.Remove(len - 2, 2);
					palatalise();

					return;
				}
				if (current.ToString().Substring(len - 3, 3).Equals("i\u00e1n") || //-ián
					current.ToString().Substring(len - 3, 3).Equals("ist") ||
					current.ToString().Substring(len - 3, 3).Equals("isk") ||
					current.ToString().Substring(len - 3, 3).Equals("i\u0161t") || //-išt
					current.ToString().Substring(len - 3, 3).Equals("itb") ||
					current.ToString().Substring(len - 3, 3).Equals("\u00edrn"))
				{
					//-írn

					current = current.Remove(len - 2, 2);
					palatalise();
					return;
				}
				if (current.ToString().Substring(len - 3, 3).Equals("och") ||
					current.ToString().Substring(len - 3, 3).Equals("ost") ||
					current.ToString().Substring(len - 3, 3).Equals("ovn") ||
					current.ToString().Substring(len - 3, 3).Equals("oun") ||
					current.ToString().Substring(len - 3, 3).Equals("out") ||
					current.ToString().Substring(len - 3, 3).Equals("ou\u0161"))
				{
					//-ouš

					current = current.Remove(len - 3, 3);
					return;
				}
				if (current.ToString().Substring(len - 3, 3).Equals("u\u0161k"))
				{
					//-ušk

					current = current.Remove(len - 3, 3);
					return;
				}
				if (current.ToString().Substring(len - 3, 3).Equals("kyn") ||
					current.ToString().Substring(len - 3, 3).Equals("\u010dan") || //-čan
					current.ToString().Substring(len - 3, 3).Equals("k\u00e1\u0159") || //kář
					current.ToString().Substring(len - 3, 3).Equals("n\u00e9\u0159") || //néř
					current.ToString().Substring(len - 3, 3).Equals("n\u00edk") || //-ník
					current.ToString().Substring(len - 3, 3).Equals("ctv") ||
					current.ToString().Substring(len - 3, 3).Equals("stv"))
				{

					current = current.Remove(len - 3, 3);
					return;
				}
			} //len>5
			if (len > 4)
			{
				if (current.ToString().Substring(len - 2, 2).Equals("\u00e1\u010d") || // -áč
					current.ToString().Substring(len - 2, 2).Equals("a\u010d") || //-ač
					current.ToString().Substring(len - 2, 2).Equals("\u00e1n") || //-án
					current.ToString().Substring(len - 2, 2).Equals("an") ||
					current.ToString().Substring(len - 2, 2).Equals("\u00e1\u0159") || //-ář
					current.ToString().Substring(len - 2, 2).Equals("as"))
				{

					current = current.Remove(len - 2, 2);
					return;
				}
				if (current.ToString().Substring(len - 2, 2).Equals("ec") ||
					current.ToString().Substring(len - 2, 2).Equals("en") ||
					current.ToString().Substring(len - 2, 2).Equals("\u011bn") || //-ěn
					current.ToString().Substring(len - 2, 2).Equals("\u00e9\u0159"))
				{
					//-éř

					current = current.Remove(len - 1, 1);
					palatalise();
					return;
				}
				if (current.ToString().Substring(len - 2, 2).Equals("\u00ed\u0159") || //-íř
					current.ToString().Substring(len - 2, 2).Equals("ic") ||
					current.ToString().Substring(len - 2, 2).Equals("in") ||
					current.ToString().Substring(len - 2, 2).Equals("\u00edn") || //-ín
					current.ToString().Substring(len - 2, 2).Equals("it") ||
					current.ToString().Substring(len - 2, 2).Equals("iv"))
				{

					current = current.Remove(len - 1, 1);
					palatalise();
					return;
				}

				if (current.ToString().Substring(len - 2, 2).Equals("ob") ||
					current.ToString().Substring(len - 2, 2).Equals("ot") ||
					current.ToString().Substring(len - 2, 2).Equals("ov") ||
					current.ToString().Substring(len - 2, 2).Equals("o\u0148"))
				{
					//-oň 

					current = current.Remove(len - 2, 2);
					return;
				}
				if (current.ToString().Substring(len - 2, 2).Equals("ul"))
				{

					current = current.Remove(len - 2, 2);
					return;
				}
				if (current.ToString().Substring(len - 2, 2).Equals("yn"))
				{

					current = current.Remove(len - 2, 2);
					return;
				}
				if (current.ToString().Substring(len - 2, 2).Equals("\u010dk") || //-čk
					current.ToString().Substring(len - 2, 2).Equals("\u010dn") || //-čn
					current.ToString().Substring(len - 2, 2).Equals("dl") ||
					current.ToString().Substring(len - 2, 2).Equals("nk") ||
					current.ToString().Substring(len - 2, 2).Equals("tv") ||
					current.ToString().Substring(len - 2, 2).Equals("tk") ||
					current.ToString().Substring(len - 2, 2).Equals("vk"))
				{

					current = current.Remove(len - 2, 2);
					return;
				}
			} //len>4
			if (len > 3)
			{
				if (current.ToString()[current.Length - 1] == 'c' ||
					current.ToString()[current.Length - 1] == '\u010d' || //-č
					current.ToString()[current.Length - 1] == 'k' ||
					current.ToString()[current.Length - 1] == 'l' ||
					current.ToString()[current.Length - 1] == 'n' ||
					current.ToString()[current.Length - 1] == 't')
				{

					current = current.Remove(len - 1, 1);
					return;
				}
			} //len>3	
		} //removeDerivational

		private void removeAugmentative()
		{
			int len = current.Length;
			//
			if ((len > 6) &&
				current.ToString().Substring(len - 4, 4).Equals("ajzn"))
			{

				current = current.Remove(len - 4, 4);
				return;
			}
			if ((len > 5) &&
				(current.ToString().Substring(len - 3, 3).Equals("izn") ||
					current.ToString().Substring(len - 3, 3).Equals("isk")))
			{

				current = current.Remove(len - 2, 2);
				palatalise();
				return;
			}
			if ((len > 4) &&
				current.ToString().Substring(len - 2, 2).Equals("\00e1k"))
			{
				//-ák

				current = current.Remove(len - 2, 2);
				return;
			}
		}

		private void removeDiminutive()
		{
			int len = current.Length;
			// 
			if ((len > 7) &&
				current.ToString().Substring(len - 5, 5).Equals("ou\u0161ek"))
			{
				//-oušek

				current = current.Remove(len - 5, 5);
				return;
			}
			if (len > 6)
			{
				if (current.ToString().Substring(len - 4, 4).Equals("e\u010dek") || //-eček
					current.ToString().Substring(len - 4, 4).Equals("\u00e9\u010dek") || //-éček
					current.ToString().Substring(len - 4, 4).Equals("i\u010dek") || //-iček
					current.ToString().Substring(len - 4, 4).Equals("\u00ed\u010dek") || //íček
					current.ToString().Substring(len - 4, 4).Equals("enek") ||
					current.ToString().Substring(len - 4, 4).Equals("\u00e9nek") || //-ének
					current.ToString().Substring(len - 4, 4).Equals("inek") ||
					current.ToString().Substring(len - 4, 4).Equals("\u00ednek"))
				{
					//-ínek

					current = current.Remove(len - 3, 3);
					palatalise();
					return;
				}
				if (current.ToString().Substring(len - 4, 4).Equals("\u00e1\u010dek") || //áček
					current.ToString().Substring(len - 4, 4).Equals("a\u010dek") || //aček
					current.ToString().Substring(len - 4, 4).Equals("o\u010dek") || //oček
					current.ToString().Substring(len - 4, 4).Equals("u\u010dek") || //uček
					current.ToString().Substring(len - 4, 4).Equals("anek") ||
					current.ToString().Substring(len - 4, 4).Equals("onek") ||
					current.ToString().Substring(len - 4, 4).Equals("unek") ||
					current.ToString().Substring(len - 4, 4).Equals("\u00e1nek"))
				{
					//-ánek

					current = current.Remove(len - 4, 4);
					return;
				}
			} //len>6
			if (len > 5)
			{
				if (current.ToString().Substring(len - 3, 3).Equals("e\u010dk") || //-ečk
					current.ToString().Substring(len - 3, 3).Equals("\u00e9\u010dk") || //-éčk 
					current.ToString().Substring(len - 3, 3).Equals("i\u010dk") || //-ičk
					current.ToString().Substring(len - 3, 3).Equals("\u00ed\u010dk") || //-íčk
					current.ToString().Substring(len - 3, 3).Equals("enk") || //-enk
					current.ToString().Substring(len - 3, 3).Equals("\u00e9nk") || //-énk 
					current.ToString().Substring(len - 3, 3).Equals("ink") || //-ink
					current.ToString().Substring(len - 3, 3).Equals("\u00ednk"))
				{
					//-ínk

					current = current.Remove(len - 3, 3);
					palatalise();
					return;
				}
				if (current.ToString().Substring(len - 3, 3).Equals("\u00e1\u010dk") || //-áčk
					current.ToString().Substring(len - 3, 3).Equals("au010dk") || //-ačk
					current.ToString().Substring(len - 3, 3).Equals("o\u010dk") || //-očk
					current.ToString().Substring(len - 3, 3).Equals("u\u010dk") || //-učk 
					current.ToString().Substring(len - 3, 3).Equals("ank") ||
					current.ToString().Substring(len - 3, 3).Equals("onk") ||
					current.ToString().Substring(len - 3, 3).Equals("unk"))
				{

					current = current.Remove(len - 3, 3);
					return;

				}
				if (current.ToString().Substring(len - 3, 3).Equals("\u00e1tk") || //-átk
					current.ToString().Substring(len - 3, 3).Equals("\u00e1nk") || //-ánk
					current.ToString().Substring(len - 3, 3).Equals("u\u0161k"))
				{
					//-ušk

					current = current.Remove(len - 3, 3);
					return;
				}
			} //len>5
			if (len > 4)
			{
				if (current.ToString().Substring(len - 2, 2).Equals("ek") ||
					current.ToString().Substring(len - 2, 2).Equals("\u00e9k") || //-ék
					current.ToString().Substring(len - 2, 2).Equals("\u00edk") || //-ík
					current.ToString().Substring(len - 2, 2).Equals("ik"))
				{

					current = current.Remove(len - 1, 1);
					palatalise();
					return;
				}
				if (current.ToString().Substring(len - 2, 2).Equals("\u00e1k") || //-ák
					current.ToString().Substring(len - 2, 2).Equals("ak") ||
					current.ToString().Substring(len - 2, 2).Equals("ok") ||
					current.ToString().Substring(len - 2, 2).Equals("uk"))
				{

					current = current.Remove(len - 1, 1);
					return;
				}
			}
			if ((len > 3) &&
				current.ToString().Substring(len - 1, 1).Equals("k"))
			{
				current = current.Remove(len - 1, 1);
			}
		}

//removeDiminutives

		private void removeComparative()
		{
			int len = current.Length;

			if ((len > 5) &&
				(current.ToString().Substring(len - 3, 3).Equals("ej\u0161") || //-ejš
					current.ToString().Substring(len - 3, 3).Equals("\u011bj\u0161")))
			{
				//-ějš

				current = current.Remove(len - 2, 2);
				palatalise();
			}
		}

		private void palatalise()
		{
			int len = current.Length;

			if (current.ToString().Substring(len - 2, 2).Equals("ci") ||
				current.ToString().Substring(len - 2, 2).Equals("ce") ||
				current.ToString().Substring(len - 2, 2).Equals("\u010di") || //-či
				current.ToString().Substring(len - 2, 2).Equals("\u010de"))
			{
				//-če

				current = StringBufferReplace(len - 2, len, current, "k");
				return;
			}
			if (current.ToString().Substring(len - 2, 2).Equals("zi") ||
				current.ToString().Substring(len - 2, 2).Equals("ze") ||
				current.ToString().Substring(len - 2, 2).Equals("\u017ei") || //-ži
				current.ToString().Substring(len - 2, 2).Equals("\u017ee"))
			{
				//-že

				current = StringBufferReplace(len - 2, len, current, "h");
				return;
			}
			if (current.ToString().Substring(len - 3, 3).Equals("\u010dt\u011b") || //-čtě
				current.ToString().Substring(len - 3, 3).Equals("\u010dti") || //-čti
				current.ToString().Substring(len - 3, 3).Equals("\u010dt\u00ed"))
			{
				//-čtí

				current = StringBufferReplace(len - 3, len, current, "ck");
				return;
			}
			if (current.ToString().Substring(len - 2, 2).Equals("\u0161t\u011b") || //-ště
				current.ToString().Substring(len - 2, 2).Equals("\u0161ti") || //-šti
				current.ToString().Substring(len - 2, 2).Equals("\u0161t\u00ed"))
			{
				//-ští

				current = StringBufferReplace(len - 2, len, current, "sk");
				return;
			}
			current = current.Remove(len - 1, 1);
		}

//palatalise

		private void removePossessives()
		{
			int len = current.Length;

			if (len > 5)
			{
				if (current.ToString().Substring(len - 2, 2).Equals("ov"))
				{
					current = current.Remove(len - 2, 2);
					return;
				}
				if (current.ToString().Substring(len - 2, 2).Equals("\u016fv"))
				{
					//-ův
					current = current.Remove(len - 2, 2);
					return;
				}
				if (current.ToString().Substring(len - 2, 2).Equals("in"))
				{
					current = current.Remove(len - 1, 1);
					palatalise();
				}
			}
		}

//removePossessives

		private void removeCase()
		{
			int len = current.Length;
			// 
			if ((len > 7) &&
				current.ToString().Substring(len - 5, 5).Equals("atech"))
			{
				current = current.Remove(len - 5, 5);
				return;
			} //len>7
			if (len > 6)
			{
				if (current.ToString().Substring(len - 4, 4).Equals("\u011btem"))
				{
					//-ětem

					current = current.Remove(len - 3, 3);
					palatalise();
					return;
				}
				if (current.ToString().Substring(len - 4, 4).Equals("at\u016fm"))
				{
					//-atům
					current = current.Remove(len - 4, 4);
					return;
				}
			}
			if (len > 5)
			{
				if (current.ToString().Substring(len - 3, 3).Equals("ech") ||
					current.ToString().Substring(len - 3, 3).Equals("ich") ||
					current.ToString().Substring(len - 3, 3).Equals("\u00edch"))
				{
					//-ích

					current = current.Remove(len - 2, 2);
					palatalise();
					return;
				}
				if (current.ToString().Substring(len - 3, 3).Equals("\u00e9ho") || //-ého
					current.ToString().Substring(len - 3, 3).Equals("\u011bmi") || //-ěmu
					current.ToString().Substring(len - 3, 3).Equals("emi") ||
					current.ToString().Substring(len - 3, 3).Equals("\u00e9mu") ||
					// -ému				                                                                current.substring( len-3,len).equals("ete")||
					current.ToString().Substring(len - 3, 3).Equals("eti") ||
					current.ToString().Substring(len - 3, 3).Equals("iho") ||
					current.ToString().Substring(len - 3, 3).Equals("\u00edho") || //-ího
					current.ToString().Substring(len - 3, 3).Equals("\u00edmi") || //-ími
					current.ToString().Substring(len - 3, 3).Equals("imu"))
				{

					current = current.Remove(len - 2, 2);
					palatalise();
					return;
				}
				if (current.ToString().Substring(len - 3, 3).Equals("\u00e1ch") || //-ách
					current.ToString().Substring(len - 3, 3).Equals("ata") ||
					current.ToString().Substring(len - 3, 3).Equals("aty") ||
					current.ToString().Substring(len - 3, 3).Equals("\u00fdch") || //-ých
					current.ToString().Substring(len - 3, 3).Equals("ama") ||
					current.ToString().Substring(len - 3, 3).Equals("ami") ||
					current.ToString().Substring(len - 3, 3).Equals("ov\u00e9") || //-ové
					current.ToString().Substring(len - 3, 3).Equals("ovi") ||
					current.ToString().Substring(len - 3, 3).Equals("\u00fdmi"))
				{
					//-ými

					current = current.Remove(len - 3, 3);
					return;
				}
			}
			if (len > 4)
			{
				if (current.ToString().Substring(len - 2, 2).Equals("em"))
				{
					current = current.Remove(len - 1, 1);
					palatalise();
					return;

				}
				if (current.ToString().Substring(len - 2, 2).Equals("es") ||
					current.ToString().Substring(len - 2, 2).Equals("\u00e9m") || //-ém
					current.ToString().Substring(len - 2, 2).Equals("\u00edm"))
				{
					//-ím

					current = current.Remove(len - 2, 2);
					palatalise();
					return;
				}
				if (current.ToString().Substring(len - 2, 2).Equals("\u016fm"))
				{

					current = current.Remove(len - 2, 2);
					return;
				}
				if (current.ToString().Substring(len - 2, 2).Equals("at") ||
					current.ToString().Substring(len - 2, 2).Equals("\u00e1m") || //-ám
					current.ToString().Substring(len - 2, 2).Equals("os") ||
					current.ToString().Substring(len - 2, 2).Equals("us") ||
					current.ToString().Substring(len - 2, 2).Equals("\u00fdm") || //-ým
					current.ToString().Substring(len - 2, 2).Equals("mi") ||
					current.ToString().Substring(len - 2, 2).Equals("ou"))
				{

					current = current.Remove(len - 2, 2);
					return;
				}
			} //len>4
			if (len > 3)
			{
				if (current.ToString().Substring(len - 1, 1).Equals("e") ||
					current.ToString().Substring(len - 1, 1).Equals("i"))
				{
					palatalise();
					return;
				}
				if (current.ToString().Substring(len - 1, 1).Equals("\u00ed") || //-é
					current.ToString().Substring(len - 1, 1).Equals("\u011b"))
				{
					//-ě

					palatalise();
					return;
				}
				if (current.ToString().Substring(len - 1, 1).Equals("u") ||
					current.ToString().Substring(len - 1, 1).Equals("y") ||
					current.ToString().Substring(len - 1, 1).Equals("\u016f"))
				{
					//-ů

					current = current.Remove(len - 1, 1);
					return;
				}
				if (current.ToString().Substring(len - 1, 1).Equals("a") ||
					current.ToString().Substring(len - 1, 1).Equals("o") ||
					current.ToString().Substring(len - 1, 1).Equals("\u00e1") || // -á
					current.ToString().Substring(len - 1, 1).Equals("\u00e9") || //-é
					current.ToString().Substring(len - 1, 1).Equals("\u00fd"))
				{
					//-ý

					current = current.Remove(len - 1, 1);
					return;
				}
			} //len>3
		}
	}
}
