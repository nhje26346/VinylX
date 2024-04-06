using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinylX.Repositories;

namespace VinylX.Test.Mockups.Repositories
{
    internal class RepositoryFoundationMockup : IRepositoryFoundation
    {
        private List<Action> onSaveActions;

        public RepositoryFoundationMockup()
        {
            onSaveActions = new List<Action>();
        }

        public Task<int> SaveChangesAsync()
        {
            var actions = onSaveActions;
            onSaveActions = new List<Action>();
            foreach (var action in actions)
            {
                action();
            }
            return Task.FromResult(actions.Count);
        }

        public void AddOnSaveAction(Action action)
        {
            onSaveActions.Add(action);
        }
    }
}
