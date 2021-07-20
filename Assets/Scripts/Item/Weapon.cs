using UnityEngine;
using System.Collections;

namespace Item{
	public class Weapon : BaseItem {

		private double max_damage;
		private double min_damage;

		private double damage;
		private double crit_persent;

		[Range(0, 7)]
		public int rare; //htlrjcnm 
		public int level;

		public void Init(int _price, float _weight, double _crit, double _damage, int _rare, int _level)
		{		
			base.Init (_price, _weight);

			damage = _damage;
			crit_persent = _crit;
			rare = _rare;
			level = _level;
		
			setMaxDamage ();
			setMinDamage ();
		}


		//Расчитываем разрброс урона

		private void setMaxDamage(){
			max_damage = damage + (damage * crit_persent / 2);
		}

		private void setMinDamage(){
			min_damage = damage - (damage * crit_persent / 2);
		}


		//------------Getters-----------//

		public double getMinDamage(){
			return min_damage;
		}

		public double getMaxDamage(){
			return max_damage;
		}

		public int getRare(){
			return rare;
		}

		public int getLevel(){
			return level;
		}
	}
}
