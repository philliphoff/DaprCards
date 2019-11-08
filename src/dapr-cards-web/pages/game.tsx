import React, { useContext, useState, useCallback } from 'react';
import UserContext from '../components/userContext';
import Typography from '@material-ui/core/Typography';
import useEffectAsync from '../useEffectAsync';
import { postAsync } from '../util/fetchAsync';
import GameContext, { GameCard } from '../components/game/gameContext';
import DaprAppBar from '../components/daprAppBar';
import Button from '@material-ui/core/Button';
import useCallbackAsync from '../util/client/useCallbackAsync';

export const Card =
    (props: GameCard) => {
        const { setSelectedCardId } = useContext(GameContext);
        const onClick = useCallback(
            () => {
                setSelectedCardId(props.cardId);
            },
            [setSelectedCardId]);

        return (
            <div onClick={props.isPlayed ? undefined : onClick}>
                <Typography>{props.value}</Typography>
            </div>
        );
    };

export const CardSelector =
    (props: { }) => {
        const { gameId, userId } = useContext(UserContext);
        const { cards, selectedCardId, setCards, setSelectedCardId } = useContext(GameContext);

        const onClick = useCallbackAsync(
            async () => {
                const updatedCards = await postAsync<GameCard[]>(
                    '/api/playCard',
                    { cardId: selectedCardId, gameId },
                    {
                        'X-User-ID': userId
                    });

                setSelectedCardId(undefined);
                setCards(updatedCards);
            },
            [gameId, selectedCardId, userId]);

        return (
            <>
                {
                    cards.map(card => (<Card cardId={card.cardId} isPlayed={card.isPlayed} key={card.cardId} value={card.value} />))
                }
                <Button color="primary" disabled={selectedCardId === undefined} onClick={onClick} variant="contained">Play Card</Button>
            </>
        );
    };

export const Game =
    () => {
        const { gameId, userId } = useContext(UserContext);
        const [ cards, setCards ] = useState<GameCard[]>([]);
        const [ selectedCardId, setSelectedCardId ] = useState<string>();
        
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

        const isGameComplete = cards.every(card => card.isPlayed);

        return (
            <GameContext.Provider value={{ cards, selectedCardId, setCards, setSelectedCardId }}>
                <DaprAppBar />
                {
                    isGameComplete
                        ? <Typography>This game is done!</Typography>
                        : <CardSelector />
                }
            </GameContext.Provider>
        );
    };

export default Game;
