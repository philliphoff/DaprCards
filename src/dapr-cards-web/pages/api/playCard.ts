import { NextApiRequest, NextApiResponse } from 'next';
import { getAsync, postAsync } from '../../util/fetchAsync';

const daprPort = process.env.DAPR_HTTP_PORT || 3500;
const daprActorUrl = (actorType: string, actorId: string, method: string) => `http://localhost:${daprPort}/v1.0/actors/${actorType}/${actorId}/method/${method}`;
const playGameCardUrl = (gameId: string) => daprActorUrl('GameActor', gameId, 'PlayCardAsync');

type GameCard = {
    cardId: string;
    isPlayed: boolean;
    value: number;
};

type GamePlayer = {
    cards?: GameCard[];
    userId: string;
};

type GameDetails = {
    players?: GamePlayer[];
};

type PlayCardOptions = {
    gameId: string;
    cardId: string;
};

export default async (req: NextApiRequest, res: NextApiResponse) => {
    const userId = <string>req.headers['x-user-id'];
    const options = <PlayCardOptions>req.body;

    const details = await postAsync<GameDetails>(playGameCardUrl(options.gameId), { cardId: options.cardId, userId });
    const player = details.players && details.players.find(player => player.userId === userId);
    const userCards = player && player.cards;

    if (userCards) {
        res.setHeader('Content-Type', 'application/json');
        res.statusCode = 200;
        res.end(JSON.stringify(userCards));
    } else {
        res.statusCode = 400;
        res.end();
    }
};
