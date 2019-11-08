import React, { useContext, useState } from 'react';
import UserContext from '../components/userContext';
import Typography from '@material-ui/core/Typography';
import useEffectAsync from '../useEffectAsync';
import { postAsync } from '../util/fetchAsync';

type GameCard = {
    cardId: string;
    isPlayed: boolean;
    value: number;
};

export const Card =
    (props: GameCard) => {
        return (
            <div>
                <Typography>{props.value}</Typography>
            </div>
        );
    };

export const Game =
    () => {
        const { gameId, userId } = useContext(UserContext);
        const [ cards, setCards ] = useState<GameCard[]>([]);
        
        useEffectAsync(
            async () => {
                if (gameId) {
                    const newCards = await postAsync<GameCard[]>(
                        '/api/game',
                        gameId,
                        {
                            'X-User-ID': userId
                        });
                        
                    setCards(newCards);
                }
            },
            [gameId, setCards, userId]);

        return (
            <>
                {
                    cards.map(card => (<Card cardId={card.cardId} isPlayed={card.isPlayed} key={card.cardId} value={card.value} />))
                }
            </>
        );
    };

export default Game;
