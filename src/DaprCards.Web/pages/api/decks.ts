import { NextApiRequest, NextApiResponse } from 'next';
import { getAsync } from '../../util/fetchAsync';

const daprPort = process.env.DAPR_HTTP_PORT || 3500;
const daprActorUrl = (actorType: string, actorId: string, method: string) => `http://localhost:${daprPort}/v1.0/actors/${actorType}/${actorId}/method/${method}`;
const getDeckDetailsUrl = (deckId: string) => daprActorUrl('DeckActor', deckId, 'GetDetailsAsync');
const getUserDetailsUrl = (userId: string) => daprActorUrl('UserActor', userId, 'GetDetailsAsync');

type UserDetails = {
    decks?: {
        deckId?: string;
    }[];
};

type DeckDetails = {
    name?: string;
};

export default async (req: NextApiRequest, res: NextApiResponse) => {
    const userId = <string>req.headers['x-user-id'];

    const details = await getAsync<UserDetails>(getUserDetailsUrl(userId));
    const decks = details.decks || [];

    const aggregatedDecks = await Promise.all(decks.map(
        async deck => {
            const deckDetails = await getAsync<DeckDetails>(getDeckDetailsUrl(deck.deckId));

            return {
                deckId: deck.deckId,
                name: deckDetails.name
            };
        }));

    res.setHeader('Content-Type', 'application/json');
    res.statusCode = 200;
    res.end(JSON.stringify(aggregatedDecks));
};
