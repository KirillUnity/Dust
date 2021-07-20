using UnityEngine;
using System.Collections;

namespace Item{
	public abstract class BaseItem : MonoBehaviour, Trade {

		private int price; // Цена передмета 
		private float weight; //Вес предмета(Занимаемое место в инвентаре)


		public void Init(int price, float weight)
		{
			this.price = price;
			this.weight = weight;
		}

		public virtual void sale()
		{
			
		}

		public virtual void purchase()
		{

		}
	}

	public interface Trade {
		void sale ();
		void purchase();


	}
}


