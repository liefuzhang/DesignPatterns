using System;
using System.Collections.Generic;
using System.Linq;
using NUnit;
using NUnit.Framework;

namespace Coding.Exercise {
    public abstract class Creature {
        public virtual int Attack { get; set; }
        public virtual int Defense { get; set; }
    }

    public class Goblin : Creature {
        protected Game game;
        protected int attack;
        protected int defense;

        public Goblin(Game game) {
            this.game = game;
            attack = 1;
            defense = 1;
        }

        public override int Attack => attack + game.NumberOfGoblinKings;
        public override int Defense => defense + game.NumberOfGoblins - 1;

        public override string ToString() {
            return $"{nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public class GoblinKing : Goblin {
        public GoblinKing(Game game) : base(game) {
            attack = 3;
            defense = 3;
        }

        public override int Attack => attack + game.NumberOfGoblinKings - 1;
    }

    public class Game {
        public IList<Creature> Creatures;
        public int NumberOfGoblins => Creatures.Count;
        public int NumberOfGoblinKings => Creatures.OfType<GoblinKing>().Count();

        public Game() {
            Creatures = new List<Creature>();
        }

        public static void Main(string[] args) {
            
        }
    }

    [TestFixture]
    public class GameTest {
        [Test]
        public void Test() {
            var game = new Game();
            var goblin = new Goblin(game);
            game.Creatures.Add(goblin);
            Assert.That(goblin.Attack, Is.EqualTo(1));
            Assert.That(goblin.Defense, Is.EqualTo(1));
        }

        [Test]
        public void Test1() {
            var game = new Game();
            var goblin = new Goblin(game);
            var goblin2 = new Goblin(game);
            var goblinKing = new GoblinKing(game);
            game.Creatures.Add(goblin);
            game.Creatures.Add(goblin2);
            game.Creatures.Add(goblinKing);
            Assert.That(goblin.Attack, Is.EqualTo(2));
            Assert.That(goblin.Defense, Is.EqualTo(3));
        }

    }
}
