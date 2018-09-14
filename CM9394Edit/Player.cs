using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CM9394Edit
{
    [Serializable]
    public class Player
    {
        private int id;
        private string firstName;
        private string surname;
        private int age;
        private int passing;
        private int tackling;
        private int pace;
        private int heading;
        private int flair;
        private int creativity;
        private int stamina;
        private int influence;
        private int agility;
        private int strength;
        private int fitness;
        private int weeksInjured;
        private int illness;
        private int morale;
        private int currentSkill;
        private int potentialSkill;
        private int goalscoring;
        private Position position;
        private string club;

        public int Id { get { return id; } set { id = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string SurName { get { return surname; } set { surname = value; } }
        public int Age { get { return age; } set { age = value; } }
        public int Passing { get { return passing; } set { passing = value; } }
        public int Tackling { get { return tackling; } set { tackling = value; } }
        public int Pace { get { return pace; } set { pace = value; } }
        public int Heading { get { return heading; } set { heading = value; } }
        public int Flair { get { return flair; } set { flair = value; } }
        public int Creativity { get { return creativity; } set { creativity = value; } }
        public int Stamina { get { return stamina; } set { stamina = value; } }
        public int Influence { get { return influence; } set { influence = value; } }
        public int Agility { get { return agility; } set { agility = value; } }
        public int Strength { get { return strength; } set { strength = value; } }
        public int Fitness { get { return fitness; } set { fitness = value; } }
        public int WeeksNA { get { return weeksInjured; } set { weeksInjured = value; } }
        public int Illness { get { return illness; } set { illness = value; } }
        public int Morale { get { return morale; } set { morale = value; } }
        public int CurrentSkill { get { return currentSkill; } set { currentSkill = value; } }
        public int PotentialSkill { get { return potentialSkill; } set { potentialSkill = value; } }
        public int GoalScoring { get { return goalscoring; } set { goalscoring = value; } }
        public Position Position { get { return position; } set { position = value; } }
        public string Club { get { return club; } set { club = value; } }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

    }
}
