import React, { useContext, useState, useCallback } from 'react';
import UserContext from '../components/userContext';
import Typography from '@material-ui/core/Typography';
import useEffectAsync from '../useEffectAsync';
import { postAsync } from '../util/fetchAsync';
import GameContext, { GameCard, GameDetails } from '../components/game/gameContext';
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
        const { details, selectedCardId, setDetails, setSelectedCardId } = useContext(GameContext);

        const onClick = useCallbackAsync(
            async () => {
                const updatedDetails = await postAsync<GameDetails>(
                    '/api/playCard',
                    { cardId: selectedCardId, gameId },
                    {
                        'X-User-ID': userId
                    });

                setSelectedCardId(undefined);
                setDetails(updatedDetails);
            },
            [gameId, selectedCardId, userId]);

        const userPlayer = details && details.players && details.players.find(player => player.userId === userId);

        return (
            <>
                {
                    userPlayer
                        ? userPlayer.cards.map(card => (<Card cardId={card.cardId} isPlayed={card.isPlayed} key={card.cardId} value={card.value} />))
                        : null
                }
                <Button color="primary" disabled={selectedCardId === undefined} onClick={onClick} variant="contained">Play Card</Button>
            </>
        );
    };

export const Game =
    () => {
        const { gameId, userId } = useContext(UserContext);
        const [ details, setDetails ] = useState<GameDetails>();
        const [ selectedCardId, setSelectedCardId ] = useState<string>();
        
        useEffectAsync(
            async () => {
                if (gameId) {
                    const details = await postAsync<GameDetails>(
                        '/api/game',
                        gameId,
                        {
                            'X-User-ID': userId
                        });
                        
                    setDetails(details);
                }
            },
            [gameId, setDetails, userId]);

        const isGameComplete = details && details.players && details.players.find(player => player.cards.every(card => card.isPlayed)) !== undefined;

        return (
            <GameContext.Provider value={{ details, selectedCardId, setDetails, setSelectedCardId }}>
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
