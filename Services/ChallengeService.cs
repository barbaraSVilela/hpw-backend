using Microsoft.Azure.Cosmos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPW.Database;
using HPW.Entities;
using HPW.Extensions;
using Microsoft.Azure.Cosmos.Linq;
using Challenge = HPW.Entities.Challenge;
namespace HPW.Services
{
    class ChallengeService : IChallengeService
    {
        private readonly Container _challengeContainer;
        public ChallengeService(Microsoft.Azure.Cosmos.Database database){
            _challengeContainer = database.GetContainer("Challenge");
        }

        public async Task<Challenge> GetChallenge(int challengeId){
            var query = _challengeContainer.GetItemLinqQueryable<Challenge>().Where(c => c.Id == challengeId).ToFeedIterator();

            return (await ExecuteChallengeQuery(query)).FirstOrDefault();        }
        public async Task<List<Challenge>> GetAllChallenges(){
            var query = _challengeContainer.GetItemLinqQueryable<Challenge>().ToFeedIterator();

            return (await ExecuteChallengeQuery(query)).ToList();        
               }


        private async Task<IEnumerable<Challenge>> ExecuteChallengeQuery(FeedIterator<Challenge> query)
        {
            var response = await query.ReadAllAsync();
            return response;

        }
        
    }
    
}