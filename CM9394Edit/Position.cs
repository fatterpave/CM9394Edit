using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CM9394Edit
{
    [Serializable]
    public class Position
    {
        public Position()
        {
        }

        private static Dictionary<string,Position> positionsHash = new Dictionary<string,Position>();        
     
        static Position[] position = 
        {
            new Position("18","98",false,false,false,true,true,false,false,"AR"),
            new Position("28","A8",false,false,false,true,false,true,false,"AL"),
            new Position("38","B8",false,false,false,true,true,true,false,"ARL"),
            new Position("48","C8",false,false,false,true,false,false,true,"AC"),
            new Position("58","D8",false,false,false,true,true,false,true,"ARC"),
            new Position("68","E8",false,false,false,true,false,true,true,"ALC"),
            new Position("78","F8",false,false,false,true,true,true,true,"ARLC"),

            new Position("14","94",false,false,true,false,true,false,false,"MR"),
            new Position("24","A4",false,false,true,false,false,true,false,"ML"),
            new Position("34","B4",false,false,true,false,true,true,false,"MRL"),
            new Position("44","C4",false,false,true,false,false,false,true,"MC"),
            new Position("54","D4",false,false,true,false,true,false,true,"MRC"),
            new Position("64","E4",false,false,true,false,false,true,true,"MLC"),
            new Position("74","F4",false,false,true,false,true,true,true,"MRLC"),
            
            new Position("12","92",false,true,false,false,true,false,false,"DR"),
            new Position("22","A2",false,true,false,false,false,true,false,"DL"),
            new Position("32","B2",false,true,false,false,true,true,false,"DRL"),
            new Position("42","C2",false,true,false,false,false,false,true,"DC"),
            new Position("52","D2",false,true,false,false,true,false,true,"DRC"),
            new Position("62","E2",false,true,false,false,false,true,true,"DLC"),
            new Position("72","F2",false,true,false,false,true,true,true,"DRLC"),
            
            new Position("1C","9C",false,false,true,true,true,false,false,"MAR"),
            new Position("2C","AC",false,false,true,true,false,true,false,"MAL"),
            new Position("3C","BC",false,false,true,true,true,true,false,"MARL"),
            new Position("4C","CC",false,false,true,true,false,false,true,"MAC"),
            new Position("5C","DC",false,false,true,true,true,false,true,"MARC"),
            new Position("6C","EC",false,false,true,true,false,true,true,"MALC"),
            new Position("7C","FC",false,false,true,false,true,true,true,"MARLC"),
            
            new Position("1A","9A",false,true,false,true,true,false,false,"DAR"),
            new Position("2A","AA",false,true,false,true,false,true,false,"DAL"),
            new Position("3A","BA",false,true,false,true,true,true,false,"DARL"),
            new Position("4A","CA",false,true,false,true,false,false,true,"DAC"),
            new Position("5A","DA",false,true,false,true,true,false,true,"DARC"),
            new Position("6A","EA",false,true,false,true,false,true,true,"DALC"),
            new Position("7A","FA",false,true,false,true,true,true,true,"DARLC"),

            new Position("16","96",false,true,true,false,true,false,false,"DMR"),
            new Position("26","A6",false,true,true,false,false,true,false,"DML"),
            new Position("36","B6",false,true,true,false,true,true,false,"DMRL"),
            new Position("46","C6",false,true,true,false,false,false,true,"DMC"),
            new Position("56","D6",false,true,true,false,true,false,true,"DMRC"),
            new Position("66","E6",false,true,true,false,false,true,true,"DMLC"),
            new Position("76","F6",false,true,true,false,true,true,true,"DMRLC"),

            new Position("1E","9E",false,true,true,true,true,false,false,"DMAR"),
            new Position("2E","AE",false,true,true,true,false,true,false,"DMAL"),
            new Position("3E","BE",false,true,true,true,true,true,false,"DMARL"),
            new Position("4E","CE",false,true,true,true,false,false,true,"DMAC"),
            new Position("5E","DE",false,true,true,true,true,false,true,"DMARC"),
            new Position("6E","EE",false,true,true,true,false,true,true,"DMALC"),
            new Position("7E","FE",false,true,true,true,true,true,true,"DMARLC"),

            new Position("81","01",true,false,false,false,false,false,false,"G"),

            new Position("7F","00",true,true,true,true,true,true,true,"GDMARLC")

        };

        public static Position GetPosition(bool G, bool D, bool M, bool A, bool R, bool C, bool L)
        {
            foreach (Position p in position)
            {
                if (p.G == G && p.D == D && p.M == M && p.A == A && p.R == R && p.C == C && p.L == L)
                {
                    return p;
                }
            }
            return null;
        }

        public static Position GetPosition(string hex)
        {
            Position p = null;
            bool isPresent = positionsHash.ContainsKey(hex);
            if (isPresent) p = positionsHash[hex];
            else p = positionsHash["7F"];

            return p;
        }

        public Position(string hex,string secondaryHex,bool goalie, bool defender, bool midfield, bool attacker, bool right, bool left, bool center,string descriptor)
        {
            this.hex = hex;
            this.secondaryHex = secondaryHex;
            G = goalie;
            D = defender;
            M = midfield;
            A = attacker;
            R = right;
            L = left;
            C = center;
            this.descriptor = descriptor;
            positionsHash.Add(hex, this);
            positionsHash.Add(secondaryHex, this);
        }

        public string hex;
        public string secondaryHex;
        public bool G;
        public bool D;
        public bool M;
        public bool A;
        public bool R;
        public bool L;
        public bool C;
        public string descriptor;
    }
}
