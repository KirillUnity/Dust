using UnityEngine;
using System.Collections;

namespace Item{
	public class Dagger : Weapon {

		private DamageType type;
		private Phisical phisical_type;
		private CharacterRequirement character;

		private float crit_chance;


		public void Init(int price, float weight, float _crit_chance)
		{
			base.Init (price, weight);
			crit_chance = _crit_chance;
			type = DamageType.Phisical;
			phisical_type = Phisical.transfixion;
		}

		//Получаем наносимый урон в зависимости от атаки
		public double getDamage(float rate){
			double max_damage = base.getMaxDamage();
			double min_damage = base.getMinDamage();

			if (rate == 1) {
				return max_damage;
			} else if (rate == 0.5f) {
				return (max_damage + min_damage) / 2;
			} else {
				return min_damage;
			}
		}
	}
}
