import React, { useContext, useState, useCallback } from 'react';
import UserContext from '../components/userContext';
import Typography from '@material-ui/core/Typography';
import useEffectAsync from '../useEffectAsync';
import { postAsync } from '../util/fetchAsync';
import GameContext, { GameCard, GameDetails, GamePlayer } from '../components/game/gameContext';
import DaprAppBar from '../components/daprAppBar';
import Button from '@material-ui/core/Button';
import useCallbackAsync from '../util/client/useCallbackAsync';
import { makeStyles } from '@material-ui/styles';
import Router from 'next/router';

const useStyles = makeStyles(theme => ({
    scoreboardRoot: {
        display: 'flex',
        flexDirection: 'row',
        justifyContent: 'center'
    },
    playerScore: {
        marginRight: 10
    }
}));

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

const computerPlayerUserId = '00000000-0000-0000-0000-000000000000';

const PlayerScore =
    (props: { player: GamePlayer }) => {
        const classes = useStyles(props);

        return (
            <div className={classes.playerScore}>
                <Typography>{props.player.userId !== computerPlayerUserId ? 'You' : 'Computer'}</Typography>
                <Typography>{props.player.cards.filter(card => card.isPlayed).reduce((prev, card) => prev + card.value, 0)}</Typography>
            </div>
        );
    };

export const PlayerScoreboard =
    (props) => {
        const classes = useStyles(props);
        const { details } = useContext(GameContext);

        return (
            <div className={classes.scoreboardRoot}>
                {
                    details && details.players && details.players.map(player => <PlayerScore player={player} />)
                }
            </div>
        );
    };

export const CompletedGame =
    () => {
        const { userId } = useContext(UserContext);
        const { details } = useContext(GameContext);
        const onClick = useCallback(
            () => {
                Router.push('/play');
            },
            []);

        const getScore = (player: GamePlayer) => (player && player.cards && player.cards.reduce((prev, card) => prev + card.value, 0) || 0);

        const userPlayerScore = getScore(details.players.find(player => player.userId === userId));
        const computerPlayerScore = getScore(details.players.find(player => player.userId === computerPlayerUserId));

        return (
            <div>
                <Typography>This game is done!</Typography>
                <Typography>
                    {
                        userPlayerScore > computerPlayerScore
                            ? 'You WIN!'
                            : userPlayerScore < computerPlayerScore
                                ? 'You LOSE!'
                                : 'You TIE!'
                    }
                    </Typography>
                <Button color="primary" onClick={onClick} variant="contained">Play Again</Button>
            </div>
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
                <PlayerScoreboard />
                {
                    isGameComplete
                        ? <CompletedGame />
                        : <CardSelector />
                }
            </GameContext.Provider>
        );
    };

export default Game;
