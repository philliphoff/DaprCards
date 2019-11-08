import { NextApiRequest, NextApiResponse } from 'next';
import { postAsync } from '../../util/fetchAsync';

const daprPort = process.env.DAPR_HTTP_PORT || 3500;
const daprUrl = `http://localhost:${daprPort}/v1.0/invoke`;

const daprInvocationUrl = (appId: string, methodName: string) => `${daprUrl}/${appId}/method/${methodName}`;
const getGamesUrl = () => daprInvocationUrl('dapr-game-manager', 'games');

export default async (req: NextApiRequest, res: NextApiResponse) => {
    const userId = <string>req.headers['x-user-id'];
    const deckId = <string>req.body;

    const details = await postAsync<string>(getGamesUrl(), { userId, deckId });

    res.setHeader('Content-Type', 'application/json');
    res.statusCode = 200;
    res.end(JSON.stringify(details));
};
