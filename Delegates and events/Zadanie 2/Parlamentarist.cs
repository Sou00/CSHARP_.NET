using System;

namespace Zadanie_2
{
    public class Parlamentarist
    {
        private Parlament a;
        public Parlamentarist(Parlament b)
        {
            a = b;
            b.VoteEnd += Vote;
        }
        public EventHandler Voting;
        public void Vote(object sender, EventArgs e)
        {
            Voting?.Invoke(this ,EventArgs.Empty);
        }
        
    }
}