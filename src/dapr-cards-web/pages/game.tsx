import React, { useContext } from 'react';
import UserContext from '../components/userContext';

export const Game =
    () => {
        const { gameId } = useContext(UserContext);

        return (
            <>
                { `The game ${gameId} has started!` }
            </>
        );
    };

export default Game;
