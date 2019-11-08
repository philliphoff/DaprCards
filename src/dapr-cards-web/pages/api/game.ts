import { NextApiRequest, NextApiResponse } from 'next';
import { getAsync } from '../../util/fetchAsync';

const daprPort = process.env.DAPR_HTTP_PORT || 3500;
const daprActorUrl = (actorType: string, actorId: string, method: string) => `http://localhost:${daprPort}/v1.0/actors/${actorType}/${actorId}/method/${method}`;
const getGameDetailsUrl = (gameId: string) => daprActorUrl('GameActor', gameId, 'GetDetailsAsync');

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

export default async (req: NextApiRequest, res: NextApiResponse) => {
    const userId = <string>req.headers['x-user-id'];
    const gameId = <string>req.body;

    const details = await getAsync<GameDetails>(getGameDetailsUrl(gameId), { gameId });
    const player = details.players && details.players.find(player => player.userId === userId);
    const userCards = player && player.cards;

    if (userCards) {
        res.setHeader('Content-Type', 'application/json');
        res.statusCode = 200;
        res.end(JSON.stringify(details));
    } else {
        res.statusCode = 400;
        res.end();
    }
};
