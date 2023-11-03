using Microsoft.Azure.Cosmos;

namespace HPW.Services
{
    class ChallengeService : IChallengeService
    {
        private readonly _challengeContainer;
        public ChallengeService(Database database){
            _challengeContainer = database.GetContainer("DailyChallenge");
        }

        public ChallengeService GetChallenge(){

        }

        
    }
    
}