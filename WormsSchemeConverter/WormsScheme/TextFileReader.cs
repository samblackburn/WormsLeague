﻿using System.Collections.Generic;
using System.IO;

namespace WormsScheme
{
    public class TextFileReader
    {
        private readonly string m_FilePath;

        public TextFileReader(string filePath)
        {
            m_FilePath = filePath;
        }

        public Scheme GetModel()
        {
            using (var b = new StreamReader(File.Open(m_FilePath, FileMode.Open)))
            {
                const string signature = "SCHM";
                const int version = 2;

                // Skip over some heading lines
                b.ReadLine();
                b.ReadLine();
                b.ReadLine();
                b.ReadLine();

                var hotSeatDelay = GetInt(b);
                var retreatTime = GetInt(b);
                var ropeRetreatTime = GetInt(b);
                var displayTotalRoundTime = GetBool(b);
                var automaticReplays = GetBool(b);
                var fallDamage = GetInt(b);
                var artilleryMode = GetBool(b);
                var stockpilingMode = GetByte(b);
                var wormSelect = GetByte(b);
                var suddenDeathEvent = GetByte(b);
                var waterRiseRate = GetInt(b);
                var weaponCrateProb = GetInt(b);
                var healthCrateProb = GetInt(b);
                var utilityCrateProb = GetInt(b);
                var healthCrateEnergy = GetInt(b);
                var donorCards = GetBool(b);
                var hazardObjects = GetInt(b);
                var mineDelay = GetInt(b);
                var dudMines = GetBool(b);
                var wormPlacement = GetBool(b);
                var initialWormEnergy = GetInt(b);
                var turnTime = GetInt(b);
                var roundTime = GetInt(b);
                var numberOfRounds = GetInt(b);
                var blood = GetBool(b);
                var aquaSheep = GetBool(b);
                var sheepHeaven = GetBool(b);
                var godWorms = GetBool(b);
                var indestructibleLand = GetBool(b);
                var upgradedGrenade = GetBool(b);
                var upgradedShotgun = GetBool(b);
                var upgradedClusters = GetBool(b);
                var upgradedLongbow = GetBool(b);
                var teamWeapons = GetBool(b);
                var superWeapons = GetBool(b);

                // Skip over the middle heading
                b.ReadLine();
                b.ReadLine();
                b.ReadLine();
                b.ReadLine();
                b.ReadLine();
                b.ReadLine();
                b.ReadLine();

                var weapons = new List<Weapon>();

                foreach (var weaponName in Weapons.AllWeapons)
                {
                    var values = GetInts(b);
                    var ammo = values[0];
                    var power = values[1];
                    var delay = values[2];
                    var crateProb = values[3];

                    weapons.Add(new Weapon(weaponName, ammo, power, delay, crateProb));
                }

                return new Scheme(signature, version, hotSeatDelay, retreatTime, ropeRetreatTime,
                    displayTotalRoundTime,
                    automaticReplays, fallDamage, artilleryMode, stockpilingMode, wormSelect, suddenDeathEvent,
                    waterRiseRate, weaponCrateProb, donorCards, healthCrateProb, healthCrateEnergy, utilityCrateProb,
                    hazardObjects, mineDelay, dudMines, wormPlacement, initialWormEnergy, turnTime, roundTime,
                    numberOfRounds, blood, aquaSheep, sheepHeaven, godWorms, indestructibleLand, upgradedGrenade,
                    upgradedShotgun, upgradedClusters, upgradedLongbow, teamWeapons, superWeapons,
                    weapons);

            }
        }

        private static byte GetByte(StreamReader b)
        {
            return (byte)GetInt(b);
        }

        private static bool GetBool(StreamReader b)
        {
            return bool.Parse(GetValue(b.ReadLine()));
        }

        private static int GetInt(StreamReader b)
        {
            return int.Parse(GetValue(b.ReadLine()));
        }

        private static int[] GetInts(StreamReader b)
        {
            var values = new int[4];
            var line = b.ReadLine();
            values[0] = int.Parse(GetValue(line.Substring(0, 44)));
            values[1] = int.Parse(GetValue(line.Substring(44, 10)));
            values[2] = int.Parse(GetValue(line.Substring(55, 20)));
            values[3] = int.Parse(GetValue(line.Substring(75, line.Length - 75)));

            return values;
        }

        private static string GetValue(string text)
        {
            var startIndex = text.IndexOf('[') + 1;
            var endIndex = text.IndexOf(']');
            var substring = text.Substring(startIndex, endIndex - startIndex);
            return substring;
        }
    }
}
