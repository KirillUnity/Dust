namespace Item{
	public struct CharacterRequirement {
		int level; 
		int agility;
		int intelligence;
		int body;

		public CharacterRequirement (int _level, int _agility, int _intelligence, int _body){

			agility = _agility;
			body = _body;
			level = _level;
			intelligence = _intelligence;
		}
	}
}
