import { NextApiRequest, NextApiResponse } from 'next';
import * as request from 'request-promise-native';

const daprPort = process.env.DAPR_HTTP_PORT || 3500;
const daprUrl = `http://localhost:${daprPort}/v1.0/invoke`;

const daprInvocationUrl = (appId: string, methodName: string) => `${daprUrl}/${appId}/method/${methodName}`;
const getUsersUrl = () => daprInvocationUrl('dapr-user-manager', 'users/signin');

export default async (req: NextApiRequest, res: NextApiResponse) => {
    var getUsersResponse = await request.post(
        getUsersUrl(),
        {
            headers: {
                'Accept': 'application/json'
            },
            body: JSON.stringify(req.body),
            json: false
        }
    );

    res.setHeader('Content-Type', 'application/json');
    res.statusCode = 200;
    res.end(getUsersResponse);
};
