import { NextApiRequest, NextApiResponse } from 'next';
import { getAsync } from '../../util/fetchAsync';

const daprPort = process.env.DAPR_HTTP_PORT || 3500;
const daprActorUrl = (actorType: string, actorId: string, method: string) => `http://localhost:${daprPort}/v1.0/actors/${actorType}/${actorId}/method/${method}`;
const getUserDetailsUrl = (userId: string) => daprActorUrl('UserActor', userId, 'GetDetailsAsync');

type UserDetails = {
    decks?: {
        deckId?: string;
    }[];
};

export default async (req: NextApiRequest, res: NextApiResponse) => {
    const userId = <string>req.headers['x-user-id'];

    const details = await getAsync<UserDetails>(getUserDetailsUrl(userId));
    const decks = details.decks || [];

    res.setHeader('Content-Type', 'application/json');
    res.statusCode = 200;
    res.end(JSON.stringify(decks.map(deck => deck.deckId)));
};
