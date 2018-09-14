using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace CM9394Edit
{
    public class DataUtil
    {
        private List<Club> clubs = new List<Club>();
        private List<PlayerName> playerNames = new List<PlayerName>();
        private List<Player> players = new List<Player>();
        private string path;
        private string extendednameFilePath;

        public DataUtil(string path,string extendednamefilePath)
        {
            this.path = path;
            this.extendednameFilePath = extendednamefilePath;
            GetClubs();
            GetNames();
            GetPlayers();
            GetPositions();
        }

        public void SavePlayer(int index, int age, string club, int passing, int tackling, int pace, int heading, int flair, int creativity, int stamina, int influence, int agility, int strength, int fitness, int illness, int weeks, int morale, int goals, int currSkill, int potSkill,Position pos)
        {
            try
            {
                BinaryWriter bw = new BinaryWriter(File.OpenWrite(path));

                bw.BaseStream.Position = HexAddress.AGE_FROM + index;
                string hex = age.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.CLUB + index;
                Console.WriteLine(">>>>>"+GetHexFromClubName(club).ToString());
                bw.Write(Convert.ToByte(GetHexFromClubName(club).ToString(), 16));
                

                bw.BaseStream.Position = HexAddress.PASSING_FROM + index;
                hex = passing.ToString("X");
                bw.Write(Convert.ToByte(hex,16));

                bw.BaseStream.Position = HexAddress.TACKLING_FROM + index;
                hex = tackling.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.PACE_FROM + index;
                hex = pace.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.HEADING_FROM + index;
                hex = heading.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.FLAIR_FROM + index;
                hex = flair.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.CREATIVITY_FROM + index;
                hex = creativity.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.STAMINA_FROM +index;
                hex = stamina.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.INFLUENCE_FROM + index;
                hex = influence.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.AGILITY_FROM + index;
                hex = agility.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.STRENGTH_FROM + index;
                hex = strength.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.FITNESS_FROM + index;
                hex = fitness.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.ILLNESS_FROM + index;
                hex = illness.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.WEEKSNA_FROM + index;
                hex = weeks.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.MORALE_FROM + index;
                hex = morale.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.GOALSCORING_FROM + index;
                hex = goals.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.CURRENTSKILL_FROM + index;
                hex = currSkill.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.POTENTIALSKILL_FROM + index;
                hex = potSkill.ToString("X");
                bw.Write(Convert.ToByte(hex, 16));

                bw.BaseStream.Position = HexAddress.POSITION_FROM + (index*4);
                hex = pos.hex;
                bw.Write(Convert.ToByte(hex, 16));


                bw.Close();

                MessageBox.Show("Saved");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error saving: \n" + e.ToString());
            }
        }

        public List<Player> FindPlayers(string lastname, string firstname, int minage, int maxage, int goalscoring, int currskill, int potskill, string club, Position pos)
        {
            List<Player> filteredPlayers = new List<Player>();
            

            for (int i = 0; i < players.Count; i++)
            {
                Player currentP = players[i];
                bool addPlayer = true;

                if (lastname != null && lastname.Length > 0 && addPlayer)
                {
                    if (currentP.SurName.ToLower().IndexOf(lastname.ToLower()) == 0) addPlayer = true;
                    else addPlayer = false;
                }

                if (firstname != null && firstname.Length > 0 && addPlayer)
                {
                    if (currentP.SurName.ToLower().IndexOf(lastname.ToLower()) == 0) addPlayer = true;
                    else addPlayer = false;
                }

                if (currentP.Age >= minage && addPlayer) addPlayer = true;
                else addPlayer = false;

                if (currentP.Age <= maxage && addPlayer) addPlayer = true;
                else addPlayer = false;

                if (currentP.GoalScoring >= goalscoring && addPlayer) addPlayer = true;
                else addPlayer = false;

                if (currentP.CurrentSkill >= currskill && addPlayer) addPlayer = true;
                else addPlayer = false;

                if (currentP.PotentialSkill >= potskill && addPlayer) addPlayer = true;
                else addPlayer = false;

                if (club != null && club.Length > 0 && addPlayer)
                {
                    if (currentP.Club == club) addPlayer = true;
                    else addPlayer = false;
                }

                if (pos != null && addPlayer)
                {
                    if(addPlayer && (pos.G || pos.D || pos.M || pos.A))
                    {
                        if (pos.G && currentP.Position.G) addPlayer = true;
                        else if ((pos.D && !pos.M && !pos.A) && currentP.Position.D) addPlayer = true;
                        else if ((!pos.D && pos.M && !pos.A) && currentP.Position.M) addPlayer = true;
                        else if ((!pos.D && !pos.M && pos.A) && currentP.Position.A) addPlayer = true;
                        else if ((pos.D && pos.M && !pos.A) && currentP.Position.D && currentP.Position.M) addPlayer = true;
                        else if ((pos.D && !pos.M && pos.A) && currentP.Position.D && currentP.Position.A) addPlayer = true;
                        else if ((!pos.D && pos.M && pos.A) && currentP.Position.M && currentP.Position.A) addPlayer = true;
                        else if ((pos.D && pos.M && pos.A) && currentP.Position.D && currentP.Position.M && currentP.Position.A) addPlayer = true;
                        else addPlayer = false;
                    }

                    if (addPlayer && (pos.R || pos.C || pos.L))
                    {
                        if ((pos.R && !pos.C && !pos.L) && currentP.Position.R) addPlayer = true;
                        else if ((!pos.R && pos.C && !pos.L) && currentP.Position.C) addPlayer = true;
                        else if ((!pos.R && !pos.C && pos.L) && currentP.Position.L) addPlayer = true;
                        else if ((pos.R && pos.C && !pos.L) && currentP.Position.R && currentP.Position.C) addPlayer = true;
                        else if ((pos.R && !pos.C && pos.L) && currentP.Position.R && currentP.Position.L) addPlayer = true;
                        else if ((!pos.R && pos.C && pos.L) && currentP.Position.C && currentP.Position.L) addPlayer = true;
                        else if ((pos.R && pos.C && !pos.L) && currentP.Position.R && currentP.Position.C) addPlayer = true;
                        else if ((pos.R && pos.C && pos.L) && currentP.Position.R && currentP.Position.C && currentP.Position.L) addPlayer = true;
                        else addPlayer = false;
                    }
                }

                if (addPlayer) filteredPlayers.Add(currentP);
            }


            return filteredPlayers;
        }

        private void GetClubs()
        {
            BinaryReader br = null;
            string hex = "";
            int j = 0;
            try
            {
                br = new BinaryReader(File.OpenRead(path));

                for (int i = HexAddress.CLUB_INDEX_CMEDV2_FROM; i <= HexAddress.CLUB_INDEX_CMEDV2_TO; i++)
                {
                    br.BaseStream.Position = i;
                    hex = br.ReadByte().ToString("X2");
                    //Console.WriteLine(">>>>"+hex);
                    Club c = new Club();
                    c.HexAddress = hex;
                    c.Name = GetResource("teams.txt")[j];
                    
                    clubs.Add(c);
                    j++;
                }
                //Console.WriteLine("------------------------------------------------------------");
                br.Close();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString()+" HEX: "+hex);
            }
        }


        private void GetPlayers()
        {
            BinaryReader br = null;
            string hex = "";
            try
            {
                br = new BinaryReader(File.OpenRead(path));

                for (int i = 0; i < 1860; i++)
                {
                    Player p = new Player();

                    p.Id = i+1;
                    p.FirstName = playerNames[i].firstName;
                    p.SurName = playerNames[i].surName;

                    br.BaseStream.Position = HexAddress.AGE_FROM + i;
                    hex = br.ReadByte().ToString("X2");
                    
                    p.Age =  Convert.ToInt32(hex,16);

                    br.BaseStream.Position = HexAddress.AGILITY_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Agility = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.CREATIVITY_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Creativity = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.FITNESS_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Fitness = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.FLAIR_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Flair = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.CURRENTSKILL_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.CurrentSkill = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.GOALSCORING_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.GoalScoring = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.HEADING_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Heading = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.ILLNESS_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Illness = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.INFLUENCE_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Influence = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.MORALE_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Morale = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.PACE_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Pace = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.PASSING_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Passing = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.POTENTIALSKILL_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.PotentialSkill = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.STAMINA_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Stamina = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.STRENGTH_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Strength = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.TACKLING_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Tackling = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.WEEKSNA_FROM + i;
                    hex = br.ReadByte().ToString("X2");

                    p.WeeksNA = Convert.ToInt32(hex, 16);

                    br.BaseStream.Position = HexAddress.CLUB + i;
                    hex = br.ReadByte().ToString("X2");

                    p.Club = GetClubNameFromHex(hex);
                    //Console.WriteLine(hex);
                    players.Add(p);
                }

                br.Close();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

        }

        public string GetHexFromClubName(string name)
        {
            foreach(Club c in clubs)
            {
                if (name == c.Name) return c.HexAddress;
            }
            return null;
        }

        public string GetClubNameFromHex(string hex)
        {
            foreach (Club c in clubs)
            {
                if (hex== c.HexAddress) return c.Name;
            }
            return "";
        }

        private void GetNames()
        {
            foreach (string s in GetResource("names.txt"))
            {
                PlayerName p = new PlayerName();
                
                string[] split = s.Split(new char[] {','});
                if (split.Length < 2) p.surName = "noname";
                else
                {
                    string first = split[0];
                    if (first.IndexOf("@") >= 0) first = first.Substring(1);
                    p.firstName = split[1];
                    p.surName = first;
                }
                playerNames.Add(p);
            }

            if(extendednameFilePath!=null)
            {
                string[] extendedNamesLines = File.ReadAllLines(extendednameFilePath);
                foreach (string extendedNamesLine in extendedNamesLines)
                {
                    string[] split = extendedNamesLine.Split(':');
                    int id = Convert.ToInt32(split[0]);
                    string name = split[1];
                    string[] names = name.Split(' ');
                    string firstName = names[0];
                    string surName = names[1];
                    PlayerName newName = new PlayerName();
                    newName.firstName = firstName;
                    newName.surName = surName;
                    playerNames[id - 1] = newName;
                }

            }
        }

        public void GetPositions()
        {
            BinaryReader br = null;
            string hex = "";
            int i = 0;
            try
            {
                br = new BinaryReader(File.OpenRead(path));
                int pos = HexAddress.POSITION_FROM;

                for (; i < 1860; i++)
                {
                    br.BaseStream.Position = pos;
                    hex = br.ReadByte().ToString("X2");
                    players[i].Position = Position.GetPosition(hex);
                    //Console.WriteLine(hex);
                    pos = pos + 4;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString() + " HEX: " + hex + " i: " + i);
            }
        }

        private List<string> GetResource(string filename)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("CM9394Edit."+filename));
                List<string> lines = new List<string>();
                while(!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }
                sr.Close();
                return lines;
            }
            catch
            {
                MessageBox.Show("Error accessing resources!");
            }

            return new List<string>();
        }

        public List<Club> Clubs
        {
            get { return clubs; }
            set { clubs = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public List<Player> Players
        {
            get { return players; }
            set { players = value; }
        }

        public struct Club
        {
            public string HexAddress;
            public string Name;
        }

        public struct PlayerName
        {
            public string firstName;
            public string surName;
        }
    }
}
